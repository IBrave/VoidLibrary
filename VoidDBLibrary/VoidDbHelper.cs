using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace VoidDBLibrary
{
    public class VoidDbHelper
    {
        public static VoidDbHelper Instance;

        public virtual void Create(string fileNameNoExtension)
        {
        }

        public virtual void Open()
        {
        }

        public virtual void Close()
        {
        }

        public virtual int ExecuteNonQuery(string sql)
        {
            return -1;
        }

        public virtual int ExecuteNonQuery(string commandText, params DbParameter[] parameters)
        {
            return -1;
        }

        public virtual DbDataReader ExecuteReader(string commandText)
        {
            return null;
        }

        public virtual DbDataReader ExecuteReader(string commandText, params DbParameter[] parameters)
        {
            return null;
        }

        public virtual DbParameter CreateParameter(string key, object value, DbType type)
        {
            return null;
        }
    }
}
