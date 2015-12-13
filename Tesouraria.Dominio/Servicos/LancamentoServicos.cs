using System.Collections.Generic;
using Tesouraria.Dominio.Entidades;
using Tesouraria.Dominio.Interfaces.Repositorios;
using Tesouraria.Dominio.Interfaces.Servicos;

namespace Tesouraria.Dominio.Servicos
{
    public class LancamentoServicos : ServicosBase<Lancamento>, ILancamentoServicos
    {
        private readonly ILancamentoRepositorio _lancamentoRepositorio;

        public LancamentoServicos(ILancamentoRepositorio lancamentoRepositorio)
            : base(lancamentoRepositorio)
        {
            _lancamentoRepositorio = lancamentoRepositorio;
        }

        public void CadastraLancamentos(List<Lancamento> lancamentos)
        {
            _lancamentoRepositorio.CadastraLancamentos(lancamentos);
        }
    }
}