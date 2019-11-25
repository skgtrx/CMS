using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentStatusUpdateJob.SQL_Access
{
    class SqlHelper
    {
        string ConnectionString;
        SqlConnection connection;
        public void SetConnection()
        {
            try
            {
                ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                connection = new SqlConnection(ConnectionString);
                connection.Open();
            }
            catch
            {
                
                //MessageBox.Show("Unable to establish connection to database!!!!");
            }

        }
        public bool CloseConnection()
        {
            try
            {
                this.connection.Close();
                this.connection.Dispose();
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }

        }
        public DataTable ExecuteQuery(string query)
        {
            SetConnection();

            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand(query, this.connection);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                return dt;
            }
            catch (Exception exc)
            {
                return null;
            }
            finally
            {
                CloseConnection();

            }

        }
        public int ExecuteUpdate(string query)
        {
            SetConnection();
            SqlCommand sqlQuery = new SqlCommand(query, connection);
            int RowsAffected = 0;
            try
            {
                RowsAffected = sqlQuery.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return 0;
            }
            CloseConnection();
            return RowsAffected;
        }
    }
}