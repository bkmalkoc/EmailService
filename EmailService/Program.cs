using Autofac;
using EmailService.Model;
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
        private static IContainer _container;

        private static IFile _readFromFileRetriever;
        private static IParse _parseEmail;
        private static IEmail _email;
        private static IGenerator _generator;

        private static void Initialize()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ReadEmailFromFile>();
            builder.RegisterType<ParseEmail>();
            builder.RegisterType<SendEmail>();
            builder.RegisterType<GenerateNumber>();

            _container = builder.Build();
        }

        private static void ResolveDependencies()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                _readFromFileRetriever = scope.Resolve<ReadEmailFromFile>();
                _parseEmail = scope.Resolve<ParseEmail>();
                _email = scope.Resolve<SendEmail>();
                _generator = scope.Resolve<GenerateNumber>();
            }
        }

        public static void Main(string[] args)
        {
            Initialize();
            ResolveDependencies();

            List<string> emailsInList = new List<string>();

            if (args.Length > 0)
            {
                emailsInList = _readFromFileRetriever.ReadFromFile(args[0]);
            }
            else
            {
                string fileName = AskForFilePath();
                emailsInList = _readFromFileRetriever.ReadFromFile(fileName);
            }
            
            List<EmailSections> parsedEmails = _parseEmail.ParseEmailFile(emailsInList);

            List<EmailSuccessResult> emailResult = new List<EmailSuccessResult>();
            emailResult = _email.RetrieveEmails(parsedEmails);

            Console.WriteLine("Output: ");

            foreach (var item in emailResult)
            {
                Console.Write(item.EmailSender + " ");
                Console.Write(item.Attempt + " ");
                item.Providers.ForEach(x => Console.Write(x + ", "));
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        public static string AskForFilePath()
        {
            Console.WriteLine("Enter file name: ");
            string fileName = Console.ReadLine();
            return fileName;
        }

        //public static List<string> ReadEmailFile(string fileInput)
        //{
        //    string validValue = "input.txt";

        //    string file = fileInput;
        //    var list = new List<string>();
        //    //handle for a input file name
        //    try
        //    {
        //        while (!file.Equals(validValue))
        //        {
        //            Console.WriteLine("Enter right input: ");
        //            file = Console.ReadLine();
        //        }

        //        var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
        //        using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
        //        {
        //            string line;
        //            while ((line = streamReader.ReadLine()) != null)
        //            {
        //                list.Add(line);
        //            }
        //        }
        //        list.ForEach(x => Console.WriteLine(x + ", "));
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Wrong file path");
        //    }
        //    return list;
        //}

        //public static List<EmailSections> ParseEmail(List<string> emailsList)
        //{
        //    List<EmailSections> emailDetailList = new List<EmailSections>();

        //    foreach (var item in emailsList)
        //    {
        //        string senderEmail = item.Substring(0, item.IndexOf(" ", StringComparison.Ordinal));
        //        string receiverEmail = item.Substring(senderEmail.Length + 1, item.IndexOf(" ", senderEmail.Length + 1) - senderEmail.Length);

        //        int firstQuote = item.IndexOf("\"", StringComparison.Ordinal);
        //        string subjectandBody = item.Substring(firstQuote, item.Length - item.IndexOf("\"", firstQuote, StringComparison.Ordinal));

        //        String[] words = subjectandBody.Split(new string[] { "\" \"" }, StringSplitOptions.None);
        //        string subjectEmail = words[0].Remove(0, 1);
        //        string bodyEmail = words[1].Remove(words[1].Length - 1);

        //        EmailSections emailSections = new EmailSections()
        //        {
        //            EmailSender = senderEmail,
        //            EmailReceiver = receiverEmail,
        //            EmailSubject = subjectEmail,
        //            EmailBody = bodyEmail
        //        };
        //        emailDetailList.Add(emailSections);
        //    }

        //    return emailDetailList;
        //}

        //public static List<EmailSuccessResult> SendEmails(List<EmailSections> emailList)
        //{
        //    List<EmailSuccessResult> emailResults = new List<EmailSuccessResult>();
        //    foreach (var item in emailList)
        //    {
        //        emailResults = SendEmail(item, emailResults);
        //    }
        //    return emailResults;
        //}

        //public static List<EmailSuccessResult> SendEmail(EmailSections emailList, List<EmailSuccessResult> emailSuccessResultList)
        //{
        //    Providers providers = new Providers();
        //    bool sent = false;
        //    List<int> usedNumbers = new List<int>();
        //    int attempt = 0;
        //    int sentAttempt = 0;
        //    List<string> providersList = new List<string>();

        //    while (!sent)
        //    {
        //        if (usedNumbers.Count() == 3) { break; }
        //        int randomForProvider = GenerateNumberForProvider(usedNumbers);
        //        usedNumbers.Add(randomForProvider);
        //        IProviders emailProvider = providers.EmailProviders.ElementAt(randomForProvider);
        //        sent = emailProvider.Connect();
        //        EmailSuccessResult emailSuccessResult = new EmailSuccessResult();
        //        if (!sent)
        //        {
        //            if (emailSuccessResultList.Where(x => x.EmailSender.Equals(emailList.EmailSender)).Count() > 0) 
        //            {
        //                providersList.Add(emailProvider.ToString());
        //            }
        //            else
        //            {
        //                providersList.Add(emailProvider.ToString());
        //                emailSuccessResult.Providers = providersList;
        //                emailSuccessResult.EmailSender = emailList.EmailSender;
        //                emailSuccessResultList.Add(emailSuccessResult);
        //            }
        //            attempt++;
        //            continue;
        //        }
        //        else
        //        {
        //            if(emailSuccessResultList.Where(x => x.EmailSender.Equals(emailList.EmailSender)).Count() > 0)
        //            {
        //                providersList.Add(emailProvider.ToString());
        //            }
        //            else
        //            {
        //                providersList.Add(emailProvider.ToString());
        //                emailSuccessResult.Providers = providersList;
        //                emailSuccessResult.EmailSender = emailList.EmailSender;
        //                emailSuccessResultList.Add(emailSuccessResult);
        //            }

        //            sent = emailProvider.Send(emailList);

        //            if (sent)
        //            {
        //                sentAttempt++;
        //            }
        //        }
        //    }
        //    return emailSuccessResultList;
        //}
        
        //public static int GenerateNumberForProvider(List<int> usedNumbers)
        //{
        //    Random random = new Random();
        //    bool okay = false;
        //    int randomNumber = 0;
        //    while (!okay)
        //    {
        //        randomNumber = random.Next(0, 3);
        //        if (!usedNumbers.Contains(randomNumber))
        //        {
        //            okay = true;
        //        }
        //    }
        //    return randomNumber;
        //}

    }
}