using AdmCC.Domain.CadastroBase.Interfaces;
using AdmCC.Domain.CargosLiderancas.Interfaces;
using AdmCC.Domain.ConexoesParcerias.Interfaces;
using AdmCC.Domain.Equipes.Interfaces;
using AdmCC.Domain.Estrategia.Interfaces;
using AdmCC.Domain.OperacaoSemanalReuniaoCC.Interfaces;
using AdmCC.Domain.PerfilPublico.Interfaces;
using AdmCC.Domain.RelatoriosAuditorias.Interfaces;
using AdmCC.Domain.Seguro.Interfaces;
using AdmCC.Domain.VisitantesSubstitutos.Interfaces;
using AdmCC.InfraData.Contexts;
using AdmCC.InfraData.Repositories;
using AdmCC.InfraData.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AdmCC.InfraData.DependencyInjection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddAdmCCInfrastructure(this IServiceCollection services, string connectionString)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

            services.AddDbContext<AdmCCContext>(options =>
                options.UseSqlServer(
                    connectionString,
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsAssembly(typeof(AdmCCContext).Assembly.FullName);
                    }));

            RegisterRepositories(services);
            RegisterServices(services);

            return services;
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IAnuidadeRepository, AnuidadeRepository>();
            services.AddScoped<IAssociadoRepository, AssociadoRepository>();

            services.AddScoped<ICargoLiderancaRepository, CargoLiderancaRepository>();

            services.AddScoped<IConexaoEstrategicaRepository, ConexaoEstrategicaRepository>();
            services.AddScoped<IParceriaAssociadoRepository, ParceriaAssociadoRepository>();

            services.AddScoped<IEquipeRepository, EquipeRepository>();
            services.AddScoped<IOcorrenciaReuniaoEquipeRepository, OcorrenciaReuniaoEquipeRepository>();

            services.AddScoped<IAtuacaoEspecificaRepository, AtuacaoEspecificaRepository>();
            services.AddScoped<IClusterRepository, ClusterRepository>();
            services.AddScoped<IGrupamentoEstrategicoRepository, GrupamentoEstrategicoRepository>();

            services.AddScoped<ICicloSemanalRepository, CicloSemanalRepository>();
            services.AddScoped<IReuniaoCCRepository, ReuniaoCCRepository>();

            services.AddScoped<IPerfilAssociadoRepository, PerfilAssociadoRepository>();

            services.AddScoped<ILogAuditoriaRepository, LogAuditoriaRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ISeguroAssociadoRepository, SeguroAssociadoRepository>();

            services.AddScoped<IVisitanteRepository, VisitanteRepository>();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<CadastroBaseService>();
            services.AddScoped<CargosLiderancasService>();
            services.AddScoped<ConexoesParceriasService>();
            services.AddScoped<EquipesService>();
            services.AddScoped<EstrategiaService>();
            services.AddScoped<OperacaoSemanalReuniaoCCService>();
            services.AddScoped<PerfilPublicoService>();
            services.AddScoped<RelatoriosAuditoriasService>();
            services.AddScoped<SeguroService>();
            services.AddScoped<VisitantesSubstitutosService>();
        }
    }
}
