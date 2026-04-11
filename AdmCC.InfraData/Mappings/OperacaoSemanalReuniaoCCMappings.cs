using AdmCC.Domain.CadastroBase.Entities;
using AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdmCC.InfraData.Mappings
{
    public class CicloSemanalMap : IEntityTypeConfiguration<CicloSemanal>
    {
        public void Configure(EntityTypeBuilder<CicloSemanal> builder)
        {
            builder.ToTable("CiclosSemanais");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.DataInicio)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.DataEncerramento)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.HasIndex(x => new { x.DataInicio, x.DataEncerramento })
                .IsUnique();
        }
    }

    public class ParametroPontuacaoEducacionalMap : IEntityTypeConfiguration<ParametroPontuacaoEducacional>
    {
        public void Configure(EntityTypeBuilder<ParametroPontuacaoEducacional> builder)
        {
            builder.ToTable("ParametrosPontuacaoEducacional");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.TipoPontuacaoEducacional)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime2");
        }
    }

    public class RegistroEducacionalMap : IEntityTypeConfiguration<RegistroEducacional>
    {
        public void Configure(EntityTypeBuilder<RegistroEducacional> builder)
        {
            builder.ToTable("RegistrosEducacionais");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.TipoPontuacaoEducacional)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.Titulo)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.CodigoExterno)
                .HasMaxLength(100);

            builder.Property(x => x.DataOcorrencia)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.DataValidacao)
                .HasColumnType("datetime2");

            builder.HasIndex(x => x.AssociadoId);

            builder.HasOne(x => x.Associado)
                .WithMany()
                .HasForeignKey(x => x.AssociadoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ParametroPontuacaoEducacional)
                .WithMany()
                .HasForeignKey(x => x.ParametroPontuacaoEducacionalId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class ReuniaoCCMap : IEntityTypeConfiguration<ReuniaoCC>
    {
        public void Configure(EntityTypeBuilder<ReuniaoCC> builder)
        {
            builder.ToTable("ReunioesCC");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.TipoReuniaoCC)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.StatusReuniaoCC)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.LocalReuniao)
                .HasMaxLength(200);

            builder.Property(x => x.LinkReuniaoOnline)
                .HasMaxLength(250);

            builder.Property(x => x.DataAgendada)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.HasIndex(x => new { x.CicloSemanalId, x.AssociadoOrigemId, x.AssociadoDestinoId })
                .IsUnique();

            builder.HasOne(x => x.CicloSemanal)
                .WithMany(x => x.ReunioesCC)
                .HasForeignKey(x => x.CicloSemanalId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.AssociadoOrigem)
                .WithMany()
                .HasForeignKey(x => x.AssociadoOrigemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AssociadoDestino)
                .WithMany()
                .HasForeignKey(x => x.AssociadoDestinoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Validacoes)
                .WithOne(x => x.ReuniaoCC)
                .HasForeignKey(x => x.ReuniaoCCId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class ValidacaoReuniaoCCMap : IEntityTypeConfiguration<ValidacaoReuniaoCC>
    {
        public void Configure(EntityTypeBuilder<ValidacaoReuniaoCC> builder)
        {
            builder.ToTable("ValidacoesReunioesCC");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.DataValidacao)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.HasIndex(x => new { x.ReuniaoCCId, x.AssociadoId })
                .IsUnique();

            builder.HasOne(x => x.ReuniaoCC)
                .WithMany(x => x.Validacoes)
                .HasForeignKey(x => x.ReuniaoCCId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Associado)
                .WithMany()
                .HasForeignKey(x => x.AssociadoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Prospects)
                .WithOne(x => x.ValidacaoReuniaoCC)
                .HasForeignKey(x => x.ValidacaoReuniaoCCId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class ProspectReuniaoCCMap : IEntityTypeConfiguration<ProspectReuniaoCC>
    {
        public void Configure(EntityTypeBuilder<ProspectReuniaoCC> builder)
        {
            builder.ToTable("ProspectsReunioesCC");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.NomeProspect)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.NomeEmpresa)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasOne(x => x.ValidacaoReuniaoCC)
                .WithMany(x => x.Prospects)
                .HasForeignKey(x => x.ValidacaoReuniaoCCId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
