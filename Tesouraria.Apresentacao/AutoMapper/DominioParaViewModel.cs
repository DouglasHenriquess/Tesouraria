using AutoMapper;
using Tesouraria.Apresentacao.ViewModels;
using Tesouraria.Dominio.Entidades;

namespace Tesouraria.Apresentacao.AutoMapper
{
    public class DominioParaViewModel : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "ViewModelParaDominio";
            }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<PessoaViewModel, Pessoa>();
            Mapper.CreateMap<TaxaViewModel, Taxa>();
            Mapper.CreateMap<LancamentoViewModel, Lancamento>();
            Mapper.CreateMap<CaixaViewModel, Caixa>();
        }
    }
}