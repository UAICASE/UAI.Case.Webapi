using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Academico;
using UAI.Case.Domain.Roles;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface IDocenteAlumnoCursoAppService : IAppService<AlumnoCursoGrupo>
    {
        IList<Grupo> GetMisGruposActivos(Guid idUsuario);
        IList<Alumno> GetAlumnosFromGrupo(Guid idGrupo);
        IList<Alumno> GetAlumnosSinGrupo(Guid idCurso);
    }
    public class DocenteAlumnoCursoAppService : AppService<AlumnoCursoGrupo>, IDocenteAlumnoCursoAppService
    {
        public DocenteAlumnoCursoAppService(IRepository<AlumnoCursoGrupo> repository) : base(repository) { }


        public IList<Alumno> GetAlumnosSinGrupo(Guid idCurso)
        {
            IList<AlumnoCursoGrupo> alc = GetAll(_ => _.Curso, _ => _.Alumno, _ => _.Grupo)
                  .Where(c => c.Curso.Id.Equals(idCurso) && c.Grupo==null).ToList();
            IList<Alumno> alumnos = new List<Alumno>();
            foreach (var item in alc)
            {
                alumnos.Add(item.Alumno);
            }

            return alumnos;
        }

        public IList<Alumno> GetAlumnosFromGrupo(Guid idGrupo)
        {
           IList<AlumnoCursoGrupo> alc = GetAll(_ => _.Curso, _ => _.Alumno, _ => _.Grupo)
                 .Where(c => c.Grupo.Id.Equals(idGrupo) && c.Curso.Activo).ToList();
            IList<Alumno> alumnos = new List<Alumno>();
            foreach (var item in alc)
            {
                alumnos.Add(item.Alumno);
            }

            return alumnos;
        }

        public IList<Grupo> GetMisGruposActivos(Guid idUsuario)
        {
            Grupo dto = null;
            IList<Grupo> grupos = new List<Grupo>();

            IList<AlumnoCursoGrupo> lista = GetAll(_ => _.Curso, _ => _.Alumno, _ => _.Grupo)
                 .Where(c => c.Alumno.Id.Equals(idUsuario) && c.Curso.Activo).ToList();
            foreach (AlumnoCursoGrupo item in lista)
            {
                dto = Mapper.Map<Grupo>(item.Grupo);
                if (!grupos.Contains(dto) && dto != null)
                    grupos.Add(dto);
            }

            return grupos;
        }
    }
}
