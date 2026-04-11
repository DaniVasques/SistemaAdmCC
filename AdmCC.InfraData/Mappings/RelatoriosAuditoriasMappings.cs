using AdmCC.Domain.RelatoriosAuditorias.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdmCC.InfraData.Mappings
{
    public class LogAuditoriaMap : IEntityTypeConfiguration<LogAuditoria>
    {
        public void Configure(EntityTypeBuilder<LogAuditoria> builder)
        {
            builder.ToTable("LogsAuditoria");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Entidade)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Acao)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.DataHora)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.DadosAnterioresJson)
                .HasColumnType("nvarchar(max)");

            builder.Property(x => x.DadosNovosJson)
                .HasColumnType("nvarchar(max)");

            builder.HasIndex(x => new { x.Entidade, x.EntidadeId });
            builder.HasIndex(x => x.UsuarioResponsavelId);
        }
    }

    public class NotificacaoInternaMap : IEntityTypeConfiguration<NotificacaoInterna>
    {
        public void Configure(EntityTypeBuilder<NotificacaoInterna> builder)
        {
            builder.ToTable("NotificacoesInternas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Titulo)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Mensagem)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.DataCriacao)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(x => x.DataLeitura)
                .HasColumnType("datetime2");

            builder.HasIndex(x => x.UsuarioDestinoId);
            builder.HasIndex(x => x.Lida);
        }
    }
}
