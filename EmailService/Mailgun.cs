using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    class Mailgun : IEmail
    {
        public void Connect()
        {
            Console.WriteLine("Mailgun connection setup");
        }

        public void Send()
        {
            Console.WriteLine("Mailgun email sent");
        }
    }
}
