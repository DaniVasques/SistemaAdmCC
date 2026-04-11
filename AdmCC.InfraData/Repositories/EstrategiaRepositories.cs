using AdmCC.Domain.Estrategia.Entities;
using AdmCC.Domain.Estrategia.Interfaces;
using AdmCC.InfraData.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdmCC.InfraData.Repositories
{
    public class AtuacaoEspecificaRepository : IAtuacaoEspecificaRepository
    {
        private readonly AdmCCContext _context;

        public AtuacaoEspecificaRepository(AdmCCContext context)
        {
            _context = context;
        }

        public Task<AtuacaoEspecifica?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return BuildAtuacaoQuery(trackChanges: false)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<AtuacaoEspecifica>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await BuildAtuacaoQuery(trackChanges: false)
                .OrderBy(x => x.Nome)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<AtuacaoEspecifica>> GetByClusterIdAsync(Guid clusterId, CancellationToken cancellationToken = default)
        {
            return await BuildAtuacaoQuery(trackChanges: false)
                .Where(x => x.ClusterId == clusterId)
                .OrderBy(x => x.Nome)
                .ToListAsync(cancellationToken);
        }

        public Task<bool> ExistsByNomeAsync(Guid clusterId, string nome, CancellationToken cancellationToken = default)
        {
            return _context.AtuacoesEspecificas
                .AnyAsync(x => x.ClusterId == clusterId && x.Nome == nome, cancellationToken);
        }

        public Task AddAsync(AtuacaoEspecifica atuacaoEspecifica, CancellationToken cancellationToken = default)
        {
            return _context.AtuacoesEspecificas.AddAsync(atuacaoEspecifica, cancellationToken).AsTask();
        }

        public Task UpdateAsync(AtuacaoEspecifica atuacaoEspecifica, CancellationToken cancellationToken = default)
        {
            _context.AtuacoesEspecificas.Update(atuacaoEspecifica);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var atuacao = await _context.AtuacoesEspecificas
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (atuacao is null)
            {
                return;
            }

            _context.AtuacoesEspecificas.Remove(atuacao);
        }

        private IQueryable<AtuacaoEspecifica> BuildAtuacaoQuery(bool trackChanges)
        {
            var query = _context.AtuacoesEspecificas
                .Include(x => x.Cluster)
                .Include(x => x.Associados)
                .AsQueryable();

            return trackChanges ? query : query.AsNoTracking();
        }
    }

    public class ClusterRepository : IClusterRepository
    {
        private readonly AdmCCContext _context;

        public ClusterRepository(AdmCCContext context)
        {
            _context = context;
        }

        public Task<Cluster?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return BuildClusterQuery(trackChanges: false)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<Cluster>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await BuildClusterQuery(trackChanges: false)
                .OrderBy(x => x.Nome)
                .ToListAsync(cancellationToken);
        }

        public Task<bool> ExistsByNomeAsync(string nome, CancellationToken cancellationToken = default)
        {
            return _context.Clusters.AnyAsync(x => x.Nome == nome, cancellationToken);
        }

        public Task AddAsync(Cluster cluster, CancellationToken cancellationToken = default)
        {
            return _context.Clusters.AddAsync(cluster, cancellationToken).AsTask();
        }

        public Task UpdateAsync(Cluster cluster, CancellationToken cancellationToken = default)
        {
            _context.Clusters.Update(cluster);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var cluster = await _context.Clusters
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (cluster is null)
            {
                return;
            }

            _context.Clusters.Remove(cluster);
        }

        private IQueryable<Cluster> BuildClusterQuery(bool trackChanges)
        {
            var query = _context.Clusters
                .Include(x => x.AtuacoesEspecificas)
                .Include(x => x.Associados)
                .AsQueryable();

            return trackChanges ? query : query.AsNoTracking();
        }
    }

    public class GrupamentoEstrategicoRepository : IGrupamentoEstrategicoRepository
    {
        private readonly AdmCCContext _context;

        public GrupamentoEstrategicoRepository(AdmCCContext context)
        {
            _context = context;
        }

        public Task<GrupamentoEstrategico?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return BuildGrupamentoQuery(trackChanges: false)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<GrupamentoEstrategico>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await BuildGrupamentoQuery(trackChanges: false)
                .OrderBy(x => x.Nome)
                .ToListAsync(cancellationToken);
        }

        public Task<bool> ExistsByNomeAsync(string nome, CancellationToken cancellationToken = default)
        {
            return _context.GrupamentosEstrategicos.AnyAsync(x => x.Nome == nome, cancellationToken);
        }

        public Task<bool> ExistsBySiglaAsync(string sigla, CancellationToken cancellationToken = default)
        {
            return _context.GrupamentosEstrategicos.AnyAsync(x => x.Sigla == sigla, cancellationToken);
        }

        public Task AddAsync(GrupamentoEstrategico grupamentoEstrategico, CancellationToken cancellationToken = default)
        {
            return _context.GrupamentosEstrategicos.AddAsync(grupamentoEstrategico, cancellationToken).AsTask();
        }

        public Task UpdateAsync(GrupamentoEstrategico grupamentoEstrategico, CancellationToken cancellationToken = default)
        {
            _context.GrupamentosEstrategicos.Update(grupamentoEstrategico);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var grupamento = await _context.GrupamentosEstrategicos
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (grupamento is null)
            {
                return;
            }

            _context.GrupamentosEstrategicos.Remove(grupamento);
        }

        private IQueryable<GrupamentoEstrategico> BuildGrupamentoQuery(bool trackChanges)
        {
            var query = _context.GrupamentosEstrategicos
                .Include(x => x.AssociadosGrupamentos)
                    .ThenInclude(x => x.Associado)
                .AsQueryable();

            return trackChanges ? query : query.AsNoTracking();
        }
    }
}
