using AdmCC.Domain.CadastroBase.Entities;
using AdmCC.Domain.Equipes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdmCC.InfraData.Mappings
{
    public class EquipeMap : IEntityTypeConfiguration<Equipe>
    {
        public void Configure(EntityTypeBuilder<Equipe> builder)
        {
            builder.ToTable("Equipes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.NomeEquipe)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(x => x.DataInicioFormacao)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.DataPrevisaoLancamento)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.DataEfetivaLancamento)
                .HasColumnType("datetime2");

            builder.Property(x => x.StatusEquipe)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.DiaReuniaoEquipe)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.HorarioReuniao)
                .IsRequired()
                .HasColumnType("time");

            builder.Property(x => x.ModeloReuniaoDeEquipe)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.LinkReuniaoOnline)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasIndex(x => x.NomeEquipe)
                .IsUnique();

            builder.HasOne(x => x.LocalReuniaoPresencial)
                .WithMany()
                .HasForeignKey(x => x.LocalReuniaoPresencialId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.OcorrenciasReuniao)
                .WithOne(x => x.Equipe)
                .HasForeignKey(x => x.EquipeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class OcorrenciaReuniaoEquipeMap : IEntityTypeConfiguration<OcorrenciaReuniaoEquipe>
    {
        public void Configure(EntityTypeBuilder<OcorrenciaReuniaoEquipe> builder)
        {
            builder.ToTable("OcorrenciasReunioesEquipes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.DataReuniao)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.HasIndex(x => new { x.EquipeId, x.DataReuniao })
                .IsUnique();

            builder.HasOne(x => x.Equipe)
                .WithMany(x => x.OcorrenciasReuniao)
                .HasForeignKey(x => x.EquipeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Presencas)
                .WithOne(x => x.OcorrenciaReuniaoEquipe)
                .HasForeignKey(x => x.OcorrenciaReuniaoEquipeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class ParametroPontuacaoEquipeMap : IEntityTypeConfiguration<ParametroPontuacaoEquipe>
    {
        public void Configure(EntityTypeBuilder<ParametroPontuacaoEquipe> builder)
        {
            builder.ToTable("ParametrosPontuacaoEquipe");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.HasIndex(x => new { x.QuantidadeMinimaAssociados, x.QuantidadeMaximaAssociados })
                .IsUnique();
        }
    }

    public class PresencaReuniaoEquipeMap : IEntityTypeConfiguration<PresencaReuniaoEquipe>
    {
        public void Configure(EntityTypeBuilder<PresencaReuniaoEquipe> builder)
        {
            builder.ToTable("PresencasReunioesEquipes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.DataRegistro)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.HasIndex(x => new { x.OcorrenciaReuniaoEquipeId, x.AssociadoId })
                .IsUnique();

            builder.HasOne(x => x.OcorrenciaReuniaoEquipe)
                .WithMany(x => x.Presencas)
                .HasForeignKey(x => x.OcorrenciaReuniaoEquipeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Associado)
                .WithMany()
                .HasForeignKey(x => x.AssociadoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
