using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoToolBox
{
    class Program
    {
        static void Main(string[] args)
        {
            ContactRepository repo = new ContactRepository();

            foreach (Contact contact in repo.Get())
            {
                Console.WriteLine($"{contact.Id} - {contact.LastName} {contact.FirstName}");
            }

            Contact newContact = new Contact()
            {
                LastName = "Norris",
                FirstName = "Chuck",
                Birthdate = new DateTime(1940, 3, 10)
            };

            newContact = repo.Insert(newContact);
            Console.WriteLine(newContact.Id);

            Console.ReadLine();
        }
    }
}
