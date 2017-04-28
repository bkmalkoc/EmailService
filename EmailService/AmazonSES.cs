using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService 
{
    class AmazonSES : IEmail
    {
        public void Connect()
        {
            Console.WriteLine("AmazonSES connection setup");
        }

        public void Send()
        {
            Console.WriteLine("AmazonSES email sent");
        }
    }
}
