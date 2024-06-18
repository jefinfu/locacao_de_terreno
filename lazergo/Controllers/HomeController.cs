using lazergo.Dto;
using lazergo.Models;
using lazergo.Services.Agenda;
using lazergo.Services.Sessaologado;
using lazergo.Services.Usuario;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace lazergo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAgendaInterface _agendaInterface;
        private readonly IUsuarioInterface _usuarioInterface;
        private readonly ISessaoInterface _sessaoInterface;

        public HomeController(IAgendaInterface agendaInterface, IUsuarioInterface usuarioInterface, ISessaoInterface sessaoInterface)
        {
            _agendaInterface = agendaInterface;
            _usuarioInterface = usuarioInterface;
            _sessaoInterface = sessaoInterface;
        }

        public async Task<IActionResult> Index(string? pesquisar)
        {
            if(pesquisar == null)
            {
                var agendaModels = await _agendaInterface.GetAgendas();
                return View(agendaModels);
            }
            else
            {
                var agendaModels = await _agendaInterface.GetAgendasFiltro(pesquisar);
                return View(agendaModels);

            }
            
        }

        public IActionResult Login()
        { 
            return View();
        }


        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _usuarioInterface.Login(loginDto);

                if (usuario.Id == 0)
                {
                    TempData["MensagemErro"] = "Dados Inválidos!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["MensagemSucesso"] = "Usuário logado com sucesso!";
                    _sessaoInterface.CriarSessao(usuario);
                    HttpContext.Session.SetString("UsuarioNome", usuario.Usuario);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["MensagemErro"] = "Preencha todos os campos corretamente.";
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Sair()
        {
            _sessaoInterface.RemoverSessao();
            HttpContext.Session.Remove("UsuarioNome");
            return RedirectToAction("Index", "Home");
        }




    }
}
