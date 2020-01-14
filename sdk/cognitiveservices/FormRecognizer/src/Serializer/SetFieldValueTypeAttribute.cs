using System;
using System.Reflection;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    internal class SetFieldValueTypeAttribute : BaseCustomAttribute
    {
        private FieldValueType? _fieldValueType = null;

        public SetFieldValueTypeAttribute(FieldValueType value)
        {
            _fieldValueType = value;
        }

        public override bool ShouldSerialize(PropertyInfo propertyInfo, object obj)
        {
            if (_fieldValueType != null)
            {
                var fieldValue = (FieldValue)obj;
                return (fieldValue.Type == _fieldValueType.Value);
            }
            throw new Exception("Target field value type not set.");
        }
    }
}
