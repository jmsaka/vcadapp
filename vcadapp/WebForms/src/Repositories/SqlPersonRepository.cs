using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VCadApp.Models;

namespace VCadApp.Repositories
{
    public class SqlPersonRepository : IPersonRepository
    {
        private static string ConnectionString =>
            ConfigurationManager.ConnectionStrings["VcadAppDB"].ConnectionString;

        public int Insert(Person person)
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_InsertPerson", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", person.Name);
                cmd.Parameters.AddWithValue("@BirthDate", person.BirthDate);
                cmd.Parameters.AddWithValue("@Email", person.Email);
                cmd.Parameters.AddWithValue("@MaritalStatus", person.MaritalStatus);

                var outParam = new SqlParameter("@NewId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outParam);

                conn.Open();
                cmd.ExecuteNonQuery();

                if (outParam.Value is int newId)
                {
                    return newId;
                }

                throw new DataException("O Stored Procedure não retornou o ID esperado.");
            }
        }

        public List<Person> GetAll()
        {
            var people = new List<Person>();

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_GetPersons", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        people.Add(CreatePersonFromReader(reader));
                    }
                }
            }
            return people;
        }

        private static Person CreatePersonFromReader(SqlDataReader reader)
        {
            return new Person
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                BirthDate = reader.GetDateTime(reader.GetOrdinal("BirthDate")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                MaritalStatus = reader.GetString(reader.GetOrdinal("MaritalStatus"))
            };
        }
    }
}