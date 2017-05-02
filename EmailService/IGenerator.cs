using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    public interface IGenerator
    {
        int GenerateNumberForProviders(List<int> usedNumbers);
    }
}
