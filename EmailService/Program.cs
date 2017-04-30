using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EmailService
{
    public class Program
    {
        public static void Main(string[] args)
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
            List<EmailSections> emailDetailList = new List<EmailSections>();
            
            //foreach (var item in emailsList)
            //{
            //    var parts = Regex.Matches(item, @"[\""].+?[\""]|[^ ]+")
            //        .Cast<Match>()
            //        .Select(m => m.Value)
            //        .ToList();

            //    string subject = parts[2].Replace("\"", "");
            //    string body = parts[3].Replace("\"", "");

            //    EmailSections emailSections = new EmailSections()
            //    {
            //        EmailSender = parts[0],
            //        EmailReceiver = parts[1],
            //        EmailSubject = subject,
            //        EmailBody = body
            //    };
            //    emailDetailList.Add(emailSections);
            //}

            foreach(var item in emailsList)
            {
                string senderEmail = item.Substring(0, item.IndexOf(" ", StringComparison.Ordinal));
                string receiverEmail = item.Substring(senderEmail.Length + 1, item.IndexOf(" ", senderEmail.Length + 1) - senderEmail.Length);

                int firstQuote = item.IndexOf("\"", StringComparison.Ordinal);
                string subjectandBody = item.Substring(firstQuote, item.Length - item.IndexOf("\"", firstQuote, StringComparison.Ordinal));

                String[] words = subjectandBody.Split(new string[] { "\" \"" }, StringSplitOptions.None);
                string subjectEmail = words[0].Remove(0, 1);
                string bodyEmail = words[1].Remove(words[1].Length - 1);

                EmailSections emailSections = new EmailSections()
                {
                    EmailSender = senderEmail,
                    EmailReceiver = receiverEmail,
                    EmailSubject = subjectEmail,
                    EmailBody = bodyEmail
                };
                emailDetailList.Add(emailSections);
            }

            return emailDetailList;
        }

        private static void SendEmails(List<EmailSections> emailList)
        {
            Providers providers = new Providers();

            foreach (var item in emailList)
            {
            }

        }

        private static void ResultsOfEmailSendProcess()
        {

        }
    }
}