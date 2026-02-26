using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;

namespace Generator
{
    

class Generator
    {
        

static void Main()
        {
            


            foreach(int numm in RandomNumberGenerator(1, 100))
            {
                Console.WriteLine(numm);

                if (numm > 10)
break;
            }
        }



static IEnumerable<int> RandomNumberGenerator(int min, int max)
        {
            
Random random = new Random();

            while (true)
            {
                
                yield return random.Next(min, max);
            }

        }

    }

}