using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    public interface IEmail
    {
        void Connect();
        void Send();
    }
}
