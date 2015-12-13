using System.Collections.Generic;
using Tesouraria.Dominio.Entidades;
using Tesouraria.Dominio.Interfaces.Repositorios;

namespace Tesouraria.Infraestrutura.Dados.Repositorios
{
    public class LancamentoRepositorio : RepositorioBase<Lancamento>, ILancamentoRepositorio
    {
        public void CadastraLancamentos(List<Lancamento> lancamentos)
        {
            foreach (var lancamento in lancamentos)
            {
                _context.Set<Lancamento>().Add(lancamento);
                _context.Set<Pessoa>().Attach(lancamento.Pessoa);
                _context.Set<Taxa>().Attach(lancamento.Taxa);
            }
            GravaNoBancoDeDados();
        }
    }
}