using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VoidDBLibrary;
using System.Data.Common;
using System.Reflection;
using System.ComponentModel;
using System.IO;
using VoidDBLibrary.VoidAttribute;

namespace VoidDBLibrary.Model
{
    public class EntityDao<T> where T : Entity
    {
        private static Dictionary<string, DbType> CacheCSharpToDbTypeMap = new Dictionary<string, DbType>()
        {
            {typeof(double).Name, DbType.Double},
            {typeof(float).Name, DbType.Single},
            {typeof(long).Name, DbType.Int64},
            {typeof(int).Name, DbType.Int32},
            {typeof(Int16).Name, DbType.Int16},
            {typeof(bool).Name, DbType.Boolean},
            {typeof(byte).Name, DbType.Byte},
            {typeof(string).Name, DbType.String},
            {typeof(DateTime).Name, DbType.DateTime},
            {typeof(byte[]).Name, DbType.Binary}
        };

        private string TableName;

        private Dictionary<string, FieldInfo> CacheEntityFieldInfoMap = new Dictionary<string, FieldInfo>();
        private FieldInfo[] CacheEntityFieldInfoArray;

        private Column[] Columns;

        // public static EntityDao<TypeTestEntity> Instance = new EntityDao<TypeTestEntity>();
        private VoidDbHelper senssion;

        public EntityDao()
        {
            Type type = typeof(T);

            TableName = type.Name;

            FieldInfo[] fieldInfos = type.GetFields();
            int fieldNum = fieldInfos.Length;

            Columns = new Column[fieldNum];
            CacheEntityFieldInfoArray = new FieldInfo[fieldNum];

            int hasOrderNum = 0;
            for (int i = fieldNum - 1; i >= 0; i--)
            {
                FieldInfo fieldInfo = fieldInfos[i];
                CacheEntityFieldInfoMap.Add(fieldInfo.Name, fieldInfo);
                OrderAttribute[] orderAttributeArray = (OrderAttribute[])fieldInfo.GetCustomAttributes(typeof(OrderAttribute), false);
                if (orderAttributeArray.Length == 0)
                {
                    continue;
                }
                hasOrderNum++;

                int order = orderAttributeArray[0].Order;

                CacheEntityFieldInfoArray[order] = fieldInfo;

                StatementAttribute[] statementAttributeArray = (StatementAttribute[])fieldInfo.GetCustomAttributes(typeof(StatementAttribute), false);

                DbType dbType = CacheCSharpToDbTypeMap[fieldInfo.FieldType.Name];
                Column column = new Column(fieldInfo.Name, dbType);

                if (statementAttributeArray.Length > 0)
                {
                    StatementAttribute statementAttribute = statementAttributeArray[0];
                    column.is_primary_key = statementAttribute.IsPrimaryKey;
                    column.is_auto_increment = statementAttribute.IsAutoIncrement;
                    column.allow_null_state = statementAttribute.NullState;
                    column.default_value_state = statementAttribute.DefaultValueState;
                    column.default_value = statementAttribute.DefaultValue;
                }

                Columns[order] = column;
            }

            Column[] trimColumns = new Column[hasOrderNum];
            FieldInfo[] trimFieldInfoArray = new FieldInfo[hasOrderNum];
            for (int i = 0; i < hasOrderNum; ++i)
            {
                trimColumns[i] = Columns[i];
                trimFieldInfoArray[i] = CacheEntityFieldInfoArray[i];
            }

            Columns = trimColumns;
            CacheEntityFieldInfoArray = trimFieldInfoArray;
        }

        public void SetSession(VoidDbHelper session)
        {
            this.senssion = session;
        }

        public Column GetColumn(int orderValue)
        {
            return Columns[orderValue];
        }

        private FieldInfo GetFieldInfoByFieldName(string fieldName)
        {
            return CacheEntityFieldInfoMap[fieldName];
        }

        private FieldInfo[] GetFieldInfoArray()
        {
            return CacheEntityFieldInfoArray;
        }

        private string BuildMySqlFieldName(string fieldName)
        {
            //return "`" + fieldName + "`";
            return fieldName;
        }

        private string SqlStrForCreateTable()
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("CREATE TABLE IF NOT EXISTS ")
                .Append(BuildMySqlFieldName(TableName)).Append(" (");
            int column_num = Columns.Length;
            for(int i = 0; i < column_num; ++i)
            {
                Column column = Columns[i];
                sqlBuilder.Append(BuildMySqlFieldName(column.name))
                    .Append(" ")
                    .Append(column.GetFieldConfig());

                if (column_num != i + 1) {
                    sqlBuilder.Append(',');
                } else {
                    sqlBuilder.Append(");");
                }
            }

            Console.WriteLine(sqlBuilder.ToString());
            return sqlBuilder.ToString();
        }

        public bool CreateTable()
        {
            bool result = senssion.ExecuteNonQuery(SqlStrForCreateTable(), null) > 0;
            return result;
        }

        private List<DbParameter> bindValues(T entity)
        {
            DbParameter dbParameter;
            Column column = Columns[0];
            List<DbParameter> dbParameters = new List<DbParameter>();
            if (entity.id != 0)
            {
                dbParameter = senssion.CreateParameter(column.name, entity.id, column.type);
                dbParameters.Add(dbParameter);
            }

            int len = Columns.Length;
            for (int i = 1; i < len; ++i)
            {
                column = Columns[i];
                // System.Reflection.PropertyInfo propertyInfo = type.GetProperty(column.name); // Set Get Method
                FieldInfo fieldInfo = GetFieldInfoByFieldName(column.name);
                // Name And Type 都可以反射获取
                dbParameter = senssion.CreateParameter(column.name, fieldInfo.GetValue(entity), column.type);
                dbParameters.Add(dbParameter);
            }

            return dbParameters;
        }

        public int Insert(T entity)
        {
            StringBuilder paramKeyBuilder = new StringBuilder();
            StringBuilder paramNameBuilder = new StringBuilder();

            List<DbParameter> dbParameters = bindValues(entity);
            for (int i = 0; i < dbParameters.Count; ++i)
            {
                paramKeyBuilder.Append(dbParameters[i].ParameterName).Append(",");
                paramNameBuilder.Append("@" + dbParameters[i].ParameterName).Append(",");
            }
            if (dbParameters.Count > 0)
            {
                paramKeyBuilder.Remove(paramKeyBuilder.Length - 1, 1);
                paramNameBuilder.Remove(paramNameBuilder.Length - 1, 1);
            }
            string cmdText = String.Format("INSERT INTO {0} ({1}) VALUES ({2})", TableName, paramKeyBuilder.ToString(), paramNameBuilder.ToString());
            Console.WriteLine(cmdText);

            DateTime h = DateTime.Now;
            senssion.ExecuteNonQuery(cmdText, dbParameters.ToArray());
            Console.WriteLine((DateTime.Now - h).TotalMilliseconds);
            return 1;
        }

        public int Insert(List<T> entityList)
        {
            return 0;
        }

        public int Update(T entity)
        {
            StringBuilder paramKeyBuilder = new StringBuilder();

            List<DbParameter> dbParameters = bindValues(entity);
            for (int i = 0; i < dbParameters.Count; ++i)
            {
                paramKeyBuilder.Append(dbParameters[i].ParameterName).Append("=@").Append(dbParameters[i].ParameterName).Append(",");
            }
            if (dbParameters.Count > 0)
            {
                paramKeyBuilder.Remove(paramKeyBuilder.Length - 1, 1);
            }
            string cmdText = String.Format("UPDATE {0} SET {1} WHERE {2}", TableName, paramKeyBuilder.ToString(), "id=" + entity.id);

            return VoidSqlite3Helper.Instance.ExecuteNonQuery(cmdText, dbParameters.ToArray());
        }

        public int Update(List<T> entityList)
        {
            return 0;
        }

        public int Delete(T entity)
        {
            string cmdText = String.Format("DELETE FROM {0} WHERE {1}", TableName, "id=" + entity.id);
            return VoidSqlite3Helper.Instance.ExecuteNonQuery(cmdText);
        }

        public List<T> QueryAll()
        {
            string cmdText = String.Format("SELECT * FROM {0}", TableName);
            DbDataReader dbDataReader = VoidSqlite3Helper.Instance.ExecuteReader(cmdText);
            List<T> studentList = Parse<T>(dbDataReader);
            return studentList;
        }

        public List<T> Query(int id)
        {
            string cmdText = String.Format("SELECT * FROM {0} WHERE {1}", TableName, "id=" + id);
            DbDataReader dbDataReader = VoidSqlite3Helper.Instance.ExecuteReader(cmdText);
            List<T> studentList = Parse<T>(dbDataReader);
            return studentList;
        }

        /// <summary>
        /// http://blog.sina.com.cn/s/blog_ab4c5be80102wgfi.html
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbDataReader"></param>
        /// <returns></returns>
        private List<T> Parse<T>(DbDataReader dbDataReader)
        {
            Type type = typeof(T);
            FieldInfo[] fieldInfos = GetFieldInfoArray();

            List<T> entityList = new List<T>();

            while(dbDataReader.Read())
            {
                T entity = (T)type.Assembly.CreateInstance(type.GetTypeInfo().FullName);
                foreach (FieldInfo fieldInfo in fieldInfos)
                {
                    int ordinal = dbDataReader.GetOrdinal(fieldInfo.Name);
                    string dataTypeName = dbDataReader.GetDataTypeName(ordinal);
                    switch(dataTypeName)
                    {
                        case "INT16":
                            fieldInfo.SetValue(entity, dbDataReader.GetInt16(ordinal));
                            break;
                        case "INTEGER":
                        case "INT32":
                            fieldInfo.SetValue(entity, dbDataReader.GetInt32(ordinal));
                            break;
                        case "INT64":
                            fieldInfo.SetValue(entity, dbDataReader.GetInt64(ordinal));
                            break;
                        case "FLOAT":
                            fieldInfo.SetValue(entity, dbDataReader.GetFloat(ordinal));
                            break;
                        case "DOUBLE":
                            fieldInfo.SetValue(entity, dbDataReader.GetDouble(ordinal));
                            break;
                        case "DATETIME":
                            fieldInfo.SetValue(entity, dbDataReader.GetDateTime(ordinal));
                            break;
                        case "STRING":
                            if (dbDataReader.IsDBNull(ordinal))
                            {
                            }
                            else
                            {
                                fieldInfo.SetValue(entity, dbDataReader.GetValue(ordinal));
                            }
                            break;
                        case "BOOLEAN":
                            fieldInfo.SetValue(entity, dbDataReader.GetBoolean(ordinal));
                            break;
                        case "TINYINT":
                            fieldInfo.SetValue(entity, dbDataReader.GetByte(ordinal));
                            break;
                        case "BLOB":
                            fieldInfo.SetValue(entity, GetBytes(dbDataReader, ordinal));
                            break;
                    }
                    // Console.WriteLine(dataTypeName);
                }
                entityList.Add(entity);
            }

            return entityList;
        }

        static byte[] GetBytes(DbDataReader reader, int ordinal)
        {
            const int CHUNK_SIZE = 100;
            byte[] buffer = new byte[CHUNK_SIZE];
            long bytesRead;
            long fieldOffset = 0;
            using (MemoryStream stream = new MemoryStream())
            {
                while ((bytesRead = reader.GetBytes(ordinal, fieldOffset, buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, (int)bytesRead);
                    fieldOffset += bytesRead;
                }
                return stream.ToArray();
            }
        }

    }

}
