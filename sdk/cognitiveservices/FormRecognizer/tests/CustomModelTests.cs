using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.FormRecognizer;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;
using Microsoft.Azure.Test.HttpRecorder;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using Xunit;

namespace FormRecognizerSDK.Tests
{
    public class CustomModelTests : BaseTests
    {
        [Fact]
        public async Task Test_GetAnalyzeFormResultAsync()
        {
            using (MockContext context = MockContext.Start(GetType(), nameof(Test_GetAnalyzeFormResultAsync)))
            {
                HttpMockServer.Initialize(GetType(), nameof(Test_GetAnalyzeFormResultAsync));
                using (var client = GetFormRecognizerClient(HttpMockServer.CreateInstance()))
                {
                    var modelId = new Guid("f39efed8-edaf-4e0f-af3d-1ca1119368b3");
                    var resultId = new Guid("62be37bf-cd35-4dee-af5b-72a39f966ff4");
                    var resp = await client.GetAnalyzeFormResultAsync(modelId, resultId);
                    Assert.Equal(OperationStatus.Succeeded, resp.Status);
                    Assert.NotEqual(default(DateTime), resp.CreatedDateTime);
                    Assert.NotEqual(default(DateTime), resp.LastUpdatedDateTime);
                    Assert.NotNull(resp.AnalyzeResult);
                }
            }
        }

        [Theory]
        [InlineData(true, ContentTypePdf, "Invoice_1.pdf", "Test_StartAnalyzeWithCustomModelStreamPdfReturnDetailsAsync", "5eec28f3-bb0d-4d59-a48c-84944a9806e3")]
        [InlineData(false, ContentTypePdf, "Invoice_1.pdf", "Test_StartAnalyzeWithCustomModelStreamPdfNoDetailsAsync", "5b2df06b-b7cf-4ac8-8cfa-774d9fdc2112")]
        public async Task Test_StartAnalyzeWithCustomModelStreamAsync(bool includeTextDetails, string contentTypeText, string fileName, string methodName, string expectOperationId)
        {
            using (MockContext context = MockContext.Start(GetType(), methodName))
            {
                HttpMockServer.Initialize(GetType(), methodName);
                using (var client = GetFormRecognizerClient(HttpMockServer.CreateInstance()))
                {
                    var stream = File.OpenRead(Path.Join("TestImages", fileName));
                    var modelId = new Guid("f39efed8-edaf-4e0f-af3d-1ca1119368b3");
                    var contentType = ParseContentType(contentTypeText);
                    var resp = await client.StartAnalyzeWithCustomModelAsync(modelId, stream, contentType, includeTextDetails);
                    Assert.Equal(new Guid(expectOperationId), resp);
                }
            }
        }

        [Theory]
        [InlineData(true, "Test_StartAnalyzeWithCustomModelUriReturnDetailsAsync", "62be37bf-cd35-4dee-af5b-72a39f966ff4")]
        [InlineData(false, "Test_StartAnalyzeWithCustomModelUriNoDetailsAsync", "08e0cc78-8167-4970-960c-288088ded8c1")]
        public async Task Test_StartAnalyzeWithCustomModelUriAsync(bool includeTextDetails, string methodName, string expectOperationId)
        {
            using (MockContext context = MockContext.Start(GetType(), methodName))
            {
                HttpMockServer.Initialize(GetType(), methodName);
                using (var client = GetFormRecognizerClient(HttpMockServer.CreateInstance()))
                {
                    var uri = new Uri("https://example.org/fake-document.pdf");
                    var modelId = new Guid("f39efed8-edaf-4e0f-af3d-1ca1119368b3");
                    var resp = await client.StartAnalyzeWithCustomModelAsync(modelId, uri, includeTextDetails);
                    Assert.Equal(new Guid(expectOperationId), resp);
                }
            }
        }

        [Theory]
        [InlineData(true, ContentTypePdf, "Invoice_1.pdf", "Test_AnalyzeWithCustomModelStreamPdfReturnDetailsAsync")]
        [InlineData(false, ContentTypePdf, "Invoice_1.pdf", "Test_AnalyzeWithCustomModelStreamPdfNoDetailsAsync")]
        public async Task Test_AnalyzeWithCustomModelStreamAsync(bool includeTextDetails, string contentTypeText, string fileName, string methodName)
        {
            using (MockContext context = MockContext.Start(GetType(), methodName))
            {
                HttpMockServer.Initialize(GetType(), methodName);
                using (var client = GetFormRecognizerClient(HttpMockServer.CreateInstance()))
                {
                    var stream = File.OpenRead(Path.Join("TestImages", fileName));
                    var modelId = new Guid("f39efed8-edaf-4e0f-af3d-1ca1119368b3");
                    var contentType = ParseContentType(contentTypeText);
                    var resp = await client.AnalyzeWithCustomModelAsync(modelId, stream, contentType, includeTextDetails);
                    Assert.Equal(OperationStatus.Succeeded, resp.Status);
                    Assert.NotEqual(default(DateTime), resp.CreatedDateTime);
                    Assert.NotEqual(default(DateTime), resp.LastUpdatedDateTime);
                    Assert.NotNull(resp.AnalyzeResult);
                }
            }
        }

        [Theory]
        [InlineData(true, "Test_AnalyzeWithCustomModelUriReturnDetailsAsync")]
        [InlineData(false, "Test_AnalyzeWithCustomModelUriNoDetailsAsync")]
        public async Task Test_AnalyzeWithCustomModelUriAsync(bool includeTextDetails, string methodName)
        {
            using (MockContext context = MockContext.Start(GetType(), methodName))
            {
                HttpMockServer.Initialize(GetType(), methodName);
                using (var client = GetFormRecognizerClient(HttpMockServer.CreateInstance()))
                {
                    var uri = new Uri("https://example.org/fake-document.pdf");
                    var modelId = new Guid("f39efed8-edaf-4e0f-af3d-1ca1119368b3");
                    var resp = await client.AnalyzeWithCustomModelAsync(modelId, uri, includeTextDetails);

                    Assert.Equal(OperationStatus.Succeeded, resp.Status);
                    Assert.NotEqual(default(DateTime), resp.CreatedDateTime);
                    Assert.NotEqual(default(DateTime), resp.LastUpdatedDateTime);
                    Assert.NotNull(resp.AnalyzeResult);
                }
            }
        }

        [Fact]
        public async Task Test_DeleteCustomModelAsync()
        {
            using (MockContext context = MockContext.Start(GetType(), nameof(Test_DeleteCustomModelAsync)))
            {
                HttpMockServer.Initialize(GetType(), nameof(Test_DeleteCustomModelAsync));
                using (var client = GetFormRecognizerClient(HttpMockServer.CreateInstance()))
                {
                    var modelId = new Guid("5384ea4f-3d8e-41f2-987b-ed45f4cd1771");
                    await client.DeleteCustomModelAsync(modelId);
                }
            }
        }

        [Theory]
        [InlineData(true, "Test_TrainModelReturnKeysAsync", "d4deea17-8a01-43d2-acf9-4a74ec7f143d")]
        [InlineData(false, "Test_TrainModelNoKeysAsync", "2cefbbb0-ae1e-411e-8ca3-f4011ebb1e89")]
        public async Task Test_TrainModelAsync(bool includeKeys, string methodName, string expectModelId)
        {
            using (MockContext context = MockContext.Start(GetType(), methodName))
            {
                HttpMockServer.Initialize(GetType(), methodName);
                using (var client = GetFormRecognizerClient(HttpMockServer.CreateInstance()))
                {
                    var request = new TrainRequest
                    {
                        Source = "https://example.org/fake-source",
                        SourceFilter = new TrainSourceFilter
                        {
                            Prefix = "fake-prefix",
                        },
                        UseLabelFile = false,
                    };
                    var resp = await client.TrainCustomModelAsync(request, includeKeys);
                    Assert.Equal(new Guid(expectModelId), resp.ModelInfo.ModelId);
                    Assert.Equal(ModelStatus.Ready, resp.ModelInfo.Status);
                }
            }
        }

        [Fact]
        public async Task Test_StartTrainModelAsync()
        {
            using (MockContext context = MockContext.Start(GetType(), nameof(Test_StartTrainModelAsync)))
            {
                HttpMockServer.Initialize(GetType(), nameof(Test_StartTrainModelAsync));
                using (var client = GetFormRecognizerClient(HttpMockServer.CreateInstance()))
                {
                    var request = new TrainRequest
                    {
                        Source = "https://example.org/fake-source",
                        SourceFilter = new TrainSourceFilter
                        {
                            Prefix = "fake-prefix",
                        },
                        UseLabelFile = false,
                    };
                    var resp = await client.StartTrainCustomModelAsync(request);
                    Assert.Equal(new Guid("5384ea4f-3d8e-41f2-987b-ed45f4cd1771"), resp);
                }
            }
        }

        [Fact]
        public async Task Test_GetCustomModelsSummaryAsync()
        {
            using (MockContext context = MockContext.Start(GetType(), nameof(Test_GetCustomModelsSummaryAsync)))
            {
                HttpMockServer.Initialize(GetType(), nameof(Test_GetCustomModelsSummaryAsync));
                using (var client = GetFormRecognizerClient(HttpMockServer.CreateInstance()))
                {
                    var resp = await client.GetCustomModelsSummaryAsync();
                    Assert.Equal(3, resp.Count);
                    Assert.Equal(250, resp.Limit);
                    Assert.NotEqual(default(DateTime), resp.LastUpdatedDateTime);
                }
            }
        }

        [Fact]
        public async Task Test_ListCustomModelsAsync()
        {
            using (MockContext context = MockContext.Start(GetType(), nameof(Test_ListCustomModelsAsync)))
            {
                HttpMockServer.Initialize(GetType(), nameof(Test_ListCustomModelsAsync));
                using (var client = GetFormRecognizerClient(HttpMockServer.CreateInstance()))
                {
                    var resp = await client.ListCustomModelsAsync();
                    Assert.Equal(3, resp.Count);
                    Assert.Equal(new Guid("0e2906e4-298a-4249-a37c-d60fb29fc6b5"), resp[0].ModelId);
                }
            }
        }

        [Theory]
        [InlineData(false, "Test_GetCustomModelNoKeysAsync")]
        [InlineData(true, "Test_GetCustomModelReturnKeysAsync")]
        public async Task Test_GetCustomModelAsync(bool includeKeys, string methodName)
        {
            using (MockContext context = MockContext.Start(GetType(), methodName))
            {
                HttpMockServer.Initialize(GetType(), methodName);
                using (var client = GetFormRecognizerClient(HttpMockServer.CreateInstance()))
                {
                    var modelId = new Guid("0e2906e4-298a-4249-a37c-d60fb29fc6b5");
                    var resp = await client.GetCustomModelAsync(modelId, includeKeys);
                    Assert.NotNull(resp.ModelInfo);
                    Assert.NotNull(resp.TrainResult);
                    Assert.Equal(modelId, resp.ModelInfo.ModelId);
                    Assert.Equal(ModelStatus.Ready, resp.ModelInfo.Status);
                    Assert.NotEqual(default(DateTime), resp.ModelInfo.CreatedDateTime);
                    Assert.NotEqual(default(DateTime), resp.ModelInfo.LastUpdatedDateTime);

                    // TODO test resp.Keys
                }
            }
        }
    }
}