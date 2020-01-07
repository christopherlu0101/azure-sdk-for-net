using Microsoft.Azure.CognitiveServices.Vision.FormRecognizer;
using Microsoft.Azure.CognitiveServices.Vision.FormRecognizer.Models;
using Xunit;
using System;
using System.IO;
using System.Text.Json;

namespace FormRecognizerSDK.Tests
{
    public class FormRecognizerTests
    {
        [Fact]
        public void test()
        {
            //var endpoint = "https://westus2.ppe.cognitiveservices.azure.com/formrecognizer/v2.0-preview/prebuilt/receipt/analyze";
            var endpoint = "https://westus2.ppe.cognitiveservices.azure.com";
            var apiKey = "184654c847d54432b8301a4b76f63045";
            var client = new FormRecognizerClient(new Uri(endpoint), apiKey);

            using (FileStream stream = new FileStream(@"D:/Data/Receipt_044_065.jpg", FileMode.Open))
            {
                var operation = client.StartAnalyzeReceiptAsync(stream, ContentType.Jpeg).Result;
                Console.WriteLine(operation.UpdateStatus());
                Console.WriteLine(operation.Value);
            }
        }

        //[Fact]
        //public void test2()
        //{
        //    //var endpoint = "https://westus2.ppe.cognitiveservices.azure.com/formrecognizer/v2.0-preview/prebuilt/receipt/analyze";
        //    var endpoint = "https://westus2.ppe.cognitiveservices.azure.com/formrecognizer/v2.0-preview/";
        //    var apiKey = "184654c847d54432b8301a4b76f63045";
        //    var client = new FormRecognizerClient(endpoint, apiKey);
        //    var id = client.StartAnalyzeReceiptAsync("https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSAwPgac6wRJwk-bDoSjUsc5UXCMouwr0ICk2nVXNDqtRhvJA8t").Result;
        //}

        [Fact]
        public void offlineTest()
        {
            var jsonString = File.ReadAllText(@"TestImages/json1.json");

            var a = JsonSerializer.Deserialize<ResponseBody>(jsonString);
            var b = JsonSerializer.Serialize(a);

            //var obj1 = FormRecognizerSerializer.Deserialize(jsonString);
            //var json1 = FormRecognizerSerializer.Serialize(obj1);
            //var obj2 = FormRecognizerSerializer.Deserialize(json1);
            //var json2 = FormRecognizerSerializer.Serialize(obj2);
            //Assert.Equal(json1, json2);
        }
    }
}
