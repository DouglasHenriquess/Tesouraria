using System.Web.Mvc;

namespace Tesouraria.Apresentacao.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}