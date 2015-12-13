using System.Collections.Generic;

namespace Tesouraria.Dominio.Entidades
{
    public class Caixa
    {
        public int CaixaId { get; set; }
        public decimal Valor { get; set; }
        public IList<Lancamento> Lancamentos { get; set; }
    }
}