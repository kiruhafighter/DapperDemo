using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ContactRepository : IContactRepository
    {
        private IDbConnection db;

        public ContactRepository(string connString)
        {
            this.db = new SqlConnection(connString);
        }

        public Contact Add(Contact contact)
        {
            throw new NotImplementedException();
        }

        public Contact Find(int id)
        {
            throw new NotImplementedException();
        }

        public List<Contact> GetAll()
        {
            //using(IDbConnection connection = new SqlConnection("Server=(LocalDB)\\MSSQLLocalDB;Database=ContactsDB;Trusted_Connection=True;TrustServerCertificate=True"))
            //{
            //    return connection.Query<Contact>("Select * from Contacts").ToList();
            //}
            return this.db.Query<Contact>("Select * from Contacts").ToList();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Contact Update(Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}
