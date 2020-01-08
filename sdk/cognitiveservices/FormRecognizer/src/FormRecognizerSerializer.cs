using System;
using System.Linq;
using System.Text.Json;
using Microsoft.Azure.CognitiveServices.Vision.FormRecognizer.Models;

namespace Microsoft.Azure.CognitiveServices.Vision.FormRecognizer
{
    public static class FormRecognizerSerializer
    {
        private static JsonSerializerOptions _defaultOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            IgnoreNullValues = true
        };

        public static ResponseBody Deserialize(string jsonString, JsonSerializerOptions options = null)
        {            
            var body = JsonSerializer.Deserialize<ResponseBody>(jsonString, options ?? _defaultOptions);
            body.AnalyzeResult?.ReadResults.ToList().
                ForEach(readResult => readResult.Lines?.ToList().
                ForEach(line => line.Language = line.Language ?? readResult.Language));
            return body;
        }

        public static string Serialize(ResponseBody responseBody, JsonSerializerOptions options = null)
        {
            return Serialize(responseBody.AnalyzeResult, options);
        }

        public static string Serialize(AnalyzeResult analyzeResult, JsonSerializerOptions options = null)
        {
            analyzeResult.ReadResults.ToList().
                ForEach(readResult => readResult.Lines.ToList().
                ForEach(line => line.Language = (line.Language == readResult.Language) ? null : line.Language));
            return JsonSerializer.Serialize(analyzeResult, options ?? new JsonSerializerOptions());
        }
    }

    public static class CasingHelper
    {
        public static string ToUpperCamelCasing(string str)
        {
            return Char.ToUpperInvariant(str[0]) + str.Substring(1);
        }

        public static string ToLowerCamelCasing(string str)
        {
            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
    }
}
