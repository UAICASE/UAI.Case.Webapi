using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Common;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface IChatAppService : IAppService<ChatMessage>
    {
    }

    public class ChatAppService : AppService<ChatMessage>, IChatAppService
    {
        
        public ChatAppService(IRepository<ChatMessage> Repository) : base(Repository){}
    }
}
