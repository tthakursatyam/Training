using System;
using Microsoft.Data.SqlClient;
namespace Top_Brains
{
    class Que_7
    {
        public static void main()
        {
            string connectionString =
                "Server=localhost,1433;" +
                "Database=Top_Brains;" +
                "User Id=sa;" +
                "Password=Satyam@123;" +
                "TrustServerCertificate=True;";

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_total_Employee_Count", connection))
                {
                    connection.Open();
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    int result = (int)command.ExecuteScalar();
                    Console.WriteLine("Tota number of employees: " + result);

                }
            }
        }
    }
}