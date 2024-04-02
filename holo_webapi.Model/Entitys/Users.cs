using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace holo_webapi.Model.Entitys
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class Users
    {
        // [SugarColumn(...)]是用于 Sugar ORM（对象关系映射）的属性特性（Attribute）

        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] // 是否主键，是否自增
        public long Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [SugarColumn(IsNullable = false)] // 是否允许为空
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string NickName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Password { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int UserType { get; set; }

    }
}
