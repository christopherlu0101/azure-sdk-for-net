using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer
{
    internal static class FormRecoginzerSerializer
    {
        public static AnalyzeResult Deserialize(string jsonString)
        {
            var analyzeResult = new AnalyzeResult();
            ReadOnlySpan<byte> jsonReadOnlySpan = Encoding.UTF8.GetBytes(jsonString);
            var json = new Utf8JsonReader(jsonReadOnlySpan, isFinalBlock: true, state: default);

            var test = new List<JsonTokenType>();

            while (json.Read())
            {
                test.Add(json.TokenType);
                if (json.TokenType == JsonTokenType.PropertyName)
                {
                    var name = json.GetString();
                    if (json.Read())
                    {
                        switch (name)
                        {
                            case "status":
                                if (json.GetString() != "succeeded")
                                {
                                    // Service not complete.
                                    return null;
                                }
                                break;
                            case "version":
                                analyzeResult.Version = json.GetString();
                                break;
                            case "readResults":
                                analyzeResult.ReadResults = ParseReadResults(ref json);
                                break;
                            case "documentResults":
                                analyzeResult.DocumentResults = ParseDocumentResults(ref json);
                                break;
                            case "pageResults":
                                analyzeResult.PageResults = ParsePageResults(ref json);
                                break;
                            case "createdDateTime":
                            case "lastUpdatedDateTime":
                            case "analyzeResult":
                                break;
                            default:
                                throw new Exception("Invalid property name.");
                        }
                    }
                    else
                    {
                        throw new Exception("Property value not found.");
                    }
                }
            }
            return analyzeResult;
        }

        private static IList<ReadResult> ParseReadResults(ref Utf8JsonReader json)
        {
            if (json.TokenType == JsonTokenType.StartArray)
            {
                var readResults = new List<ReadResult>();
                while (json.Read() && json.TokenType != JsonTokenType.EndArray)
                {
                    readResults.Add(ParseReadResult(ref json));
                }
                return readResults;
            }
            throw new Exception("Invalid property value.");
        }

        //private static T ParseObject<T>(ref Utf8JsonReader json, Func<int, T> propertyProcessor)
        //{
        //    T result;
        //    int cnt = 0;
        //    do
        //    {
        //        switch (json.TokenType)
        //        {
        //            case JsonTokenType.StartObject:
        //                cnt++;
        //                break;
        //            case JsonTokenType.EndObject:
        //                cnt--;
        //                break;
        //            case JsonTokenType.PropertyName:
        //                var name = json.GetString();
        //                var upperCamelName = Char.ToUpperInvariant(name[0]) + name.Substring(1);
        //                if (json.Read())
        //                {
        //                    var member = typeof(T).GetMember(upperCamelName)[0];
        //                }
        //                else
        //                {
        //                    throw new Exception("Property value not found.");
        //                }
        //                break;
        //        }
        //    }
        //    while (json.Read() && cnt > 0);
        //    return result;
        //}

        private static ReadResult ParseReadResult(ref Utf8JsonReader json)
        {
            var readResult = new ReadResult();
            int objectNum = 0;
            do
            {
                switch (json.TokenType)
                {
                    case JsonTokenType.StartObject:
                        objectNum++;
                        break;
                    case JsonTokenType.EndObject:
                        objectNum--;
                        break;
                    case JsonTokenType.PropertyName:
                        var name = json.GetString();
                        if (json.Read())
                        {
                            switch (name)
                            {
                                case "page":
                                    readResult.Page = json.GetInt32();
                                    break;
                                case "angle":
                                    readResult.Angle = json.GetDouble();
                                    break;
                                case "width":
                                    readResult.Width = json.GetDouble();
                                    break;
                                case "height":
                                    readResult.Height = json.GetDouble();
                                    break;
                                case "unit":
                                    Enum.TryParse(json.GetString(), true, out LengthUnit lengthUnit);
                                    readResult.Unit = lengthUnit;
                                    break;
                                case "language":
                                    readResult.Language = json.GetString();
                                    break;
                                case "lines":
                                    readResult.Lines = null;
                                    break;
                                default:
                                    throw new Exception("Invalid property name for ReadResult.");
                            }
                        }
                        else
                        {
                            throw new Exception("Property value not found.");
                        }
                        break;
                }                
            }
            while (objectNum > 0 && json.Read());
            return readResult;
        }

        private static IList<DocumentResult> ParseDocumentResults(ref Utf8JsonReader json)
        {
            if (json.TokenType == JsonTokenType.StartArray)
            {
                var documentResults = new List<DocumentResult>();
                while (json.Read() && json.TokenType != JsonTokenType.EndArray)
                {
                    documentResults.Add(ParseDocumentResult(ref json));
                }
                return documentResults;
            }
            throw new Exception("Invalid property value.");
        }

        private static DocumentResult ParseDocumentResult(ref Utf8JsonReader json)
        {
            var documentResult = new DocumentResult();
            int objectNum = 0;
            do
            {
                switch (json.TokenType)
                {
                    case JsonTokenType.StartObject:
                        objectNum++;
                        break;
                    case JsonTokenType.EndObject:
                        objectNum--;
                        break;
                    case JsonTokenType.PropertyName:
                        var name = json.GetString();
                        if (json.Read())
                        {
                            switch (name)
                            {
                                case "docType":
                                    documentResult.DocType = json.GetString();
                                    break;
                                case "pageRange":
                                    if (json.TokenType == JsonTokenType.StartArray)
                                    {
                                        var pageRange = new List<int>();
                                        while (json.Read() && json.TokenType != JsonTokenType.EndArray)
                                        {
                                            pageRange.Add(json.GetInt32());
                                        }
                                        documentResult.PageRange = pageRange;
                                        break;
                                    }                                    
                                    throw new Exception("Invalid property value for DocumentResult PageRange.");
                                case "fields":
                                    documentResult.Fields = ParseFields(ref json);
                                    break;
                                default:
                                    throw new Exception("Invalid property name for DocumentResult.");
                            }
                        }
                        else
                        {
                            throw new Exception("Property value not found.");
                        }
                        break;
                }
            }
            while (objectNum > 0 && json.Read());
            return documentResult;
        }

        private static IDictionary<string, FieldValue> ParseFields(ref Utf8JsonReader json)
        {
            var fields = new Dictionary<string, FieldValue>();
            int objectNum = 0;
            do
            {
                switch (json.TokenType)
                {
                    case JsonTokenType.StartObject:
                        objectNum++;
                        break;
                    case JsonTokenType.EndObject:
                        objectNum--;
                        break;
                    case JsonTokenType.PropertyName:
                        var fieldName = json.GetString();
                        if (json.Read())
                        {                            
                            fields.Add(fieldName, ParseField(ref json));
                            break;
                        }
                        throw new Exception("Property value not found.");                        
                    default:
                        throw new Exception("Invalid field value.");
                }
            }
            while (objectNum > 0 && json.Read());
            return fields;
        }

        private static FieldValue ParseField(ref Utf8JsonReader json)
        {
            if (json.TokenType == JsonTokenType.StartObject)
            {
                var field = new FieldValue();
                int objectNum = 0;
                do
                {
                    switch (json.TokenType)
                    {
                        case JsonTokenType.StartObject:
                            objectNum++;
                            break;
                        case JsonTokenType.EndObject:
                            objectNum--;
                            break;
                        case JsonTokenType.PropertyName:
                            var name = json.GetString();
                            if (json.Read())
                            {
                                switch (name)
                                {
                                    case "type":
                                        Enum.TryParse(json.GetString(), true, out FieldValueType fieldValueType);
                                        field.Type = fieldValueType;
                                        break;
                                    case "valueString":
                                        field.ValueString = json.GetString();
                                        break;
                                    case "valueDate":
                                        field.ValueDate = json.GetString();
                                        break;
                                    case "valueTime":
                                        field.ValueTime = json.GetString();
                                        break;
                                    case "valuePhoneNumber":
                                        field.ValuePhoneNumber = json.GetString();
                                        break;
                                    case "valueNumber":
                                        field.ValueNumber = json.GetDouble();
                                        break;
                                    case "valueInteger":
                                        field.ValueInteger = json.GetInt32();
                                        break;
                                    case "valueArray":
                                        var fieldArray = new List<FieldValue>();
                                        while (json.Read() && json.TokenType != JsonTokenType.EndArray)
                                        {
                                            fieldArray.Add(ParseField(ref json));
                                        }
                                        field.ValueArray = fieldArray;
                                        break;
                                    case "boundingBox":
                                        var boundingBox = new List<double>();
                                        while (json.Read() && json.TokenType != JsonTokenType.EndArray)
                                        {
                                            boundingBox.Add(json.GetDouble());
                                        }
                                        field.BoundingBox = boundingBox;
                                        break;
                                    case "valueObject":
                                        break;
                                    case "text":
                                        field.Text = json.GetString();
                                        break;
                                    case "confidence":
                                        field.Confidence = json.GetDouble();
                                        break;
                                    case "elements":
                                        break;
                                    case "page":
                                        field.Page = json.GetInt32();
                                        break;
                                    default:
                                        throw new Exception($"Invalid field value name : {name}.");
                                }
                                break;
                            }
                            throw new Exception("Invalid field value.");                                                        
                        default:
                            throw new Exception("Invalid field value.");
                    }
                }
                while (objectNum > 0 && json.Read());
                return field;
            }
            throw new Exception("Invalid property value for DocumentResult Fields");
        }

        private static IList<PageResult> ParsePageResults(ref Utf8JsonReader json)
        {
            return null;
        }
    }
}
