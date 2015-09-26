using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Tesouraria.Dominio.Entidades;
using Tesouraria.Infraestrutura.Dados.Mapeamentos;

namespace Tesouraria.Infraestrutura.Dados.Contexto
{
    public class TesourariaContexto : DbContext
    {
        public TesourariaContexto()
            : base("Tesouraria")
        {
            Configuration.ProxyCreationEnabled = true;
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Taxa> Taxas { get; set; }
        public DbSet<Caixa> Caixas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(x => x.Name == x.ReflectedType.Name + "Id")
                .Configure(x => x.IsKey());

            modelBuilder.Properties<string>()
                .Configure(x => x.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(x => x.HasMaxLength(100));

            modelBuilder.Configurations.Add(new PessoaMap());
            modelBuilder.Configurations.Add(new TaxaMap());
            modelBuilder.Configurations.Add(new CaixaMap());
        }
    }
}