using lazergo.Conexao;
using lazergo.Dto;
using lazergo.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace lazergo.Services.Usuario
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly AppDbContext _appDbContext;
        public UsuarioService(AppDbContext context)
        {
            _appDbContext = context;
        }
        public async Task<UsuarioModel> Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            try
            {
                CriarSenhaHash(usuarioCriacaoDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                var usuario = new UsuarioModel()
                {
                    Usuario = usuarioCriacaoDto.Usuario,
                    Email = usuarioCriacaoDto.Email,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt

                };
                _appDbContext.Add(usuario);
                await _appDbContext.SaveChangesAsync();

                return usuario;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using(HMACSHA512 hmac = new HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));

            }

        }

        public async Task<UsuarioModel> Login(LoginDto loginDto)
        {
            try
            {
                var usuario = await _appDbContext.usuarios.FirstOrDefaultAsync(user => user.Email == loginDto.Email);    

                if(usuario == null)
                {
                    return new UsuarioModel();
                }

                if(!VerificarSenha(loginDto.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                {
                    return new UsuarioModel();
                }

                return usuario;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool VerificarSenha(string senha, byte[] senhaHash, byte[] senhaSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512(senhaSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return computedHash.SequenceEqual(senhaHash);
            }
        }
    }
}
