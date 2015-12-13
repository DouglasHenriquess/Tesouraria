using System.Collections.Generic;

namespace Tesouraria.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioBase<T> where T : class
    {
        void Salva(T obj);
        void Apaga(T obj);
        T ObtemPorId(int id);
        IList<T> ObtemTodos();
    }
}