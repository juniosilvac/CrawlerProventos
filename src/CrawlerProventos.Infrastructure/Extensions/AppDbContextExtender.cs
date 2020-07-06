using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace CrawlerProventos.Infrastructure.Extensions
{
    public static class AppDbContextExtender
    {
        public static int ExecuteCommand(this DbContext dbContext, string sql)
        {
            int result = -1;            
            SqlConnection connection = new SqlConnection("server=127.0.0.1; Uid=root; pwd=; Port=3306; database=dbproventos;");

            try
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
            }

            catch (System.Exception e)
            { }
            finally
            {
                connection.Close();
            }

            return result;
        }
    }
}
