using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

public class LRU<T, TResult>
{
    private readonly Func<T, TResult> _func;
    private readonly int _capacity;
    private readonly Dictionary<T, LinkedListNode<(T key, TResult value)>> _cache = new();

    private readonly LinkedList<(T key, TResult value)> _list = new();

    public LRU(Func<T, TResult> func, int capacity)
    {
        
        _func = func;
        _capacity = capacity;
    }

    public TResult Invoke(T arg)
    {
        if (_cache.TryGetValue(arg, out var node))
        {
            _list.Remove(node);
            _list.AddFirst(node);
            return node.Value.value;

        }
        var result = _func(arg);
        if (_cache.Count >= _capacity)
        {
            var last = _list.Last;
            _cache.Remove(last.Value.key);
            _list.RemoveLast();
        }


            var newNode = new LinkedListNode<(T, TResult)>((arg, result));
            _list.AddFirst(newNode);
            _cache[arg] = newNode;
            return result;
    }
}