using System;
using System.Collections.Generic;

public class TTL<T, TResult>
{
    private readonly Func<T, TResult> _func;
    private readonly TimeSpan _ttl;

    private class Entry
    {
        public TResult Value;
        public DateTime CreatedAt;
    }

    private readonly Dictionary<T, Entry> _cache = new();

    public TTL(Func<T, TResult> func, TimeSpan ttl)
    {
        _func = func;
        _ttl = ttl;
    }

    public TResult Invoke(T arg)
    {
        if (_cache.TryGetValue(arg, out var entry))
        {
            if (DateTime.UtcNow - entry.CreatedAt < _ttl)
                return entry.Value;

            _cache.Remove(arg);
        }

        var result = _func(arg);
        _cache[arg] = new Entry
        {
            Value = result,
            CreatedAt = DateTime.UtcNow
        };

        return result;
    }
}
