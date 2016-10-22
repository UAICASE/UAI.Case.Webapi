using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.CASE;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface IEvaluacionAppService : IAppService<Evaluacion>
    {
        IList<Evaluacion> GetEvaluacionesConRespuestas(Guid ModeloId);
    }
    public class EvaluacionAppService : AppService<Evaluacion>, IEvaluacionAppService
    {
        IEvaluacionRepository _repository;
        public EvaluacionAppService(IEvaluacionRepository Repository) : base(Repository){ _repository = Repository; }

        public IList<Evaluacion> GetEvaluacionesConRespuestas(Guid ModeloId)
        {
            return _repository.GetEvaluacionesConRespuestas(ModeloId);
        }
    }

    public interface IRespuestaEvaluacionAppService : IAppService<RespuestaEvaluacion>
    {
    }
    public class RespuestaEvaluacionAppService : AppService<RespuestaEvaluacion>, IRespuestaEvaluacionAppService
    {
        public RespuestaEvaluacionAppService(IRepository<RespuestaEvaluacion> Repository) : base(Repository) { }
    }
}
