using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace DataLayer
{
    public class ContactRepositoryEx
    {
        private IDbConnection db;
        public ContactRepositoryEx(string connString)
        {
            this.db = new SqlConnection(connString);
        }
        
        public List<Contact> GetAllContactsWithAddresses()
        {
            var sql = "Select * From Contacts AS C INNER JOIN Addresses AS A ON A.ContactId = C.Id";

            var contactDict = new Dictionary<int, Contact>();

            var contacts = this.db.Query<Contact, Address, Contact>(sql, (contact, address) =>
            {
                if (!contactDict.TryGetValue(contact.Id, out var currentContact))
                {
                    currentContact = contact;
                    contactDict.Add(currentContact.Id, currentContact);
                }

                currentContact.Addresses.Add(address);
                return contact;
            });
            return contacts.Distinct().ToList();
        }

        public List<Address> GetAddressesByState(int stateId)
        {
            return this.db.Query<Address>("Select * from Addresses Where StateId = {=stateId}", new { stateId }).ToList();
        }

        public List<Contact> GetContactsById(params int[] ids)
        {
            return this.db.Query<Contact>("Select * From Contacts Where ID in @Ids", new { Ids = ids }).ToList();
        }

        public List<dynamic> GetDynamicContactsById(params int[] ids)
        {
            return this.db.Query("Select * From Contacts Where ID in @Ids", new { Ids = ids }).ToList();
        }

        public int BulkInsertContacts(List<Contact> contacts)
        {
            var sql =
                "Insert Into Contacts (FirstName, LastName, Email, Company, Title) Values(@FirstName, @LastName, @Email, @Company, @Title); " +
                "Select Cast(SCOPE_IDENTITY() as int)";

            return this.db.Execute(sql, contacts);
        }
    }
}
