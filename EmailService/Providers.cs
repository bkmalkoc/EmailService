using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    public class Providers
    {
        public List<IEmail> EmailProviders;

        Mailgun mailgun = new Mailgun();
        SendGrid sendGrid = new SendGrid();
        AmazonSES amazonSes = new AmazonSES();

        public Providers()
        {
            EmailProviders = new List<IEmail>();
            
            EmailProviders.Add(mailgun);
            EmailProviders.Add(sendGrid);
            EmailProviders.Add(amazonSes);
        }
    }
}
