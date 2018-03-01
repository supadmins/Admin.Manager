using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IChipo.YJH.Model
{
    public interface IBaseModel
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        string CreateUser { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        string UpdateUser { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        bool IsDel { get; set; }
    }
}
