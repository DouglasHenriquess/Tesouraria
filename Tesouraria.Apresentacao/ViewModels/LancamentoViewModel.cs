using System;
using System.Collections.Generic;

namespace Tesouraria.Apresentacao.ViewModels
{
    public class LancamentoViewModel
    {
        public int LancamentoId { get; set; }
        public decimal Valor { get; set; }
        public bool Pago { get; set; }
        public DateTime DataVencimento { get; set; }
        public IList<PessoaViewModel> PessoasVM { get; set; }
    }
}