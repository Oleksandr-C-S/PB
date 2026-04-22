using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
