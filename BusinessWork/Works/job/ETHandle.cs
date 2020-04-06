using System;
using Quartz;
using System.IO;
using System.Threading.Tasks;
using NFine.Code;
using BusinessWork.EsHttpHelper;
using System.Net;
using System.Text;
using System.Web;
using System.Threading;
using System.Collections.Generic;

namespace BusinessWork.Works
{
    /// <summary>
    /// ET帮助类
    /// </summary>
    public class ETHandleJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            writeLog("开始执行任务");
            //先登录处理token
            Login();
            //、处理数据
            var result = GetData();
            if (result.aaData != null && result.aaData.Count > 0)
            {
                int i = 0;
                result.aaData.ForEach(p =>
                {

                    try
                    {
                        Thread.Sleep(5000);
                        soveData(p.ErrorID);
                        i++;
                        writeLog("成功的id" + p.ErrorID);
                    }
                    catch (Exception ex)
                    {
                        writeLog($"{ex.Message};失败的的id:" + p.ErrorID);
                    }
                });
                writeLog($"总计{result.aaData.Count}条；成功{i}条");
            }
            else
            {
                writeLog("没有要处理的数据");
            }
            writeLog("结束执行任务");
        }

        #region private
        private void writeLog(string msg)
        {
            var reportDirectory = string.Format("/reports/{0}/", DateTime.Now.ToString("yyyy-MM"));
            //reportDirectory = System.Web.Hosting.HostingEnvironment.MapPath(reportDirectory);
            reportDirectory = $"D:\\ETLog\\{DateTime.Now.ToString("yyyy-MM")}";
            if (!Directory.Exists(reportDirectory))
            {
                Directory.CreateDirectory(reportDirectory);
            }
            var dailyReportFullPath = string.Format("{0}\\report_{1}.log", reportDirectory, DateTime.Now.Day);
            var logContent = string.Format("{0}==>>{1}{2}", DateTime.Now, msg, Environment.NewLine);
            File.AppendAllText(dailyReportFullPath, logContent);
        }

        /// <summary>
        /// 解决问题
        /// </summary>
        /// <param name="errorId"></param>
        private void soveData(string errorId)
        {
            string Url = UrlConst.DomainName + UrlConst.SolevdUrl;
            DateTime dt = DateTime.Now.AddMonths(1);
            string startTimeDay = dt.ToString("yyyy-MM-dd");
            string dataTime2 = DateTime.Now.AddMonths(1).ToString("HH:mm:ss");
            dataTime2 = HttpUtility.UrlEncode(dataTime2);
            string dataTime = startTimeDay + "+" + dataTime2;
            string postData = $"errorId={errorId}&solution=%E6%AD%A3%E5%9C%A8%E5%A4%84%E7%90%86&completeTime={dataTime}&status=1&solveTime={dataTime}&rootException=&isReproduce=0";

            var resultData = PostWebRequest(Url, postData, Encoding.UTF8);
        }
        #endregion

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public void Login(string user = null, string pwd = null)
        {
            if (string.IsNullOrEmpty(user))
                user = Configs.GetValue("userName");
            if (string.IsNullOrEmpty(pwd))
                pwd = Configs.GetValue("userPwd");
            string Url = UrlConst.DomainName + UrlConst.SignInUrl;
            string postData = $"username={user}&password={pwd}&remember=true&returnUrl=";
            byte[] bytes = Encoding.Default.GetBytes(postData);
            CookieContainer myCookieContainer = new CookieContainer();
            try
            {
                //新建一个CookieContainer
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                //新建一个HttpWebRequest
                myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
                myHttpWebRequest.AllowAutoRedirect = false;
                myHttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
                myHttpWebRequest.Timeout = 60000;
                myHttpWebRequest.KeepAlive = true;
                myHttpWebRequest.ContentLength = bytes.Length;
                myHttpWebRequest.Method = "POST";
                myHttpWebRequest.CookieContainer = myCookieContainer;
                //设置HttpWebRequest
                Stream myRequestStream = myHttpWebRequest.GetRequestStream();
                myRequestStream.Write(bytes, 0, bytes.Length);
                myRequestStream.Close();
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                List<string> cokieStr = new List<string>();
                foreach (Cookie ck in myHttpWebResponse.Cookies)
                {
                    cokieStr.Add(ck.ToString());
                }
                if (cokieStr != null && cokieStr.Count > 0)
                {
                    UrlConst.tokenStr = string.Join(";", cokieStr);
                }
                myHttpWebResponse.Close();
            }
            catch (Exception ex)
            {

                return;
            }

        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public ETDataResult GetData()
        {
            ETDataResult result = new ETDataResult();

            try
            {
                string Url = UrlConst.DomainName + UrlConst.GetDataUrl;
                string startTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                string endTime = DateTime.Now.ToString("yyyy-MM-dd");
                string dataTime2 = DateTime.Now.ToString("HH:mm:ss");
                dataTime2 = HttpUtility.UrlEncode(dataTime2);

                string postData = $"startIndex=1&AppName=&GroupName=CoreHR&ErrorType=&Status=0&Start={startTime}+{dataTime2}&End={endTime}+{dataTime2}&Hash=&Tag=&TraceId=&Message=&CatchClassName=&CatchMethodName=&ErrorMessage=&ThrowClassName=&ThrowMethodName=&MachineName=&SourceFilePath=&UrlAbsolutePath=&RequestUrl=&SessionID=&TenantID=&UserID=";
                var resultData = PostWebRequest(Url, postData, Encoding.UTF8);
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<ETDataResult>(resultData);
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        /// <summary>
        /// Post数据接口
        /// </summary>
        /// <param name="postUrl">接口地址</param>
        /// <param name="paramData">提交json数据</param>
        /// <param name="dataEncode">编码方式</param>
        /// <returns></returns>
        static string PostWebRequest(string postUrl, string paramData, Encoding dataEncode, string cookie = "cookie")
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";
                if (cookie == "cookie")
                    webReq.Headers.Add("Cookie", UrlConst.tokenStr); //UrlConst.tokenStr  Configs.GetValue("tokenStr")

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return ret;
        }
    }
}