using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    public interface IFile
    {
        List<string> ReadFromFile(string fileInput);
    }
}
