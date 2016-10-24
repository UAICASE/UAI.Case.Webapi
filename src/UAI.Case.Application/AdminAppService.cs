
using UAI.Case.Application;
using UAI.Case.Domain.Roles;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface IAdminAppService : IAppService<Admin>
    {
    }

    public class AdminAppService : AppService<Admin>, IAdminAppService
    {
        public AdminAppService(IRepository<Admin> repository) : base(repository) { }

        
    }
}
