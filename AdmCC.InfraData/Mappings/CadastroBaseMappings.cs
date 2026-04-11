using AdmCC.Domain.CadastroBase.Entities;
using AdmCC.Domain.Equipes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdmCC.InfraData.Mappings
{
    public class AnuidadeMap : IEntityTypeConfiguration<Anuidade>
    {
        public void Configure(EntityTypeBuilder<Anuidade> builder)
        {
            builder.ToTable("Anuidades");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.StatusAnuidade)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.DataPagamentoPrimeiraAnuidade)
                .HasColumnType("datetime2");

            builder.Property(x => x.VencimentoAtual)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.DataUltimaRenovacao)
                .HasColumnType("datetime2");
        }
    }

    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Enderecos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Cep)
                .IsRequired()
                .HasMaxLength(9);

            builder.Property(x => x.Logradouro)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Numero)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Complemento)
                .HasMaxLength(100);

            builder.Property(x => x.Bairro)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(x => x.Cidade)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(x => x.Estado)
                .IsRequired()
                .HasMaxLength(2);
        }
    }

    public class EmpresaMap : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("Empresas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Cnpj)
                .IsRequired()
                .HasMaxLength(18);

            builder.Property(x => x.RazaoSocial)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(x => x.Cnpj)
                .IsUnique();

            builder.HasOne(x => x.EnderecoComercial)
                .WithMany()
                .HasForeignKey(x => x.EnderecoComercialId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class EquipeOrigemMap : IEntityTypeConfiguration<EquipeOrigem>
    {
        public void Configure(EntityTypeBuilder<EquipeOrigem> builder)
        {
            builder.ToTable("EquipesOrigem");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.TipoOrigem)
                .IsRequired()
                .HasConversion<int>();

            builder.HasOne<Equipe>()
                .WithMany()
                .HasForeignKey(x => x.EquipeOrigemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class HistoricoAssociadoMap : IEntityTypeConfiguration<HistoricoAssociado>
    {
        public void Configure(EntityTypeBuilder<HistoricoAssociado> builder)
        {
            builder.ToTable("HistoricosAssociado");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.StatusAnterior)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.StatusNovo)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.DataAlteracao)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.Motivo)
                .HasMaxLength(500);

            builder.HasIndex(x => x.AssociadoId);

            builder.HasOne(x => x.Associado)
                .WithMany(x => x.HistoricoStatus)
                .HasForeignKey(x => x.AssociadoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class AssociadoMap : IEntityTypeConfiguration<Associado>
    {
        public void Configure(EntityTypeBuilder<Associado> builder)
        {
            builder.ToTable("Associados");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.NomeCompleto)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Cpf)
                .IsRequired()
                .HasMaxLength(14);

            builder.Property(x => x.EmailPrincipal)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.TelefoneWhatsappPrincipal)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.DataNascimento)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.DataIngresso)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.StatusAssociado)
                .IsRequired()
                .HasConversion<int>();

            builder.HasIndex(x => x.Cpf)
                .IsUnique();

            builder.HasIndex(x => x.EmailPrincipal)
                .IsUnique();

            builder.HasIndex(x => x.AnuidadeId)
                .IsUnique();

            builder.HasIndex(x => x.EquipeAtualId);
            builder.HasIndex(x => x.ClusterId);
            builder.HasIndex(x => x.AtuacaoEspecificaId);

            builder.HasOne(x => x.Endereco)
                .WithMany()
                .HasForeignKey(x => x.EnderecoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Empresa)
                .WithMany()
                .HasForeignKey(x => x.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Anuidade)
                .WithMany()
                .HasForeignKey(x => x.AnuidadeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Padrinho)
                .WithMany(x => x.Indicados)
                .HasForeignKey(x => x.PadrinhoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.EquipeOrigem)
                .WithMany()
                .HasForeignKey(x => x.EquipeOrigemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Equipe>()
                .WithMany()
                .HasForeignKey(x => x.EquipeAtualId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Cluster)
                .WithMany(x => x.Associados)
                .HasForeignKey(x => x.ClusterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AtuacaoEspecifica)
                .WithMany(x => x.Associados)
                .HasForeignKey(x => x.AtuacaoEspecificaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.HistoricoStatus)
                .WithOne(x => x.Associado)
                .HasForeignKey(x => x.AssociadoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
