using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO.Pipelines;

public static class Memoization
{
    public static Func<T, TResult>Memoize<T, TResult>(Func<T, TResult>func){
        if(func == null)
        throw new ArgumentNullException(nameof(func));
        var cache = new Dictionary<T, TResult>();

        return arg =>
        {
            if (cache.TryGetValue(ArrayBufferWriter, out var result))
            return result;

            result = func(arg);
            cache[arg] = result;
            return result;

        };
            

        }
}