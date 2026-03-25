using System;
using System.Collections.Generic;
public class LFUMemoizer<T, TResult>
{
    private readonly Func<T, TResult> _func;
    private readonly int _capacity;
    private class Entry
    {
        public TResult Value;
        public int Frequency;
    }
    private readonly Dictionary<T, Entry> _cache = new();
    public LFUMemoizer(Func<T, TResult> func, int capacity)
    {
        _func = func;
        _capacity = capacity;
    }
    public TResult Invoke(T arg)
    {
        if (_cache.TryGetValue(arg, out var entry))
        {
            entry.Frequency++;
            return entry.Value;
        }
        var result = _func(arg);
        if (_cache.Count >= _capacity)
        {
            var lfuKey = default(T);
            int minFreq = int.MaxValue;
            foreach (var kvp in _cache)
            {
                if (kvp.Value.Frequency < minFreq)
                {
                minFreq = kvp.Value.Frequency;
                lfuKey = kvp.Key;
                }
            }
            _cache.Remove(lfuKey);
        }
        _cache[arg] = new Entry { Value = result, Frequency = 1 };
        return result;
    }
}
