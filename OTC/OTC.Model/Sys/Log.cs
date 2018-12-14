using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OTC.Data.Sys
{
    [Table("cms_const")]
    public partial class Log : IKey<int>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 应用
        /// </summary>
        //[Description("应用")]
        //[DisplayName("应用")]
        public string Application { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? Logged { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 日志
        /// </summary>
        public string Logger { get; set; }
        /// <summary>
        /// 调用位置
        /// </summary>
        public string Callsite { get; set; }
        /// <summary>
        /// 异常
        /// </summary>
        public string Exception { get; set; }
    }
}
