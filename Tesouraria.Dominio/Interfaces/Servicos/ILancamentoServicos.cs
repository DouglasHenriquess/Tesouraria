using System.Collections.Generic;
using Tesouraria.Dominio.Entidades;

namespace Tesouraria.Dominio.Interfaces.Servicos
{
    public interface ILancamentoServicos : IServicosBase<Lancamento>
    {
        void AddLancamentos(List<Lancamento> lancamentos);
    }
}