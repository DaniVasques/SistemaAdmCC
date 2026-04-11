using AdmCC.Domain.VisitantesSubstitutos.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdmCC.InfraData.Mappings
{
    public class SubstitutoAssociadoMap : IEntityTypeConfiguration<SubstitutoAssociado>
    {
        public void Configure(EntityTypeBuilder<SubstitutoAssociado> builder)
        {
            builder.ToTable("SubstitutosAssociados");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.StatusValidacaoPresenca)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.DataValidacao)
                .HasColumnType("datetime2");

            builder.HasIndex(x => x.OcorrenciaReuniaoEquipeId);
            builder.HasIndex(x => x.AssociadoTitularId);
            builder.HasIndex(x => x.AssociadoSubstitutoId);

            builder.HasOne(x => x.OcorrenciaReuniaoEquipe)
                .WithMany()
                .HasForeignKey(x => x.OcorrenciaReuniaoEquipeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AssociadoTitular)
                .WithMany()
                .HasForeignKey(x => x.AssociadoTitularId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AssociadoSubstituto)
                .WithMany()
                .HasForeignKey(x => x.AssociadoSubstitutoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class SubstitutoExternoMap : IEntityTypeConfiguration<SubstitutoExterno>
    {
        public void Configure(EntityTypeBuilder<SubstitutoExterno> builder)
        {
            builder.ToTable("SubstitutosExternos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.TipoPessoa)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.NomeCompleto)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.TelefonePrincipal)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Email)
                .HasMaxLength(150);

            builder.Property(x => x.Cpf)
                .HasMaxLength(14);

            builder.Property(x => x.NomeEmpresa)
                .HasMaxLength(150);

            builder.Property(x => x.StatusValidacaoPresenca)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.DataValidacao)
                .HasColumnType("datetime2");

            builder.HasIndex(x => x.OcorrenciaReuniaoEquipeId);
            builder.HasIndex(x => x.AssociadoTitularId);
            builder.HasIndex(x => x.Cpf);

            builder.HasOne(x => x.OcorrenciaReuniaoEquipe)
                .WithMany()
                .HasForeignKey(x => x.OcorrenciaReuniaoEquipeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AssociadoTitular)
                .WithMany()
                .HasForeignKey(x => x.AssociadoTitularId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class VisitaInternaMap : IEntityTypeConfiguration<VisitaInterna>
    {
        public void Configure(EntityTypeBuilder<VisitaInterna> builder)
        {
            builder.ToTable("VisitasInternas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.StatusValidacaoPresenca)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.DataValidacao)
                .HasColumnType("datetime2");

            builder.HasIndex(x => x.OcorrenciaReuniaoEquipeId);
            builder.HasIndex(x => x.AssociadoVisitanteId);

            builder.HasOne(x => x.OcorrenciaReuniaoEquipe)
                .WithMany()
                .HasForeignKey(x => x.OcorrenciaReuniaoEquipeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AssociadoVisitante)
                .WithMany()
                .HasForeignKey(x => x.AssociadoVisitanteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class VisitanteExternoMap : IEntityTypeConfiguration<VisitanteExterno>
    {
        public void Configure(EntityTypeBuilder<VisitanteExterno> builder)
        {
            builder.ToTable("VisitantesExternos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.TipoPessoa)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.NomeCompleto)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.TelefonePrincipal)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Email)
                .HasMaxLength(150);

            builder.Property(x => x.Cpf)
                .HasMaxLength(14);

            builder.Property(x => x.NomeEmpresa)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.StatusValidacaoPresenca)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.DataValidacao)
                .HasColumnType("datetime2");

            builder.HasIndex(x => x.OcorrenciaReuniaoEquipeId);
            builder.HasIndex(x => x.AssociadoResponsavelId);
            builder.HasIndex(x => x.Cpf);

            builder.HasOne(x => x.OcorrenciaReuniaoEquipe)
                .WithMany()
                .HasForeignKey(x => x.OcorrenciaReuniaoEquipeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AssociadoResponsavel)
                .WithMany()
                .HasForeignKey(x => x.AssociadoResponsavelId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
