using Microsoft.Data.SqlClient;
namespace Top_Brains
{
    class Que_8
    {
        public static void main()
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
                SqlTransaction trans = connection.BeginTransaction();

                try
                {
                    string query1 = "Update Bank SET "
                    SqlCommand command1 = new SqlCommand()
                }
            }
        }
    }
}
