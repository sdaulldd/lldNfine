/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: Sdaulld
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/

using NFine.Domain.Entity.Work;
using System.Data.Entity.ModelConfiguration;

namespace NFine.Mapping.Work
{
    public class DailyMap : EntityTypeConfiguration<DailyEntity>
    {
        public DailyMap()
        {
            this.ToTable("Daily");
            this.HasKey(t => t.F_Id);
        }
    }
}
