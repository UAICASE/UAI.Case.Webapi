
using UAI.Case.Domain.Common;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface IArchivoAppService : IAppService<Archivo>
    {
    }
    public class ArchivoAppService : AppService<Archivo>, IArchivoAppService
    {
        public ArchivoAppService(IRepository<Archivo> repository) : base(repository) { }
    }
}
