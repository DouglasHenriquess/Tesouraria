using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using Tesouraria.Dominio.Interfaces.Repositorios;
using Tesouraria.Infraestrutura.Dados.Contexto;

namespace Tesouraria.Infraestrutura.Dados.Repositorios
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : class
    {
        protected TesourariaContexto _context = new TesourariaContexto();

        public virtual void Salva(T obj)
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
            AtualizaContexto();
            return _context.Set<T>().Find(id);
        }

        public virtual IList<T> ObtemTodos()
        {
            AtualizaContexto();
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
            }
        }

        protected void AtualizaContexto()
        {
            var objetosAlterados = _context.ChangeTracker.Entries().Select(x => x.Entity).ToList();
            ((IObjectContextAdapter)_context).ObjectContext.Refresh(RefreshMode.StoreWins, objetosAlterados);
        }
    }
}