﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService.Model 
{
    class AmazonSES : IProviders
    {
        public bool Connect()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 9);

            while (randomNumber > 6)
            {
                return true;
            }
            return false;
        }

        public bool Send(EmailSections emailSections)
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 9);

            while (randomNumber > 2)
            {
                return true;
            }
            return false;
        }
    }
}
