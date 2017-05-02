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
            bool sent = false;
            List<int> usedNumbers = new List<int>();
            int attempt = 0;
            int sentAttempt = 0;
            List<string> providersList = new List<string>();

            while (!sent)
            {
                if (usedNumbers.Count == 3) { break; }
                int randomForProvider = GenerateNumberForProviders(usedNumbers);
                usedNumbers.Add(randomForProvider);
                IProviders emailProvider = providers.EmailProviders.ElementAt(randomForProvider);
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

                    sent = emailProvider.Send(emailList);

                    if (sent)
                    {
                        sentAttempt++;
                    }
                }
            }
            return emailSuccessResultList;
        }
    }
}
