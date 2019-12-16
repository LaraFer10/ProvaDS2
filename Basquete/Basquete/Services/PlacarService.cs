using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Basquete.Models;
using Jose;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;

namespace Basquete.Services
{
    public class PlacarService
    {
        private BasqueteContext _context;
        public PlacarService(BasqueteContext context)
        {
            _context = context;
        }
        public List<Placar> EncontrarTodos()
        {
            return _context.Placar.Include(p => p.Jogador).ToList();
        }
        public async Task<Placar> EncontrarPorIdAsync(int id)
        {
            return await _context.Placar
                .Include(p => p.Jogador)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<List<Placar>> Top10()
        {
            return await _context.Placar.OrderByDescending(obj => obj.TotalPontos).ToListAsync();
        }
        public async Task InsertAsync(Placar placar)
        {
            _context.Add(placar);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Placar placar)
        {
            bool hasAny = await _context.Placar.AnyAsync(x => x.Id == placar.Id);
            if (!hasAny)
            {
                throw new NotFoundException("ID da Placa não foi encontrado");
            }
            try
            {
                _context.Update(placar);
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
                var obj = await _context.Placar.FindAsync(id);
                _context.Placar.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DBConcurrencyException e)
            {
                throw new IntegrityException(e.Message);
            }
        }
    }
}