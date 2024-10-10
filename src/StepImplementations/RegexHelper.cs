/// <summary>
/// Initially built as a RegexHelper, this is now a general purpose Helper class.
/// </summary>
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace StepImplementations;

public static class RegexHelper
{
    /// <summary>
    /// Parses through ane endpoint and locates embedded variables, typically denoted between {} brackets. 
    /// Checks to see if a value for that specific variable is within the DataStore (scenario).
    /// Returns a dictionary containing varaiables as key and their values, which will be used by JPHClient.SendRequest
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static Dictionary<string, string> GetSegParams(string url)
    {
        var retVal = new Dictionary<string, string>();
        foreach(Match match in Regex.Matches(url, @"\{(\w+)\}"))
        {
            var segParam = match.Groups[1].Value;
            var segValue = DataStoreGuard.GetValue(segParam) ?? throw new Exception($"No value for {segParam} cached in Data Store.");
            retVal.Add(segParam, segValue.ToString());
        }

        return retVal.Count > 0 ? retVal: null;
    }

    /// <summary>
    /// Reads a resource file located under testData/ folder and returns content as a string.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string ReadTestData(string fileName)
    {
        var filePath = $"{Directory.GetCurrentDirectory()}/testData/{fileName}";
        var retVal = File.ReadAllText(filePath);
        return retVal;
    }

    /// <summary>
    /// Extracts property from an object, accounting for different objects such as JObject.
    /// </summary>
    /// <param name="parentObj"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public static object GetPropertyValue(object parentObj, string propertyName)
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
