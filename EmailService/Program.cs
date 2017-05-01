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
                string fileName = AskForFilePath();
                emailsInList = ReadEmailFile(fileName);
            }

            List<EmailSections> parsedEmails = ParseEmail(emailsInList);
            List<EmailSuccessResult> emailResult = new List<EmailSuccessResult>();
            emailResult = SendEmails(parsedEmails);
            Console.WriteLine("Output: ");
            foreach (var item in emailResult)
            {
                Console.Write(item.EmailSender + " ");
                Console.Write(item.Attempt + " ");
                item.Providers.ForEach(x => Console.Write(x + ", "));
                Console.WriteLine();
            }
            //if (emailResult.Select(x => x.Providers).Count() > 0)
            //{
                
            //}
            Console.ReadLine();
        }

        public static string AskForFilePath()
        {
            Console.WriteLine("Enter file name: ");
            string fileName = Console.ReadLine();
            return fileName;
        }

        public static List<string> ReadEmailFile(string fileInput)
        {
            string file = fileInput;
            var list = new List<string>();
            //handle for a input file name
            try
            {
                var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        list.Add(line);
                    }
                }
                list.ForEach(x => Console.WriteLine(x + ", "));
            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong file path");
            }
            return list;
        }

        public static List<EmailSections> ParseEmail(List<string> emailsList)
        {
            List<EmailSections> emailDetailList = new List<EmailSections>();

            foreach (var item in emailsList)
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

        public static List<EmailSuccessResult> SendEmails(List<EmailSections> emailList)
        {
            List<EmailSuccessResult> emailResults = new List<EmailSuccessResult>();
            foreach (var item in emailList)
            {
                emailResults = SendEmail(item, emailResults);
            }
            return emailResults;
        }

        public static List<EmailSuccessResult> SendEmail(EmailSections emailList, List<EmailSuccessResult> emailSuccessResultList)
        {
            Providers providers = new Providers();
            bool sent = false;
            List<int> usedNumbers = new List<int>();
            int attempt = 0;
            int sentAttempt = 0;
            List<string> providersList = new List<string>();

            while (!sent)
            {
                if (usedNumbers.Count() == 3) { break; }
                int randomForProvider = GenerateNumberForProvider(usedNumbers);
                usedNumbers.Add(randomForProvider);
                IEmail emailProvider = providers.EmailProviders.ElementAt(randomForProvider);
                sent = emailProvider.Connect();
                EmailSuccessResult emailSuccessResult = new EmailSuccessResult();
                if (!sent)
                {
                    if (emailSuccessResultList.Where(x => x.EmailSender.Equals(emailList.EmailSender)).Count() > 0) 
                    {
                        providersList.Add(emailProvider.ToString());
                    }
                    else
                    {
                        providersList.Add(emailProvider.ToString());
                        emailSuccessResult.Providers = providersList;
                        emailSuccessResult.EmailSender = emailList.EmailSender;
                        emailSuccessResultList.Add(emailSuccessResult);
                    }
                    attempt++;
                    continue;
                }
                else
                {
                    if(emailSuccessResultList.Where(x => x.EmailSender.Equals(emailList.EmailSender)).Count() > 0)
                    {
                        providersList.Add(emailProvider.ToString());
                    }
                    else
                    {
                        providersList.Add(emailProvider.ToString());
                        emailSuccessResult.Providers = providersList;
                        emailSuccessResult.EmailSender = emailList.EmailSender;
                        emailSuccessResultList.Add(emailSuccessResult);
                    }

                    sent = emailProvider.Send(emailList);

                    if (sent)
                    {
                        sentAttempt++;
                    }
                }
            }
            return emailSuccessResultList;
        }

        public static int GenerateNumberForProvider(List<int> usedNumbers)
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