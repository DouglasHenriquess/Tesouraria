using System;
using Microsoft.Practices.Unity;
using Tesouraria.Dominio.Interfaces.Repositorios;
using Tesouraria.Dominio.Interfaces.Servicos;
using Tesouraria.Dominio.Interfaces.Servicos.InjecaoDependencia;
using Tesouraria.Dominio.Servicos;
using Tesouraria.Infraestrutura.Dados.Repositorios;

namespace Tesouraria.Infraestrutura.InjecaoDependencia
{
    public class ResolvedorDependenciasUnity : IResolvedorDependencias
    {
        private static ResolvedorDependenciasUnity _resolvedorDependencias;
        private readonly IUnityContainer _unityContainer;

        private ResolvedorDependenciasUnity()
        {
            _unityContainer = new UnityContainer();
            MapeiaDependencias();
        }

        public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }

        public T Resolve<T>(Type type)
        {
            return (T)_unityContainer.Resolve(type);
        }

        public static ResolvedorDependenciasUnity Instancia()
        {
            return _resolvedorDependencias ?? (_resolvedorDependencias ?? new ResolvedorDependenciasUnity());
        }

        private void MapeiaDependencias()
        {
            _unityContainer
                .RegisterType<IPessoaRepositorio, PessoaRepositorio>(new InjectionConstructor())
                .RegisterType<ITaxaRepositorio, TaxaRepositorio>(new InjectionConstructor())
                .RegisterType<ICaixaRepositorio, CaixaRepositorio>(new InjectionConstructor())
                .RegisterType<ILancamentoRepositorio, LancamentoRepositorio>(new InjectionConstructor())

                .RegisterType<IPessoaServicos, PessoaServicos>(new InjectionConstructor(_unityContainer.Resolve<IPessoaRepositorio>()))
                .RegisterType<ITaxaServicos, TaxaServicos>(new InjectionConstructor(_unityContainer.Resolve<ITaxaRepositorio>()))
                .RegisterType<ICaixaServicos, CaixaServicos>(new InjectionConstructor(_unityContainer.Resolve<ICaixaRepositorio>()))
                .RegisterType<ILancamentoServicos, LancamentoServicos>(new InjectionConstructor(_unityContainer.Resolve<ILancamentoRepositorio>()))
                ;
        }
    }
}