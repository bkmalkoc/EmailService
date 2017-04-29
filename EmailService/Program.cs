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

            List<String> emailsInList = new List<string>();
            emailsInList = ReadEmailFile(fileName);

            ParseEmail(emailsInList);
            

            Console.ReadLine();

        }

        private static List<String> ReadEmailFile(String fileInput)
        {
            String file = fileInput;

            //string text;
            //var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            //using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            //{
            //    text = streamReader.ReadToEnd();
            //}
            //Console.WriteLine(text);

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
            list.ForEach(x => Console.WriteLine(x + ", "));
            return list;
        }

        private static void ParseEmail(List<String> emailsList)
        {
            string[] seperatingSenderAndReciever = { " " };
            EmailSections emailSection = new EmailSections();
            string[] words = new string[2];

            foreach (var item in emailsList)
            {
                words = item.Split(seperatingSenderAndReciever, System.StringSplitOptions.RemoveEmptyEntries);
            }

            emailSection.EmailSender = words[0];
            emailSection.EmailReceiver = words[1];            
        }

        private static void SendEmails(List<EmailSections> email)
        {
            Providers providers = new Providers();
            

        }

        private static void ResultsOfEmailSendProcess()
        {

        }
    }
}