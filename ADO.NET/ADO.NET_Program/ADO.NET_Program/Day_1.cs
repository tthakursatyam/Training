using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADO.NET_Program
{
    internal class Day_1
    {
        public static void func1()
        {
                string connectionString =
                    "Server=localhost,1433;" +
                    "Database=Top_Brains;" +
                    "User Id=sa;" +
                    "Password=Satyam@123;" +
                    "TrustServerCertificate=True;";

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Connected to SQL Server successfully!");

            string query = "SELECT * FROM COURSE_INFO";

            using SqlCommand command = new SqlCommand(query, connection);
            using SqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t\t\t" + reader[3]);
            }
        }
        public static void func2()
        {
            string connectionString =
                "Server=localhost,1433;" +
                "Database=Test;" +
                "User Id=sa;" +
                "Password=Satyam@123;" +
                "TrustServerCertificate=True;";

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Connected to SQL Server successfully!");


            string query = "USP_GetHostelStudentName";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                   while(reader.Read())
                    {
                        Console.WriteLine(reader[0]);
                    }
                }
            }
        }
    }
}


