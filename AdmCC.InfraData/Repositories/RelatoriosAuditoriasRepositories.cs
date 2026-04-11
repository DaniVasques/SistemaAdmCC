using AdmCC.Domain.RelatoriosAuditorias.Entities;
using AdmCC.Domain.RelatoriosAuditorias.Interfaces;
using AdmCC.InfraData.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdmCC.InfraData.Repositories
{
    public class LogAuditoriaRepository : ILogAuditoriaRepository
    {
        private readonly AdmCCContext _context;

        public LogAuditoriaRepository(AdmCCContext context)
        {
            _context = context;
        }

        public Task<LogAuditoria?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _context.LogsAuditoria
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<LogAuditoria>> GetByEntidadeAsync(string entidade, CancellationToken cancellationToken = default)
        {
            return await _context.LogsAuditoria
                .AsNoTracking()
                .Where(x => x.Entidade == entidade)
                .OrderByDescending(x => x.DataHora)
                .ToListAsync(cancellationToken);
        }

        public Task AddAsync(LogAuditoria logAuditoria, CancellationToken cancellationToken = default)
        {
            return _context.LogsAuditoria.AddAsync(logAuditoria, cancellationToken).AsTask();
        }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AdmCCContext _context;

        public UnitOfWork(AdmCCContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
