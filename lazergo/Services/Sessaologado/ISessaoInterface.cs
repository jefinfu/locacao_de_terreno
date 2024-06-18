using lazergo.Models;

namespace lazergo.Services.Sessaologado
{
    public interface ISessaoInterface
    {
        UsuarioModel BuscarSessao();

        void CriarSessao(UsuarioModel usuario);
        void RemoverSessao();
    }
}
