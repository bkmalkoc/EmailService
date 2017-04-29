using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EmailService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter file name: ");
            string fileName = Console.ReadLine();

            List<string> emailsInList = new List<string>();
            emailsInList = ReadEmailFile(fileName);

            ParseEmail(emailsInList);
            Console.ReadLine();

        }

        private static List<string> ReadEmailFile(string fileInput)
        {
            String file = fileInput;
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

        private static List<EmailSections> ParseEmail(List<string> emailsList)
        {
            char[] seperatingSenderAndReciever = { ' '};

            List<EmailSections> emailDetailList = new List<EmailSections>();

            foreach (var item in emailsList)
            {
                string[] words = item.Split(seperatingSenderAndReciever);
                EmailSections emailSections = new EmailSections()
                {
                    EmailSender = words[0],
                    EmailReceiver = words[1]
                };
                emailDetailList.Add(emailSections);
            }
            return emailDetailList;
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