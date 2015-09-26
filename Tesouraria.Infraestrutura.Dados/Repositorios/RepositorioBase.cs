using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Tesouraria.Dominio.Interfaces.Repositorios;
using Tesouraria.Infraestrutura.Dados.Contexto;

namespace Tesouraria.Infraestrutura.Dados.Repositorios
{
    public class RepositorioBase<T> : IDisposable, IRepositorioBase<T> where T : class
    {
        protected TesourariaContexto _context = new TesourariaContexto();

        public virtual void Add(T obj)
        {
            _context.Set<T>().Add(obj);
            _context.SaveChanges();
        }

        public void Update(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void AddOrUpdate(T obj)
        {
            _context.Set<T>().AddOrUpdate(obj);
            _context.SaveChanges();
        }

        public void Remove(T obj)
        {
            _context.Set<T>().Remove(obj);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public virtual T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual IList<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
    }
}