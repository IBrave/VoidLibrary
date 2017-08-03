using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace VoidDBLibrary.Model
{
    public class Column
    {
        private static Dictionary<DbType, string> CacheDbTypeToSqliteStrMap = new Dictionary<DbType, string>()
        {
            {DbType.Double, "DOUBLE"},
            {DbType.Single, "FLOAT"},
            {DbType.Int64, "INT64"},
            {DbType.Int32, "INT32"},
            {DbType.Int16, "INT16"},
            {DbType.Boolean, "BOOLEAN"},
            {DbType.Byte, "TINYINT"},
            {DbType.String, "STRING"},
            {DbType.DateTime, "DATETIME"},
            {DbType.Binary, "BLOB"}
        };

        public string name;
        public DbType type;
        public int type_length;

        public bool is_primary_key;
        public bool is_auto_increment;

        /// <summary>
        /// 0: no null 1: null 2: not null
        /// </summary>
        public int allow_null_state;
        /// <summary>
        /// 0: no 1: has
        /// </summary>
        public int default_value_state;
        public string default_value;

        public const int NULL_STATE_NO_NULL_PARAM = 0;
        public const int NULL_STATE_HAS_NULL = 1;
        public const int NULL_STATE_SET_NOT_NULL = 2;

        public const int DEFAULT_VALUE_STATE_NO = 0;
        public const int DEFAULT_VALUE_STATE_HAS = 1;

        public Column()
        {
        }

        public Column(string name, DbType type)
        {
            this.name = name;
            this.type = type;
        }

        public Column Init(string name, DbType type)
        {
            this.name = name;
            this.type = type;
            return this;
        }

        public Column SetTypeLen(int len)
        {
            this.type_length = len;
            return this;
        }

        public Column SetPrimaryKey(bool is_primary_key)
        {
            this.is_primary_key = is_primary_key;
            return this;
        }

        public Column SetAutoIncrement(bool is_auto_increment)
        {
            this.is_auto_increment = is_auto_increment;
            return this;
        }

        public Column SetNullState(int null_state)
        {
            this.allow_null_state = null_state;
            return this;
        }

        public Column SetDefaultValueState(int defaultValueState)
        {
            this.default_value_state = defaultValueState;
            return this;
        }

        public Column SetDefaultValue(string defaultValue)
        {
            SetDefaultValueState(DEFAULT_VALUE_STATE_HAS);
            this.default_value = defaultValue;
            return this;
        }

        public string GetFieldConfig()
        {
            string sqliteTypeStr = CacheDbTypeToSqliteStrMap[type];
            StringBuilder paramsBuilder = new StringBuilder();
            paramsBuilder.Append(sqliteTypeStr);
            if (is_primary_key)
            {
                paramsBuilder.Clear();
                paramsBuilder.Append("INTEGER");
                paramsBuilder.Append(" PRIMARY KEY");
                if (is_auto_increment)
                {
                    paramsBuilder.Append(" AUTOINCREMENT");
                }
            }

            switch (allow_null_state)
            {
                case NULL_STATE_NO_NULL_PARAM:
                    break;
                case NULL_STATE_HAS_NULL:
                    paramsBuilder.Append(" NULL");
                    break;
                case NULL_STATE_SET_NOT_NULL:
                    paramsBuilder.Append(" NOT NULL");
                    break;

            }
            switch (default_value_state)
            {
                case DEFAULT_VALUE_STATE_NO:
                    break;
                case DEFAULT_VALUE_STATE_HAS:
                    paramsBuilder.Append(" DEFAULT ").Append(default_value);
                    break;
            }
            return paramsBuilder.ToString();
        }
    }
}
