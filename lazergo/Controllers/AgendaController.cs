//Aqui vai controlar as movimentação das telas.

using lazergo.Dto;
using lazergo.Filtros;
using lazergo.Models;
using lazergo.Services.Agenda;
using Microsoft.AspNetCore.Mvc;


namespace lazergo.Controllers
{
    [UsuarioLogado]
    public class AgendaController : Controller
    {
        private readonly IAgendaInterface _agendaInterface;
        public AgendaController(IAgendaInterface agendaInterface) 
        { 
            _agendaInterface = agendaInterface;
        }
        public async Task<IActionResult> Index()
        {
            var agendaModels = await _agendaInterface.GetAgendas();
            return View(agendaModels);
        }

        public IActionResult Cadastrar() 
        {
            return View();
        }

        public async Task<IActionResult>Editar(int id)
        {
            var agenda = await _agendaInterface.GetAgendaPorId(id);
            return View(agenda);
        }

        public async Task<IActionResult>Deletar(int id)
        {
            var agenda = await _agendaInterface.DeletarAgenda(id);
            return RedirectToAction("Index", "Agenda");
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(AgendaCriacaoDto agendaCriacaoDto, List<IFormFile> fotos)
        {
            if (ModelState.IsValid)
            {
                var agenda = await _agendaInterface.CriarAgenda(agendaCriacaoDto, fotos);
                return RedirectToAction("Index", "Agenda");
            }
            else
            {
                return View(agendaCriacaoDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(AgendaModel agendaModel, List<IFormFile>? fotos)
        {
            if (ModelState.IsValid)
            {
                var agenda = await _agendaInterface.EditarAgenda(agendaModel, fotos);
                return RedirectToAction("Index", "Agenda");
            }
            else
            {
                return View(agendaModel);
            }
        }
    }
}
