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
            bool check = false;
            int randomNumber = 0;
            while (!check)
            {
                randomNumber = random.Next(0, 3);
                if (!usedNumbers.Contains(randomNumber))
                {
                    check = true;
                }
            }
            return randomNumber;
        }
    }
}
