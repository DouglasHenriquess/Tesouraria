using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Tesouraria.Apresentacao.ViewModels;
using Tesouraria.Dominio.Entidades;
using Tesouraria.Dominio.Interfaces.Servicos;
using Tesouraria.Dominio.Servicos.InjecaoDependencia;

namespace Tesouraria.Apresentacao.Controllers
{
    public class TaxaController : Controller
    {
        private readonly ITaxaServicos _taxaServicos = ResolvedorDependencias.Resolve<ITaxaServicos>();

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string pesquisa)
        {
            var _taxaVM = Mapper.Map<IList<Taxa>, IList<TaxaViewModel>>(_taxaServicos.GetAll()
                .Where(x =>
                    x.TaxaId.ToString().Contains(pesquisa) ||
                    (x.Nome != null && x.Nome.Contains(pesquisa)) ||
                    (x.Valor != null && x.Valor.ToString().Contains(pesquisa)))
                    .ToList());

            return View(_taxaVM);
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TaxaViewModel taxaViewModel)
        {
            if (ModelState.IsValid)
            {
                var _taxa = Mapper.Map<TaxaViewModel, Taxa>(taxaViewModel);
                _taxaServicos.AddOrUpdate(_taxa);
                return RedirectToAction("Index");
            }

            return View(taxaViewModel);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int taxaId)
        {
            var _taxa = _taxaServicos.GetById(taxaId);
            var _taxaVM = Mapper.Map<Taxa, TaxaViewModel>(_taxa);

            return View(_taxaVM);
        }

        [HttpPost]
        public ActionResult Edit(TaxaViewModel taxaViewModel)
        {
            if (ModelState.IsValid)
            {
                var _taxa = Mapper.Map<TaxaViewModel, Taxa>(taxaViewModel);
                _taxaServicos.AddOrUpdate(_taxa);
                return RedirectToAction("Index");
            }

            return View(taxaViewModel);
        }
        #endregion
    }
}