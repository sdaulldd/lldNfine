using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessWork.EsHttpHelper
{
    /// <summary>
    /// ET 帮助类
    /// </summary>

    #region loginModel
    /// <summary>
    /// 登录实体
    /// </summary>
    public class LoginModel
    {
        public LoginModel()
        {
            username = "liliandong";
            password = "Zhaodi0901";
            remember = true;
        }

        public string username { get; set; }
        public string password { get; set; }
        public bool remember { get; set; }
        public string returnUrl { get; set; }
    }

    public class GetDataModel
    {
        public int startIndex { get; set; }
        public int Status { get; set; } = 1;
        public string GroupName { get; set; } = "CoreHR";
        public string Start { get; set; } = "2019-12-23+19%3A58%3A26";
        public string End { get; set; } = "2019-12-24+19%3A58%3A26";
        public string AppName { get; set; } = "CoreHR"; 
    }
    #endregion

    #region Result

    public class ETDataResult
    {
        public int sEcho { get; set; }
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public int Code { get; set; }
        public List<Aadata> aaData { get; set; }
    }

    public class Aadata
    {
        public string ErrorID { get; set; }
        public string ApplicaitonName { get; set; }
        public string LastDate { get; set; }
        public string Count { get; set; }
        public string ErrorType { get; set; }
        public string SourceFilePath { get; set; }
        public string CatchClassName { get; set; }
        public string CatchLineNum { get; set; }
        public string ThrowClassName { get; set; }
        public string ThrowLineNum { get; set; }
        public string ExceptionType { get; set; }
        public string Status { get; set; }
        public string Hash { get; set; }
    }

    #endregion
}