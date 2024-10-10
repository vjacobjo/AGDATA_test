using Gauge.CSharp.Lib;
using FluentAssertions;

namespace StepImplementations;

public static class DataStoreGuard
{
    public static object GetValue(string key, bool specLevel=false)
    {
        var storeValue = specLevel? SpecDataStore.Get(key):ScenarioDataStore.Get(key);
        storeValue.Should().NotBeNull($"Did not retrieve any value for {key}");
        return storeValue;
    }

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
