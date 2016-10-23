using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain;
using UAI.Case.Domain.Common;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface INumeradorAppService : IAppService<Numerador>
    {
        long ProximoNumero<T>(Guid IdUsuario);
        void Reinicializar<T>(Guid IdUsuario);
    }
    public class NumeradorAppService: AppService<Numerador>, INumeradorAppService
    {
        public NumeradorAppService(IRepository<Numerador> numeradorRepository) : base(numeradorRepository)
        {
           
        }

        public long ProximoNumero<T>(Guid IdUsuario)
        {
            return Calcular(typeof(T).Name, 1, IdUsuario);
        }

        

        public void Reinicializar<T>(Guid IdUsuario)
        {
            Reinicializar<T>(null,IdUsuario);
        }



        public void Reinicializar<T>(object subIdentificador, Guid IdUsuario)
        {
            var entidad = (subIdentificador == null ? typeof(T).Name : typeof(T).Name + "_" + subIdentificador.ToString());

                var numerador = GetAll().FirstOrDefault(n => n.TipoEntidad == entidad && n.Usuario.Id.Equals(IdUsuario));

                if (numerador != null)
                {
                this.SaveOrUpdate(numerador);
                }

            
        }


        private long Calcular(string entidad, long cantidadNecesaria, Guid IdUsuario)
        {
            var numeroActual = 0L;

            //.FirstOrDefault(n => n.TipoDocumento == entidad && n.Usuario.Id == IdUsuario);
            var numerador = GetAll().FirstOrDefault(n => n.TipoEntidad == entidad && n.Usuario.Id.Equals(IdUsuario));


            if (numerador == null)
                {
                    numerador = new Numerador();
                    numerador.TipoEntidad = entidad;
                    
                }
                else
                    numeroActual = numerador.UltimoNumero;

                numerador.UltimoNumero += cantidadNecesaria;
            this.SaveOrUpdate(numerador);
            
            

            return numeroActual + 1;
        }

    }
}
