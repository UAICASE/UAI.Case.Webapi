
using Microsoft.AspNetCore.Http;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Academico;
using UAI.Case.Domain.Common;
using UAI.Case.EFProvider;

namespace UAI.Case.Repositories
{
    public interface ITodoRepository : IRepository<Todo>
    {
         long  GetPendientes(Guid IdUsuario);
    }
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        public TodoRepository(IDbContext db, IHttpContextAccessor context) : base(context,db) { }

        public long  GetPendientes(Guid IdUsuario)
        {

            try
            {
                String qry = "SELECT COUNT(ag.id) FROM AlumnoCursoGrupo ag, GrupoTodo gt "
            + "WHERE gt.Grupo.Id=ag.Grupo.Id AND ag.Alumno.Id = @p0 AND gt.Done = @p1";



                //String strQry = "FROM GrupoTodo as todo WHERE todo.Done='false' AND todo.Grupo.Id  IN (SELECT Grupo.Id From AlumnoCursoGrupo ac WHERE ac.Alumno.Id = :id)";
                //IQuery qry = Session.CreateQuery(qry2);

                //qry.SetParameter("id", IdUsuario);
                //qry.SetParameter("done", false);


                long res =  _db.FromSQL<AlumnoCursoGrupo>(qry, IdUsuario.ToString(), "False").ToList().Count();
                return res;
            }
            catch (Exception)
            {

                return -1;
            }

            
        }
    }
}
