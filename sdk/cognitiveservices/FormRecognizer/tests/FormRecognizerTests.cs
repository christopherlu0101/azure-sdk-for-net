using Microsoft.Azure.CognitiveServices.Vision.FormRecognizer;
using Microsoft.Azure.Test.HttpRecorder;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using Newtonsoft.Json;
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
            var endpoint = "https://westus2.ppe.cognitiveservices.azure.com/formrecognizer/v2.0-preview/prebuilt/receipt/analyze";
            var apiKey = "184654c847d54432b8301a4b76f63045";
            var client = new FormRecognizerClient(endpoint, apiKey);

            using (FileStream stream = new FileStream(@"D:/Data/Receipt_000_000.jpg", FileMode.Open))
            {
                var id = client.StartAnalyzeReceiptAsync(stream).Result;

            }
        }
    }
}
