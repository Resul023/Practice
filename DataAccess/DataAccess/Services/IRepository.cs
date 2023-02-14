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
    interface IRepository
    {
         List<Person> GetAllPerson();
         void InsertPerson(Person person);
         List<Person> GetPerson(string value);
         void DeletePerson(int Id);
    }
}
