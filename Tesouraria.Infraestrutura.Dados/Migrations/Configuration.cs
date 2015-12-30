namespace Tesouraria.Infraestrutura.Dados.Migrations
{
    using System.Data.Entity.Migrations;
    using Tesouraria.Infraestrutura.Dados.Contexto;

    internal sealed class Configuration : DbMigrationsConfiguration<TesourariaContexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TesourariaContexto context)
        {
        }
    }
}