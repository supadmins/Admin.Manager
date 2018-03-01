using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IChipo.YJH.Model
{
    public class YJH_Model
    {
    }
    /// <summary>
    /// 用户表
    /// </summary>
    [Alias("yjh_user")]
    public class yjh_user : IBaseModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserPwd { get; set; }

        /// <summary>
        /// 用户类型 1 管理员 2 商户
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 用户状态 1 正常 2 禁用
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }


        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }
        public bool IsDel { get; set; }
    }

    /// <summary>
    /// 系统设置
    /// </summary>
    [Alias("yjh_setting")]
    public class yjh_setting {

    }
}
