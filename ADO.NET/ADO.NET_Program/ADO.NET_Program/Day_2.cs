using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using Microsoft.Data.SqlClient;

namespace ADO.NET_Program
{
    internal class Day_2
    {
        public static void func1()
        {
            string connectionString =
                "Server=localhost,1433;" +
                "Database=College;" +
                "User Id=sa;" +
                "Password=Satyam@123;" +
                "TrustServerCertificate=True;";
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Connected to SQL Server successfully!");


            string query = "SELECT count(*) FROM Hostel_Students";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                int count = (int)command.ExecuteScalar();

                if (count > 5)
                {
                    string query1 = "DELETE FROM Hostel_Students WHERE Age<18";
                    using (SqlCommand command1 = new SqlCommand(query1, connection))
                    {
                        int rowAffected = command1.ExecuteNonQuery();
                        Console.WriteLine("Deleted " + rowAffected + " rows from Hostel_Students table.");
                    }

                }
                else
                {
                    string query2 = "SELECT * FROM Hostel_Students";
                    using SqlCommand command2 = new SqlCommand(query2, connection);
                    using SqlDataReader reader = command2.ExecuteReader();


                    while (reader.Read())
                    {
                        Console.WriteLine(reader["StudentID"] + "\t" + reader["Name"] + "\t" + reader["Age"]);
                    }
                }
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

            string query1 = "select * from GetDepartmentName(@name)";

            using (SqlCommand command = new SqlCommand(query1, connection))
            {
                command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value="Satyam";
                SqlDataReader reader = command.ExecuteReader();
                
                while(reader.Read())
                {
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t\t\t" + reader[3]);
                }
            }
        }
        public static void func3()
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

            string query1 = "select GetHighestM2";

            using (SqlCommand command = new SqlCommand(query1, connection))
            {
                int hightest = (int)command.ExecuteScalar();

                Console.WriteLine("Hightest M2 marks is: " + hightest);
            }


        }
        public static void func4()
        {
            

            DataSet ds = new DataSet();

            DataTable dt = new DataTable("Students");

            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Department", typeof(string));

            dt.Rows.Add(1,"Satyam","IT");
            dt.Rows.Add(2,"Rahul","CS");
            dt.Rows.Add(3,"Rohit","EC");

            Console.WriteLine("Student Details:");
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["Id"]} - {row["Name"]} - {row["Department"]}");
            }

            ds.Tables.Add(dt);
            Console.WriteLine(ds.Tables.Count);
            Console.ReadLine();
        }
        public static void func5()
        {
            string connectionString =
            "Server=127.0.0.1,1433;" +
            "Database=Test;" +
            "User Id=sa;" +
            "Password=Satyam@123;" +
            "TrustServerCertificate=True;"+
            "Pooling=false;";

            Stopwatch sw = Stopwatch.StartNew();

            for(int i=0;i<100;i++)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                }
                 
            }
            sw.Stop(); 
            Console.Write("Ms: "+sw.ElapsedMilliseconds);
        }
    }
}
