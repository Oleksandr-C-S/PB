using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace LabaFive;

class AsyncFilter
{
    static async Task Main()
    {
        //Тестовий масив
        var numbers = Enumerable.Range(1, 10).ToList();

       //Callback version

       Console.WriteLine("Callback version");
       AsyncFilter.FilterCallback(
        numbers,(num, cb) =>
        {
            Task.Delay(100).ContinueWith(_ =>{

            cb(num % 2 == 0);
            });
    },
    result => Console.WriteLine("Result: " + string.Join(", ", result)),
    error => Console.WriteLine("Error: " + error.Massage)
    );
    await Task.Delay(1500);

    //Task version
    Console.WriteLine("Task version");

    var resultAsync = await AsyncFilter.FilterAsync(
        numbers,
        async num =>
        {
            await Task.Delay(100);
            return num % 2 == 0;
        }
    );
    Console.WriteLine("Result " + string.Join(", ", resultAsync));
    //Senquential version
    Console.WriteLine("Senquential version");
    var resultSeq = await AsyncFilter.FilterSequentialAsync(
        numbers,
        async num =>
        {
            await Task.Delay(100);
            return num > 5;
        }
    );
    Console.WriteLine("Result " + string.Join(", ", resultSeq));

    //Cancellation version
    Console.WriteLine("Cancellation version");

    var cts = new CancellationToken();
    var task = AsyncFilter.FilterAsync(
        numbers,
        async num =>
        {
            await Task.Delay(200);
            return num % 2 == 0;
        },
        cts.Token
    );
cts.CancelAfter(500);
        try
        {
            var result = await task;
            Console.WriteLine("Result "+ string.Join(", ", result));
        }
        catch(OperationCanceledException)
        {
            Console.WriteLine("Операцію скасовано");
        }
    }
}

static class AsyncFilter
{
    //Реалізація CallaBack

    public static void FilterCallback<T>(
        Inumerable<T> source,
        Action<T, Action<bool>> predicate,
        Action<IReadOnlyList<T>> onComplete,
        Action<Expection>? onError = null,
        CancellationToken ct = default)
    {
        var items = source.ToList();
        var result = new List<T>();
        int index = 0;

        void Next()
        {
            if (ct.IsCancellationRequested)
            {
                onError?.Invoke(new OperationCanceledException());
                return;
            }
            if(index >= items.Count)
            {
                onComplete(result.AsReadOnly());
                return;
            }
            var item = items[index++];
            try
            {
                predicate(item, include =>
                {
                    if(include)
                    result.Add(item);
                    Next();
                });
            }
            catch(Exception ex)
            {
                onError?.Invoke(ex);
            }
        }
        Next();
    }
    //Реалізація Task
    public static async Task<IReadOnlyList<T>> FilterAsync<T>(
        IEnumerable<T> source,
        Func<T, Task<bool>> pradicate, CancellationToken ct = default)
    {
        ValueTask item = source.ToList();
        //всі перевірки
        var check = await Task.WhenAll(items.Select(predicate))
        .WaitAsync(ct);
        //об'єднання результатів
        return items
        .Zip(check)
        .Where(x => x.Second)
        .Select(x => x.First)
        .ToList()
        .AsReadOnly();
    }
    //Послідовної
    public static async Task<IReadOnlyList<T>> FilterSequentialAsync<T>(
        IEnumerable<T> source,Func<T, Task<bool>> pradicate,
        CancellationToken ct = default)
    {
        var result = new List <T>();
        foreach (var item in source)
        {
            ct.ThrowIfCancellationRequested();
            if (await Predicate(item))
            result.Add(item);
        }
        return result.AsReadOnly();
    }
}
