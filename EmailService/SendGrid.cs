using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    class SendGrid : IEmail
    {
        public void Connect()
        {
            Console.WriteLine("SendGrid connection setup");
        }

        public void Send()
        {
            Console.WriteLine("SendGrid email sent");
        }
    }
}
