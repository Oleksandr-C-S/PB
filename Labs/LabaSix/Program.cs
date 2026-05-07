using System;
using System.Threading.Tasks;//Бібліотека для асинхронних задач

namespace LabaSix
{
    class Program
    {
        static async Task Main(string[] args)//Головний асинхронний метод
        {
            string filePath = "data.txt";
            int linesCount = 1000000;
            Console.WriteLine("Creating big file");

            LargeFileGenerator.Generate(filePath, linesCount);//Генерація великого файлу
            Console.WriteLine("File successful created");

            int counter = 0;//лічильник рядків
            Console.WriteLine("Starting stream file processing");
            await foreach (string line in AsyncFileReader.ReadLinesAsync(filePath))//
            {
                counter ++;//збільшив лічильник
                if (counter % 100000 == 0)//Виведення кожного 1000000 рядка
                {
                    Console.WriteLine("Processed lines: {counter}");
                }
            }
            Console.WriteLine("Processing completed");
            Console.WriteLine("Total processed lines: {counter}");
        }
    }
}