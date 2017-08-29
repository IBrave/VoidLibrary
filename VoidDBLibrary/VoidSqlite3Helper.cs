//                                                                        101010101010101010101010101010101                                                    
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Text;
using VoidDBLibrary.Config;
using VoidLibrary.Utils;

namespace VoidDBLibrary
{
    /// <summary>
    /// http://system.data.sqlite.org/index.html/doc/trunk/www/downloads.wiki#sqlite-netFx35-binary-PocketPC-ARM-2008
    /// </summary>
    public class VoidSqlite3Helper : VoidDbHelper
    {
        private SQLiteConnection connection;

        private bool _use_app_confi_dir;

        public SQLiteConnection SQLiteConnection
        {
            set { connection = value; }
        }

        public VoidSqlite3Helper()
        {
            Instance = this;
        }

        public void UseConfig(bool use_config_dir)
        {
            _use_app_confi_dir = use_config_dir;
        }

        public static string GetDBDir(string child_dir = null)
        {
            DBConfig dbConfig = new DBConfig();
            string[] configs = dbConfig.Sqlite3Config;
            string db_dir = configs[configs.Length - 1];
            if (child_dir != null)
            {
                db_dir = Path.Combine(db_dir, child_dir);
            }
            if (!Directory.Exists(db_dir))
            {
                Directory.CreateDirectory(db_dir);
            }

            return db_dir;
        }

        public SQLiteConnection CreateConnection(string fileNameNoExtension, string child_dir = null)
        {
            DBConfig dbConfig = new DBConfig();
            string[] configs = dbConfig.Sqlite3Config;
            string db_dir = configs[configs.Length - 1];
            if (_use_app_confi_dir)
            {
                db_dir = Path.Combine(Directory.GetCurrentDirectory(), "config_dir");
            }
            if (child_dir != null)
            {
                db_dir = Path.Combine(db_dir, child_dir);
            }
            if (!Directory.Exists(db_dir))
            {
                Directory.CreateDirectory(db_dir);
            }
            string dbPath = Path.Combine(db_dir, fileNameNoExtension + ".rec");
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
            }

            return new SQLiteConnection(string.Format("Data Source={0};Version=3;", dbPath));
        }

        public override Object Begin()
        {
            return connection.BeginTransaction();
        }

        public override void Commit(object obj)
        {
            ((SQLiteTransaction)obj).Commit();
        }

        public override void Rollback(object obj)
        {
            ((SQLiteTransaction)obj).Rollback();
        }

        public override void Create(string fileNameNoExtension, string child_dir_name = null)
        {
            connection = CreateConnection(fileNameNoExtension, child_dir_name);
        }

        public override void Open()
        {
            connection.Open();
        }

        public override void Close()
        {
            SQLiteConnection connection = this.connection;
            if (connection == null)
            {
                return;
            }

            try
            {
                connection.Close();
            }
            catch (Exception e)
            {
                FileLog.WriteE(e.ToString());
            }
        }

        public override int ExecuteNonQuery(string sql)
        {
            int result = 0;

            DbTransaction trans = connection.BeginTransaction();
            try
            {
                SQLiteCommand command = new SQLiteCommand(sql, (SQLiteConnection)connection);
                result = command.ExecuteNonQuery();
                trans.Commit();
            } catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }

            return result;
        }

        public override int ExecuteNonQuery(string commandText, params DbParameter[] parameters)
        {
            /*
            int affectedRows = 0;
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                DbTransaction trans = connection.BeginTransaction();
                try
                {
                    command.CommandText = commandText;
                    if (parameters !=null && parameters.Length != 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    affectedRows = command.ExecuteNonQuery();

                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    FileLog.WriteE(e.ToString());
                    throw e;
                }
            }
            return affectedRows;
             * */
            int affectedRows = 0;
            SQLiteCommand command = new SQLiteCommand(connection);
            {
                //DbTransaction trans = connection.BeginTransaction();
                try
                {
                    command.CommandText = commandText;
                    if (parameters != null && parameters.Length != 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    affectedRows = command.ExecuteNonQuery();

                    //trans.Commit();
                }
                catch (Exception e)
                {
                    //trans.Rollback();
                    FileLog.WriteE(e.ToString());
                    throw e;
                }
            }
            return affectedRows;
        }

        public override DbDataReader ExecuteReader(string commandText)
        {
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = commandText;
            return command.ExecuteReader();
        }

        public override DbDataReader ExecuteReader(string commandText, params DbParameter[] parameters)
        {
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = commandText;
            if (parameters != null && parameters.Length != 0)
            {
                command.Parameters.AddRange(parameters);
            }

            return command.ExecuteReader();
        }

        public override DbParameter CreateParameter(string key, object value, DbType type)
        {
            DbParameter parameter = new SQLiteParameter(key, type);
            parameter.Value = value;
            return parameter;
        }

    }
}
