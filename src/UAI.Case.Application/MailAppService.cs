using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Common;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface IMailAppService : IAppService<Mail>
    {
    }
    public class MailAppService : AppService<Mail>, IMailAppService
    {
        public MailAppService(IRepository<Mail> repository) : base(repository) { }
    }
}
