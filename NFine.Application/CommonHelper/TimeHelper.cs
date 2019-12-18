using NFine.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.CommonHelper
{
    public static class TimeHelper
    {
        /// <summary>
        /// 最近今天组件的时间类型处理
        /// </summary>
        /// <param name="timeType"></param>
        /// <returns></returns>
        public static DateTime HandStartTime(string timeType)
        {
            DateTime startTime = DateTime.MinValue.ToDate();
            switch (timeType)
            {
                case "1":
                    startTime = DateTime.Now.ToString("yyyy-MM-dd").ToDate();
                    break;
                case "2":
                    startTime = DateTime.Now.AddDays(-7);
                    break;
                case "3":
                    startTime = DateTime.Now.AddMonths(-1);
                    break;
                case "4":
                    startTime = DateTime.Now.AddMonths(-3);
                    break;
                default:
                    break;
            }

            return startTime;
        }
    }
}
