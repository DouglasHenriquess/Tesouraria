using Tesouraria.Dominio.Entidades;
using Tesouraria.Dominio.Interfaces.Repositorios;
using Tesouraria.Dominio.Interfaces.Servicos;

namespace Tesouraria.Dominio.Servicos
{
    public class PessoaServicos : ServicosBase<Pessoa>, IPessoaServicos
    {
        private readonly IPessoaRepositorio _pessoaRepositorio;

        public PessoaServicos(IPessoaRepositorio pessoaRepositorio)
            : base(pessoaRepositorio)
        {
            _pessoaRepositorio = pessoaRepositorio;
        }
    }
}