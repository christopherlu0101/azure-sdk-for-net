using Microsoft.Azure.CognitiveServices.Vision.FormRecognizer;
using Xunit;
using System;
using System.IO;

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

            using (FileStream stream = new FileStream(@"D:/Data/Receipt_000_000.jpg", FileMode.Open))
            {
                var id = client.StartAnalyzeReceiptAsync(stream, ContentType.Jpeg).Result;
                //var result = client.GetReceiptAnalyzeResultAsync(id).Result;
                //Console.WriteLine(result);
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
            var obj1 = FormRecognizerSerializer.Deserialize(jsonString);
            var json1 = FormRecognizerSerializer.Serialize(obj1);
            var obj2 = FormRecognizerSerializer.Deserialize(json1);
            var json2 = FormRecognizerSerializer.Serialize(obj2);
            Assert.Equal(json1, json2);
        }
    }
}
