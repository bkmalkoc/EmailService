using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void TestMethod1()
        {
            string assemblyCodeBase = System.Reflection.Assembly.GetEntryAssembly().CodeBase;
            string dirName = Path.GetDirectoryName(assemblyCodeBase);
        }


    }
}
