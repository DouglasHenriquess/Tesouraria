using System;
using Tesouraria.Dominio.Interfaces.Servicos.InjecaoDependencia;

namespace Tesouraria.Dominio.Servicos.InjecaoDependencia
{
    public static class ResolvedorDependencias
    {
        private static IResolvedorDependencias _resolvedorDependencias;

        public static void Inicializa(IResolvedorDependencias resolvedorDependencias)
        {
            _resolvedorDependencias = resolvedorDependencias;
        }

        public static T Resolve<T>()
        {
            return _resolvedorDependencias.Resolve<T>();
        }

        public static T Resolve<T>(Type type)
        {
            return _resolvedorDependencias.Resolve<T>(type);
        }
    }
}