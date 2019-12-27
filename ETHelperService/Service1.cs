using BusinessWork.Works;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ETHelperService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //启动定时任务
            ReportJobScheduler.ExecuteByCron<ETHandleJob>("0 50,55 21 * * ? ");
        }

        protected override void OnStop()
        {
        }
    }
}
