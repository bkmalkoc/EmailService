using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService.Model
{
    public class EmailSections
    {
        public string EmailSender { get; set; }
        public string EmailReceiver { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
    }
}
