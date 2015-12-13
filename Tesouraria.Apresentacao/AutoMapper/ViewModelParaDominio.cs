using AutoMapper;
using Tesouraria.Apresentacao.ViewModels;
using Tesouraria.Dominio.Entidades;

namespace Tesouraria.Apresentacao.AutoMapper
{
    public class ViewModelParaDominio : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "DominioParaViewModel";
            }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Pessoa, PessoaViewModel>();
            Mapper.CreateMap<Taxa, TaxaViewModel>();
            Mapper.CreateMap<Lancamento, LancamentoViewModel>();
        }
    }
}