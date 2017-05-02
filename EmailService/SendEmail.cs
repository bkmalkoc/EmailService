using EmailService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmailService
{
    public class SendEmail : IEmail, IGenerator
    {
        public int GenerateNumberForProviders(List<int> usedNumbers)
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

        public List<EmailSuccessResult> RetrieveEmails(List<EmailSections> emailList)
        {
            List<EmailSuccessResult> emailResults = new List<EmailSuccessResult>();
            foreach (var item in emailList)
            {
                emailResults = SendEmails(item, emailResults);
            }
            return emailResults;
        }

        public List<EmailSuccessResult> SendEmails(EmailSections emailList, List<EmailSuccessResult> emailSuccessResultList)
        {
            Providers providers = new Providers();
            bool connected = true, emailSent = true;
            List<int> usedNumbers = new List<int>();
            int sentAttempt = 0;
            List<string> providersList = new List<string>();

            while (connected)
            {
                if (usedNumbers.Count == 3) { break; }
                int randomForProvider = GenerateNumberForProviders(usedNumbers);
                usedNumbers.Add(randomForProvider);

                IProviders emailProvider = providers.EmailProviders.ElementAt(randomForProvider);
                connected = emailProvider.Connect();

                bool listContainsSender = emailSuccessResultList.Any(x => x.EmailSender == emailList.EmailSender);
                EmailSuccessResult emailSuccessResult = new EmailSuccessResult();

                if (connected)
                {
                    providersList.Add(emailProvider.GetType().Name);
                    if (listContainsSender)
                    {
                        break;
                    }
                    else
                    {
                        emailSuccessResult.Providers = providersList;
                        emailSuccessResult.EmailSender = emailList.EmailSender;
                        emailSuccessResultList.Add(emailSuccessResult);
                    }

                    emailSent = emailProvider.Send(emailList);

                    if (emailSent)
                    {
                        sentAttempt++;
                    }
                }
                else
                {
                    providersList.Add(emailProvider.GetType().Name);
                    if (listContainsSender)
                    {
                        break;
                    }
                    else
                    {
                        emailSuccessResult.Providers = providersList;
                        emailSuccessResult.EmailSender = emailList.EmailSender;
                        emailSuccessResultList.Add(emailSuccessResult);
                    }
                }
            }
            return emailSuccessResultList;
        }
    }
}
