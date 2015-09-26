using System;
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
    public class CaixaController : Controller
    {
        private readonly ICaixaServicos _caixaServicos = ResolvedorDependencias.Resolve<ICaixaServicos>();
        private readonly IPessoaServicos _pessoaServicos = ResolvedorDependencias.Resolve<IPessoaServicos>();
        private readonly ITaxaServicos _taxaServicos = ResolvedorDependencias.Resolve<ITaxaServicos>();

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string pesquisa)
        {
            var _caixaVM = Mapper.Map<IList<Caixa>, IList<CaixaViewModel>>(_caixaServicos.GetAll()
                .Where(x =>
                    x.CaixaId.ToString().Contains(pesquisa) ||
                    x.Pessoa.Nome.Contains(pesquisa) ||
                    x.Valor.ToString().Contains(pesquisa))
                    .ToList());

            return View(_caixaVM);
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            var _caixaVM = new CaixaViewModel
            {
                TaxasVM = Mapper.Map<IList<Taxa>, IList<TaxaViewModel>>(_taxaServicos.GetAll()),
                PessoasVM = Mapper.Map<IList<Pessoa>, IList<PessoaViewModel>>(_pessoaServicos.GetAll().OrderBy(x => x.Nome).ToList())
            };

            return View(_caixaVM);
        }

        [HttpPost]
        public ActionResult Create(CaixaViewModel caixaViewModel)
        {
            if (caixaViewModel.PessoasVM.Where(x => x.Selecionado).Count() > 1)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Selecione somente uma pessoa para o pagamento!'); window.location.href = '/Caixa/Index';</script>");
                RedirectToAction("Index");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var _caixa = Mapper.Map<CaixaViewModel, Caixa>(caixaViewModel);
                    _caixa.Pessoa = Mapper.Map<PessoaViewModel, Pessoa>(caixaViewModel.PessoasVM.Where(x => x.Selecionado).First());
                    _caixa.Taxas = Mapper.Map<IList<TaxaViewModel>, IList<Taxa>>(caixaViewModel.TaxasVM.Where(x => x.Selecionado).ToList());

                    _caixaServicos.Add(_caixa);
                    return RedirectToAction("Index");
                }
                return View(caixaViewModel);
            }
        }
        #endregion

        #region Details
        public ActionResult Details(int caixaId)
        {
            var _caixa = _caixaServicos.GetById(caixaId);
            var _caixaVM = Mapper.Map<Caixa, CaixaViewModel>(_caixa);
            _caixaVM.TaxasVM = Mapper.Map<IList<Taxa>, IList<TaxaViewModel>>(_caixa.Taxas.ToList());

            return View(_caixaVM);
        }
        #endregion
    }
}