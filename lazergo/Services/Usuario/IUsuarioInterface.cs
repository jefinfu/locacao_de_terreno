using lazergo.Dto;
using lazergo.Models;

namespace lazergo.Services.Usuario
{
    public interface IUsuarioInterface
    {
        Task<UsuarioModel>Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto);
        Task<UsuarioModel> Login(LoginDto loginDto);
    }
}
