﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Tesouraria.Dominio.Entidades;

namespace Tesouraria.Infraestrutura.Dados.Mapeamentos
{
    public class CaixaMap : EntityTypeConfiguration<Caixa>
    {
        public CaixaMap()
        {
            HasKey(x => x.CaixaId)
                .Property(x => x.CaixaId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Valor);

            Property(x => x.DataPagamento);

            HasOptional(x => x.Pessoa)
                .WithMany()
                .Map(x => x.MapKey("PessoaId"));

            HasMany(x => x.Lancamentos)
                .WithMany()
                .Map(x =>
                    {
                        x.ToTable("LancamentoToCaixa");
                        x.MapLeftKey("CaixaId");
                        x.MapRightKey("LancamentoId");
                    });
        }
    }
}