using PB.Labs.GeneratorsLabaOne;
using System;

namespace PB
{
    class MainClass
    {
                static void Main()
        {
            while (true)
            {
                Console.WriteLine("Виберіть лабораторну:");
                Console.WriteLine("1 - Лабораторна 1");

                var choose = Console.ReadLine();

                switch (choose)
                {
                    case "1":
                    LabaOne.Run();
                    break;
                    
                    return;
                }
                Console.WriteLine();

            }

        }
    }

}