using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    class SendGrid : IEmail
    {
        public bool Connect()
        {
            string connection = "success";
            
            for (int i = 0; i < 7; i++)
            {
                if (connection.Equals("success"))
                {
                    Console.WriteLine("SendGrid connection successful");
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
                    Console.WriteLine("Email sent to SendGrid");
                    return true;
                }
            }
            return false;
        }
    }
}
