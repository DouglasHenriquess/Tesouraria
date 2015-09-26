using Tesouraria.Dominio.Entidades;
using Tesouraria.Dominio.Interfaces.Repositorios;
using Tesouraria.Dominio.Interfaces.Servicos;

namespace Tesouraria.Dominio.Servicos
{
    public class TaxaServicos : ServicosBase<Taxa>, ITaxaServicos
    {
        private readonly ITaxaRepositorio _taxaRepositorio;

        public TaxaServicos(ITaxaRepositorio taxaRepositorio)
            : base(taxaRepositorio)
        {
            _taxaRepositorio = taxaRepositorio;
        }
    }
}