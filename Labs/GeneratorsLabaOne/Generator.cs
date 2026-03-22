using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;

namespace PB.Labs.GeneratorsLabaOne
{
public static class Generator
    {
 public static IEnumerable<int> RandomNumberGenerator(int min, int max)
        {      
Random random = new Random();

            while (true)
            {
                yield return random.Next(min, max);
            }

        }

    }

}