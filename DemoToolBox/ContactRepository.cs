using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox.Database;

namespace DemoToolBox
{
    public class ContactRepository : IRepository<int, Contact>
    {
        private Connection dbConnection;

        public ContactRepository()
        {
            dbConnection = new Connection(@"Data Source=AW-BRIAREOS\SQL2016DEV;Initial Catalog=Test;Integrated Security=True");
        }

        public IEnumerable<Contact> Get()
        {
            Command command = new Command("Select Id, LastName, FirstName from Contact;");
            return dbConnection.ExecuteReader(command, (dr) => new Contact() { Id = (int)dr["Id"], LastName = (string)dr["LastName"], FirstName = (string)dr["FirstName"] });
        }

        public Contact Get(int key)
        {
            Command command = new Command("Select Id, LastName, FirstName from Contact where Id = @Id;");
            command.AddParameter("Id", key);
            return dbConnection.ExecuteReader(command, (dr) => new Contact() { Id = (int)dr["Id"], LastName = (string)dr["LastName"], FirstName = (string)dr["FirstName"] }).SingleOrDefault();
        }

        public Contact Insert(Contact entity)
        {
            Command cmd = new Command("insert into Contact(LastName, FirstName, Email, Birthdate) output inserted.Id values (@LastName, @FirstName, @Email, @Birthdate);");
            cmd.AddParameter("LastName", entity.LastName);
            cmd.AddParameter("FirstName", entity.FirstName);
            cmd.AddParameter("Email", entity.Email);
            cmd.AddParameter("Birthdate", entity.Birthdate);
            entity.Id = (int)dbConnection.ExecuteScalar(cmd);
            return entity;
        }

        public bool Update(int key, Contact entity)
        {
            Command cmd = new Command("update Contact set LastName = @LastName, FirstName = @FirstName, Birthdate = @BirthDate where Id = @Id;");
            cmd.AddParameter("LastName", entity.LastName);
            cmd.AddParameter("FirstName", entity.FirstName);
            cmd.AddParameter("Birthdate", entity.Birthdate);
            cmd.AddParameter("Id", key);
            return dbConnection.ExecuteNonQuery(cmd) == 1;
        }

        public bool Delete(int key)
        {
            Command cmd = new Command("delete from Contact where Id = @Id;");            
            cmd.AddParameter("Id", key);
            return dbConnection.ExecuteNonQuery(cmd) == 1;
        }
    }
}
