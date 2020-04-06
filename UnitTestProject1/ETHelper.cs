using System;
using BusinessWork.Works;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class ETHelper
    {
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                ETHandleJob job = new ETHandleJob();
                job.Login("","");
                job.GetData();
            }
            catch (Exception ex)
            {

            }

        }
    }
}
