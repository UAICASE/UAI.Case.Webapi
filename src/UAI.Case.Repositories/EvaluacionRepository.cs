using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.CASE;
using UAI.Case.Domain.Common;


namespace UAI.Case.Repositories
{
  
        public interface IEvaluacionRepository : IRepository<Evaluacion>
        {
            IList<Evaluacion> GetEvaluacionesConRespuestas(Guid ModeloId);
        }
        public class EvaluacionRepository : Repository<Evaluacion>, IEvaluacionRepository
        {
            public EvaluacionRepository(IDbContext<Evaluacion> db, IHttpContextAccessor context) : base(context,db) { }

            public IList<Evaluacion> GetEvaluacionesConRespuestas(Guid ModeloId)
            {
            
                try
                {
                

                
                //TODO: Pendiente
                String qry2 = "SELECT * FROM Evaluacion e JOIN RespuestaEvaluacion re ON e.ModeloId== WHERE  e.ModeloId= @p0";


                //String strQry = "FROM GrupoTodo as todo WHERE todo.Done='false' AND todo.Grupo.Id  IN (SELECT Grupo.Id From AlumnoCursoGrupo ac WHERE ac.Alumno.Id = :id)";
                //IQuery qry = Session.CreateQuery(qry2);

                //qry.SetParameter("id",ModeloId);

                var res = _db.GetAll().ToList() ;// _db.FromSQL<Evaluacion>(qry2, ModeloId.ToString()).ToList();


                //IList<Evaluacion> res = qry.List<Evaluacion>();
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
