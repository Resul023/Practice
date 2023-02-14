using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public  class PersonService
    {
        private const string _CONNECTION_STRING = @"Server=DESKTOP-0UDOH5O;Database=PhoneBook;Trusted_Connection=True;";

        public static List<Person> GetAllPerson()
        {
            SqlConnection connection = new SqlConnection(_CONNECTION_STRING);

            SqlCommand command = new SqlCommand("", connection);
            command.CommandText = "SELECT [Id],[FirstName],[LastName],[Phone],[Email] FROM People";
            if (connection.State == ConnectionState.Closed) connection.Open();
            SqlDataReader dr = command.ExecuteReader();
            List<Person> list = new List<Person>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Person person = new Person();
                    person.Id = Convert.ToInt32(dr[0]);
                    person.FirstName = dr["FirstName"].ToString();
                    person.LastName = dr["LastName"].ToString();
                    person.Phone = dr["Phone"].ToString();
                    person.Email = dr["Email"].ToString();
                    list.Add(person);
                }
            }
            dr.Close();
            connection.Close();
            return list;
        }

        public static void InsertPerson(Person person)
        {
            SqlConnection connection = new SqlConnection(_CONNECTION_STRING);

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO People VALUES ( @FirstName, @LastName, @Phone, @Email)";
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = person.FirstName;
            command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = person.LastName;
            command.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = person.Phone;
            command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = person.Email;

            if (connection.State == ConnectionState.Closed) connection.Open();

            bool result = command.ExecuteNonQuery() > 0  ;
            if (result)
            {
                Console.WriteLine("Operation done with success");
            }
            else
            {
                Console.WriteLine("There are problem");
            }
            connection.Close();

        }

        public static List<Person> GetPerson(string value)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _CONNECTION_STRING;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            command.CommandText = "SELECT [Id],[FirstName],[LastName],[Phone],[Email] FROM People " +
                "WHERE FirstName = @Value OR " +
                "LastName = @Value OR " +
                "Phone = @Value OR " +
                "Email = @Value"
                ;
            command.Parameters.Add("@Value", SqlDbType.NVarChar).Value = value;

            if (connection.State == ConnectionState.Closed) connection.Open();
            SqlDataReader dr = command.ExecuteReader();
            List<Person> list = new List<Person>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Person person = new Person();
                    person.Id = Convert.ToInt32(dr[0]);
                    person.FirstName = dr["FirstName"].ToString();
                    person.LastName = dr["LastName"].ToString();
                    person.Phone = dr["Phone"].ToString();
                    person.Email = dr["Email"].ToString();
                    list.Add(person);
                }
            }
            dr.Close();
            connection.Close();
            return list;

        }



        public static void DeletePerson(int Id)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _CONNECTION_STRING;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            if (connection.State == ConnectionState.Closed) connection.Open();

            command.CommandText = "DELETE FROM People WHERE Id = @ids";
            command.Parameters.Add("@ids", SqlDbType.Int).Value = Id;
            SqlDataReader dr = command.ExecuteReader();
            dr.Close();
            connection.Close();    


        }

    }
}
