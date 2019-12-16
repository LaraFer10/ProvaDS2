using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Basquete.Models;
using Basquete.Models.ViewModels;
using Basquete.Services;
using Jose;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Basquete.Controllers
{
    public class PlacarsController : Controller
    {
        private PlacarService _placarService;
        private JogadorService _jogadorService;
        public PlacarsController(PlacarService placarService, JogadorService jogadorService)
        {
            _placarService = placarService;
            _jogadorService = jogadorService;
        }

        public IActionResult Index()
        {
            var list = _placarService.EncontrarTodos();
            return View(list);
        }


        public IActionResult Informacoes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID da Placar não foi encontrado" });
            }

            var placar = _placarService.EncontrarPorIdAsync(id.Value);
            if (placar == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Placar não foi encontrado" });
            }

            return View(placar);
        }

        public async Task<IActionResult> Cadastrar()
        {
            var jogadores = await _jogadorService.EncontrarTodos();
            var viewModel = new PlacarFormViewModel()
            {
                Jogadores = jogadores
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(Placar placar)
        {
            if (ModelState.IsValid)
            {
                await _placarService.InsertAsync(placar);
                return RedirectToAction(nameof(Index));
            }
            var jogadores = await _jogadorService.EncontrarTodos();
            PlacarFormViewModel viewModel = new PlacarFormViewModel()
            {
                Jogadores = jogadores,
                Placar = placar
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Top10()
        {
            var lista = await _placarService.Top10();
            List<Placar> top10 = new List<Placar>();
            for (int i = 0; i < 10; i++)
            {
                top10.Add(lista[i]);
            }
            return View(top10);
        }


        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID da Placar não foi encontrado" });
            }

            var placar = await _placarService.EncontrarPorIdAsync(id.Value);
            if (placar == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Placar não foi encontrado" });
            }
            var jogadores = await _jogadorService.EncontrarTodos();
            PlacarFormViewModel viewModel = new PlacarFormViewModel()
            {
                Jogadores = jogadores,
                Placar = placar
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Placar placar)
        {
            if (id != placar.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "ID não corresponde" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _placarService.Update(placar);
                }
                catch (ApplicationException e)
                {
                    return RedirectToAction(nameof(Error), new { message = e.Message });
                }
                return RedirectToAction(nameof(Index));
            }
            var jogadores = await _jogadorService.EncontrarTodos();
            PlacarFormViewModel viewModel = new PlacarFormViewModel()
            {
                Jogadores = jogadores,
                Placar = placar
            };
            return View(viewModel);

        }


        public async Task<IActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "ID do Placar não foi encontrado" });
            }

            var placar = await _placarService.EncontrarPorIdAsync(id.Value);
            if (placar == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Placar não foi encontrado" });
            }

            return View(placar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _placarService.Remove(id);
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