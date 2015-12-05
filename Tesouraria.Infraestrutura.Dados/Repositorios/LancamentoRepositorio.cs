using System.Collections.Generic;
using Tesouraria.Dominio.Entidades;
using Tesouraria.Dominio.Interfaces.Repositorios;
using Tesouraria.Infraestrutura.Dados.Contexto;

namespace Tesouraria.Infraestrutura.Dados.Repositorios
{
    public class LancamentoRepositorio : RepositorioBase<Lancamento>, ILancamentoRepositorio
    {
        public void AddLancamentos(List<Lancamento> objs)
        {
            using (var context = new TesourariaContexto())
            {
                foreach (var obj in objs)
                {
                    _context.Set<Lancamento>().Add(obj);
                    _context.Set<Pessoa>().Attach(obj.Pessoa);
                    _context.Set<Taxa>().Attach(obj.Taxa);
                }
                _context.SaveChanges();
            }
        }
    }
}