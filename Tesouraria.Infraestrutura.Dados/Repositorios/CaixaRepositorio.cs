using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Tesouraria.Dominio.Entidades;
using Tesouraria.Dominio.Interfaces.Repositorios;

namespace Tesouraria.Infraestrutura.Dados.Repositorios
{
    public class CaixaRepositorio : RepositorioBase<Caixa>, ICaixaRepositorio
    {
        public override void Salva(Caixa caixa)
        {
            caixa.Lancamentos.ToList().ForEach(x => _context.Entry(x).State = EntityState.Modified);
            caixa.Pessoa = _context.Pessoas.Find(caixa.Pessoa.PessoaId);
            _context.Entry(caixa).State = EntityState.Added;
            GravaNoBancoDeDados();
        }

        public override IList<Caixa> ObtemTodos()
        {
            AtualizaContexto();
            return _context.Set<Caixa>().Include("Pessoa").ToList();
        }
    }
}