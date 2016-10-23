using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Common;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface IRequestRegisterTokenAppService : IAppService<RequestRegisterToken>
    {
    }
    public class RequestRegisterTokenAppService : AppService<RequestRegisterToken>, IRequestRegisterTokenAppService
    {
        public RequestRegisterTokenAppService(IRepository<RequestRegisterToken> repository) : base(repository) { }
    }
}
