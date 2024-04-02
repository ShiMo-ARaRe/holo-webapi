using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace holo_webapi.Model.Entitys
{
    /// <summary>
    /// 订单表
    /// </summary>
    public class Order
    {
        // [SugarColumn(...)]是用于 Sugar ORM（对象关系映射）的属性特性（Attribute）

        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] // 是否主键，是否自增
        public long Id { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [SugarColumn(IsNullable = false)] // 是否允许为空
        public string OrderNumber { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public decimal Price { get; set; }
        /// <summary>
        /// 订单日期
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// 鲜花Id
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long FlowerId { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long UserId { get; set; }
    }
}
