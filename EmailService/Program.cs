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
    }
}