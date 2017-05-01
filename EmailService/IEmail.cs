using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    public interface IEmail
    {
        bool Connect();
        bool Send(EmailSections emailsections);
    }
}
