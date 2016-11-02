using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Common;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface ILogAppService : IAppService<LogMessage>
    {
    }
    public class LogAppService : AppService<LogMessage>, ILogAppService
    {

        public LogAppService(IRepository<LogMessage> Repository) : base(Repository) { }
    }
}