using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
                lancamento.Pessoa = _context.Pessoas.Find(lancamento.Pessoa.PessoaId);
                lancamento.Taxa = _context.Taxas.Find(lancamento.Taxa.TaxaId);
                _context.Entry(lancamento).State = EntityState.Added;
            }
            GravaNoBancoDeDados();
        }

        public override IList<Lancamento> ObtemTodos()
        {
            AtualizaContexto();
            return _context.Set<Lancamento>().Include("Pessoa").Include("Taxa").ToList();
        }
    }
}