using AdmCC.Domain.Seguro.Entities;
using AdmCC.Domain.Seguro.Interfaces;
using AdmCC.InfraData.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdmCC.InfraData.Repositories
{
    public class SeguroAssociadoRepository : ISeguroAssociadoRepository
    {
        private readonly AdmCCContext _context;

        public SeguroAssociadoRepository(AdmCCContext context)
        {
            _context = context;
        }

        public Task<SeguroAssociado?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return BuildSeguroQuery(trackChanges: false)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task<SeguroAssociado?> GetByAssociadoIdAsync(Guid associadoId, CancellationToken cancellationToken = default)
        {
            return BuildSeguroQuery(trackChanges: false)
                .FirstOrDefaultAsync(x => x.AssociadoId == associadoId, cancellationToken);
        }

        public async Task<IReadOnlyCollection<SeguroAssociado>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await BuildSeguroQuery(trackChanges: false)
                .OrderByDescending(x => x.DataCadastro)
                .ToListAsync(cancellationToken);
        }

        public Task AddAsync(SeguroAssociado seguroAssociado, CancellationToken cancellationToken = default)
        {
            return _context.SegurosAssociados.AddAsync(seguroAssociado, cancellationToken).AsTask();
        }

        public Task UpdateAsync(SeguroAssociado seguroAssociado, CancellationToken cancellationToken = default)
        {
            _context.SegurosAssociados.Update(seguroAssociado);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var seguro = await _context.SegurosAssociados
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (seguro is null)
            {
                return;
            }

            _context.SegurosAssociados.Remove(seguro);
        }

        private IQueryable<SeguroAssociado> BuildSeguroQuery(bool trackChanges)
        {
            var query = _context.SegurosAssociados
                .Include(x => x.Associado)
                .Include(x => x.Beneficiarios)
                .Include(x => x.ContatoEmergencia)
                .Include(x => x.ConsentimentoLgpd)
                .Include(x => x.SolicitacoesAlteracaoBeneficiario)
                .AsQueryable();

            return trackChanges ? query : query.AsNoTracking();
        }
    }
}
