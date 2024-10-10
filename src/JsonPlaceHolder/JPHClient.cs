/// <summary>
/// This defines the main class which wraps around the RESTSharp library and NewtonSoft.Json libraries, decoupling it from Step Implementations.
/// </summary>
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonPlaceHolder;

/// <summary>
/// Packages the StatusCode and Content from the RestReponse for use outside this library.
/// </summary>
/// <param name="StatusCode"></param>
/// <param name="Content"></param>
public record ResponseObj(int StatusCode, string Content);

/// <summary>
/// Wrapper around RestClient from RestSharp.
/// </summary>
public class JPHClient: IDisposable
{
    /// <summary>
    /// Maintains one instance of the RestClient.
    /// </summary>
    private readonly RestClient client;

    /// <summary>
    /// Main constructor of class.
    /// </summary>
    /// <param name="baseurl"></param>
    public JPHClient(string baseurl)
    {
        var options = new RestClientOptions(baseurl);
        client = new RestClient(options);
    }

    public void Dispose()
    {
        client?.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Wrapper around sychronous call "RestRequest.Execute". 
    /// Returns the response status code and content in a RequestObj record.
    /// </summary>
    /// <param name="request_method"></param>
    /// <param name="endpoint"></param>
    /// <param name="body"></param>
    /// <param name="parameters"></param>
    /// <param name="segParams"></param>
    /// <returns></returns>
    public ResponseObj SendRequest(string request_method, string endpoint, object body = null, object parameters = null, Dictionary<string, string> segParams = null)
    {
        // Dynamically gets method type from string argument.
        Method method_type = (Method)Enum.Parse(typeof(Method), request_method, true);
        var request = new RestRequest(endpoint, method_type);

        // If body is provided, adds body as ContentType.Json
        if (body is not null)
        {
            var jsonBody = ConvertToString(body);
            request.AddStringBody(jsonBody!, ContentType.Json);
        }

        // Adds query parameters if provided.
        if (parameters is not null)
        {
            // JObjects are more complex than the typical class used for AddObject, so we iteratively AddParameters.
            if (parameters.GetType().Name == "JObject")
            {
                foreach (var j in (JObject)parameters)
                {
                    request.AddParameter(j.Key, j.Value?.ToString());
                }
            }
            else
            {
                request.AddObject(parameters);
            }
        }

        // For any embedded variables in the endpoint, the segParams dictionary contains values to substitute for them.
        if (segParams is not null)
        {
            foreach((string key, string value) in segParams)
            {
                request.AddUrlSegment(key, value!);
            }
        }

        // Executes request and packages response status code and content into ResponseObj and returns it
        var responseObj =  client.Execute(request);
        return (responseObj is null) ? null : new ResponseObj((int)responseObj.StatusCode, responseObj.Content);
    }
    /// <summary>
    /// Converts any object into a string or returns the value if input is already a string. 
    /// For future, consider using JsonCoverter.Serialize.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public string ConvertToString(object input)
    {
        return (input.GetType().Name == "String") ? (string)input: input.ToString();
    }

    /// <summary>
    /// Converts the string to a JObject, or returns the input if already an object. 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public object ConvertToObject(object input)
    {
        var input_type = input?.GetType().Name;
        return (input_type == "String") ? JsonConvert.DeserializeObject((string)input!): input;
    }
}
