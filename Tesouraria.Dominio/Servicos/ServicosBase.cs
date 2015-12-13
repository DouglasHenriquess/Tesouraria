using System.Collections.Generic;
using Tesouraria.Dominio.Interfaces.Repositorios;
using Tesouraria.Dominio.Interfaces.Servicos;

namespace Tesouraria.Dominio.Servicos
{
    public class ServicosBase<T> : IServicosBase<T> where T : class
    {
        private readonly IRepositorioBase<T> _repositorioBase;

        public ServicosBase(IRepositorioBase<T> repositorioBase)
        {
            _repositorioBase = repositorioBase;
        }

        public ServicosBase()
        {
        }

        public void Salva(T obj)
        {
            _repositorioBase.Salva(obj);
        }

        public void Apaga(T obj)
        {
            _repositorioBase.Apaga(obj);
        }

        public T ObtemPorId(int id)
        {
            return _repositorioBase.ObtemPorId(id);
        }

        public IList<T> ObtemTodos()
        {
            return _repositorioBase.ObtemTodos();
        }
    }
}