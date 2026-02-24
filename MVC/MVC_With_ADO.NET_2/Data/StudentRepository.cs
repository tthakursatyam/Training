using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using global::MVC_With_ADO.NET_2.Models;
using Microsoft.Extensions.Configuration;


namespace MVC_With_ADO.NET_2.Data
{
    public class StudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT Name, Age FROM StudentsMaster";

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        Name = reader["Name"].ToString(),
                        Age = (int)reader["Age"]
                    });
                }
            }

            return students;
        }
    }
}