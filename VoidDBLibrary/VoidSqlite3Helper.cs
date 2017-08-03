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

        public SQLiteConnection SQLiteConnection
        {
            set { connection = value; }
        }

        public VoidSqlite3Helper()
        {
            Instance = this;
        }

        public static SQLiteConnection CreateConnection(string fileNameNoExtension)
        {
            DBConfig dbConfig = new DBConfig();
            string[] configs = dbConfig.Sqlite3Config;
            string db_dir = configs[configs.Length - 1];
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

        public Object Begin()
        {
            return connection.BeginTransaction();
        }

        public void Commit(object obj)
        {
            ((SQLiteTransaction)obj).Commit();
        }

        public void Rollback(object obj)
        {
            ((SQLiteTransaction)obj).Rollback();
        }

        public override void Create(string fileNameNoExtension)
        {
            connection = CreateConnection(fileNameNoExtension);
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
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = commandText;
                return command.ExecuteReader();
            }
        }

        public override DbDataReader ExecuteReader(string commandText, params DbParameter[] parameters)
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = commandText;
                if (parameters != null && parameters.Length != 0)
                {
                    command.Parameters.AddRange(parameters);
                }

                return command.ExecuteReader();
            }
        }

        public override DbParameter CreateParameter(string key, object value, DbType type)
        {
            DbParameter parameter = new SQLiteParameter(key, type);
            parameter.Value = value;
            return parameter;
        }

    }
}
