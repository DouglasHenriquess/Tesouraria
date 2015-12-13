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
    public class LancamentoController : Controller
    {
        private readonly IPessoaServicos _pessoaServicos = ResolvedorDependencias.Resolve<IPessoaServicos>();
        private readonly ITaxaServicos _taxaServicos = ResolvedorDependencias.Resolve<ITaxaServicos>();
        private readonly ILancamentoServicos _lancamentoServicos = ResolvedorDependencias.Resolve<ILancamentoServicos>();

        #region INDEX
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region CREATE
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(DateTime dataInicio, DateTime dataFim, List<Taxa> taxas, List<Pessoa> pessoas)
        {
            try
            {
                List<LancamentoViewModel> lancamentosViewModel = new List<LancamentoViewModel>();
                for (DateTime data = dataInicio; data <= dataFim; data = data.AddMonths(1))
                {
                    foreach (var taxa in taxas)
                    {
                        foreach (var pessoa in pessoas)
                        {
                            lancamentosViewModel.Add(new LancamentoViewModel()
                            {
                                DataVencimento = data,
                                Valor = taxa.Valor,
                                Pessoa = pessoa,
                                Taxa = taxa
                            });
                        }
                    }
                }
                List<Lancamento> lancamentos = new List<Lancamento>(
                    Mapper.Map<List<LancamentoViewModel>, List<Lancamento>>(lancamentosViewModel));
                _lancamentoServicos.CadastraLancamentos(lancamentos);

                return Json(new
                {
                    Success = true
                }, "application/json", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new
                {
                    Success = false
                }, "application/json", JsonRequestBehavior.AllowGet);
            }
            return View();
        }
        #endregion

        #region MÉTODOS
        public JsonResult PesquisaPessoas(string pesquisa)
        {
            try
            {
                var pessoas = _pessoaServicos.ObtemTodos()
                    .Where(x =>
                        x.Nome != null && x.Nome.Contains(pesquisa) ||
                        x.Lugar != null && x.Lugar.Contains(pesquisa))
                    .ToList();

                return Json(new
                {
                    Success = true,
                    Pessoas = pessoas
                }, "application/json", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new
                {
                    Success = false
                }, "application/json", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult PesquisaTaxas(string pesquisa)
        {
            try
            {
                var taxas = _taxaServicos.ObtemTodos()
                    .Where(x =>
                        x.Nome != null && x.Nome.Contains(pesquisa) ||
                        x.Valor != null && x.Valor.ToString().Contains(pesquisa))
                    .ToList();

                return Json(new
                {
                    Success = true,
                    Taxas = taxas
                }, "application/json", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new
                {
                    Success = false
                }, "application/json", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}