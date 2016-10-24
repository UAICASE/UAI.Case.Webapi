using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Common;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using UAI.Case.Domain.Roles;
using UAI.Case.Domain.Academico;
using UAI.Case.Domain.CASE;
using UAI.Case.Domain.Proyectos;
using System.ComponentModel.DataAnnotations.Schema;

namespace UAI.Case.EFProvider
{
    public class UaiCaseContext : DbContext, IDbContext
    {

        public DbSet<Evaluacion> Evaluacion { get; set; }
        public DbSet<RespuestaEvaluacion> RespuestaEvaluacion { get; set; }
        public DbSet<Archivo> Archivo { get; set; }
        public DbSet<ChatMessage> ChatMessage { get; set; }
        public DbSet<JoinCursoRequest> JoinCursoRequest { get; set; }
        public DbSet<LogMessage> LogMessage { get; set; }
        public DbSet<Mail> Mail { get; set; }
        public DbSet<Numerador> Numerador { get; set; }
        public DbSet<RequestRegisterToken> RequestRegisterToken { get; set; }
        public DbSet<Todo> Todo { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Elemento> Elemento { get; set; }
        public DbSet<Modelo> Modelo { get; set; }
        public DbSet<Proyecto> UProyectosuario { get; set; }
        public DbSet<Revision> Revision { get; set; }
        public DbSet<TipoDiagramaMateria> TipoDiagramaMateria { get; set; }
        public DbSet<AlumnoCursoGrupo> AlumnoCursoGrupo { get; set; }
        public DbSet<ArchivoMateria> ArchivoMateria { get; set; }
        public DbSet<Clase> Clase { get; set; }
        public DbSet<ContenidoMateria> ContenidoMateria { get; set; }
        public DbSet<Contenido> Contenido { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Grupo> Grupo { get; set; }
        public DbSet<Materia> Materia { get; set; }
        public DbSet<Nota> Nota { get; set; }
        public DbSet<Unidad> Unidad { get; set; }
        public DbSet<UnidadClase> UnidadClase { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Docente> Docentes { get; set; }




        String _cs;
        public UaiCaseContext(String cs)
        {
             _cs = cs;
        }

        public void Commit()
        {
            SaveChanges();
        }

   



        public IQueryable<T> FromSQL<T>(string qry, params string[] parameters) where T : class
        {
          return  this.Set<T>().FromSql(qry, parameters);
        }


        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {

            optionsBuilder.UseNpgsql(_cs);
            //optionsBuilder.UseSqlServer(_cs);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            //TODO: Fluent configuration
            //builder.Entity<Entity>()
            //    .Property(e => e.Id)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);



            foreach (var entity in builder.Model.GetEntityTypes())
            {
               entity.Relational().TableName = entity.DisplayName();
            }
            
            base.OnModelCreating(builder);
            builder.HasPostgresExtension("uuid-ossp");
        }
    }


   
}




