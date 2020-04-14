using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonInfoApp
{
    static class SqlHelper
    {
        static readonly string strCon = ConfigurationManager.ConnectionStrings["sqlserverConnect"].ConnectionString;
        /// <summary>
        /// insert,update,delete操作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlParameters"></param>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] sqlParameters)
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                using (SqlCommand com = new SqlCommand(sql, con))
                {
                    con.Open();
                    if (sqlParameters != null)
                    {
                        com.Parameters.AddRange(sqlParameters);
                    }

                    return com.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 获取单个值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] sqlParameters)
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                using (SqlCommand com = new SqlCommand(sql, con))
                {
                    con.Open();
                    if (sqlParameters != null)
                    {
                        com.Parameters.AddRange(sqlParameters);
                    }

                    return com.ExecuteScalar();
                }
            }
        }
        /// <summary>
        /// select操作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] sqlParameters)
        {
            SqlConnection con = new SqlConnection(strCon);
            using (SqlCommand com = new SqlCommand(sql, con))
            {
                if (sqlParameters != null)
                {
                    com.Parameters.AddRange(sqlParameters);
                }
                try
                {
                    con.Open();
                    return com.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                }
                catch
                {
                    con.Close();
                    con.Dispose();
                    throw;
                }
            }
        }
    }
}

