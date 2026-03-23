using System;
using System.Collections.Generic;

public class GenericMemoizer<T, TResult>{

    private readonly Func<T, TResult> _func;
    private readonly int _capacity;
    private readonly Func<Dictionary<T, TResult>, T> _evictStrategy;

    private readonly Dictionary<T, TResult> _cache = new();

    public GenericMemoizer(

        Func<T, TResult> func,
        int capacity,
        Func<Dictionary<T, TResult>, T> evictStrategy)
    {
        _func = func;
        _capacity = capacity;
        _evictStrategy = evictStrategy;
        
    }

public TResult Invoke(T arg)
    {
        if(_cache.TryGetValue(arg, out var result))
        return result;
        result = _func(arg);
    if (_cache.Count >= _capacity)
        {
            var keyToRemove =_evictStrategy(_cache);
            _cache.Remove(keyToRemove);
        }
        _cache[arg] = result;
        return result;
    }

    
}