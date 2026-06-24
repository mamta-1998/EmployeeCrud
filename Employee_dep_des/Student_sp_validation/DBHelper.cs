using Microsoft.Data.SqlClient;
using System.Data;

namespace FinalMVC.Data
{
    public class DBHelper
    {
        private readonly IConfiguration _configuration;
        public DBHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private SqlConnection GetConnectionString()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public int ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            using SqlConnection conn = GetConnectionString();
            {
                using SqlCommand cmd = new SqlCommand(query, conn);
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null, string mode = "")
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = GetConnectionString())
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if(mode=="sp")
                    cmd.CommandType = CommandType.StoredProcedure; // Important

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;

        }
        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection con = GetConnectionString())
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    con.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}
