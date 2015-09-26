using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Tesouraria.Dominio.Entidades;

namespace Tesouraria.Infraestrutura.Dados.Mapeamentos
{
    public class TaxaMap : EntityTypeConfiguration<Taxa>
    {
        public TaxaMap()
        {
            HasKey(x => x.TaxaId)
                .Property(x => x.TaxaId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Nome); Property(x => x.Valor);
        }
    }
}