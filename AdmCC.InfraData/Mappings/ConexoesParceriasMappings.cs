using AdmCC.Domain.CadastroBase.Entities;
using AdmCC.Domain.ConexoesParcerias.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdmCC.InfraData.Mappings
{
    public class ConexaoEstrategicaMap : IEntityTypeConfiguration<ConexaoEstrategica>
    {
        public void Configure(EntityTypeBuilder<ConexaoEstrategica> builder)
        {
            builder.ToTable("ConexoesEstrategicas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.NomeContatoOuEmpresa)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.TelefoneContato)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Complemento)
                .HasMaxLength(500);

            builder.Property(x => x.TipoDeConexao)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.StatusConexao)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.DataEnvio)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.HasIndex(x => x.AssociadoOrigemId);
            builder.HasIndex(x => x.AssociadoDestinoId);

            builder.HasOne<Associado>()
                .WithMany()
                .HasForeignKey(x => x.AssociadoOrigemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Associado>()
                .WithMany()
                .HasForeignKey(x => x.AssociadoDestinoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ValidacaoRecebimento)
                .WithOne(x => x.ConexaoEstrategica)
                .HasForeignKey<NegocioRecebidoValidacao>(x => x.ConexaoEstrategicaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class NegocioRecebidoValidacaoMap : IEntityTypeConfiguration<NegocioRecebidoValidacao>
    {
        public void Configure(EntityTypeBuilder<NegocioRecebidoValidacao> builder)
        {
            builder.ToTable("NegociosRecebidosValidacoes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.StatusConexao)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.MotivoNegocioNaoFechado)
                .HasConversion<int?>();

            builder.Property(x => x.ValorNegocioFechado)
                .HasPrecision(18, 2);

            builder.Property(x => x.DataValidacao)
                .HasColumnType("datetime2");

            builder.Property(x => x.DataPrazoEstourado)
                .HasColumnType("datetime2");

            builder.HasIndex(x => x.ConexaoEstrategicaId)
                .IsUnique();

            builder.HasIndex(x => x.AssociadoReceptorId);

            builder.HasOne(x => x.ConexaoEstrategica)
                .WithOne(x => x.ValidacaoRecebimento)
                .HasForeignKey<NegocioRecebidoValidacao>(x => x.ConexaoEstrategicaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Associado>()
                .WithMany()
                .HasForeignKey(x => x.AssociadoReceptorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class ParceriaAssociadoMap : IEntityTypeConfiguration<ParceriaAssociado>
    {
        public void Configure(EntityTypeBuilder<ParceriaAssociado> builder)
        {
            builder.ToTable("ParceriasAssociados");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.DataParceria)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.HasIndex(x => new { x.AssociadoOrigemId, x.AssociadoDestinoId })
                .IsUnique();

            builder.HasOne<Associado>()
                .WithMany()
                .HasForeignKey(x => x.AssociadoOrigemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Associado>()
                .WithMany()
                .HasForeignKey(x => x.AssociadoDestinoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
