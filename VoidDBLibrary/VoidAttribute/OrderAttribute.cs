using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VoidDBLibrary.VoidAttribute
{
    /// <summary>
    /// https://stackoverflow.com/questions/5473455/c-sharp-get-fieldinfos-propertyinfos-in-the-original-order
    /// no use https://stackoverflow.com/questions/9062235/get-properties-in-order-of-declaration-using-reflection
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    [ImmutableObject(true)]
    public sealed class OrderAttribute : System.Attribute
    {
        private readonly int order;
        public int Order { get { return order; } }
        public OrderAttribute(int order) { this.order = order; }
    }

}
