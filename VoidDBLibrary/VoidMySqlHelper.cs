using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using VoidDBLibrary;
using VoidDBLibrary.Config;
using VoidLibrary.Utils;

namespace ElecParamsServer.Helper
{
    public class VoidMySqlHelper : VoidDbHelper
    {
        private string DatabaseName;
        private string connectionString;

        public void InitDB()
        {
            DBConfig mySqlConfig = new DBConfig();
            string[] db_config = mySqlConfig.MySqlConfig;

            DatabaseName = db_config[db_config.Length - 1];
            string connectToMySql = String.Format("server={0};port={1};uid={2};pwd={3};", db_config[0], db_config[1], db_config[2], db_config[3]);
            connectionString = String.Format(connectToMySql + "database={0};", DatabaseName);

            try
            {
                MySqlConnection connection = new MySqlConnection(connectToMySql);
                MySqlCommand command = new MySqlCommand(String.Format("CREATE DATABASE IF NOT EXISTS {0};", DatabaseName), connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        // XtraMessageBox.Show("无法连接到数据库，请联系管理员。", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        FileLog.WriteE("Cannot connect to server.  Contact administrator");
                        break;
                    case 1042:
                        // XtraMessageBox.Show("Unable to connect to any of the specified MySQL hosts.", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        FileLog.WriteE("Unable to connect to any of the specified MySQL hosts.");
                        break;
                    case 1045:
                        // XtraMessageBox.Show("无效的用户名或密码，请重试！", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        FileLog.WriteE("Invalid username/password, please try again");
                        break;
                }
                FileLog.WriteE(ex.ToString());
                throw ex;
            }
            // WorkStationDao.CreateWorkStationTableSql();
        }

        //
        // Summary:
        //     Executes a single command against a MySQL database. A new MySql.Data.MySqlClient.MySqlConnection
        //     is created using the MySql.Data.MySqlClient.MySqlConnection.ConnectionString
        //     given.
        //
        // Parameters:
        //   connectionString:
        //     MySql.Data.MySqlClient.MySqlConnection.ConnectionString to use
        //
        //   commandText:
        //     SQL command to be executed
        //
        //   parms:
        //     Array of MySql.Data.MySqlClient.MySqlParameter objects to use with the command.
        public override int ExecuteNonQuery(string commandText, params DbParameter[] parameters)
        {
            MySqlParameter[] mySqlParameters = CastToMySqlParameter(parameters);
            return MySqlHelper.ExecuteNonQuery(connectionString, commandText, mySqlParameters);
        }

        public override DbDataReader ExecuteReader(string commandText)
        {
            return MySqlHelper.ExecuteReader(connectionString, commandText);
        }

        public override DbDataReader ExecuteReader(string commandText, params DbParameter[] parameters)
        {
            MySqlParameter[] mySqlParameters = CastToMySqlParameter(parameters);
            return MySqlHelper.ExecuteReader(connectionString, commandText, mySqlParameters);
        }

        public override DbParameter CreateParameter(string key, object value, DbType type)
        {
            DbParameter parameter = new MySqlParameter(key, type);
            parameter.Value = value;
            return parameter;
        }

        private MySqlParameter[] CastToMySqlParameter(DbParameter[] parameters)
        {
            MySqlParameter[] mySqlParameters = new MySqlParameter[parameters.Length];
            for (int i = parameters.Length - 1; i >= 0; --i)
            {
                mySqlParameters[i] = (MySqlParameter)parameters[i];
            }

            return mySqlParameters;
        }

    }
}
