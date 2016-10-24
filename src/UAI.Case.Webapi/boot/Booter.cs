using AutoMapper;

//using NHibernate;
using StructureMap;
using System;
using UAI.Case.Domain.Academico;
using UAI.Case.Domain.CASE;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Proyectos;
using UAI.Case.Domain.Roles;
using UAI.Case.Dto;
using UAI.Case.Repositories;
using UAI.Case.Security;
using Microsoft.AspNetCore.Http;
using UAI.Case.Domain.Interfaces;
using UAI.Case.EFProvider;

namespace UAI.Case.Boot
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class Booter
    {

        public static void RunAutoMapperConfig()
        {

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Docente, Docente>();
                cfg.CreateMap<Docente, Docente>();
                cfg.CreateMap<Curso, Curso>();
                cfg.CreateMap<Todo, Todo>();
                cfg.CreateMap<Nota, Nota>();
                cfg.CreateMap<Materia, Materia>();
                cfg.CreateMap<Alumno, Alumno>();
                cfg.CreateMap<Curso, CursoDTO>();
                cfg.CreateMap<Proyecto, ProyectoDTO>();
                cfg.CreateMap<Usuario, Usuario>();
                cfg.CreateMap<Grupo, Grupo>();
                cfg.CreateMap<Mail, Mail>();
                cfg.CreateMap<ContenidoMateria, ContenidoMateria>();
                cfg.CreateMap<Contenido, Contenido>();
                cfg.CreateMap<Unidad, Unidad>();
                cfg.CreateMap<Archivo, Archivo>();
                cfg.CreateMap<Clase, Clase>();
                cfg.CreateMap<UnidadClase, UnidadClase>();
                cfg.CreateMap<Evaluacion, Evaluacion>();
                cfg.CreateMap<Elemento, Elemento>();
                cfg.CreateMap<Evaluacion, EvaluacionDTO>();
                cfg.CreateMap<AlumnoCursoGrupo, AlumnoCursoGrupo>();
            });
         
            

        }

        

        public static Container Run(ICreds creds)
        {
            //var configuration = NHibernateInitializer.Initialize(cn);
            var container = new Container(c =>
            {


                c.For(typeof(ICreds)).Singleton().Use(creds);

                c.For<IDbContext>().Singleton().Use(() => new UaiCaseContext(creds.cs));
                c.For(typeof(IRepository<>)).Use(typeof(Repository<>));
                c.For<IAuthDataExtractor>().Use<AuthDataExtractor>();
                c.For<IHttpContextAccessor>().Use<HttpContextAccessor>();
                c.For<IAuthenticatedData>().Use(_ => _.GetInstance<IAuthDataExtractor>().GetCurrentData());
                c.For(typeof(ITodoRepository)).Use(typeof(TodoRepository));
                c.AddRegistry(new AppServiceRegistry());
                c.AddRegistry(new RepositoryRegistry());

          

            });
            //NHibernateInitializer.UpdateSchema(configuration, container.GetInstance<ISessionFactory>());

            RunAutoMapperConfig();
            return container;
        }

    }
}


