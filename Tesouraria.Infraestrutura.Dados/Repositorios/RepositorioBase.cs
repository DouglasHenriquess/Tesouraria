using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Tesouraria.Dominio.Interfaces.Repositorios;
using Tesouraria.Infraestrutura.Dados.Contexto;

namespace Tesouraria.Infraestrutura.Dados.Repositorios
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : class
    {
        protected TesourariaContexto _context = new TesourariaContexto();

        public void Salva(T obj)
        {
            _context.Set<T>().AddOrUpdate(obj);
            GravaNoBancoDeDados();
        }

        public void Apaga(T obj)
        {
            _context.Set<T>().Remove(obj);
            GravaNoBancoDeDados();
        }

        public virtual T ObtemPorId(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual IList<T> ObtemTodos()
        {
            return _context.Set<T>().ToList();
        }

        protected void GravaNoBancoDeDados()
        {
            using (var transacao = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChanges();
                    transacao.Commit();
                }
                catch
                {
                    transacao.Rollback();
                }
                transacao.Dispose();
            }
        }
    }
}