using Bogus;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public class ContactsRepositoryMock : IContactsRepository
    {
        static int id = 1;
        public static Faker<Contact> contacts = new Faker<Contact>().RuleFor(c => c.ContactID, f => id++)
                                                                    .RuleFor(c => c.ContactName, f => f.Name.FirstName())
                                                                    .RuleFor(c => c.EmailAddress, (f, c) => f.Internet.Email(c.ContactName))
                                                                    .RuleFor(c => c.Message, f => f.Lorem.Sentence(10));
        List<Contact> output = contacts.Generate(5);

        public void AddContact(Contact contact)
        {

            output.Add(contact);
        }

        public List<Contact> GetAll()
        {
            return output;
        }
    }
}
