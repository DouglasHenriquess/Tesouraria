using System;
using Tesouraria.Dominio.Entidades;

namespace Tesouraria.Apresentacao.ViewModels
{
    public class LancamentoViewModel
    {
        public int LancamentoId { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool Pago { get; set; }
        public bool Cancelado { get; set; }
        public Pessoa Pessoa { get; set; }
        public Taxa Taxa { get; set; }
    }
}