using EmailService.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService.UnitTest
{
    [TestClass]
    public class EmailParseTest
    {
        public Mock<IParse> _mockParse = new Mock<IParse>();

        [TestMethod]
        public void Parse_Email_String()
        {
            List<string> emailList = new List<string>();

            List<EmailSections> list = new List<EmailSections>();

            _mockParse.Setup(x => x.ParseEmailFile(It.IsAny<List<string>>())).Returns(list);

            List<EmailSections> result = _mockParse.Object.ParseEmailFile(emailList);

            Assert.AreEqual(result, list);

        }
    }
}
