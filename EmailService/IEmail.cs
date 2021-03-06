﻿using EmailService.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    public interface IEmail
    {
        List<EmailSuccessResult> RetrieveEmails(List<EmailSections> emailList);
    }
}
