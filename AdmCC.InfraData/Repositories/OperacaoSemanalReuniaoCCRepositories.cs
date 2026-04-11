using AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities;
using AdmCC.Domain.OperacaoSemanalReuniaoCC.Enums;
using AdmCC.Domain.OperacaoSemanalReuniaoCC.Interfaces;
using AdmCC.InfraData.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdmCC.InfraData.Repositories
{
    public class CicloSemanalRepository : ICicloSemanalRepository
    {
        private readonly AdmCCContext _context;

        public CicloSemanalRepository(AdmCCContext context)
        {
            _context = context;
        }

        public Task<CicloSemanal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return BuildCicloQuery(trackChanges: false)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task<CicloSemanal?> GetAtivoAsync(CancellationToken cancellationToken = default)
        {
            return BuildCicloQuery(trackChanges: false)
                .OrderByDescending(x => x.DataInicio)
                .FirstOrDefaultAsync(x => x.Ativo, cancellationToken);
        }

        public Task<CicloSemanal?> GetByPeriodoAsync(DateTime dataInicio, DateTime dataEncerramento, CancellationToken cancellationToken = default)
        {
            return BuildCicloQuery(trackChanges: false)
                .FirstOrDefaultAsync(
                    x => x.DataInicio == dataInicio && x.DataEncerramento == dataEncerramento,
                    cancellationToken);
        }

        public async Task<IReadOnlyCollection<CicloSemanal>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await BuildCicloQuery(trackChanges: false)
                .OrderByDescending(x => x.DataInicio)
                .ToListAsync(cancellationToken);
        }

        public Task AddAsync(CicloSemanal cicloSemanal, CancellationToken cancellationToken = default)
        {
            return _context.CiclosSemanais.AddAsync(cicloSemanal, cancellationToken).AsTask();
        }

        public Task UpdateAsync(CicloSemanal cicloSemanal, CancellationToken cancellationToken = default)
        {
            _context.CiclosSemanais.Update(cicloSemanal);
            return Task.CompletedTask;
        }

        private IQueryable<CicloSemanal> BuildCicloQuery(bool trackChanges)
        {
            var query = _context.CiclosSemanais
                .Include(x => x.ReunioesCC)
                .AsQueryable();

            return trackChanges ? query : query.AsNoTracking();
        }
    }

    public class ReuniaoCCRepository : IReuniaoCCRepository
    {
        private readonly AdmCCContext _context;

        public ReuniaoCCRepository(AdmCCContext context)
        {
            _context = context;
        }

        public Task<ReuniaoCC?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return BuildReuniaoQuery(trackChanges: false)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<ReuniaoCC>> GetByCicloSemanalIdAsync(Guid cicloSemanalId, CancellationToken cancellationToken = default)
        {
            return await BuildReuniaoQuery(trackChanges: false)
                .Where(x => x.CicloSemanalId == cicloSemanalId)
                .OrderBy(x => x.DataAgendada)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<ReuniaoCC>> GetPendentesByAssociadoIdAsync(Guid associadoId, CancellationToken cancellationToken = default)
        {
            return await BuildReuniaoQuery(trackChanges: false)
                .Where(x =>
                    x.StatusReuniaoCC == StatusReuniaoCC.Pendente &&
                    (x.AssociadoOrigemId == associadoId || x.AssociadoDestinoId == associadoId))
                .OrderBy(x => x.DataAgendada)
                .ToListAsync(cancellationToken);
        }

        public Task AddAsync(ReuniaoCC reuniaoCC, CancellationToken cancellationToken = default)
        {
            return _context.ReunioesCC.AddAsync(reuniaoCC, cancellationToken).AsTask();
        }

        public Task UpdateAsync(ReuniaoCC reuniaoCC, CancellationToken cancellationToken = default)
        {
            _context.ReunioesCC.Update(reuniaoCC);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var reuniao = await _context.ReunioesCC
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (reuniao is null)
            {
                return;
            }

            _context.ReunioesCC.Remove(reuniao);
        }

        private IQueryable<ReuniaoCC> BuildReuniaoQuery(bool trackChanges)
        {
            var query = _context.ReunioesCC
                .Include(x => x.CicloSemanal)
                .Include(x => x.AssociadoOrigem)
                .Include(x => x.AssociadoDestino)
                .Include(x => x.Validacoes)
                    .ThenInclude(x => x.Prospects)
                .AsQueryable();

            return trackChanges ? query : query.AsNoTracking();
        }
    }
}
