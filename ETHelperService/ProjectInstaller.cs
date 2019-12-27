using BusinessWork.EsHttpHelper;
using BusinessWork.Works;
using NFine.Code;
using System.ComponentModel;
using System.Configuration.Install;

namespace ETHelperService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
            //启动定时任务
            ReportJobScheduler.ExecuteByCron<ETHandleJob>(Configs.GetValue("timeStr"));//UrlConst.timeStr
        }
    }
}
