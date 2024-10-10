using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace example_steps;

public static class RegexHelper
{
    public static Dictionary<string, string?>? GetSegParams(string url)
    {
        var retVal = new Dictionary<string, string?>();
        foreach(Match match in Regex.Matches(url, @"\{(\w+)\}"))
        {
            var segParam = match.Groups[1].Value;
            var segValue = DataStoreGuard.GetValue(segParam) ?? throw new Exception($"No value for {segParam} cached in Data Store.");
            retVal.Add(segParam, segValue.ToString());
        }

        return retVal.Count > 0 ? retVal: null;
    }

    public static string ReadTestData(string fileName)
    {
        var filePath = $"{Directory.GetCurrentDirectory()}/testData/{fileName}";
        var retVal = File.ReadAllText(filePath);
        return retVal;
    }

    public static object? GetPropertyValue(object? parentObj, string propertyName)
    {
        if (parentObj is not null)
        {
            var parentType = parentObj?.GetType();
            if (parentType?.Name == "JObject")
            {
                return ((JObject)parentObj!)[propertyName];
            }
            else
            {
                return parentObj?.GetType().GetProperty(propertyName)?.GetValue(parentObj);
            }
        }

        return null;
    }
}
