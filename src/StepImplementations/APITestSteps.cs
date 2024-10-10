/// <summary>
/// This implements the Gauge Steps that pertain to API Testing. 
/// Examples of the Steps can be found in specs/Part1.spec.
/// </summary>
using FluentAssertions;
using Gauge.CSharp.Lib;
using Gauge.CSharp.Lib.Attribute;
using JsonPlaceHolder;
using Newtonsoft.Json.Linq;

namespace StepImplementations;

public class APITestSteps
{
    /// <summary>
    /// private instance of the rest client used to execute API calls.
    /// </summary>
    private JPHClient restClient;

    [Step("Initialize connection to <baseUrl>")]
    public void InitializeClient(string baseUrl)
    {
        restClient = new JPHClient(baseUrl);
    }

    [Step("Send request <requestName> to <method_type> <endpoint>")]
    public void SendBasicRequest(string requestName, string method_type, string endpoint)
    {
        SendFullRequest(requestName:requestName, method_type:method_type, endpoint:endpoint);
    }

    [Step("Send request <requestName> to <method_type> <endpoint> with <bodyVariable> as body")]
    public void SendRequestWithBody(string requestName, string method_type, string endpoint, string bodyVariable)
    {
        SendFullRequest(requestName:requestName, method_type:method_type, endpoint:endpoint, bodyVariable:bodyVariable);
    }

    [Step("Send request <requestName> to <method_type> <endpoint> with <paramVariable> as parameters")]
    public void SendRequestWithParams(string requestName, string method_type, string endpoint, string paramVariable)
    {
        SendFullRequest(requestName:requestName, method_type:method_type, endpoint:endpoint, paramVariable:paramVariable);
    }

    [Step("Send request <requestName> to <method_type> <endpoint> with <bodyVariable> as body and <paramVariable> as parameters")]
    public void SendFullRequest(string requestName, string method_type, string endpoint, string bodyVariable=null, string paramVariable=null)
    {
        var responseObj=restClient?.SendRequest(request_method:method_type, 
                                                endpoint:endpoint, 
                                                body:bodyVariable is not null ? (JObject)DataStoreGuard.GetValue(bodyVariable):null, 
                                                parameters:paramVariable is not null? DataStoreGuard.GetValue(paramVariable):null, 
                                                segParams:RegexHelper.GetSegParams(endpoint) ?? null);
        responseObj.Should().NotBeNull("Did not get response object from client.");
        DataStoreGuard.CacheValue(requestName, responseObj);
    }

    [Step("Verify status code of <requestName> is <statusCode>")]
    public void VerifyStatusCode(string requestName, int statusCode)
    {
        var responseObj = (ResponseObj)DataStoreGuard.GetValue(requestName);
        responseObj?.StatusCode.Should().Be(statusCode);
    }

    [Step("Print response content of <requestName>")]
    public void PrintResponseContent(string requestName)
    {
        var responseObj = (ResponseObj)DataStoreGuard.GetValue(requestName);
        GaugeMessages.WriteMessage(responseObj?.Content ?? "Content is Empty");
    }

    [Step("Set response content of <requestName> as <castType> to <variableName>")]
    public void CastAndStoreResponse(string requestName, string castType, string variableName)
    {
        var responseObj = (ResponseObj)DataStoreGuard.GetValue(requestName);
        var responseContent = responseObj?.Content;
        var modelInstance = (castType=="String")?restClient?.ConvertToString(responseContent): restClient?.ConvertToObject(responseContent);
        modelInstance.Should().NotBeNull();
        DataStoreGuard.CacheValue(variableName, modelInstance);
    }

    [Step("Set <jsonString> as <castType> to <variableName>")]
    public void SaveObjectToVariable(string jsonString, string castType, string variableName)
    {
        var modelInstance = (castType=="String")? jsonString: restClient?.ConvertToObject(jsonString);
        modelInstance.Should().NotBeNull();
        DataStoreGuard.CacheValue(variableName, modelInstance);
    }

    [Step("Set <propertyName> in <cachedVariable> as <castType> to <variableName>")]
    public void SetPropertyToVariable(string propertyName, string cachedVariable, string castType, string variableName)
    {
        var cachedObj = cachedVariable.Contains(".json")? 
                        restClient?.ConvertToObject(RegexHelper.ReadTestData(cachedVariable)):
                        DataStoreGuard.GetValue(cachedVariable);
        var propertyValueRaw = RegexHelper.GetPropertyValue(cachedObj, propertyName);
        propertyValueRaw.Should().NotBeNull($"Unable to extract value of {propertyName} from object saved in {cachedVariable}");
        var propertyValue = (castType=="String")? restClient?.ConvertToString(propertyValueRaw): restClient?.ConvertToObject(propertyValueRaw);
        DataStoreGuard.CacheValue(variableName, propertyValue);
    }
}
