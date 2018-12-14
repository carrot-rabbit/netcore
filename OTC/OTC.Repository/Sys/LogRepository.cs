using OTC.Data;
using OTC.Data.Sys;
using System;
using System.Collections.Generic;
using System.Text;

namespace OTC.Repository.Sys
{
    public class LogRepository : EFContextBase<Log, int>, ILogRepository
    {
        /// <summary>
        /// 获取所有日志
        /// </summary>
        /// <returns></returns>
        public List<Log> GetAll()
        {
            return FindAll();
        }
    }
}
