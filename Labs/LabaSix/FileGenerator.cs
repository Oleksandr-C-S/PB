using System.IO;//Бібліотека для роботи з файлом

namespace LabaSix
{
    public static class FileGenerator
    {
        public static void Generate(string path, int linesCount)//Метод створення файлу
        {
            using StreamWriter writer = new StreamWriter(path);
            for (int i = 1; i <= linesCount; i++)//Цикл генерації великої кількості рядків
            {
                writer.WriteLine("Line nummber: {i}");
            }
        }
    }
}