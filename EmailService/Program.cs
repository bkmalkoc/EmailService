using System;
using System.IO;
using System.Text;

namespace EmailService
{
    class Program
    {
        static void Main(string[] args)
        {

            //string contents = File.ReadAllText(@"C:\temp\test.txt");
            ReadAndDisplayFilesAsync();

            Console.ReadLine();
        }

        private async static void ReadAndDisplayFilesAsync()
        {

            String filename = "input.txt";
            byte[] byteArray = Encoding.UTF8.GetBytes(filename);
            MemoryStream stream = new MemoryStream(byteArray);

            try
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    string line;
                    line = sr.ReadToEnd();
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }
    }
}