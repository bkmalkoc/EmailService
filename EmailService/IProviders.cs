using EmailService.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    public interface IProviders
    {
        bool Connect();
        bool Send(EmailSections emailsections);
    }
}
