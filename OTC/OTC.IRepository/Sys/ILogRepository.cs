using OTC.Data;
using OTC.Data.Sys;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTC.Repository.Sys
{
    public interface ILogRepository : IEFContextBase<Log, int>
    {
        /// <summary>
        /// 获取所有日志
        /// </summary>
        /// <returns></returns>
        List<Log> GetAll();
    }
}
