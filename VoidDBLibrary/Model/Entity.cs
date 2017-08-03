using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VoidDBLibrary.VoidAttribute;

namespace VoidDBLibrary.Model
{
    public class Entity
    {
        [Order(0)]
        [Statement(IsPrimaryKey = true, IsAutoIncrement = true)]
        public long id;
    }
}
