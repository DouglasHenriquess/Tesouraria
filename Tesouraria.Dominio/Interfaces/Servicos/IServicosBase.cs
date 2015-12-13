using System.Collections.Generic;

namespace Tesouraria.Dominio.Interfaces.Servicos
{
    public interface IServicosBase<T> where T : class
    {
        void Salva(T obj);
        void Apaga(T obj);
        T ObtemPorId(int id);
        IList<T> ObtemTodos();
    }
}