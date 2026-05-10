using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LabaSix
{
    public static class AsyncFileReader
    {
        public static async IAsyncEnumerable<string> ReadLinesAsync(string path)
        {
            using StreamReader reader = new StreamReader(path);
            string line;
            while((line = await reader.ReadLineAsync()) != null)
            {
                yield return line;
            }
        }
    }
}