/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: Sdaulld
 * Description: NFine快速开发平台
 * Website：http://www.lild.cn
*********************************************************************************/
using System;

namespace NFine.Domain.Entity.Work
{
    public class DailyEntity : IEntity<DailyEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        /// <summary>
        /// 迭代
        /// </summary>
        public string Sprint { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        ///内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 障碍或者问题
        /// </summary>
        public string Problem { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public int? Status { get; set; }
        public string F_Id { get; set; }
        /// <summary>
        /// 日志所属人
        /// </summary>
        public string F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public bool? F_DeleteMark { get; set; }
        public string F_DeleteUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
    }
}
