using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    public class Mailgun : IEmail
    {
        public bool Connect()
        {
            string connection = "false";
            
            for (int i = 0; i < 7; i++)
            {
                if (connection.Equals("success"))
                {
                    Console.WriteLine("Mailgun connection successful");
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
                    Console.WriteLine("Email sent to Mailgun");
                    return true;
                }
            }
            return false;
        }
    }
}
