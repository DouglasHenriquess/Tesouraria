using System.Collections.Generic;

namespace Tesouraria.Dominio.Entidades
{
    public class Caixa
    {
        public int CaixaId { get; set; }
        public decimal Valor { get; set; }
        public Pessoa Pessoa { get; set; }
        public IList<Taxa> Taxas { get; set; }
    }
}