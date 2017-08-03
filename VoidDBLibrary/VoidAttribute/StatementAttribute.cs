using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VoidDBLibrary.Model;

namespace VoidDBLibrary.VoidAttribute
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    [ImmutableObject(true)]
    public class StatementAttribute : System.Attribute
    {
        private bool isPrimaryKey;
        private bool isAutoIncrement;
        private int nullState;
        private int defaultValueState;
        private string defaultValue;

        public bool IsPrimaryKey
        {
            set
            {
                isPrimaryKey = value;
            }
            get
            {
                return isPrimaryKey;
            }
        }

        public bool IsAutoIncrement
        {
            set
            {
                isAutoIncrement = value;
            }
            get
            {
                return isAutoIncrement;
            }
        }

        public int NullState
        {
            set
            {
                nullState = value;
            }
            get
            {
                return nullState;
            }
        }

        public int DefaultValueState
        {
            set
            {
                defaultValueState = value;
            }
            get
            {
                return defaultValueState;
            }
        }

        public string DefaultValue
        {
            set
            {
                defaultValueState = Column.DEFAULT_VALUE_STATE_HAS;
                defaultValue = value;
            }
            get
            {
                return defaultValue;
            }
        }

    }
}
