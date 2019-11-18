using Microsoft.Azure.CognitiveServices.FormRecognizer;
using Microsoft.Azure.Test.HttpRecorder;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using Xunit;
using System.IO;

namespace FormRecognizerSDK.Tests
{
    public class FormRecognizerTests : BaseTests
    {
        [Fact]
        public void Test_ApiKey_Endpoint_Overload()
        {
            var client = new FormRecognizerClient("fake-key", "http://example.org");
        }

        [Fact(Skip = "not completed")]
        public void FormRecognizerSDK_AnalyzeWithCustomModelAsync()
        {
            using (MockContext context = MockContext.Start(this.GetType()))
            {
                HttpMockServer.Initialize(this.GetType(), "FormRecognizerSDK_AnalyzeWithCustomModelAsync");

                using (IFormRecognizerClient client = GetFormRecognizerClient(HttpMockServer.CreateInstance()))
                using (FileStream stream = new FileStream(GetTestImagePath("Receipt_003_934.jpg"), FileMode.Open))
                {
                    //var result = client.AnalyzeWithCustomModelAsync(new Guid(), false, stream).Result;
                }
            }
        }

        // [Fact]
        // public void FormRecognizerSDK_AnalyzeReceiptAsync()
        // {
        //     var expectedResultJson = JObject.Parse(File.ReadAllText(GetTestImagePath("Receipt_003_934.json")));
        //     var expectedResult = JsonConvert.DeserializeObject<AnalyzeResult>(expectedResultJson.ToString());
        //     using (MockContext context = MockContext.Start(this.GetType()))
        //     {
        //         HttpMockServer.Initialize(this.GetType(), "FormRecognizerSDK_AnalyzeReceiptAsync");

        //         using (IFormRecognizerClient client = GetFormRecognizerClient(HttpMockServer.CreateInstance()))
        //         {
        //             using (FileStream stream = new FileStream(GetTestImagePath("Receipt_003_934.jpg"), FileMode.Open))
        //             {
        //                 var streamResult = client.AnalyzeReceiptAsync(stream).Result;
        //                 // TODO - check if result match expectation

        //                 using (var streamReader = new MemoryStream())
        //                 {
        //                     // stream.CopyTo(streamReader);
        //                     // var byteArray = streamReader.ToArray();
        //                     // var byteResult = client.AnalyzeReceiptAsync(byteArray).Result;
        //                     // TODO - check if result match expectation
        //                 }
        //             }
        //             //var uriResult = client.AnalyzeReceiptAsync("test1").Result;
        //             // TODO - check if result match expectation
        //         }
        //     }
        // }

        //[Fact]
        //public void testInbackend()
        //{
        //    using (IFormRecognizerClient client = new FormRecognizerClient("13e16dad1b48493fb91817d9a76ce8f5", @"https://forms-recognizer-v2.use2.dev.api.cog.trafficmanager.net/svc"))
        //    {
        //        using (FileStream stream = new FileStream(GetTestImagePath("Receipt_003_934.jpg"), FileMode.Open))
        //        {
        //            var streamResult = client.AnalyzeReceiptAsync(stream).Result;

        //        }
        //    }
        //}
    }
}
