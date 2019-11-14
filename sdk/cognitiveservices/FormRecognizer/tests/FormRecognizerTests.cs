//using Microsoft.Azure.CognitiveServices.FormRecognizer;
//using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;
//using Microsoft.Azure.Test.HttpRecorder;
//using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using Xunit;
//using Xunit.Abstractions;
//using System;
//using System.IO;
//using Moq;

//namespace FormRecognizerSDK.Tests
//{
//    public class FormRecognizerTests : BaseTests
//    {
//        private ITestOutputHelper _output;

//        public FormRecognizerTests(ITestOutputHelper output)
//        {
//            _output = output;
//        }

//        [Fact]
//        public void VerifyFormClientObjectCreation()
//        {
//            var client = GetFormRecognizerClient(null);            
//            Assert.True(client.GetType() == typeof(FormRecognizerClient));
//        }

//        [Fact(Skip = "not completed")]
//        public void FormRecognizerSDK_AnalyzeWithCustomModelAsync()
//        {
//            using (MockContext context = MockContext.Start(this.GetType()))
//            {
//                HttpMockServer.Initialize(this.GetType(), "FormRecognizerSDK_AnalyzeWithCustomModelAsync");

//                using (IFormRecognizerClient client = GetFormRecognizerClient(HttpMockServer.CreateInstance()))
//                using (FileStream stream = new FileStream(GetTestImagePath("Receipt_003_934.jpg"), FileMode.Open))
//                {
//                    //var result = client.AnalyzeWithCustomModelAsync(new Guid(), false, stream).Result;
//                }
//            }
//        }

//        [Fact]
//        public void FormRecognizerSDK_AnalyzeReceiptAsync()
//        {
//            var expectedResultJson = JObject.Parse(File.ReadAllText(GetTestImagePath("Receipt_003_934.json")));
//            var expectedResult = JsonConvert.DeserializeObject<AnalyzeResult>(expectedResultJson.ToString());
//            using (MockContext context = MockContext.Start(this.GetType()))
//            {
//                HttpMockServer.Initialize(this.GetType(), "FormRecognizerSDK_AnalyzeReceiptAsync");
                
//                using (IFormRecognizerClient client = GetFormRecognizerClient(HttpMockServer.CreateInstance()))
//                {
//                    using (FileStream stream = new FileStream(GetTestImagePath("Receipt_003_934.jpg"), FileMode.Open))
//                    {
//                        var streamResult = client.AnalyzeReceiptAsync(stream, AnalysisContentType.Jpeg).Result;
//                        // TODO - check if result match expectation

//                        using (var streamReader = new MemoryStream())
//                        {
//                            // stream.CopyTo(streamReader);
//                            // var byteArray = streamReader.ToArray();
//                            // var byteResult = client.AnalyzeReceiptAsync(byteArray).Result;
//                            // TODO - check if result match expectation
//                        }
//                    }
//                    //var uriResult = client.AnalyzeReceiptAsync("test1").Result;
//                    // TODO - check if result match expectation
//                }
//            }
//        }


//        [Fact]
//        public void mocktest()
//        {

//            var mockClientBase = new Mock<FormRecognizerClient>().Object;
//            using (FileStream stream = new FileStream(GetTestImagePath("Receipt_003_934.jpg"), FileMode.Open))
//            {

//                var client = new FormRecognizerClient("184654c847d54432b8301a4b76f63045", @"https://westus2.ppe.cognitiveservices.azure.com");

//                //mockClientBase.Setup(op => op.ToString()).Returns("a");


//                //var o = mockClientBase.Object;
//                //_output.WriteLine(o.ToString());

//                //var streamResult = client.AnalyzeReceiptAsync(stream, AnalysisContentType.Jpeg).Result.AnalyzeResult;
//                //var serializedResult = JsonConvert.SerializeObject(streamResult);

//            }


            
//        }

//        [Fact]
//        public void AnalyzeReceiptAsync_APIM_StreamExpectedResult()
//        {
//            var expectedResult = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<AnalyzeResult>(File.ReadAllText(GetTestImagePath("Receipt_003_934.json"))));
//            using (IFormRecognizerClient client = new FormRecognizerClient("184654c847d54432b8301a4b76f63045", @"https://westus2.ppe.cognitiveservices.azure.com"))
//            {
//                using (FileStream stream = new FileStream(GetTestImagePath("Receipt_003_934.jpg"), FileMode.Open))
//                {
//                    var streamResult = client.AnalyzeReceiptAsync(stream, AnalysisContentType.Jpeg).Result.AnalyzeResult;
//                    var serializedResult = JsonConvert.SerializeObject(streamResult);
//                    Assert.Equal(expectedResult, serializedResult);
//                }                
//            }
//        }

//        [Fact]
//        public void AnalyzeReceiptAsync_APIM_ByteArrayExpectedResult()
//        {
//            var expectedResult = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<AnalyzeResult>(File.ReadAllText(GetTestImagePath("Receipt_003_934.json"))));
//            using (IFormRecognizerClient client = new FormRecognizerClient("184654c847d54432b8301a4b76f63045", @"https://westus2.ppe.cognitiveservices.azure.com"))
//            {
//                using (FileStream stream = new FileStream(GetTestImagePath("Receipt_003_934.jpg"), FileMode.Open))
//                {
//                    using (var reader = new MemoryStream())
//                    {
//                        stream.CopyTo(reader);
//                        var streamResult = client.AnalyzeReceiptAsync(reader.ToArray(), AnalysisContentType.Jpeg).Result;
//                        var serializedResult = JsonConvert.SerializeObject(streamResult);
//                        Assert.Equal(expectedResult, serializedResult);
//                    }
//                }
//            }
//        }

//        [Fact]
//        public void AnalyzeLayoutAsync_APIM_StreamExpectedResult()
//        {
//            var expectedResult = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<AnalyzeResult>(File.ReadAllText(GetTestImagePath("Receipt_003_934.json"))));
//            using (IFormRecognizerClient client = new FormRecognizerClient("184654c847d54432b8301a4b76f63045", @"https://westus2.ppe.cognitiveservices.azure.com"))
//            {
//                using (FileStream stream = new FileStream(GetTestImagePath("layout_input.pdf"), FileMode.Open))
//                {
//                    var streamResult = client.AnalyzeReceiptAsync(stream, AnalysisContentType.Pdf).Result;
//                    var serializedResult = JsonConvert.SerializeObject(streamResult);
//                    Assert.Equal(expectedResult, serializedResult);
//                }
//            }
//        }

//        [Fact]
//        public void AnalyzeLayoutAsync_APIM_ByteArrayExpectedResult()
//        {
//            var expectedResult = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<AnalyzeResult>(File.ReadAllText(GetTestImagePath("Receipt_003_934.json"))));
//            using (IFormRecognizerClient client = new FormRecognizerClient("184654c847d54432b8301a4b76f63045", @"https://westus2.ppe.cognitiveservices.azure.com"))
//            {
//                using (FileStream stream = new FileStream(GetTestImagePath("layout_input.pdf"), FileMode.Open))
//                {
//                    using (var reader = new MemoryStream())
//                    {
//                        stream.CopyTo(reader);
//                        var streamResult = client.AnalyzeReceiptAsync(reader.ToArray(), AnalysisContentType.Pdf).Result;
//                        var serializedResult = JsonConvert.SerializeObject(streamResult);
//                        Assert.Equal(expectedResult, serializedResult);
//                    }
//                }
//            }
//        }
//    }
//}
