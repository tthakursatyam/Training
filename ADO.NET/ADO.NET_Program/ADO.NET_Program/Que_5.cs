using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Top_Brains
{
    class Que_5
    {
        public static void main()
        {
            string connectionString =
                "Server=localhost,1433;" +
                "Database=Top_Brains;" +
                "User Id=sa;" +
                "Password=Satyam@123;" +
                "TrustServerCertificate=True;";

            Console.WriteLine("Enter the number of rows to insert:");
            int n = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                Console.Write("Id: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Marks: ");
                int marks = Convert.ToInt32(Console.ReadLine());
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connected to SQL Server successfully!");
                    string query = "insert into Students(Id,Name,Marks) values (@Id,@Name,@Marks)";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Marks",marks);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    Console.WriteLine("Inserted Successfully");
                }
            }
            Console.ReadLine();
        }
    }
}