using Gauge.CSharp.Lib;
using FluentAssertions;

namespace example_steps;

public static class DataStoreGuard
{
    public static object? GetValue(string key)
    {
        var storeValue = ScenarioDataStore.Get(key);
        storeValue.Should().NotBeNull($"Did not retrieve any value for {key}");
        return storeValue;
    }

    public static void CacheValue(string key, object? value)
    {
        ScenarioDataStore.Add(key, value);
    }
}
