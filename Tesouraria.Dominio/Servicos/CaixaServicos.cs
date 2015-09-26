using Tesouraria.Dominio.Entidades;
using Tesouraria.Dominio.Interfaces.Repositorios;
using Tesouraria.Dominio.Interfaces.Servicos;

namespace Tesouraria.Dominio.Servicos
{
    public class CaixaServicos : ServicosBase<Caixa>, ICaixaServicos
    {
        private readonly ICaixaRepositorio _caixaRepositorio;

        public CaixaServicos(ICaixaRepositorio caixaRepositorio)
            : base(caixaRepositorio)
        {
            _caixaRepositorio = caixaRepositorio;
        }
    }
}