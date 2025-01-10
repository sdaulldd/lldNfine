using NFine.Code;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using UserfullProgram.EsHttpHelper;

namespace UserfullProgram.Helper
{

    /// <summary>
    /// wiki关注一键清除
    /// </summary>
    public class WikiNoticeClear
    {

        #region  public Method
        public void GetAllDatas()
        {

        }

        public void HandleAllDatas()
        {
            //登录
            Login();
            string token = "";
            //获取数据
            var datas = GetData(ref token);
            //处理数据
            RemoveData(datas, token);
        }

        /// <summary>
        /// 解决问题
        /// </summary>
        /// <param name="errorId"></param>
        private void RemoveData(Dictionary<int, string> errorId, string token)
        {
            foreach (var id in errorId)
            {
                string Url = UrlConst.DomainName + UrlConst.SolevdUrl;
                string postData = $"pageId={id.Key}&atl_token={token}";
                var resultData = PostWebRequest(Url, postData, Encoding.UTF8);
            }
        }

        #endregion

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
        private void Login(string user = null, string pwd = null)
        {
            if (string.IsNullOrEmpty(user))
                user = "liliandong";// Configs.GetValue("userName");
            if (string.IsNullOrEmpty(pwd))
                pwd = "Zhaodi09011";// Configs.GetValue("userPwd");
            string Url = UrlConst.DomainName + UrlConst.SignInUrl;
            string postData = $"os_username={user}&os_password={pwd}&login=%E7%99%BB%E5%BD%95&os_destination=";
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
        public Dictionary<int, string> GetData(ref string token)
        {

            Dictionary<int, string> resultIds = new Dictionary<int, string>();
            try
            {

                string Url = UrlConst.DomainName + UrlConst.GetDataUrl;
                //第一步 获取总计多少数量
                var resultData = GetWebRequest(Url, Encoding.UTF8);
                string rowContent = string.Empty;

                int rowcount = GetPageNum(resultData, ref token);

                for (int i = rowcount; i > 0; i--)
                {
                    var dataByPage = GetAllIds(i);
                    if (dataByPage != null && dataByPage.Count > 0)
                    {
                        foreach (var item in dataByPage)
                        {
                            if (!resultIds.ContainsKey(item.Key))
                                resultIds.Add(item.Key, item.Value);
                        }
                    }

                }

            }
            catch (Exception ex)
            {

            }

            return resultIds;
        }
        #region WikiHelper
        public int GetPageNum(string content, ref string token)
        {
            int pageNum = 0;
            //先获取所有的数量
            MatchCollection idList = Regex.Matches(content, @"(?<=startIndex=).*(?=</a>)",
                  RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            if (idList.Count > 0)
            {
                var allStr = idList[idList.Count - 2].ToString();
                allStr = allStr.Remove(0, allStr.Length - 1);
                int.TryParse(allStr, out pageNum);
            }
            //\u0022 代表双引号
            MatchCollection tokenStr = Regex.Matches(content, @"(?<=atlassian-token\u0022 content=\u0022).*(?=\u0022>)",
                 RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            if (idList.Count > 0)
            {
                token = tokenStr[0].ToString();
            }

            return pageNum;
        }

        public Dictionary<int, string> GetAllIds(int pageNum)
        {
            Dictionary<int, string> resultIds = new Dictionary<int, string>();
            string Url = UrlConst.DomainName + UrlConst.GetDataUrl + "?startIndex=" + ((pageNum - 1) * 20).ToString();
            //第一步 获取总计多少数量
            var resultData = GetWebRequest(Url, Encoding.UTF8);
            string rowContent = string.Empty;

            MatchCollection rowCollection = Regex.Matches(resultData, @"<tr[^>]*>[\s\S]*?<\/tr>", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture); //对tr进行筛选
            for (int i = 0; i < rowCollection.Count; i++)
            {
                rowContent = rowCollection[i].Value;
                MatchCollection columnCollection = Regex.Matches(rowContent, @"<td[^>]*>[\s\S]*?<\/td>",
                    RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture); //对td进行筛选
                if (columnCollection.Count == 2)
                {
                    MatchCollection idList = Regex.Matches(rowContent, @"(?<=pageId=).*(?=</a>)",
                   RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                    if (idList.Count > 0)
                    {
                        try
                        {
                            var ff = idList[0].ToString().Trim();
                            var splictData = ff.Split('>');
                            ff = splictData[0].Remove(splictData[0].Length - 1);
                            int idInt = 0;
                            if (int.TryParse(ff, out idInt))
                            {
                                if (!resultIds.ContainsKey(idInt))
                                    resultIds.Add(idInt, splictData[1]);
                            }
                        }
                        catch (Exception ex)
                        {
                        }

                    }
                }

            }

            return resultIds;
        }
        #endregion
        #region  Public

        #endregion
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

        /// <summary>
        /// Post数据接口
        /// </summary>
        /// <param name="postUrl">接口地址</param>
        /// <param name="paramData">提交json数据</param>
        /// <param name="dataEncode">编码方式</param>
        /// <returns></returns>
        static string GetWebRequest(string postUrl, Encoding dataEncode, string cookie = "cookie")
        {
            string ret = string.Empty;
            try
            {
                //Stream stm = new System.IO.Compression.GZipStream(response.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "GET";
                webReq.ContentType = "text/html,application/xhtml+xml,application/xml";
                if (cookie == "cookie")
                    webReq.Headers.Add("Cookie", UrlConst.tokenStr); //UrlConst.tokenStr  Configs.GetValue("tokenStr")
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return ret;
        }
    }
}