using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EmailService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter file name: ");
            String fileName = Console.ReadLine();
            ReadAndDisplayFilesAsync(fileName);

            Console.ReadLine();

        }

        private static void ReadAndDisplayFilesAsync(String fileInput)
        {
            String file = fileInput;

            string text;
            var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }
            Console.WriteLine(text);


            string[] lines;
            var list = new List<string>();
            var fileStream2 = new FileStream(file, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream2, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
            lines = list.ToArray();


            var fileStream3 = new FileStream(file, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream3, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }

        }
    }
}