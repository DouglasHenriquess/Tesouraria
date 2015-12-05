using System.Collections.Generic;
using Tesouraria.Dominio.Entidades;

namespace Tesouraria.Dominio.Interfaces.Repositorios
{
    public interface ILancamentoRepositorio : IRepositorioBase<Lancamento>
    {
        void AddLancamentos(List<Lancamento> lancamentos);
    }
}