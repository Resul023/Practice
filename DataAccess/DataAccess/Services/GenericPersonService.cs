using DataAccess.Models;
using Pluralize.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Services
{
    public class GenericPersonService<T> where T: new()
    {
        public const string _CONNECTION_STRING = @"Server=DESKTOP-0UDOH5O;Database=PhoneBook;Trusted_Connection=True;";

        

        public static List<T> GetAllPerson()
        {
            SqlConnection connection = new SqlConnection(_CONNECTION_STRING);
            SqlCommand command = new SqlCommand("", connection);
            if (connection.State == ConnectionState.Closed) connection.Open();
            Pluralizer pl = new Pluralizer();
            string tableName = pl.Pluralize(typeof(T).Name);
            command.CommandText = $"SELECT [Id],[FirstName],[LastName],[Phone],[Email] FROM {tableName}";
            command.Parameters.Add("@TableName", SqlDbType.NVarChar).Value = tableName;
            SqlDataReader dr = command.ExecuteReader();
            List<T> list = new List<T>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    T person = new T();
                    foreach (var item in person.GetType().GetProperties())
                    {
                        person.GetType().GetProperty($"{item.Name}").SetValue(person, dr[$"{item.Name}"], null);
                    }
                    list.Add(person);
                }
            }
            dr.Close();
            connection.Close();
            return list;
        }
        #region ExpressionConverter
        //public static HashSet<Condition> CheckCondition(HashSet<Condition> conditions, Expression expression)
        //{
        //    if (expression is BinaryExpression binaryExpression)
        //    {
        //        if (expression.NodeType == ExpressionType.AndAlso)
        //        {
        //            CheckCondition(conditions, binaryExpression.Left as BinaryExpression);
        //            CheckCondition(conditions, binaryExpression.Right as BinaryExpression);
        //        }
        //        else
        //        {
        //            conditions.Add(GetCondition(binaryExpression));
        //        }
        //    }
        //    return conditions;
        //}

        //public static Condition GetCondition(BinaryExpression binaryExpression)
        //{
        //    var condition = new Condition();
        //    if (binaryExpression.Left is MemberExpression left)
        //    {
        //        condition.Name = left.Member.Name;
        //    }
        //    if (binaryExpression.Right is Expression right)
        //    {
        //        var lambdaExpression = Expression.Lambda(right);
        //        var value = lambdaExpression.Compile();
        //        condition.Value = value.DynamicInvoke();
        //    }
        //    return condition;
        //}
        #endregion

        public static List<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            Pluralizer pl = new Pluralizer();
            string tableName = pl.Pluralize(typeof(T).Name);

            var list = CheckCondition(new HashSet<Condition>(), predicate.Body);
            List<string> keyValuePairs = new List<string>();
            foreach (var item in list)
            {
                keyValuePairs.Add($"{item.Name} = '{item.Value}'");
            }

            string command = $"SELECT Id, FirstName, LastName, Phone, Email FROM {tableName} Where {string.Join($" And ", keyValuePairs)}";

            SqlDataAdapter da = new SqlDataAdapter(command, _CONNECTION_STRING);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<T> myData = new List<T>();
            T myType = new T();
            foreach (DataRow dr in dt.Rows)
            {
                var model = Activator.CreateInstance(myType.GetType());
                foreach (PropertyInfo prop in myType.GetType().GetProperties())
                {
                    prop.SetValue(model, dr[prop.Name]);
                }

                myData.Add((T)model);
            }

            return myData;
        }

        public static HashSet<Condition> CheckCondition(HashSet<Condition> conditions, Expression expression)
        {
            if (expression is BinaryExpression binaryExpression)
            {
                if (expression.NodeType == ExpressionType.AndAlso)
                {
                    CheckCondition(conditions, binaryExpression.Left as BinaryExpression);
                    CheckCondition(conditions, binaryExpression.Right as BinaryExpression);
                }
                else
                {
                    conditions.Add(GetCondition(binaryExpression));
                }
            }
            return conditions;
        }

        public static Condition GetCondition(BinaryExpression binaryExpression)
        {
            var condition = new Condition();
            if (binaryExpression.Left is MemberExpression left)
            {
                condition.Name = left.Member.Name;
            }
            if (binaryExpression.Right is Expression right)
            {
                var lambdaExpression = Expression.Lambda(right);
                var value = lambdaExpression.Compile();
                condition.Value = value.DynamicInvoke();
            }
            return condition;
        }


    }
}
