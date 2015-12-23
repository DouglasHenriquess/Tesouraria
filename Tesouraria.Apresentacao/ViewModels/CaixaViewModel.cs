using System.Collections.Generic;
using Tesouraria.Dominio.Entidades;

namespace Tesouraria.Apresentacao.ViewModels
{
    public class CaixaViewModel
    {
        public int CaixaId { get; set; }
        public decimal Valor { get; set; }
        public IList<Lancamento> Lancamentos { get; set; }
    }
}