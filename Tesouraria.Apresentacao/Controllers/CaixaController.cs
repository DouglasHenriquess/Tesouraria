using System;
using System.Linq;
using System.Web.Mvc;
using Tesouraria.Dominio.Interfaces.Servicos;
using Tesouraria.Dominio.Servicos.InjecaoDependencia;

namespace Tesouraria.Apresentacao.Controllers
{
    public class CaixaController : Controller
    {
        private readonly IPessoaServicos _pessoaServicos = ResolvedorDependencias.Resolve<IPessoaServicos>();
        private readonly ILancamentoServicos _lancamentoServicos = ResolvedorDependencias.Resolve<ILancamentoServicos>();

        #region CREATE
        public ActionResult Create()
        {
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
                    Success = false,
                }, "application/json", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ObtemLancamentos(int pessoaId)
        {
            try
            {
                var lancamentos = _lancamentoServicos.ObtemTodos()
                    .Where(x =>
                        x.Pessoa.PessoaId == pessoaId &&
                        x.DataVencimento.Date <= DateTime.Now.Date)
                    .OrderBy(x => x.DataVencimento)
                    .ToList();

                return Json(new
                {
                    Success = true,
                    Lancamentos = lancamentos
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
        public JsonResult ObtemLancamentosFuturos(int pessoaId)
        {
            try
            {
                var lancamentos = _lancamentoServicos.ObtemTodos()
                    .Where(x => x.Pessoa.PessoaId == pessoaId)
                    .OrderBy(x => x.DataVencimento)
                    .ToList();

                return Json(new
                {
                    Success = true,
                    Lancamentos = lancamentos
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