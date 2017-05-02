using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    public class GenerateNumber : IGenerator
    {
        public int GenerateNumberForProviders(List<int> usedNumbers)
        {
            Random random = new Random();
            bool okay = false;
            int randomNumber = 0;
            while (!okay)
            {
                randomNumber = random.Next(0, 3);
                if (!usedNumbers.Contains(randomNumber))
                {
                    okay = true;
                }
            }
            return randomNumber;
        }
    }
}
