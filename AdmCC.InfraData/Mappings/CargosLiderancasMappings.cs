using AdmCC.Domain.CadastroBase.Entities;
using AdmCC.Domain.CargosLiderancas.Entities;
using AdmCC.Domain.Equipes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdmCC.InfraData.Mappings
{
    public class CargoLiderancaMap : IEntityTypeConfiguration<CargoLideranca>
    {
        public void Configure(EntityTypeBuilder<CargoLideranca> builder)
        {
            builder.ToTable("CargosLideranca");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.ClassificacaoCargo)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.HasIndex(x => x.Nome)
                .IsUnique();
        }
    }

    public class AssociadoCargoLiderancaMap : IEntityTypeConfiguration<AssociadoCargoLideranca>
    {
        public void Configure(EntityTypeBuilder<AssociadoCargoLideranca> builder)
        {
            builder.ToTable("AssociadosCargosLideranca");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.DataInicio)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.DataFim)
                .HasColumnType("datetime2");

            builder.HasIndex(x => new { x.AssociadoId, x.CargoLiderancaId, x.DataInicio })
                .IsUnique();

            builder.HasOne<Associado>()
                .WithMany()
                .HasForeignKey(x => x.AssociadoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<CargoLideranca>()
                .WithMany(x => x.AssociadosCargos)
                .HasForeignKey(x => x.CargoLiderancaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class DesignacaoLiderancaEquipeMap : IEntityTypeConfiguration<DesignacaoLiderancaEquipe>
    {
        public void Configure(EntityTypeBuilder<DesignacaoLiderancaEquipe> builder)
        {
            builder.ToTable("DesignacoesLiderancaEquipe");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.DataInicio)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.DataFim)
                .HasColumnType("datetime2");

            builder.HasIndex(x => new { x.EquipeId, x.CargoLiderancaId, x.AssociadoId, x.DataInicio })
                .IsUnique();

            builder.HasOne<Equipe>()
                .WithMany()
                .HasForeignKey(x => x.EquipeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<CargoLideranca>()
                .WithMany(x => x.Designacoes)
                .HasForeignKey(x => x.CargoLiderancaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Associado>()
                .WithMany()
                .HasForeignKey(x => x.AssociadoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class DiretoriaEquipeVinculoMap : IEntityTypeConfiguration<DiretoriaEquipeVinculo>
    {
        public void Configure(EntityTypeBuilder<DiretoriaEquipeVinculo> builder)
        {
            builder.ToTable("DiretoriasEquipesVinculos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.TipoVinculo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.DataInicio)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.DataFim)
                .HasColumnType("datetime2");

            builder.HasIndex(x => x.EquipeId);
            builder.HasIndex(x => x.UsuarioId);

            builder.HasOne<Equipe>()
                .WithMany()
                .HasForeignKey(x => x.EquipeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class EquipeCargoAtivoMap : IEntityTypeConfiguration<EquipeCargoAtivo>
    {
        public void Configure(EntityTypeBuilder<EquipeCargoAtivo> builder)
        {
            builder.ToTable("EquipesCargosAtivos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.HasIndex(x => new { x.EquipeId, x.CargoLiderancaId })
                .IsUnique();

            builder.HasOne<Equipe>()
                .WithMany()
                .HasForeignKey(x => x.EquipeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<CargoLideranca>()
                .WithMany(x => x.EquipesCargosAtivos)
                .HasForeignKey(x => x.CargoLiderancaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
