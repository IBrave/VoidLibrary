﻿//                                                                        101010101010101010101010101010101                                                    
//                                                                    10101010101010101010101010101010101010101010                                             
//                                                       101      10101010101010101010101010101010101010101010101010                                           
//                                               1       1010101010101010101010101010101010101010101010101010101010101                                         
//                                               1     10101010101010101010101010101010101010101010101010101010101010 1                                        
//                                             1     1010101010101010101010101010101010101010101010101010101010101010 1010                                     
//                                            1    1010101010101010101010101010101010101010101010101010101010101010101010101                                   
//                                          10    1010101010101010101010101010101010101010101010101010101010101010101010101010                                 
//                                         10    101010101010101010101010101010101010101010101010101010101010101010101010101010                                
//                                         10    101010101010101010101010101010101010101010101010101010101010101010101010101010                                
//                                        10     101010101010101010101010101010101010101010101010101010101010101010101010101010                                
//                                       101 010101010101010101010101010101010101010        10101010101010101010101010101010101                                
//                                       10  101010101010101010101010101010101010             10101010101010101010101010101010                                 
//                                       10  101010101010101010101010101010101                 1010101010101010101010101010101                                 
//                                      101  01010101010101010101010101010                               101010101010101010101                                 
//                                      101  0101010101010101010101010                                     1010101010101010101010                              
//                                      10101010101010101010101010                                            1010101010101010101                              
//                                    101010 101010101010101010                                                101010101010101010                              
//                                   10101010101010101010101                                                       10101010101010                              
//                                   101010101010101010                                                               101010101010                             
//                                  10101010101010                                                                      101010101                              
//                                  1010101010101                                                                        101010101                             
//                                 1010101010101        1010101010                                                       1010101010                            
//                                 101010101010 1    101010101010101010                                                 10101010101                            
//                                 101010101010                   10101010                                             101010101010                            
//                                 10101010101                        10101010                                        1010101010101                            
//                                101010101010                           10101010                                     1010101010101                            
//                                101010101010             10 10101010       10101                                    1010101010101                            
//                                 1010101010             1010  1010101010                        101010101010        101010101010                             
//                                 101010101                 10101010101010                   10101010101010101010    10101010101                              
//                                 101010101                    10101   10101                           1            101010101010                              
//                                1010101010                           10101                                         101010101010                              
//                               10101010101                                                   1010101010101         10101010101                               
//                              101010101010                                                  10  1010101 0101      101010101010                               
//                              1010101010101                                                 1  0101010  10101     1010101010101                              
//                             10101010101010                                                         10101         1010101010101                              
//                            101010101010101                                                                      1010101010101                               
//                           1010101010101010                                                                      1010101010101                               
//                          101010101010101010                                                                    101010101010                                 
//                         1010101010101010101                                                                    10101010101                                  
//                        101010101010101010101                                                                  101010101010                                  
//                       1010101010101010101010                          1                                      1010101010101                                  
//                      101010101010101010101010                                                              10101010101010                                   
//                     1010101010101010101010101                                   10                        1010101010 1010                                   
//                    101010101010101010101010101                                                           101010101                                          
//                    1010101010101010101010101010                                                        1010101010                                           
//                   101010101010101010101010101010             10                                       1010101010                                            
//                  10101010101010101010101010101010              1010                                 1010101010                                              
//                 10101010101010101010101010101010101              1010101  0101     1010           101010101                                                 
//                1010101010101010101010101010101010101               1010101010101010             10101010                                                    
//               101010101010101010101010101010101010 10                 101010101              10101010   10101010101010                                      
//               10101010101010101010101010101010      10                                     101  01                    10101                                 
//             101010101010101010101010101010101        101                                  10                               1010                             
//            101010101010101010101010101010              101                            1010                                    1010                          
//           101010101010101010101010101                     101                       101                                         1010                        
//          101010101010101010101010                          1010                  1010                                              10                       
//         1010101010101010101010                                10101       10101010                                                  10                      
//        10101010101010101                                           1010101010                                                        10                     
//        1010101010101                                                                                                                  10                    
//       1010101010                                                                                                                       10                   
//      10101010101                                                                                                                        10                  
//     10101010                                                                                                                             10                 
//     1010101                                                                                                                              10                 
//    10101                                                                                                                                  1                 
//   10                                                                                                                                      10                
//  10                                                                                                                                       10                
// 10                                                                                                                                         1                
//1   
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

        public VoidMySqlHelper()
        {
            Instance = this;
        }

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
            DbParameter parameter = new MySqlParameter(key, DbTypeToMySqlDbType(type));
            parameter.Value = value;
            return parameter;
        }

        private MySqlParameter[] CastToMySqlParameter(DbParameter[] parameters)
        {
            MySqlParameter[] mySqlParameters = new MySqlParameter[parameters == null ? 0 : parameters.Length];
            for (int i = parameters.Length - 1; i >= 0; --i)
            {
                mySqlParameters[i] = (MySqlParameter)parameters[i];
            }

            return mySqlParameters;
        }

        private MySqlDbType DbTypeToMySqlDbType(DbType type)
        {
            MySqlDbType mySqlDbType = MySqlDbType.Int64;
            switch(type)
            {
                case DbType.Binary:
                    mySqlDbType = MySqlDbType.Binary;
                    break;
                case DbType.Boolean:
                    break;
                case DbType.String:
                    mySqlDbType = MySqlDbType.String;
                    break;
                case DbType.UInt64:
                    mySqlDbType = MySqlDbType.UInt64;
                    break;
                case DbType.UInt32:
                    mySqlDbType = MySqlDbType.UInt32;
                    break;
                case DbType.UInt16:
                    mySqlDbType = MySqlDbType.UInt16;
                    break;
                case DbType.Int64:
                    mySqlDbType = MySqlDbType.Int64;
                    break;
                case DbType.Int32:
                    mySqlDbType = MySqlDbType.Int32;
                    break;
                case DbType.Int16:
                    mySqlDbType = MySqlDbType.Int16;
                    break;
                case DbType.Double:
                    mySqlDbType = MySqlDbType.Double;
                    break;
                case DbType.Decimal:
                    mySqlDbType = MySqlDbType.Decimal;
                    break;
            }

            return mySqlDbType;
        }

    }
}
