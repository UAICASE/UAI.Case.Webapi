using UAI.Case.Domain.Common;
using UAI.Case.Domain.Enums;

namespace UAI.Case.Domain.Academico
{
    public class Contenido : Entity
    {
        public virtual string Descripcion { get; set; }
        public virtual Archivo Archivo {get; set;}
        public virtual TipoContenido TipoContenido { get; set; }
    }
}