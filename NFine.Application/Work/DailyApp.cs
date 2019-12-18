/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: Sdaulld
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Newtonsoft.Json.Linq;
using NFine.Application.CommonHelper;
using NFine.Code;
using NFine.Domain.Entity.Work;
using NFine.Domain.IRepository.Work;
using NFine.Repository.Work;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFine.Application.SystemManage
{
    public class DailyApp
    {
        private IDailyRepository service = new DailyRepository();

        public List<DailyEntity> GetList()
        {
            return service.IQueryable().ToList();
        }
        /// <summary>
        /// 根据userid分页获取数据
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public List<DailyEntity> GetListByCondition(string queryJson, Pagination pagination, string userID)
        {
            var expression = ExtLinq.True<DailyEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                expression = expression.And(t => t.Title.Contains(keyword));
            }
            if (!queryParam["timeType"].IsEmpty())
            {
                DateTime endTime = DateTime.Now.ToString("yyyy-MM-dd").ToDate().AddDays(1);
                DateTime startTime = TimeHelper.HandStartTime(queryParam["timeType"].ToString());
                expression = expression.And(t => t.F_CreatorTime >= startTime && t.F_CreatorTime <= endTime);
            }


            return service.FindList(expression, pagination).ToList();
        }


        public DailyEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.F_Id == keyValue);
        }
        public void SubmitForm(DailyEntity DailyEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                DailyEntity.Modify(keyValue);
                service.Update(DailyEntity);
            }
            else
            {
                DailyEntity.Create();
                service.Insert(DailyEntity);
            }
        }
    }
}
