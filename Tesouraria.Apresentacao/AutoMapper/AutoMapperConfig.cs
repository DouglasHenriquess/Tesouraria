using AutoMapper;

namespace Tesouraria.Apresentacao.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void RegistraMapeamentos()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DominioParaViewModel>();
                x.AddProfile<ViewModelParaDominio>();
            });
        }
    }
}