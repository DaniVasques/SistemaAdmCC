using AdmCC.Domain.VisitantesSubstitutos.Entities;
using AdmCC.Domain.VisitantesSubstitutos.Enums;
using AdmCC.Domain.VisitantesSubstitutos.Interfaces;
using AdmCC.InfraData.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdmCC.InfraData.Repositories
{
    public class VisitanteRepository : IVisitanteRepository
    {
        private readonly AdmCCContext _context;

        public VisitanteRepository(AdmCCContext context)
        {
            _context = context;
        }

        public Task<VisitanteExterno?> GetVisitanteExternoByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return BuildVisitanteExternoQuery(trackChanges: false)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<VisitanteExterno>> GetVisitantesExternosAsync(CancellationToken cancellationToken = default)
        {
            return await BuildVisitanteExternoQuery(trackChanges: false)
                .OrderByDescending(x => x.DataCadastro)
                .ToListAsync(cancellationToken);
        }

        public Task AddVisitanteExternoAsync(VisitanteExterno visitanteExterno, CancellationToken cancellationToken = default)
        {
            return _context.VisitantesExternos.AddAsync(visitanteExterno, cancellationToken).AsTask();
        }

        public Task UpdateVisitanteExternoAsync(VisitanteExterno visitanteExterno, CancellationToken cancellationToken = default)
        {
            _context.VisitantesExternos.Update(visitanteExterno);
            return Task.CompletedTask;
        }

        public async Task DeleteVisitanteExternoAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var visitante = await _context.VisitantesExternos
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (visitante is null)
            {
                return;
            }

            _context.VisitantesExternos.Remove(visitante);
        }

        public Task<VisitaInterna?> GetVisitaInternaByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return BuildVisitaInternaQuery(trackChanges: false)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<VisitaInterna>> GetVisitasInternasAsync(CancellationToken cancellationToken = default)
        {
            return await BuildVisitaInternaQuery(trackChanges: false)
                .OrderByDescending(x => x.DataCadastro)
                .ToListAsync(cancellationToken);
        }

        public Task AddVisitaInternaAsync(VisitaInterna visitaInterna, CancellationToken cancellationToken = default)
        {
            return _context.VisitasInternas.AddAsync(visitaInterna, cancellationToken).AsTask();
        }

        public Task UpdateVisitaInternaAsync(VisitaInterna visitaInterna, CancellationToken cancellationToken = default)
        {
            _context.VisitasInternas.Update(visitaInterna);
            return Task.CompletedTask;
        }

        public async Task DeleteVisitaInternaAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var visitaInterna = await _context.VisitasInternas
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (visitaInterna is null)
            {
                return;
            }

            _context.VisitasInternas.Remove(visitaInterna);
        }

        public Task<SubstitutoAssociado?> GetSubstitutoAssociadoByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return BuildSubstitutoAssociadoQuery(trackChanges: false)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<SubstitutoAssociado>> GetSubstitutosAssociadosAsync(CancellationToken cancellationToken = default)
        {
            return await BuildSubstitutoAssociadoQuery(trackChanges: false)
                .OrderByDescending(x => x.DataCadastro)
                .ToListAsync(cancellationToken);
        }

        public Task AddSubstitutoAssociadoAsync(SubstitutoAssociado substitutoAssociado, CancellationToken cancellationToken = default)
        {
            return _context.SubstitutosAssociados.AddAsync(substitutoAssociado, cancellationToken).AsTask();
        }

        public Task UpdateSubstitutoAssociadoAsync(SubstitutoAssociado substitutoAssociado, CancellationToken cancellationToken = default)
        {
            _context.SubstitutosAssociados.Update(substitutoAssociado);
            return Task.CompletedTask;
        }

        public async Task DeleteSubstitutoAssociadoAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var substitutoAssociado = await _context.SubstitutosAssociados
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (substitutoAssociado is null)
            {
                return;
            }

            _context.SubstitutosAssociados.Remove(substitutoAssociado);
        }

        public Task<SubstitutoExterno?> GetSubstitutoExternoByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return BuildSubstitutoExternoQuery(trackChanges: false)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<SubstitutoExterno>> GetSubstitutosExternosAsync(CancellationToken cancellationToken = default)
        {
            return await BuildSubstitutoExternoQuery(trackChanges: false)
                .OrderByDescending(x => x.DataCadastro)
                .ToListAsync(cancellationToken);
        }

        public Task AddSubstitutoExternoAsync(SubstitutoExterno substitutoExterno, CancellationToken cancellationToken = default)
        {
            return _context.SubstitutosExternos.AddAsync(substitutoExterno, cancellationToken).AsTask();
        }

        public Task UpdateSubstitutoExternoAsync(SubstitutoExterno substitutoExterno, CancellationToken cancellationToken = default)
        {
            _context.SubstitutosExternos.Update(substitutoExterno);
            return Task.CompletedTask;
        }

        public async Task DeleteSubstitutoExternoAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var substitutoExterno = await _context.SubstitutosExternos
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (substitutoExterno is null)
            {
                return;
            }

            _context.SubstitutosExternos.Remove(substitutoExterno);
        }

        public async Task<IReadOnlyCollection<VisitanteExterno>> GetVisitantesExternosByOcorrenciaIdAsync(Guid ocorrenciaReuniaoEquipeId, CancellationToken cancellationToken = default)
        {
            return await BuildVisitanteExternoQuery(trackChanges: false)
                .Where(x => x.OcorrenciaReuniaoEquipeId == ocorrenciaReuniaoEquipeId)
                .OrderByDescending(x => x.DataCadastro)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<VisitaInterna>> GetVisitasInternasByOcorrenciaIdAsync(Guid ocorrenciaReuniaoEquipeId, CancellationToken cancellationToken = default)
        {
            return await BuildVisitaInternaQuery(trackChanges: false)
                .Where(x => x.OcorrenciaReuniaoEquipeId == ocorrenciaReuniaoEquipeId)
                .OrderByDescending(x => x.DataCadastro)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<SubstitutoAssociado>> GetSubstitutosAssociadosByOcorrenciaIdAsync(Guid ocorrenciaReuniaoEquipeId, CancellationToken cancellationToken = default)
        {
            return await BuildSubstitutoAssociadoQuery(trackChanges: false)
                .Where(x => x.OcorrenciaReuniaoEquipeId == ocorrenciaReuniaoEquipeId)
                .OrderByDescending(x => x.DataCadastro)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<SubstitutoExterno>> GetSubstitutosExternosByOcorrenciaIdAsync(Guid ocorrenciaReuniaoEquipeId, CancellationToken cancellationToken = default)
        {
            return await BuildSubstitutoExternoQuery(trackChanges: false)
                .Where(x => x.OcorrenciaReuniaoEquipeId == ocorrenciaReuniaoEquipeId)
                .OrderByDescending(x => x.DataCadastro)
                .ToListAsync(cancellationToken);
        }

        public async Task AtualizarStatusValidacaoAsync(Guid registroId, string tipoRegistro, StatusValidacaoPresenca statusValidacaoPresenca, CancellationToken cancellationToken = default)
        {
            switch (tipoRegistro)
            {
                case nameof(VisitanteExterno):
                    await AtualizarVisitanteExternoAsync(registroId, statusValidacaoPresenca, cancellationToken);
                    break;

                case nameof(VisitaInterna):
                    await AtualizarVisitaInternaAsync(registroId, statusValidacaoPresenca, cancellationToken);
                    break;

                case nameof(SubstitutoAssociado):
                    await AtualizarSubstitutoAssociadoAsync(registroId, statusValidacaoPresenca, cancellationToken);
                    break;

                case nameof(SubstitutoExterno):
                    await AtualizarSubstitutoExternoAsync(registroId, statusValidacaoPresenca, cancellationToken);
                    break;

                default:
                    throw new ArgumentException($"Tipo de registro '{tipoRegistro}' nao suportado.", nameof(tipoRegistro));
            }
        }

        private async Task AtualizarVisitanteExternoAsync(Guid registroId, StatusValidacaoPresenca status, CancellationToken cancellationToken)
        {
            var visitante = await _context.VisitantesExternos
                .FirstOrDefaultAsync(x => x.Id == registroId, cancellationToken);

            if (visitante is null)
            {
                return;
            }

            visitante.StatusValidacaoPresenca = status;
            visitante.DataValidacao = status == StatusValidacaoPresenca.Pendente ? null : DateTime.UtcNow;
        }

        private async Task AtualizarVisitaInternaAsync(Guid registroId, StatusValidacaoPresenca status, CancellationToken cancellationToken)
        {
            var visita = await _context.VisitasInternas
                .FirstOrDefaultAsync(x => x.Id == registroId, cancellationToken);

            if (visita is null)
            {
                return;
            }

            visita.StatusValidacaoPresenca = status;
            visita.DataValidacao = status == StatusValidacaoPresenca.Pendente ? null : DateTime.UtcNow;
        }

        private async Task AtualizarSubstitutoAssociadoAsync(Guid registroId, StatusValidacaoPresenca status, CancellationToken cancellationToken)
        {
            var substituto = await _context.SubstitutosAssociados
                .FirstOrDefaultAsync(x => x.Id == registroId, cancellationToken);

            if (substituto is null)
            {
                return;
            }

            substituto.StatusValidacaoPresenca = status;
            substituto.DataValidacao = status == StatusValidacaoPresenca.Pendente ? null : DateTime.UtcNow;
        }

        private async Task AtualizarSubstitutoExternoAsync(Guid registroId, StatusValidacaoPresenca status, CancellationToken cancellationToken)
        {
            var substituto = await _context.SubstitutosExternos
                .FirstOrDefaultAsync(x => x.Id == registroId, cancellationToken);

            if (substituto is null)
            {
                return;
            }

            substituto.StatusValidacaoPresenca = status;
            substituto.DataValidacao = status == StatusValidacaoPresenca.Pendente ? null : DateTime.UtcNow;
        }

        private IQueryable<VisitanteExterno> BuildVisitanteExternoQuery(bool trackChanges)
        {
            var query = _context.VisitantesExternos
                .Include(x => x.OcorrenciaReuniaoEquipe)
                    .ThenInclude(x => x.Equipe)
                .Include(x => x.AssociadoResponsavel)
                .AsQueryable();

            return trackChanges ? query : query.AsNoTracking();
        }

        private IQueryable<VisitaInterna> BuildVisitaInternaQuery(bool trackChanges)
        {
            var query = _context.VisitasInternas
                .Include(x => x.OcorrenciaReuniaoEquipe)
                    .ThenInclude(x => x.Equipe)
                .Include(x => x.AssociadoVisitante)
                .AsQueryable();

            return trackChanges ? query : query.AsNoTracking();
        }

        private IQueryable<SubstitutoAssociado> BuildSubstitutoAssociadoQuery(bool trackChanges)
        {
            var query = _context.SubstitutosAssociados
                .Include(x => x.OcorrenciaReuniaoEquipe)
                    .ThenInclude(x => x.Equipe)
                .Include(x => x.AssociadoTitular)
                .Include(x => x.AssociadoSubstituto)
                .AsQueryable();

            return trackChanges ? query : query.AsNoTracking();
        }

        private IQueryable<SubstitutoExterno> BuildSubstitutoExternoQuery(bool trackChanges)
        {
            var query = _context.SubstitutosExternos
                .Include(x => x.OcorrenciaReuniaoEquipe)
                    .ThenInclude(x => x.Equipe)
                .Include(x => x.AssociadoTitular)
                .AsQueryable();

            return trackChanges ? query : query.AsNoTracking();
        }
    }
}
