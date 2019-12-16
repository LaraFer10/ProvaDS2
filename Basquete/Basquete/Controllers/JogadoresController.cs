using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Basquete.Models;
using Basquete.Services;
using Jose;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Basquete.Controllers
{
    public class JogadoresController : Controller
    {
        private JogadorService _jogadorService;
        public JogadoresController(JogadorService jogadorService)
        {
            _jogadorService = jogadorService;
        }


        public async Task<IActionResult> Index()
        {
            var list = await _jogadorService.EncontrarTodos();
            return View(list);
        }


        public IActionResult Informacoes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID Placar não foi encontrado" });
            }

            var jogador = _jogadorService.EncontrarPorIdAsync(id.Value);
            if (jogador == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Jogador não foi encontrado" });
            }

            return View(jogador);
        }


        public IActionResult Cadastrar()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(Jogador jogador)
        {
            if (ModelState.IsValid)
            {
                await _jogadorService.InsertAsync(jogador);
                return RedirectToAction(nameof(Index));
            }
            return View(jogador);
        }


        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID do Jogador não foi encontrado" });
            }

            var jogador = await _jogadorService.EncontrarPorIdAsync(id.Value);
            if (jogador == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Jogador não foi encontrado" });
            }
            return View(jogador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Jogador jogador)
        {
            if (id != jogador.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não corresponde" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _jogadorService.Update(jogador);
                }
                catch (ApplicationException e)
                {
                    return RedirectToAction(nameof(Error), new { message = e.Message });
                }
                return RedirectToAction(nameof(Index));
            }
            return View(jogador);
        }

        public async Task<IActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID do Jogador não foi encontrado" });
            }

            var jogador = await _jogadorService.EncontrarPorIdAsync(id.Value);
            if (jogador == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Jogador não foi encontrado" });
            }

            return View(jogador);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _jogadorService.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }


    }
}