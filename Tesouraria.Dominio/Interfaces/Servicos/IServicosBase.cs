using System.Collections.Generic;

namespace Tesouraria.Dominio.Interfaces.Servicos
{
    public interface IServicosBase<T> where T : class
    {
        void Add(T obj);
        void Update(T obj);
        void AddOrUpdate(T obj);
        void Remove(T obj);
        void Dispose();

        T GetById(int id);
        IList<T> GetAll();
    }
}