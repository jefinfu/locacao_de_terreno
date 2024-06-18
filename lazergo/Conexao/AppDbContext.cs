using lazergo.Models;
using Microsoft.EntityFrameworkCore;

namespace lazergo.Conexao
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<AgendaModel> agendaModels { get; set; }
        public DbSet<UsuarioModel> usuarios { get; set; }
        public DbSet<ClienteModel> clientes { get; set; }
    }
}
