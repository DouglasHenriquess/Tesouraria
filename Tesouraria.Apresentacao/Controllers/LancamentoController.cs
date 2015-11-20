using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tesouraria.Dominio.Interfaces.Servicos;
using Tesouraria.Dominio.Servicos.InjecaoDependencia;
using Tesouraria.Apresentacao.ViewModels;
using AutoMapper;
using Tesouraria.Dominio.Entidades;

namespace Tesouraria.Apresentacao.Controllers
{
    public class LancamentoController : Controller
    {
        private readonly IPessoaServicos _pessoaServicos = ResolvedorDependencias.Resolve<IPessoaServicos>();
        private readonly ITaxaServicos _taxaServicos = ResolvedorDependencias.Resolve<ITaxaServicos>();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {           
            return View();
        }

        [HttpPost]
        public JsonResult PesquisaPessoas(string pesquisa)
        {
            var _pessoas = _pessoaServicos.GetAll()
                .Where(x =>
                    x.Nome != null && x.Nome.Contains(pesquisa) ||
                    x.Lugar != null && x.Lugar.Contains(pesquisa))
                .ToList();

            return Json(_pessoas);
        }

        [HttpPost]
        public JsonResult PesquisaTaxas(string pesquisa)
        {
            var _taxas = _taxaServicos.GetAll()
                .Where(x =>
                    x.Nome != null && x.Nome.Contains(pesquisa) ||
                    x.Valor != null && x.Valor.ToString().Contains(pesquisa))
                .ToList();

            return Json(_taxas);
        }
    }
}