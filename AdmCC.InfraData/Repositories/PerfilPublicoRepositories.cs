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

        public async Task<IReadOnlyCollection<PerfilAssociado>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await BuildPerfilQuery(trackChanges: false)
                .OrderBy(x => x.AssociadoId)
                .ToListAsync(cancellationToken);
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

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var perfil = await _context.PerfisAssociados
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (perfil is null)
            {
                return;
            }

            _context.PerfisAssociados.Remove(perfil);
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
