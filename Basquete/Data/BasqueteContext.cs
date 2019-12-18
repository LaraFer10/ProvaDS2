using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Basquete.Models;

namespace Basquete.Models
{
    public class BasqueteContext : DbContext
    {
        public BasqueteContext (DbContextOptions<BasqueteContext> options)
            : base(options)
        {
        }

        public DbSet<Basquete.Models.Jogador> Jogador { get; set; }

        public DbSet<Basquete.Models.Placar> Placar { get; set; }
    }
}
