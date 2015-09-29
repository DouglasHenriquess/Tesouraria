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
    public class PessoaController : Controller
    {
        private readonly IPessoaServicos _pessoaServicos = ResolvedorDependencias.Resolve<IPessoaServicos>();

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string pesquisa)
        {
            var _pessoaVM = Mapper.Map<IList<Pessoa>, IList<PessoaViewModel>>(_pessoaServicos.GetAll()
                .Where(x =>
                    x.PessoaId.ToString().Contains(pesquisa) ||
                    (x.Nome != null && x.Nome.Contains(pesquisa)) ||
                    (x.Lugar != null && x.Lugar.Contains(pesquisa)) ||
                    (x.Telefone != null && x.Telefone.Contains(pesquisa)))
                    .OrderBy(x => x.Nome)
                    .ToList());

            return View(_pessoaVM);
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            ViewBag.Lugares = new SelectList(new string[]
            {
                "SELECIONE",
                "NÃO SÓCIO",
                "QUADRO DE SÓCIO", 
                "CORPO INSTRUTIVO", 
                "CORPO DO CONSELHO", 
                "QUADRO DE MESTRE"
            });

            return View();
        }

        [HttpPost]
        public ActionResult Create(PessoaViewModel pessoaViewModel)
        {
            if (ModelState.IsValid)
            {
                var _pessoa = Mapper.Map<PessoaViewModel, Pessoa>(pessoaViewModel);
                _pessoaServicos.AddOrUpdate(_pessoa);
                return RedirectToAction("Index");
            }

            return View(pessoaViewModel);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int pessoaId)
        {
            ViewBag.Lugares = new SelectList(new string[]
            {
                "SELECIONE",
                "NÃO SÓCIO",
                "QUADRO DE SÓCIO", 
                "CORPO INSTRUTIVO", 
                "CORPO DO CONSELHO", 
                "QUADRO DE MESTRE"
            });

            var _pessoa = _pessoaServicos.GetById(pessoaId);
            var _pessoaVM = Mapper.Map<Pessoa, PessoaViewModel>(_pessoa);

            return View(_pessoaVM);
        }

        [HttpPost]
        public ActionResult Edit(PessoaViewModel pessoaViewModel)
        {
            if (ModelState.IsValid)
            {
                var _pessoa = Mapper.Map<PessoaViewModel, Pessoa>(pessoaViewModel);
                _pessoaServicos.AddOrUpdate(_pessoa);
                return RedirectToAction("Index");
            }

            return View(pessoaViewModel);
        }
        #endregion
    }
}