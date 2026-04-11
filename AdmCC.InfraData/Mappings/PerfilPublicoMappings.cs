using AdmCC.Domain.PerfilPublico.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdmCC.InfraData.Mappings
{
    public class PerfilAssociadoMap : IEntityTypeConfiguration<PerfilAssociado>
    {
        public void Configure(EntityTypeBuilder<PerfilAssociado> builder)
        {
            builder.ToTable("PerfisAssociados");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.FotoProfissionalUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.DescricaoProfissional)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(x => x.NomeEmpresaExibicao)
                .HasMaxLength(150);

            builder.Property(x => x.LogomarcaEmpresaUrl)
                .HasMaxLength(500);

            builder.Property(x => x.Site)
                .HasMaxLength(250);

            builder.Property(x => x.EmailPublico)
                .HasMaxLength(150);

            builder.HasIndex(x => x.AssociadoId)
                .IsUnique();

            builder.HasOne(x => x.Associado)
                .WithMany()
                .HasForeignKey(x => x.AssociadoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Midias)
                .WithOne(x => x.PerfilAssociado)
                .HasForeignKey(x => x.PerfilAssociadoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class MidiaAssociadoMap : IEntityTypeConfiguration<MidiaAssociado>
    {
        public void Configure(EntityTypeBuilder<MidiaAssociado> builder)
        {
            builder.ToTable("MidiasAssociados");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.NomeMidia)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Url)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasIndex(x => new { x.PerfilAssociadoId, x.OrdemExibicao });

            builder.HasOne(x => x.PerfilAssociado)
                .WithMany(x => x.Midias)
                .HasForeignKey(x => x.PerfilAssociadoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
