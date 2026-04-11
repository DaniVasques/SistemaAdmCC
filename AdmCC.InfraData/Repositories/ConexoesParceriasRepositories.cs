using AdmCC.Domain.ConexoesParcerias.Entities;
using AdmCC.Domain.ConexoesParcerias.Interfaces;
using AdmCC.InfraData.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdmCC.InfraData.Repositories
{
    public class ConexaoEstrategicaRepository : IConexaoEstrategicaRepository
    {
        private readonly AdmCCContext _context;

        public ConexaoEstrategicaRepository(AdmCCContext context)
        {
            _context = context;
        }

        public Task<ConexaoEstrategica?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return BuildConexaoQuery(trackChanges: false)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<ConexaoEstrategica>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await BuildConexaoQuery(trackChanges: false)
                .OrderByDescending(x => x.DataEnvio)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<ConexaoEstrategica>> GetByAssociadoOrigemIdAsync(Guid associadoOrigemId, CancellationToken cancellationToken = default)
        {
            return await BuildConexaoQuery(trackChanges: false)
                .Where(x => x.AssociadoOrigemId == associadoOrigemId)
                .OrderByDescending(x => x.DataEnvio)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<ConexaoEstrategica>> GetByAssociadoDestinoIdAsync(Guid associadoDestinoId, CancellationToken cancellationToken = default)
        {
            return await BuildConexaoQuery(trackChanges: false)
                .Where(x => x.AssociadoDestinoId == associadoDestinoId)
                .OrderByDescending(x => x.DataEnvio)
                .ToListAsync(cancellationToken);
        }

        public Task AddAsync(ConexaoEstrategica conexaoEstrategica, CancellationToken cancellationToken = default)
        {
            return _context.ConexoesEstrategicas.AddAsync(conexaoEstrategica, cancellationToken).AsTask();
        }

        public Task UpdateAsync(ConexaoEstrategica conexaoEstrategica, CancellationToken cancellationToken = default)
        {
            _context.ConexoesEstrategicas.Update(conexaoEstrategica);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var conexao = await _context.ConexoesEstrategicas
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (conexao is null)
            {
                return;
            }

            _context.ConexoesEstrategicas.Remove(conexao);
        }

        private IQueryable<ConexaoEstrategica> BuildConexaoQuery(bool trackChanges)
        {
            var query = _context.ConexoesEstrategicas
                .Include(x => x.ValidacaoRecebimento)
                .AsQueryable();

            return trackChanges ? query : query.AsNoTracking();
        }
    }

    public class ParceriaAssociadoRepository : IParceriaAssociadoRepository
    {
        private readonly AdmCCContext _context;

        public ParceriaAssociadoRepository(AdmCCContext context)
        {
            _context = context;
        }

        public Task<ParceriaAssociado?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _context.ParceriasAssociados
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<ParceriaAssociado>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.ParceriasAssociados
                .AsNoTracking()
                .OrderByDescending(x => x.DataParceria)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<ParceriaAssociado>> GetAtivasByAssociadoIdAsync(Guid associadoId, CancellationToken cancellationToken = default)
        {
            return await _context.ParceriasAssociados
                .AsNoTracking()
                .Where(x => x.Ativa && (x.AssociadoOrigemId == associadoId || x.AssociadoDestinoId == associadoId))
                .OrderByDescending(x => x.DataParceria)
                .ToListAsync(cancellationToken);
        }

        public Task AddAsync(ParceriaAssociado parceriaAssociado, CancellationToken cancellationToken = default)
        {
            return _context.ParceriasAssociados.AddAsync(parceriaAssociado, cancellationToken).AsTask();
        }

        public Task UpdateAsync(ParceriaAssociado parceriaAssociado, CancellationToken cancellationToken = default)
        {
            _context.ParceriasAssociados.Update(parceriaAssociado);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var parceria = await _context.ParceriasAssociados
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (parceria is null)
            {
                return;
            }

            _context.ParceriasAssociados.Remove(parceria);
        }
    }
}
