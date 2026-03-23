using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace practice
{
	class Problem1
	{
		public static void part1()
		{
			try
			{
				string connectionString =
					"Server=localhost,1433;" +
					"Database=Top_Brains;" +
					"User Id=sa;" +
					"Password=Satyam@123;" +
					"TrustServerCertificate=True;";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					SqlCommand command = new SqlCommand("sp_GetEmployeesByDepartment", connection);
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.Add("@Department", SqlDbType.VarChar).Value = "IT";

					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						Console.WriteLine(
							reader["EmpId"] + "\t" +
							reader["Name"] + "\t" +
							reader["Department"] + "\t" +
							reader["Phone"] + "\t" +
							reader["Email"]);
					}
				}
				Console.ReadLine();
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		public static void part2()
		{
			try
			{
				string connectionString =
					"Server=localhost,1433;" +
					"Database=Top_Brains;" +
					"User Id=sa;" +
					"Password=Satyam@123;" +
					"TrustServerCertificate=True;";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					SqlCommand command =new SqlCommand("sp_GetDepartmentEmployeeCount", connection);
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.Add("@Department", SqlDbType.VarChar).Value = "IT";

					int count = Convert.ToInt32(command.ExecuteScalar()); ;

					Console.WriteLine($"Number of Employee in IT:{count}");
				}
				Console.ReadLine();
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}