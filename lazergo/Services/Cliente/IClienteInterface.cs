using lazergo.Dto;
using lazergo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lazergo.Services.Cliente
{
    public interface IClienteInterface
    {
        Task<ClienteModel> CriarCliente(ClienteCriacaoDto clienteCriacaoDto);
        Task<List<ClienteModel>> GetClientes();
        Task<ClienteModel> GetClientePorId(int id);
        Task<ClienteModel> EditarCliente(ClienteModel clienteModel);
        Task<ClienteModel> DeletarCliente(int id);
        bool ClienteModelExists(int id);
    }
}
