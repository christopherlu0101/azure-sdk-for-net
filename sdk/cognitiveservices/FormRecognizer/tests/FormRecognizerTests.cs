// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Xunit;
using System;
using System.IO;
using System.Text.Json;
using Azure.AI.FormRecognizer;
using System.Text.Json.Serialization;

namespace FormRecognizerSDK.Tests
{
    public class FormRecognizerTests
    {
        private const string APIKEY = "184654c847d54432b8301a4b76f63045";
        private static Uri _endpoint = new Uri("https://westus2.ppe.cognitiveservices.azure.com");

        [Fact]
        public void AnalyzeReceipt_APIM_StreamContent()
        {
            var client = new FormRecognizerClient(_endpoint, APIKEY);
            using (FileStream stream = new FileStream(@"TestImages/Receipt_044_065.jpg", FileMode.Open))
            {
                var contentType = new System.Net.Mime.ContentType { MediaType = System.Net.Mime.MediaTypeNames.Image.Jpeg };
                var operation = client.StartAnalyzeReceiptAsync(stream, contentType, true).Result;
                var result = operation.WaitForCompletionAsync().Result;
                Assert.True(result.Value != null);
            }
        }

        [Fact]
        public void AnalyzeReceipt_APIM_UriContent()
        {
            var client = new FormRecognizerClient(_endpoint, APIKEY);
            var operation = client.StartAnalyzeReceiptAsync(new Uri("https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSAwPgac6wRJwk-bDoSjUsc5UXCMouwr0ICk2nVXNDqtRhvJA8t"), true).Result;
            var result = operation.WaitForCompletionAsync().Result;
            Assert.True(result.Value != null);
        }

        [Fact]
        public void AnalyzeReceipt_APIM_expectedFail()
        {
            try
            {
                var client = new FormRecognizerClient(_endpoint, APIKEY);
                using (FileStream stream = new FileStream(@"TestImages/Receipt_044_065.jpg", FileMode.Open))
                {
                    var uri = new Uri("https://westus2.ppe.cognitiveservices.azure.com/formrecognizer/v2.0-preview/prebuilt/receipt/analyzeResults/not_exist_guid");
                    var operation = client.StartAnalyzeReceipt(uri, true);
                    var result = operation.WaitForCompletionAsync().Result;
                }
                throw new Exception("Expected fail somehow success.");
            }
            catch
            {
                Console.WriteLine();
            }
        }


        //[Fact]
        //public void Serialization()
        //{
        //    var options = new JsonSerializerOptions
        //    {
        //        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        //        IgnoreNullValues = true,
        //        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        //    };
        //    var jsonString = File.ReadAllText(@"TestImages/testJson.json");
        //    var obj = FormRecognizerSerializer.Deserialize(jsonString, options);
        //    var json = FormRecognizerSerializer.Serialize(obj, options);
        //}
    }
}
