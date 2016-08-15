using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Workday.Common;
using System.Data.SqlClient;
using System.Data;

namespace Workday.DataAccess
{
    public class TimeSheetDataAccess
    {
        private static string _conn = System.Configuration.ConfigurationManager.ConnectionStrings["Myconnection"].ToString();

        public static bool AddTimeSheet(TimeSheet thistime)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();

                try
                {
                    string sql = "insert into [TimeSheet] (UserId, WorkDate, StartTime, StartIp, StartImage,TimeSheetId) values (@value1, @value2, @value3,@value4, @value5,@value6); ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@value1", thistime.UserId);
                    cmd.Parameters.AddWithValue("@value2", thistime.Date);
                    cmd.Parameters.AddWithValue("@value3", thistime.StartTime);
                    cmd.Parameters.AddWithValue("@value4", thistime.StartIp);
                    cmd.Parameters.AddWithValue("@value6", thistime.TimeSheetId);
                    SqlParameter param = cmd.Parameters.Add("@value5", SqlDbType.VarBinary);
                    param.Value = thistime.StartImage;
                    var result = cmd.ExecuteNonQuery();

                    if (result != 0 & result != -1)
                    {
                        return true;
                    }
                }
                catch (SqlException ex)
                {


                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                conn.Close();
            }

            return false;
        }

        public static byte[] GetStartImageByID(TimeSheet thistime)
        {
            byte[] imagecontent;
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();

                try
                {
                    string sql = "select StartImage from [TimeSheet] where TimeSheetId=@value1";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@value1", thistime.TimeSheetId);
                    imagecontent = cmd.ExecuteScalar() as byte[];

                }
                catch (SqlException ex)
                {


                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                conn.Close();
            }
            return imagecontent;
        }

        private DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                return dt;
            }
        }
    }
}


