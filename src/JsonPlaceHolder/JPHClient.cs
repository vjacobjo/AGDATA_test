using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonPlaceHolder;

public record ResponseObj(int StatusCode, string Content);

public class JPHClient: IDisposable
{
    private readonly RestClient client;

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

    public ResponseObj SendRequest(string request_method, string endpoint, object body = null, object parameters = null, Dictionary<string, string> segParams = null)
    {
        Method method_type = (Method)Enum.Parse(typeof(Method), request_method, true);
        var request = new RestRequest(endpoint, method_type);
        if (body is not null)
        {
            var jsonBody = ConvertToString(body);
            request.AddStringBody(jsonBody!, ContentType.Json);
        }
        if (parameters is not null)
        {
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
        if (segParams is not null)
        {
            foreach((string key, string value) in segParams)
            {
                request.AddUrlSegment(key, value!);
            }
        }
        var responseObj =  client.Execute(request);
        return (responseObj is null) ? null : new ResponseObj((int)responseObj.StatusCode, responseObj.Content);
    }

    public string ConvertToString(object input)
    {
        return (input?.GetType().Name == "String") ? (string)input: input?.ToString();
    }

    public object ConvertToObject(object input)
    {
        var input_type = input?.GetType().Name;
        return (input_type == "String") ? JsonConvert.DeserializeObject((string)input!): input;
    }
}
