using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ATM_Machine.Helper;

namespace ATM_Machine.Helper
{
    public class MSSQLAccess : IDatabase
    {
        public void ClearSqlObjects(ref SqlConnection connection, ref SqlCommand command)
        {
            if (command != null)
            {
                command.Dispose();
                command = null;
            }
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
        }

        public void ClearSqlObjects(ref SqlConnection connection, ref SqlCommand command, ref SqlDataAdapter adaptor)
        {
            if (adaptor != null)
            {
                adaptor.Dispose();
                adaptor = null;
            }
            if (command != null)
            {
                command.Dispose();
                command = null;
            }
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
        }

        public void CreateConnection(ref SqlConnection connection)
        {
            if (connection != null)
            {
                try
                {
                    connection.Close();
                    connection.Dispose();
                }
                finally
                {
                    connection = null;
                }
            }
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ME_Conn"].ConnectionString);
            connection.Open();
        }

        public void InitializeSqlCommandComponent(ref SqlConnection connection, ref SqlCommand command, string storedProcedureName)
        {
            if ((storedProcedureName == "") || (storedProcedureName == null))
            {
                throw new Exception("Please provide a valide name for the storedprocedure.");
            }
            if ((connection.State == ConnectionState.Closed) || (connection.State == ConnectionState.Closed))
            {
                connection.Open();
            }
            if (command == null)
            {
                command = new SqlCommand();
            }
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = storedProcedureName;
        }
    }
}