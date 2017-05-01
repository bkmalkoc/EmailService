using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService 
{
    class AmazonSES : IEmail
    {
        public bool Connect()
        {
            string connection = "success";
            
            for(int i = 0; i < 7; i++)
            {
                if (connection.Equals("success"))
                {
                    Console.WriteLine("AmazonSES connection successful");
                    return true;
                }
            }
            return false;
        }

        public bool Send(EmailSections emailSections)
        {
            string connection = "success";

            for (int i = 0; i < 7; i++)
            {
                if (connection.Equals("success"))
                {
                    Console.WriteLine("Email sent to AmazonSES");
                    return true;
                }
            }
            return false;
        }
    }
}
