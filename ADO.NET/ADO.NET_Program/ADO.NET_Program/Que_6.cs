using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Top_Brains
{
    class Que_6
    {
        public static void main()
        {
            string connectionString = "Server=localhost,1433;" +
                "Database=Top_Brains;" +
                "User Id=sa;" +
                "Password=Satyam@123;" +
                "TrustServerCertificate=True;";

            List<Product> productList = new List<Product>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "Select Count(*) from Product";
                SqlCommand command = new SqlCommand(query, connection);
                int n = (int)command.ExecuteScalar();
                int tempId = 0;
                string tempName = "";
                decimal tempPrice = 0;

                string newQuery = "select Id,Name,Price from Product";
                using (SqlCommand newcommand = new SqlCommand(newQuery, connection))
                {
                    using (SqlDataReader reader = newcommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tempId = (int)reader["Id"];
                            tempName = (string)reader["Name"];
                            tempPrice = (decimal)reader["Price"];
                            productList.Add(new Product { ProductId = tempId, ProductName = tempName, ProductPrice = tempPrice });
                        }
                    }

                }

            }
            foreach (var i in productList)
            {
                Console.WriteLine($"Id:{i.ProductId,-5} Name:{i.ProductName,-30} Price:{i.ProductPrice,12:F2}");
            }
        }
    }
    class Product
    {
        public int ProductId { get; set;}
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
    }
}