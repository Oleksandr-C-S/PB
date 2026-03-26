using System;

class Program
{
    static void Main()
    {
        var memo = Memoization.Memoize<int, int>(x =>
        {
            Console.WriteLine("Calculating...");
            return x * x;
        });

        Console.WriteLine(memo(5));
        Console.WriteLine(memo(5));
    }
}
