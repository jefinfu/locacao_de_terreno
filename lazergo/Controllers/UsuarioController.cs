﻿using lazergo.Dto;
using lazergo.Filtros;
using lazergo.Models;
using lazergo.Services.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace lazergo.Controllers
{
    
    public class UsuarioController : Controller
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioModel>>Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            if(ModelState.IsValid)
            {
                var usuario = await _usuarioInterface.Cadastrar(usuarioCriacaoDto);

                if (usuario != null)
                {
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["MensagemErro"] = "Ocorreu um erro no momento do Cadastro";
                    return View(usuarioCriacaoDto);
                }

            }
            else
            {
                return View(usuarioCriacaoDto);
            }
        }
    }

}
