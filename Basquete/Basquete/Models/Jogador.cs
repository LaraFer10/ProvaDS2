using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Basquete.Models
{
    public class Jogador
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "{0} é um campo obrigatório.")]
        public int Idade { get; set; }
        public string Nacionalidade { get; set; }

        public Jogador(int id, string nome, int idade, string nacionalidade)
        {
            Id = id;
            Nome = nome;
            Idade = idade;
            Nacionalidade = nacionalidade;
        }

        public Jogador()
        {
        }
    }
}
