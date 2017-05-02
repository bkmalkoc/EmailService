using EmailService.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    public class ParseEmail : IParse
    {
        public List<EmailSections> ParseEmailFile(List<string> emailsList)
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
    }
}
