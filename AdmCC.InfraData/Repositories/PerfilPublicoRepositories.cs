using AdmCC.Domain.PerfilPublico.Entities;
using AdmCC.Domain.PerfilPublico.Interfaces;
using AdmCC.InfraData.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdmCC.InfraData.Repositories
{
    public class PerfilAssociadoRepository : IPerfilAssociadoRepository
    {
        private readonly AdmCCContext _context;

        public PerfilAssociadoRepository(AdmCCContext context)
        {
            _context = context;
        }

        public Task<PerfilAssociado?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return BuildPerfilQuery(trackChanges: false)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task<PerfilAssociado?> GetByAssociadoIdAsync(Guid associadoId, CancellationToken cancellationToken = default)
        {
            return BuildPerfilQuery(trackChanges: false)
                .FirstOrDefaultAsync(x => x.AssociadoId == associadoId, cancellationToken);
        }

        public Task AddAsync(PerfilAssociado perfilAssociado, CancellationToken cancellationToken = default)
        {
            return _context.PerfisAssociados.AddAsync(perfilAssociado, cancellationToken).AsTask();
        }

        public Task UpdateAsync(PerfilAssociado perfilAssociado, CancellationToken cancellationToken = default)
        {
            _context.PerfisAssociados.Update(perfilAssociado);
            return Task.CompletedTask;
        }

        private IQueryable<PerfilAssociado> BuildPerfilQuery(bool trackChanges)
        {
            var query = _context.PerfisAssociados
                .Include(x => x.Associado)
                .Include(x => x.Midias.OrderBy(m => m.OrdemExibicao))
                .AsQueryable();

            return trackChanges ? query : query.AsNoTracking();
        }
    }
}
