using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessWork.Works;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFine.Code;
using UnitTestProject1.ResultModel;

namespace UnitTestProject1
{
    [TestClass]
    public class CommonTest
    {
        [TestMethod]
        public void TestMethod1()
        {

            Dictionary<int, int> dic = null;

            foreach(var item in dic)
            {

            }

            int maxCount = 10;
            string msg = "sdfsadfasdfsadfsdfdfsdfsdf";
            int totalNum = msg.Length % maxCount > 0 ? msg.Length / maxCount + 1 : msg.Length / maxCount;

             
            for (int i = 0; i < totalNum; i++)
            {
                //var str = msg.Split(100).Substring(i * maxCount, (i + 1) * maxCount);
            }
            try
            {
                ETHandleJob job = new ETHandleJob();
                job.Login("", "");
                job.GetData();
            }
            catch (Exception ex)
            {

            }

        }
        public Task MakeEffectiveOfDutyTransfer()
        {
            int coreTotalCount = 0;
            int failTotalCount = 0;
            TaskFactory taskFactory = new TaskFactory();
            Task[] dutyTransferTasks = new Task[]
            {
                //组织角色
                taskFactory.StartNew(() =>
                {
                   Thread.Sleep(1000);
                }),
                //下属员工&其他人员角色
                taskFactory.StartNew(() =>
                {
                    Thread.Sleep(5000);
                }),
                //入离职待办事项规则
                taskFactory.StartNew(() =>
                {
                   Thread.Sleep(10000);
                })
            };
            Task result = taskFactory.ContinueWhenAll(dutyTransferTasks, (tasks) =>
            {
                 
            });

            return result;
        }

    }
     
}
