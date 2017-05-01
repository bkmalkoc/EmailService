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

        //List of Providers


        //public int UseMailGun(EmailSections emailSections)
        //{
        //    int result = 0;
        //    result = mailgun.Connect();
        //    if(result == 0)
        //    {
        //        mailgun.Send(emailSections);
        //    }
        //    return result;
        //}

        //public int UseSendGrid()
        //{
        //    return sendgrid.Connect();
        //}

        //public int UseAmazonSES()
        //{
        //    return amazonses.Connect();
        //}
    }
}
