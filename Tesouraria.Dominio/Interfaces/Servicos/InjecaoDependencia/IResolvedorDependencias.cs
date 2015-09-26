using System;

namespace Tesouraria.Dominio.Interfaces.Servicos.InjecaoDependencia
{
    public interface IResolvedorDependencias
    {
        T Resolve<T>();
        T Resolve<T>(Type type);
    }
}