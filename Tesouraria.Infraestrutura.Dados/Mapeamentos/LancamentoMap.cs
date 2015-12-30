using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Tesouraria.Dominio.Entidades;

namespace Tesouraria.Infraestrutura.Dados.Mapeamentos
{
    public class LancamentoMap : EntityTypeConfiguration<Lancamento>
    {
        public LancamentoMap()
        {
            HasKey(x => x.LancamentoId)
                .Property(x => x.LancamentoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Valor);

            Property(x => x.DataVencimento);

            Property(x => x.Pago);

            HasOptional(x => x.Pessoa)
                .WithMany()
                .Map(x => x.MapKey("PessoaId"));

            HasOptional(x => x.Taxa)
                .WithMany()
                .Map(x => x.MapKey("TaxaId"));
        }
    }
}