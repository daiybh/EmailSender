using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;

namespace EmailSender
{
    public class DBHelper
    {
        public static string ACCESS_DB_PATH = ConfigurationManager.AppSettings["SourceFile"];

        public static DataTable ExecuteSQL(string sqlStrW)
        {
            DataSet dsData = new DataSet();
            try
            {
                //OleDbConnection connW = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ACCESS_DB_PATH);
                //OleDbCommand command = new OleDbCommand(sqlStrW, connW);
                //command.CommandTimeout = 3000000;
                //command.Connection.Open();
                //OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                //adapter.Fill(dsData);
                //command.Connection.Close();

                OdbcConnection connW = new OdbcConnection("Driver={Microsoft Access Driver (*.mdb)};Dbq=" + ACCESS_DB_PATH + ";Uid=Admin;Pwd=;");
                OdbcCommand command = new OdbcCommand(sqlStrW, connW);
                command.CommandTimeout = 3000000;
                command.Connection.Open();
                OdbcDataAdapter adapter = new OdbcDataAdapter(command);
                adapter.Fill(dsData);
                command.Connection.Close();
                return dsData.Tables[0];
            }
            catch (Exception e)
            {
                LogManager.Log(e.Message);
                return null;
            }
        }

        public static void ExecuteNonQuery(string sqlStrW)
        {
            DataSet dsData = new DataSet();
            try
            {
                //OleDbConnection connW = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ACCESS_DB_PATH);
                //OleDbCommand command = new OleDbCommand(sqlStrW, connW);
                //command.CommandTimeout = 3000000;
                //command.Connection.Open();
                //command.ExecuteNonQuery();
                //command.Connection.Close();

                OdbcConnection connW = new OdbcConnection("Driver={Microsoft Access Driver (*.mdb)};Dbq=" + ACCESS_DB_PATH + ";Uid=Admin;Pwd=;");
                OdbcCommand command = new OdbcCommand(sqlStrW, connW);
                command.CommandTimeout = 3000000;
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (Exception e)
            {
                LogManager.Log(e.Message);
            }
        }
    }
}
