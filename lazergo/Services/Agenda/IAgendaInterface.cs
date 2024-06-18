//aqui foi criado uma classe do tipo interface
//o controller vai chamar essa tela e essa tela vai chamar o agendaservice

using lazergo.Dto;
using lazergo.Models;

namespace lazergo.Services.Agenda
{
    public interface IAgendaInterface
    {
        Task<AgendaModel> CriarAgenda(AgendaCriacaoDto agendaCriacaoDto, List<IFormFile> fotos);
        Task<List<AgendaModel>> GetAgendas();
        Task<AgendaModel> GetAgendaPorId(int id);
        Task<AgendaModel> EditarAgenda(AgendaModel agendaModel, List<IFormFile>? fotos);
        Task<AgendaModel> DeletarAgenda (int id);
        Task<List<AgendaModel>> GetAgendasFiltro(string? pesquisar);
    }
}
