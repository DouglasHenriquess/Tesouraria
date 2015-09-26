using System;
using System.Collections.Generic;
using Tesouraria.Dominio.Interfaces.Repositorios;
using Tesouraria.Dominio.Interfaces.Servicos;

namespace Tesouraria.Dominio.Servicos
{
    public class ServicosBase<T> : IDisposable, IServicosBase<T> where T : class
    {
        private readonly IRepositorioBase<T> _repositorioBase;

        public ServicosBase(IRepositorioBase<T> repositorioBase)
        {
            _repositorioBase = repositorioBase;
        }

        public ServicosBase()
        {
        }

        public void Add(T obj)
        {
            _repositorioBase.Add(obj);
        }

        public void Update(T obj)
        {
            _repositorioBase.Update(obj);
        }

        public void AddOrUpdate(T obj)
        {
            _repositorioBase.AddOrUpdate(obj);
        }

        public void Remove(T obj)
        {
            _repositorioBase.Remove(obj);
        }

        public void Dispose()
        {
            _repositorioBase.Dispose();
        }

        public T GetById(int id)
        {
            return _repositorioBase.GetById(id);
        }

        public IList<T> GetAll()
        {
            return _repositorioBase.GetAll();
        }
    }
}