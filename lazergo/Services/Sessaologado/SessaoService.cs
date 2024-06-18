using lazergo.Models;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace lazergo.Services.Sessaologado
{
    public class SessaoService : ISessaoInterface
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public SessaoService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void CriarSessao(UsuarioModel usuario)
        {
            string usuarioString = JsonConvert.SerializeObject(usuario);
            _contextAccessor.HttpContext.Session.SetString("UsuarioAtivo", usuarioString);
            _contextAccessor.HttpContext.Session.SetString("UsuarioNome", usuario.Usuario);
        }

        public void RemoverSessao()
        {
            _contextAccessor.HttpContext.Session.Remove("UsuarioAtivo");
            _contextAccessor.HttpContext.Session.Remove("UsuarioNome");
        }

        public UsuarioModel BuscarSessao()
        {
            string usuarioString = _contextAccessor.HttpContext.Session.GetString("UsuarioAtivo");
            if (string.IsNullOrEmpty(usuarioString)) return null;
            return JsonConvert.DeserializeObject<UsuarioModel>(usuarioString);
        }
    }


}
