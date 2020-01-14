using System;
using System.Reflection;

namespace Azure.AI.FormRecognizer
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    internal abstract class BaseCustomAttribute : Attribute
    {
        public BaseCustomAttribute()
        {
        }
        public abstract bool ShouldSerialize(PropertyInfo propertyInfo, object obj);
    }
}
