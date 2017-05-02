using EmailService.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    public interface IParse
    {
        List<EmailSections> ParseEmailFile(List<string> emailsList);

    }
}
