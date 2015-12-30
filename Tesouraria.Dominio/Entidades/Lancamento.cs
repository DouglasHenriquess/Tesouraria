using System;

namespace Tesouraria.Dominio.Entidades
{
    public class Lancamento
    {
        public int LancamentoId { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool Pago { get; set; }
        public Pessoa Pessoa { get; set; }
        public Taxa Taxa { get; set; }
    }
}