using System;
using System.Reflection;

namespace Azure.AI.FormRecognizer.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    internal class IgnoreDefaultAttribute : BaseCustomAttribute
    {
        private int? _intValue = null;
        private double? _doubleValue = null;

        public IgnoreDefaultAttribute(int value)
        {
            _intValue = value;
        }

        public IgnoreDefaultAttribute(double value)
        {
            _doubleValue = value;
        }

        public override bool ShouldSerialize(PropertyInfo propertyInfo, object obj)
        {
            var value = propertyInfo.GetValue(obj);
            if (_intValue != null)
            {
                return (_intValue.Value != (int)value);
            }
            else if (_doubleValue != null)
            {
                return (_doubleValue.Value != (double)value);
            }
            else
            {
                throw new Exception("Only support default value for int and double.");
            }
        }
    }
}
