using Basquete.Models;
using Jose;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Basquete.Services
{
    public class JogadorService
    {
        private BasqueteContext _context;

        public JogadorService(BasqueteContext context)
        {
            _context = context;
        }
        public async Task<List<Jogador>> EncontrarTodos()
        {
            return await _context.Jogador.ToListAsync();
        }
        public async Task<Jogador> EncontrarPorIdAsync(int id)
        {
            return await _context.Jogador
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task InsertAsync(Jogador jogador)
        {
            _context.Add(jogador);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Jogador jogador)
        {
            bool hasAny = await _context.Jogador.AnyAsync(x => x.Id == jogador.Id);
            if (!hasAny)
            {
                throw new NotFoundException("ID do Jogador não foi encontrado");
            }
            try
            {
                _context.Update(jogador);
                await _context.SaveChangesAsync();
            }
            catch (DBConcurrencyException e)
            {
                throw new DBConcurrencyException(e.Message);
            }
        }
        public async Task Remove(int id)
        {
            try
            {
                var obj = await _context.Jogador.FindAsync(id);
                _context.Jogador.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DBConcurrencyException e)
            {
                throw new IntegrityException(e.Message);
            }
        }
    }
}
