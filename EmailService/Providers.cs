using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    public class Providers
    {
        Mailgun mailgun = new Mailgun();
        SendGrid sendgrid = new SendGrid();
        AmazonSES amazonses = new AmazonSES();

        private string providerName;
    }
}
