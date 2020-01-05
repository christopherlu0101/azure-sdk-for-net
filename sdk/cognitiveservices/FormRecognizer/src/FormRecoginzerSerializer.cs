using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;
using Microsoft.Azure.CognitiveServices.Vision.FormRecognizer.models;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer
{
    public static class FormRecoginzerSerializer2
    {
        public static void Deserialize(string jsonString)
        {
            var result = new ResponseBody();
            var analyzeResult = new AnalyzeResult();
            ReadOnlySpan<byte> jsonReadOnlySpan = Encoding.UTF8.GetBytes(jsonString);
            var reader = new Utf8JsonReader(jsonReadOnlySpan, isFinalBlock: true, state: default);
            
            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var name = reader.GetString();
                    var upperCamelName = Char.ToUpperInvariant(name[0]) + name.Substring(1);
                    var property = typeof(ResponseBody).GetProperty(upperCamelName);
                    if (property != null)
                    {
                        var propertyType = property.PropertyType;
                        var propertyValue = ParseProperty(ref reader, propertyType);
                        property.SetValue(result, propertyValue, null);
                    }                                     
                }                
            }
        }

        private static object ParseProperty(ref Utf8JsonReader reader, Type type)
        {
            var name = type.FullName;
            if (name == typeof(string).FullName)
            {
                if (reader.Read() && reader.TokenType == JsonTokenType.String)
                {
                    return reader.GetString();
                }
                throw new Exception("Invalid string property.");
            }
            else if(name == typeof(DateTime).FullName)
            {
                if (reader.Read() && reader.TokenType == JsonTokenType.String)
                {
                    return reader.GetDateTime();
                }
                throw new Exception("Invalid DateTime property.");
            }
            else if (name == typeof(IList<ReadResult>).FullName)
            {
                return null;
            }
            else if (name == typeof(IList<PageResult>).FullName)
            {
                return null;
            }
            else if (name == typeof(IList<DocumentResult>).FullName)
            {
                return null;
            }
            else
            {
                return null;
            }
        }

        private static void ParseObject(ref Utf8JsonReader reader, Type type)
        {           
            var obj = Activator.CreateInstance(type);
            foreach (var info in type.GetProperties())                
            {
                try
                {
                    var value = CreateInstanceByType(info.PropertyType);
                    info.SetValue(obj, value, null);
                }
                catch
                {
                    continue;
                }
            }
        }

        private static object CreateInstanceByType(Type type)
        {
            var name = type.FullName;
            if (name == typeof(String).FullName)
            {
                return "abc";
            }
            else if (name == typeof(IList<ReadResult>).FullName)
            {
                return new List<ReadResult>();
            }
            else
            {
                return null;
            }
        }
    }
}
