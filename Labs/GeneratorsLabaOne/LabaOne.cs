using System;

namespace PB.Labs.GeneratorsLabaOne
{
    class LabaOne
    {
        public static void Run()
        {
            foreach (int numm in Generator.RandomNumberGenerator(1, 100))
            {
                Console.WriteLine(numm);

                if (numm > 10)
                    break;
            }

            foreach (int numm in Iterator.Fibonacci())
            {
                Console.WriteLine(numm);

                if (numm > 100)
                    break;
            }
        }
    }
}