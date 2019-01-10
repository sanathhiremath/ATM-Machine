using ATM_Machine.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ATM_Machine.Helper;
using ATM_Machine.Models;
using System.Data.SqlClient;
using System.Data;

namespace ATM_Machine.DataLayer
{
    public class Home
    {
        public static string AddNewUser(UserDetails user)
        {
            SqlCommand command = null;
            SqlConnection connection = null;
            DataSet dataSet = new DataSet();
            try
            {
                MSSQLAccess mSSQL = new MSSQLAccess();
                mSSQL.CreateConnection(ref connection);
                mSSQL.InitializeSqlCommandComponent(ref connection, ref command, "AddAtmUser");
                command.Parameters.AddWithValue("@CardNo", user.CardNo);
                command.Parameters.AddWithValue("@CardPin", user.CardPin);
                command.Parameters.AddWithValue("@FirstName", user.Firstame ?? "");
                command.Parameters.AddWithValue("@LastName", user.LastName ?? "");
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.ExecuteNonQuery();
                //new SqlDataAdapter(command).Fill(dataSet);

            }
            catch (Exception exception)
            {
                
            }
            return string.Empty;

        }

        public static DataSet IsUserExists(UserDetails user)
        {
            SqlCommand command = null;
            SqlConnection connection = null;
            DataSet dataSet = new DataSet();
            try
            {
                MSSQLAccess mSSQL = new MSSQLAccess();
                mSSQL.CreateConnection(ref connection);
                mSSQL.InitializeSqlCommandComponent(ref connection, ref command, "IsUserExists");
                command.Parameters.AddWithValue("@CardNo", user.CardNo);
                command.Parameters.AddWithValue("@CardPin", user.CardPin); 
                new SqlDataAdapter(command).Fill(dataSet);
            }
            catch (Exception exception)
            {
                
            }
            return dataSet;
        }

        public static DataSet AddAmt(Transaction tr)
        {
            SqlCommand command = null;
            SqlConnection connection = null;
            DataSet dataSet = new DataSet();
            try
            {
                MSSQLAccess mSSQL = new MSSQLAccess();
                mSSQL.CreateConnection(ref connection);
                mSSQL.InitializeSqlCommandComponent(ref connection, ref command, "AddAmt");
                command.Parameters.AddWithValue("@CardNo", tr.CardNo);
                command.Parameters.AddWithValue("@DepositAmt", tr.DepositAmt);
                command.Parameters.AddWithValue("@IsDeposit", 1);
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                
            }
            return dataSet;
        }

        public static DataTable Balance(Transaction tr)
        {
            SqlCommand command = null;
            SqlConnection connection = null;
            DataTable data = new DataTable();
            try
            {
                MSSQLAccess mSSQL = new MSSQLAccess();
                mSSQL.CreateConnection(ref connection);
                mSSQL.InitializeSqlCommandComponent(ref connection, ref command, "Balance");
                command.Parameters.AddWithValue("@CardNo", tr.CardNo);                
                new SqlDataAdapter(command).Fill(data);
            }
            catch (Exception exception)
            {
                
            }
            return data;
        }

        public static string WithdrawAmt(Transaction tr)
        {
            SqlCommand command = null;
            SqlConnection connection = null;
            DataSet dataSet = new DataSet();
            try
            {
                DataTable dd = Balance(tr);
                if (dd.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dd.Rows[0]["Balance"]) < tr.WithdrawalAmt)
                        return "false";
                }
                else 
                {
                    return "false";
                }
                MSSQLAccess mSSQL = new MSSQLAccess();
                mSSQL.CreateConnection(ref connection);
                mSSQL.InitializeSqlCommandComponent(ref connection, ref command, "WithdrawAmt");
                command.Parameters.AddWithValue("@CardNo", tr.CardNo);
                command.Parameters.AddWithValue("@WithdrawAmt", tr.WithdrawalAmt);
                command.Parameters.AddWithValue("@IsDeposit", 0);
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                
            }
            return "true";
        }


    }
}