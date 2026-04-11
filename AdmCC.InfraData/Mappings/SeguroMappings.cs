using AdmCC.Domain.Seguro.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdmCC.InfraData.Mappings
{
    public class SeguroAssociadoMap : IEntityTypeConfiguration<SeguroAssociado>
    {
        public void Configure(EntityTypeBuilder<SeguroAssociado> builder)
        {
            builder.ToTable("SegurosAssociados");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.EstadoCivil)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.Profissao)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.HasIndex(x => x.AssociadoId)
                .IsUnique();

            builder.HasOne(x => x.Associado)
                .WithMany()
                .HasForeignKey(x => x.AssociadoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Beneficiarios)
                .WithOne(x => x.SeguroAssociado)
                .HasForeignKey(x => x.SeguroAssociadoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ContatoEmergencia)
                .WithOne(x => x.SeguroAssociado)
                .HasForeignKey<ContatoEmergencia>(x => x.SeguroAssociadoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ConsentimentoLgpd)
                .WithOne(x => x.SeguroAssociado)
                .HasForeignKey<ConsentimentoLgpdSeguro>(x => x.SeguroAssociadoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.SolicitacoesAlteracaoBeneficiario)
                .WithOne(x => x.SeguroAssociado)
                .HasForeignKey(x => x.SeguroAssociadoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class BeneficiarioSeguroMap : IEntityTypeConfiguration<BeneficiarioSeguro>
    {
        public void Configure(EntityTypeBuilder<BeneficiarioSeguro> builder)
        {
            builder.ToTable("BeneficiariosSeguro");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.NomeCompleto)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Cpf)
                .IsRequired()
                .HasMaxLength(14);

            builder.Property(x => x.GrauParentesco)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Percentual)
                .HasPrecision(5, 2);

            builder.Property(x => x.Telefone)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(x => x.SeguroAssociadoId);
        }
    }

    public class ContatoEmergenciaMap : IEntityTypeConfiguration<ContatoEmergencia>
    {
        public void Configure(EntityTypeBuilder<ContatoEmergencia> builder)
        {
            builder.ToTable("ContatosEmergencia");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.NomeCompleto)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.TelefonePrincipal)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.TelefoneSecundario)
                .HasMaxLength(20);

            builder.HasIndex(x => x.SeguroAssociadoId)
                .IsUnique();
        }
    }

    public class ConsentimentoLgpdSeguroMap : IEntityTypeConfiguration<ConsentimentoLgpdSeguro>
    {
        public void Configure(EntityTypeBuilder<ConsentimentoLgpdSeguro> builder)
        {
            builder.ToTable("ConsentimentosLgpdSeguro");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.DataAceite)
                .HasColumnType("datetime2");

            builder.Property(x => x.TextoConsentimento)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.HasIndex(x => x.SeguroAssociadoId)
                .IsUnique();
        }
    }

    public class SolicitacaoAlteracaoBeneficiarioMap : IEntityTypeConfiguration<SolicitacaoAlteracaoBeneficiario>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoAlteracaoBeneficiario> builder)
        {
            builder.ToTable("SolicitacoesAlteracaoBeneficiario");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.DataSolicitacao)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.StatusSolicitacaoBeneficiario)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.ObservacaoSolicitante)
                .HasMaxLength(1000);

            builder.Property(x => x.ObservacaoAnalise)
                .HasMaxLength(1000);

            builder.Property(x => x.DataConclusao)
                .HasColumnType("datetime2");

            builder.HasIndex(x => x.SeguroAssociadoId);
        }
    }
}
