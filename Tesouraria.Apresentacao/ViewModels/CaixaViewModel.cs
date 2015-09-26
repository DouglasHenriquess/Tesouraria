using System.Collections.Generic;
using Tesouraria.Dominio.Entidades;

namespace Tesouraria.Apresentacao.ViewModels
{
    public class CaixaViewModel
    {
        public int CaixaId { get; set; }
        public decimal Valor { get; set; }
        public Pessoa Pessoa { get; set; }
        public IList<PessoaViewModel> PessoasVM { get; set; }
        public IList<TaxaViewModel> TaxasVM { get; set; }
    }
}