using System;
using System.Linq;
using UAI.Case.Application;
using UAI.Case.Domain.Common;
using UAI.Case.Repositories;
using UAI.Case.Security;
using UAI.Case.Utilities;

namespace UAI.Case.Application
{
    public interface IUsuarioAppService : IAppService<Usuario>
    {
        Usuario GetByUsernamePassword(string username, string password);
        Usuario SendActivationRequest(Usuario usuario, string currentUrl, Guid IdUsuarioSolicitud,IRegistrosMailAccount mail);

    }
    public class UsuarioAppService : AppService<Usuario>, IUsuarioAppService
    {

        public Usuario SendActivationRequest(Usuario usuario, String currentUrl, Guid IdUsuarioSolicitud, IRegistrosMailAccount mail)
        {
            if (usuario != null)
            {
                string newPassword = Guid.NewGuid().ToString().Split('-')[0];


                usuario.Password = newPassword;
                usuario.Active = false;
                usuario.RegisterToken = Security.RegisterHandler.GenerateRegisterToken(usuario.Id);
                usuario.IdRegisterRequestUserId = IdUsuarioSolicitud;
                Mailer.SendActivateRequest(usuario, currentUrl,mail);
                SaveOrUpdate(usuario);




            }
            return usuario;
        }


        
        public override void OnBeforeSaveOrUpdate(Usuario entity)
        {
        }

        
        public UsuarioAppService(IRepository<Usuario> usuarioRepository) : base(usuarioRepository)
        {
            
        }

        public Usuario GetByUsernamePassword(string mail, string password)
        {
            string passwordHashed = Security.Cryptography.MD5Hash(password);

            return GetAll().Where(p => p.Mail == mail && p.Password.Equals(passwordHashed)).FirstOrDefault();
        }

        
    }
}
