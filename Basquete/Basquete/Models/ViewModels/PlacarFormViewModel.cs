using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basquete.Models.ViewModels
{
    public class PlacarFormViewModel
    {
        public Placar Placar { get; set; }
        public ICollection<Jogador> Jogadores { get; set; }
    }
}
