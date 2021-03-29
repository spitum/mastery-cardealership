using Dapper;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Dapper
{
    public class ContactsRepositoryDapper : IContactsRepository
    {
        private readonly string _connectionstring;
        public ContactsRepositoryDapper(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public void AddContact(Contact contact)
        {
            using (var cn = new SqlConnection(_connectionstring))
            {
                var param = new DynamicParameters();
                param.Add("ContactName", contact.ContactName);
                param.Add("EmailAddress", contact.EmailAddress);
                param.Add("PhoneNumber", contact.PhoneNumber);
                param.Add("Message", contact.Message);

                cn.Query<Contact>("dbo.ContactsInsert", param, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Contact> GetAll()
        {
            using (var cn = new SqlConnection(_connectionstring))
            {
                var output = cn.Query<Contact>("SelectContactsAll", commandType: CommandType.StoredProcedure).ToList();
                return output;
            }
        }
    }
}
