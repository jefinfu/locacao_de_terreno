using lazergo.Conexao;
using lazergo.Dto;
using lazergo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lazergo.Services.Cliente
{
    public class ClienteService : IClienteInterface
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ClienteModel> CriarCliente(ClienteCriacaoDto clienteCriacaoDto)
        {
            try
            {
                var cliente = new ClienteModel
                {
                    // Criação do cliente
                };

                _context.Add(cliente);
                await _context.SaveChangesAsync();

                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ClienteModel>> GetClientes()
        {
            return await _context.clientes.ToListAsync();
        }

        public async Task<ClienteModel> GetClientePorId(int id)
        {
            return await _context.clientes.FindAsync(id);
        }

        public async Task<ClienteModel> EditarCliente(ClienteModel clienteModel)
        {
            _context.Update(clienteModel);
            await _context.SaveChangesAsync();

            return clienteModel;
        }

        public async Task<ClienteModel> DeletarCliente(int id)
        {
            var cliente = await _context.clientes.FindAsync(id);
            _context.clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return cliente;
        }

        public bool ClienteModelExists(int id)
        {
            return _context.clientes.Any(e => e.Id == id);
        }
    }
}
