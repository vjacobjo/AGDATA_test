/// <summary>
/// The Gauge Framework provides memory space for steps to store values to pass between different step instances. 
/// This class wraps around the Data Store, checking to see if the value is null when retrieving a value, or 
/// deciding at what level to store a value in, at the scenario level or the spec level.
/// Note:
/// * Values stored at the Scenario level persist within the scenario execution and get deleted when it ends.
/// * Values stored at the spec level persist with the spec execution and get deleted when execution is ended.
/// </summary>
using Gauge.CSharp.Lib;
using FluentAssertions;

namespace StepImplementations;

public static class DataStoreGuard
{
    /// <summary>
    /// Gets the value from the data store, with the key provided.
    /// You can also specify if the value is to be retrieved from the ScenarioDataStore or the SpecDataStore.
    /// Also checks to see if the value is None, for which it will throw an exception.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="specLevel"></param>
    /// <returns></returns>
    public static object GetValue(string key, bool specLevel=false)
    {
        var storeValue = specLevel? SpecDataStore.Get(key):ScenarioDataStore.Get(key);
        storeValue.Should().NotBeNull($"Did not retrieve any value for {key}");
        return storeValue;
    }

    /// <summary>
    /// Saves the value in either the Spec or Scenario DataStore, assigning a key in the process.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="specLevel"></param>
    public static void CacheValue(string key, object value, bool specLevel=false)
    {
        if (specLevel)
        {
            SpecDataStore.Add(key, value);
        }
        else
        {  
            ScenarioDataStore.Add(key, value);
        }
    }
}
