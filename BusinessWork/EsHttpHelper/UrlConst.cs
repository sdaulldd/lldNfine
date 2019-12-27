using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessWork.EsHttpHelper
{
    public static class UrlConst
    {
        /// <summary>
        /// 域名
        /// </summary>
        public static string DomainName = "http://ops.beisencorp.com";

        /// <summary>
        /// token
        /// </summary>
        public static string tokenStr = "LoginId=liliandong; BSOps=0102197199694A88D708FE19F1FD61DD9FD708000A6C0069006C00690061006E0064006F006E0067001E6C0069006C00690061006E0064006F006E0067002C004E67DE8F1C4E2C00360037007C00330030007C002C006F00700073007C006F00700073007C00012F00FF";

        public static string timeStr = "0 5,23,25,39 17 * * ? ";//"0 50,55 21 * * ? ";
        /// <summary>
        /// 登录
        /// </summary>
        public static string SignInUrl = "/OpsAdmin/Account/SignIn";

        /// <summary>
        /// 获取数据url
        /// </summary>
        public static string GetDataUrl = "/OpsAdmin/errortracker/GetExceptionCounts";

        /// <summary>
        /// 获取数据url
        /// </summary>
        public static string SolevdUrl = "/OpsAdmin/errortracker/SolevdException";
       

    }
}