using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    public class EmailSuccessResult
    {
        public string EmailSender { get; set; }
        public List<string> Providers { get; set; }
        public int Attempt { get { return Providers.Count; } }
    }
}
