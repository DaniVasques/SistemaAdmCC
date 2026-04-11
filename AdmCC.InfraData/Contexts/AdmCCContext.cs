using AdmCC.Domain.CadastroBase.Entities;
using AdmCC.Domain.CargosLiderancas.Entities;
using AdmCC.Domain.ConexoesParcerias.Entities;
using AdmCC.Domain.Equipes.Entities;
using AdmCC.Domain.Estrategia.Entities;
using AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities;
using AdmCC.Domain.PerfilPublico.Entities;
using AdmCC.Domain.RelatoriosAuditorias.Entities;
using AdmCC.Domain.Seguro.Entities;
using AdmCC.Domain.VisitantesSubstitutos.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdmCC.InfraData.Contexts
{
    public class AdmCCContext : DbContext
    {
        public AdmCCContext(DbContextOptions<AdmCCContext> options) : base(options)
        {
        }

        public DbSet<Anuidade> Anuidades => Set<Anuidade>();
        public DbSet<Associado> Associados => Set<Associado>();
        public DbSet<Empresa> Empresas => Set<Empresa>();
        public DbSet<Endereco> Enderecos => Set<Endereco>();
        public DbSet<EquipeOrigem> EquipesOrigem => Set<EquipeOrigem>();
        public DbSet<HistoricoAssociado> HistoricosAssociado => Set<HistoricoAssociado>();

        public DbSet<AssociadoCargoLideranca> AssociadosCargosLideranca => Set<AssociadoCargoLideranca>();
        public DbSet<CargoLideranca> CargosLideranca => Set<CargoLideranca>();
        public DbSet<DesignacaoLiderancaEquipe> DesignacoesLiderancaEquipe => Set<DesignacaoLiderancaEquipe>();
        public DbSet<DiretoriaEquipeVinculo> DiretoriasEquipesVinculos => Set<DiretoriaEquipeVinculo>();
        public DbSet<EquipeCargoAtivo> EquipesCargosAtivos => Set<EquipeCargoAtivo>();

        public DbSet<ConexaoEstrategica> ConexoesEstrategicas => Set<ConexaoEstrategica>();
        public DbSet<NegocioRecebidoValidacao> NegociosRecebidosValidacoes => Set<NegocioRecebidoValidacao>();
        public DbSet<ParceriaAssociado> ParceriasAssociados => Set<ParceriaAssociado>();

        public DbSet<Equipe> Equipes => Set<Equipe>();
        public DbSet<OcorrenciaReuniaoEquipe> OcorrenciasReunioesEquipes => Set<OcorrenciaReuniaoEquipe>();
        public DbSet<ParametroPontuacaoEquipe> ParametrosPontuacaoEquipe => Set<ParametroPontuacaoEquipe>();
        public DbSet<PresencaReuniaoEquipe> PresencasReunioesEquipes => Set<PresencaReuniaoEquipe>();

        public DbSet<AssociadoGrupamento> AssociadosGrupamentos => Set<AssociadoGrupamento>();
        public DbSet<AtuacaoEspecifica> AtuacoesEspecificas => Set<AtuacaoEspecifica>();
        public DbSet<Cluster> Clusters => Set<Cluster>();
        public DbSet<GrupamentoEstrategico> GrupamentosEstrategicos => Set<GrupamentoEstrategico>();

        public DbSet<CicloSemanal> CiclosSemanais => Set<CicloSemanal>();
        public DbSet<ParametroPontuacaoEducacional> ParametrosPontuacaoEducacional => Set<ParametroPontuacaoEducacional>();
        public DbSet<ProspectReuniaoCC> ProspectsReunioesCC => Set<ProspectReuniaoCC>();
        public DbSet<RegistroEducacional> RegistrosEducacionais => Set<RegistroEducacional>();
        public DbSet<ReuniaoCC> ReunioesCC => Set<ReuniaoCC>();
        public DbSet<ValidacaoReuniaoCC> ValidacoesReunioesCC => Set<ValidacaoReuniaoCC>();

        public DbSet<MidiaAssociado> MidiasAssociados => Set<MidiaAssociado>();
        public DbSet<PerfilAssociado> PerfisAssociados => Set<PerfilAssociado>();

        public DbSet<LogAuditoria> LogsAuditoria => Set<LogAuditoria>();
        public DbSet<NotificacaoInterna> NotificacoesInternas => Set<NotificacaoInterna>();

        public DbSet<BeneficiarioSeguro> BeneficiariosSeguro => Set<BeneficiarioSeguro>();
        public DbSet<ConsentimentoLgpdSeguro> ConsentimentosLgpdSeguro => Set<ConsentimentoLgpdSeguro>();
        public DbSet<ContatoEmergencia> ContatosEmergencia => Set<ContatoEmergencia>();
        public DbSet<SeguroAssociado> SegurosAssociados => Set<SeguroAssociado>();
        public DbSet<SolicitacaoAlteracaoBeneficiario> SolicitacoesAlteracaoBeneficiario => Set<SolicitacaoAlteracaoBeneficiario>();

        public DbSet<SubstitutoAssociado> SubstitutosAssociados => Set<SubstitutoAssociado>();
        public DbSet<SubstitutoExterno> SubstitutosExternos => Set<SubstitutoExterno>();
        public DbSet<VisitaInterna> VisitasInternas => Set<VisitaInterna>();
        public DbSet<VisitanteExterno> VisitantesExternos => Set<VisitanteExterno>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdmCCContext).Assembly);
        }
    }
}
