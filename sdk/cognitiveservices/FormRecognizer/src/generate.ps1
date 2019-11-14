Start-AutoRestCodeGeneration -ResourceProvider "cognitiveservices/data-plane/FormRecognizer" -AutoRestVersion "latest" 

$generatedFileStream = '(?m)(.*)if\(fileStream != null\)(.*)[\r\n](.*){((.*)[\r\n](.*)){4}'
$customizedFileStream = @"
if(fileStream != null)
{
    if (fileStream is Uri)
    {
        var request = new AnalyzeUrlRequest { Source = (fileStream as Uri) };
        var json = JsonConvert.SerializeObject(request, SerializationSettings);
        _httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");
        _requestContent = json;                    
    }
    else if (fileStream is Stream)
    {
        _httpRequest.Content = new StreamContent(fileStream as Stream);
        _requestContent = fileStream.GetType().Name;
        if (contentType != null)
        {
            _httpRequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
        }
    }
    else
    {
        throw new ArgumentException("Unsupported content type.");
    }
}
"@
$contentType = "object fileStream = default(object),"
$appendUsing = @"
using System.Threading.Tasks;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
"@

ls .\Generated\FormRecognizerClient.cs | foreach {
    $fileContent = Get-Content $_.FullName -Raw
    $newContent = $fileContent -replace $generatedFileStream, $customizedFileStream    
    $newContent = $newContent -replace 'public', 'private'
    $newContent = $newContent -replace 'object fileStream = default\(object\),', 'object fileStream = default(object), string contentType = null,'
    $newContent = $newContent -replace 'using System.Threading.Tasks;', $appendUsing
    $newContent = $newContent -replace 'private FormRecognizerClient', 'public FormRecognizerClient'
    $newContent = $newContent -replace 'private string Endpoint', 'public string Endpoint'
    $newContent = $newContent -replace 'private JsonSerializerSettings', 'public JsonSerializerSettings'
    $newContent = $newContent -replace 'private ServiceClientCredentials', 'public ServiceClientCredentials'
    $newContent = $newContent -replace 'private partial class FormRecognizerClient', 'public partial class FormRecognizerClient'
    [System.IO.File]::WriteAllText(".\Generated\FormRecognizerClient.cs", $newContent)
}

Remove-Item Generated\FormRecognizerClientExtensions.cs
Remove-Item Generated\IFormRecognizerClient.cs