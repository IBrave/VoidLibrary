using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoidDBLibrary.Model;
using VoidDBLibrary.VoidAttribute;

namespace Test.VoidAttribute
{
    public class TypeTestEntity : Entity
    {
        // public long id;  DefaultValue = "" 不可以
        [Order(1)]
        [Statement(NullState = Column.NULL_STATE_SET_NOT_NULL, DefaultValue = "1")]
        public string name;
        [Order(2)]
        public int int_value;
        [Order(3)]
        public float float_value;
        [Order(4)]
        public double double_value;
        [Order(5)]
        public Int16 int16_value;
        [Order(6)]
        public bool bool_value;
        [Order(7)]
        public byte byte_value;
        [Order(8)]
        public DateTime date_time_value;
        [Order(9)]
        public byte[] byte_array_value;

        public int no_any;
    }
}
