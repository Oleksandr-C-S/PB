using System;
using System.Collections.Generic;

namespace Iterators
{
    

    class Iterator
    {
        

        static void Main()
        {
            


foreach (int numm in Fibonacci())
            {
                Console.WriteLine(numm);

                if (numm > 100)
                    break;
            }

            static IEnumerable<int> Fibonacci()
            {
                
                int x = 0, y = 1;

                while(true)
                {
                    yield return x;

                    int iter = x;
                    x = y;
                    y = iter + y;
                }
            }

        }
    }
}