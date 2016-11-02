using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Academico;
using UAI.Case.Domain.CASE;
using UAI.Case.Domain.Common;
using UAI.Case.Repositories;

namespace UAI.Case.Repositories
{
  
        public interface IEvaluacionRepository : IRepository<Evaluacion>
        {
            IList<Evaluacion> GetEvaluacionesConRespuestas(Guid ModeloId);
        }
        public class EvaluacionRepository : Repository<Evaluacion>, IEvaluacionRepository
        {

           public EvaluacionRepository(EFProvider.IDbContext db, IHttpContextAccessor context) : base(context,db) { } //TODO: Ver tema users en el constructor de repositorybase

            public IList<Evaluacion> GetEvaluacionesConRespuestas(Guid ModeloId)
            {
            
                try
                {
                //TODO: Pendiente
                String qry2 = "SELECT * FROM Evaluacion e JOIN RespuestaEvaluacion re ON e.ModeloId== WHERE  e.ModeloId= @p0";

                var res = _db.FromSQL<Evaluacion>(qry2, ModeloId.ToString()).ToList();
                 return res;
                }
                catch (Exception e)
                {
                var a = e;
                    return null;
                }


            }
        }
    
}
