using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Tesouraria.Dominio.Entidades;

namespace Tesouraria.Infraestrutura.Dados.Mapeamentos
{
    public class PessoaMap : EntityTypeConfiguration<Pessoa>
    {
        public PessoaMap()
        {
            HasKey(x => x.PessoaId)
                .Property(x => x.PessoaId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Nome);


            Property(x => x.Lugar);


            Property(x => x.Telefone);


            Property(x => x.Email);
               
        }
    }
}