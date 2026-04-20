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

    }
}
