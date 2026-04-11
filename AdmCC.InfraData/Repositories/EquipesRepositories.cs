using AdmCC.Domain.Equipes.Entities;
using AdmCC.Domain.Equipes.Interfaces;
using AdmCC.InfraData.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdmCC.InfraData.Repositories
{
    public class EquipeRepository : IEquipeRepository
    {
        private readonly AdmCCContext _context;

        public EquipeRepository(AdmCCContext context)
        {
            _context = context;
        }

        public Task<Equipe?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return BuildEquipeQuery(trackChanges: false)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<Equipe>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await BuildEquipeQuery(trackChanges: false)
                .OrderBy(x => x.NomeEquipe)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<Equipe>> GetAtivasAsync(CancellationToken cancellationToken = default)
        {
            return await BuildEquipeQuery(trackChanges: false)
                .Where(x => x.StatusEquipe == Domain.Equipes.Enums.StatusEquipe.Ativa)
                .OrderBy(x => x.NomeEquipe)
                .ToListAsync(cancellationToken);
        }

        public Task AddAsync(Equipe equipe, CancellationToken cancellationToken = default)
        {
            return _context.Equipes.AddAsync(equipe, cancellationToken).AsTask();
        }

        public Task UpdateAsync(Equipe equipe, CancellationToken cancellationToken = default)
        {
            _context.Equipes.Update(equipe);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var equipe = await _context.Equipes
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (equipe is null)
            {
                return;
            }

            _context.Equipes.Remove(equipe);
        }

        private IQueryable<Equipe> BuildEquipeQuery(bool trackChanges)
        {
            var query = _context.Equipes
                .Include(x => x.LocalReuniaoPresencial)
                .Include(x => x.OcorrenciasReuniao)
                    .ThenInclude(x => x.Presencas)
                .AsQueryable();

            return trackChanges ? query : query.AsNoTracking();
        }
    }

    public class OcorrenciaReuniaoEquipeRepository : IOcorrenciaReuniaoEquipeRepository
    {
        private readonly AdmCCContext _context;

        public OcorrenciaReuniaoEquipeRepository(AdmCCContext context)
        {
            _context = context;
        }

        public Task<OcorrenciaReuniaoEquipe?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return BuildOcorrenciaQuery(trackChanges: false)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task<OcorrenciaReuniaoEquipe?> GetByEquipeEDataAsync(Guid equipeId, DateTime dataReuniao, CancellationToken cancellationToken = default)
        {
            var data = dataReuniao.Date;

            return BuildOcorrenciaQuery(trackChanges: false)
                .FirstOrDefaultAsync(
                    x => x.EquipeId == equipeId && x.DataReuniao.Date == data,
                    cancellationToken);
        }

        public async Task<IReadOnlyCollection<OcorrenciaReuniaoEquipe>> GetByEquipeIdAsync(Guid equipeId, CancellationToken cancellationToken = default)
        {
            return await BuildOcorrenciaQuery(trackChanges: false)
                .Where(x => x.EquipeId == equipeId)
                .OrderByDescending(x => x.DataReuniao)
                .ToListAsync(cancellationToken);
        }

        public Task AddAsync(OcorrenciaReuniaoEquipe ocorrenciaReuniaoEquipe, CancellationToken cancellationToken = default)
        {
            return _context.OcorrenciasReunioesEquipes.AddAsync(ocorrenciaReuniaoEquipe, cancellationToken).AsTask();
        }

        public Task UpdateAsync(OcorrenciaReuniaoEquipe ocorrenciaReuniaoEquipe, CancellationToken cancellationToken = default)
        {
            _context.OcorrenciasReunioesEquipes.Update(ocorrenciaReuniaoEquipe);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var ocorrencia = await _context.OcorrenciasReunioesEquipes
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (ocorrencia is null)
            {
                return;
            }

            _context.OcorrenciasReunioesEquipes.Remove(ocorrencia);
        }

        private IQueryable<OcorrenciaReuniaoEquipe> BuildOcorrenciaQuery(bool trackChanges)
        {
            var query = _context.OcorrenciasReunioesEquipes
                .Include(x => x.Equipe)
                    .ThenInclude(x => x.LocalReuniaoPresencial)
                .Include(x => x.Presencas)
                    .ThenInclude(x => x.Associado)
                .AsQueryable();

            return trackChanges ? query : query.AsNoTracking();
        }
    }
}
