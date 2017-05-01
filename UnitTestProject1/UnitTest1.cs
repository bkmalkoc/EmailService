using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<EmailService.EmailSections> emailSectionList = new List<EmailService.EmailSections>();
            EmailService.EmailSections emailSection = new EmailService.EmailSections();
            emailSectionList.Add(emailSection);

            Assert.AreEqual(EmailService.Program.ParseEmail(list), emailSectionList);
        }
    }
}
