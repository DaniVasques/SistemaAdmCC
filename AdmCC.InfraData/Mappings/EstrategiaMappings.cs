using AdmCC.Domain.Estrategia.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdmCC.InfraData.Mappings
{
    public class ClusterMap : IEntityTypeConfiguration<Cluster>
    {
        public void Configure(EntityTypeBuilder<Cluster> builder)
        {
            builder.ToTable("Clusters");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(x => x.Nome)
                .IsUnique();
        }
    }

    public class AtuacaoEspecificaMap : IEntityTypeConfiguration<AtuacaoEspecifica>
    {
        public void Configure(EntityTypeBuilder<AtuacaoEspecifica> builder)
        {
            builder.ToTable("AtuacoesEspecificas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(120);

            builder.HasIndex(x => new { x.ClusterId, x.Nome })
                .IsUnique();

            builder.HasOne(x => x.Cluster)
                .WithMany(x => x.AtuacoesEspecificas)
                .HasForeignKey(x => x.ClusterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class GrupamentoEstrategicoMap : IEntityTypeConfiguration<GrupamentoEstrategico>
    {
        public void Configure(EntityTypeBuilder<GrupamentoEstrategico> builder)
        {
            builder.ToTable("GrupamentosEstrategicos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Sigla)
                .IsRequired()
                .HasMaxLength(4);

            builder.HasIndex(x => x.Sigla)
                .IsUnique();
        }
    }

    public class AssociadoGrupamentoMap : IEntityTypeConfiguration<AssociadoGrupamento>
    {
        public void Configure(EntityTypeBuilder<AssociadoGrupamento> builder)
        {
            builder.ToTable("AssociadosGrupamentos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.HasIndex(x => new { x.AssociadoId, x.GrupamentoEstrategicoId })
                .IsUnique();

            builder.HasOne(x => x.Associado)
                .WithMany(x => x.AssociadosGrupamentos)
                .HasForeignKey(x => x.AssociadoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.GrupamentoEstrategico)
                .WithMany(x => x.AssociadosGrupamentos)
                .HasForeignKey(x => x.GrupamentoEstrategicoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
