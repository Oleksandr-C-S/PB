using System;
using System.Collections.Generic;

namespace PB.Labs.GeneratorsLabaOne
{
        public static class Iterator
    {
           public static IEnumerable<int> Fibonacci()
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