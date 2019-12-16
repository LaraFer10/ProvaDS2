using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Basquete.Models
{
    public class Placar
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        public int JogadorId { get; set; }
        public Jogador Jogador { get; set; }
        [Display(Name = "Total de Pontos")]
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        [Range(1, 100000, ErrorMessage = "O campo {0} deve ser entre {1} e {2}")]
        public int TotalPontos { get; set; }
        [Display(Name = "Data da Pontuação")]
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        public DateTime DataPontuacao { get; set; }

        public Placar(int id, int jogadorId, Jogador jogador, int totalPontos, DateTime dataPontuacao)
        {
            Id = id;
            JogadorId = jogadorId;
            Jogador = jogador;
            TotalPontos = totalPontos;
            DataPontuacao = dataPontuacao;
        }

        public Placar()
        {
        }
    }
}
