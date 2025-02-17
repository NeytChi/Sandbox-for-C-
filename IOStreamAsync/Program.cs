using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace IOStreamAsync
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string filePath = "randomText.txt";
            string randomText = GenerateRandomText(100);

            await WriteTextAsync(filePath, randomText);
            string readText = await ReadTextAsync(filePath);

            Console.WriteLine("Зміст файлу:");
            Console.WriteLine(readText);
        }

        static string GenerateRandomText(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return stringBuilder.ToString();
        }

        static async Task WriteTextAsync(string filePath, string text)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                await writer.WriteAsync(text);
            }
        }

        static async Task<string> ReadTextAsync(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
