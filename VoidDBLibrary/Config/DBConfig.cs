using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VoidLibrary.Utils;
using VoidLibrary.Utils.Storage;

namespace VoidDBLibrary.Config
{
    public class DBConfig
    {
        private SharedPreferences Instance = new SharedPreferencesImpl("MySqlConfig");

        private string KEY_MYSQL_CONFIG = "mysql_config";
        private string KEY_SQLITE3_CONFIG = "sqlite3_config";

        private string KEY_SERVER_NAME = "server";
        private string KEY_PORT_NAME = "port";
        private string KEY_UID_NAME = "uid";
        private string KEY_PWD_NAME = "pwd";
        private string KEY_DATABASE_NAME = "database";
        private string KEY_DATABASE_STORAGE_DIR = Path.Combine(Directory.GetCurrentDirectory(), "data_dir");

        public string[] MySqlConfig
        {
            get {
                string[] values = Instance.GetStrArray(KEY_MYSQL_CONFIG, new string[0]);
                if (values.Length == 0)
                {
                    MySqlConfig = new string[] {
                        KEY_SERVER_NAME, KEY_PORT_NAME, KEY_UID_NAME, KEY_PWD_NAME, KEY_DATABASE_NAME
                    };
                }
                return values;
            }
            set {
                Instance.Edit().PutStrArray(KEY_MYSQL_CONFIG, value).Commit();
            }
        }

        public string[] Sqlite3Config
        {
            get
            {
                string[] values = Instance.GetStrArray(KEY_SQLITE3_CONFIG, new string[0]);
                if (values.Length == 1)
                {
                    Sqlite3Config = new string[] {
                        KEY_SERVER_NAME, KEY_PORT_NAME, KEY_UID_NAME, KEY_PWD_NAME, KEY_DATABASE_NAME, KEY_DATABASE_STORAGE_DIR
                    };
                }
                return values;
            }
            set
            {
                Instance.Edit().PutStrArray(KEY_SQLITE3_CONFIG, value).Commit();
            }
        }

    }
}
