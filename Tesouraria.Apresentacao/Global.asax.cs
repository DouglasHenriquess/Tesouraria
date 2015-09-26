using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Tesouraria.Apresentacao.AutoMapper;
using Tesouraria.Dominio.Servicos.InjecaoDependencia;

namespace Tesouraria.Apresentacao
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ResolvedorDependencias.Inicializa(Tesouraria.Infraestrutura.InjecaoDependencia.ResolvedorDependenciasUnity.Instancia());
            AutoMapperConfig.RegistraMapeamentos();

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}