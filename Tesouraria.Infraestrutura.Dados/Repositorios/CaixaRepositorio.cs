using System.Collections.Generic;
using System.Linq;
using Tesouraria.Dominio.Entidades;
using Tesouraria.Dominio.Interfaces.Repositorios;

namespace Tesouraria.Infraestrutura.Dados.Repositorios
{
    public class CaixaRepositorio : RepositorioBase<Caixa>, ICaixaRepositorio
    {
        public override void Add(Caixa obj)
        {
            obj.Pessoa = _context.Pessoas.Find(obj.Pessoa.PessoaId);

            var taxas = new List<Taxa>(obj.Taxas.ToList());
            obj.Taxas.Clear();
            foreach (var taxa in taxas)
            {
                obj.Taxas.Add(_context.Taxas.Find(taxa.TaxaId));
            }

            _context.Set<Caixa>().Add(obj);
            _context.SaveChanges();
        }

        public override IList<Caixa> GetAll()
        {
            return _context.Set<Caixa>()
                .Include("Pessoa")
                .ToList();
        }

        public override Caixa GetById(int id)
        {
            return _context.Set<Caixa>()
                .Include("Taxas")
                .FirstOrDefault(x => x.CaixaId == id);
        }
    }
}