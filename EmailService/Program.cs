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
            List<string> emailsInList = new List<string>();
            if (args.Length > 0)
            {
                emailsInList = ReadEmailFile(args[0]);
            }
            else
            {
                Console.WriteLine("Enter file name: ");
                string fileName = Console.ReadLine();
                emailsInList = ReadEmailFile(fileName);
            }

            //Console.WriteLine("Enter file name: ");
            //string fileName = Console.ReadLine();
            //emailsInList = ReadEmailFile(fileName);
            

            List<EmailSections> parsedEmails = ParseEmail(emailsInList);
            List<EmailSuccessResult> emailResult = new List<EmailSuccessResult>();
            emailResult = SendEmails(parsedEmails);
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

        private static List<EmailSuccessResult> SendEmails(List<EmailSections> emailList)
        {
            List<EmailSuccessResult> emailResults = new List<EmailSuccessResult>(); 
            foreach (var item in emailList)
            {
                emailResults = SendEmail(item, emailResults);
            }
            return emailResults;
        }
        
        private static List<EmailSuccessResult> SendEmail(EmailSections emailList, List<EmailSuccessResult> emailSuccessResultList)
        {
            //List<EmailSuccessResult> emailSuccessResultList = new List<EmailSuccessResult>();

            Providers providers = new Providers();
            bool sent = false;
            List<int> usedNumbers = new List<int>();
            int attempt = 0;

            while (!sent && attempt < 10)
            {
                int randomForProvider = GenerateNumberForProvider(usedNumbers);
                usedNumbers.Add(randomForProvider);

                IEmail emailProvider = providers.EmailProviders.ElementAt(randomForProvider);
                sent = emailProvider.Connect();
                if (!sent)
                {
                    attempt++;
                    continue;
                }
                else
                {
                    sent = emailProvider.Send(emailList);
                    if (sent)
                    {
                        emailSuccessResultList.Add(
                            new EmailSuccessResult
                            {
                                EmailSender = emailList.EmailSender,
                                Provider = emailProvider.ToString()
                            });
                    }
                    else
                    {
                        attempt++;
                    }
                }
            }
            return emailSuccessResultList;
        }

        private static int GenerateNumberForProvider(List<int> usedNumbers)
        {
            Random random = new Random();
            bool okay = false;
            int randomNumber = 0;
            while (!okay)
            {
                randomNumber = random.Next(0, 3);
                if (!usedNumbers.Contains(randomNumber))
                {
                    okay = true;
                }
            }
            return randomNumber;
        }

    }
}