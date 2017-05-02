using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EmailService
{
    public class ReadEmailFromFile : IFile
    {
        public List<string> ReadFromFile(string fileInput)
        {
            string validValue = "input.txt";

            string file = fileInput;
            var list = new List<string>();
            try
            {
                while (!file.Equals(validValue))
                {
                    Console.WriteLine("Enter right input: ");
                    file = Console.ReadLine();
                }

                var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        list.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong file path");
            }
            return list;
        }

    }
}
