using System.Collections.Generic;//Бібліотека для асинхронної роботи
using System.IO;//Бібліотека для роботи з файлами
using System.Threading.Tasks;//Бібліотека для Task

namespace LabaSix
{
    public static class AsyncFileReader//Клас асинхронного читання файлу
    {
        public static async IAsyncEnumerable<string> ReadLinesAsync(string path)//Асинхронний ітератор
        {
            using StreamReader reader = new StreamReader(path);//Для асинхронного читанню файлу
            string line;//Зберігання поточного рядка
            while((line = await reader.ReadLineAsync()) != null)
            {
                yield return line;
            }
        }
    }
}