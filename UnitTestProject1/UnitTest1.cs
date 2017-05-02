using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using Moq;
using EmailService;
using EmailService.Model;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        public Mock<IEmail> _mockEmail = new Mock<IEmail>();
        public Mock<IParse> _mockParse = new Mock<IParse>();

        [TestMethod]
        public void Parse_Email_String()
        {
            List<string> emailList = new List<string>();
            emailList.Add("jlisam@insikt.com xyz@gmail.com \"work work\" \"hello world\"");


            List<EmailSections> list = new List<EmailSections>();

            _mockParse.Setup(x => x.ParseEmailFile(It.IsAny<List<string>>())).Returns(list);

            List<EmailSections> result = _mockParse.Object.ParseEmailFile(emailList);

            Assert.AreEqual(result, list);

        }

        [TestMethod]
        public void What_Returns_Back_From_A_Email_Request()
        {
            List<EmailSections> emailSection = new List<EmailSections>();

            EmailSections em = new EmailSections();
            em.EmailSender = "bb@bb.com";
            em.EmailBody = "hi";
            em.EmailSubject = "how are you";
            em.EmailReceiver = "cc@ccc.com";

            emailSection.Add(em);

            var list = new List<EmailSuccessResult>();
            _mockEmail.Setup(x => x.RetrieveEmails(It.IsAny<List<EmailSections>>())).Returns(list);

            List<EmailSuccessResult> result = _mockEmail.Object.RetrieveEmails(emailSection);

            Assert.AreEqual(result, list);
        }


    }
}
