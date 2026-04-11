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

        public Task AddVisitanteExternoAsync(VisitanteExterno visitanteExterno, CancellationToken cancellationToken = default)
        {
            return _context.VisitantesExternos.AddAsync(visitanteExterno, cancellationToken).AsTask();
        }

        public Task AddVisitaInternaAsync(VisitaInterna visitaInterna, CancellationToken cancellationToken = default)
        {
            return _context.VisitasInternas.AddAsync(visitaInterna, cancellationToken).AsTask();
        }

        public Task AddSubstitutoAssociadoAsync(SubstitutoAssociado substitutoAssociado, CancellationToken cancellationToken = default)
        {
            return _context.SubstitutosAssociados.AddAsync(substitutoAssociado, cancellationToken).AsTask();
        }

        public Task AddSubstitutoExternoAsync(SubstitutoExterno substitutoExterno, CancellationToken cancellationToken = default)
        {
            return _context.SubstitutosExternos.AddAsync(substitutoExterno, cancellationToken).AsTask();
        }

        public async Task<IReadOnlyCollection<VisitanteExterno>> GetVisitantesExternosByOcorrenciaIdAsync(Guid ocorrenciaReuniaoEquipeId, CancellationToken cancellationToken = default)
        {
            return await _context.VisitantesExternos
                .AsNoTracking()
                .Include(x => x.OcorrenciaReuniaoEquipe)
                .Include(x => x.AssociadoResponsavel)
                .Where(x => x.OcorrenciaReuniaoEquipeId == ocorrenciaReuniaoEquipeId)
                .OrderByDescending(x => x.DataCadastro)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<VisitaInterna>> GetVisitasInternasByOcorrenciaIdAsync(Guid ocorrenciaReuniaoEquipeId, CancellationToken cancellationToken = default)
        {
            return await _context.VisitasInternas
                .AsNoTracking()
                .Include(x => x.OcorrenciaReuniaoEquipe)
                .Include(x => x.AssociadoVisitante)
                .Where(x => x.OcorrenciaReuniaoEquipeId == ocorrenciaReuniaoEquipeId)
                .OrderByDescending(x => x.DataCadastro)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<SubstitutoAssociado>> GetSubstitutosAssociadosByOcorrenciaIdAsync(Guid ocorrenciaReuniaoEquipeId, CancellationToken cancellationToken = default)
        {
            return await _context.SubstitutosAssociados
                .AsNoTracking()
                .Include(x => x.OcorrenciaReuniaoEquipe)
                .Include(x => x.AssociadoTitular)
                .Include(x => x.AssociadoSubstituto)
                .Where(x => x.OcorrenciaReuniaoEquipeId == ocorrenciaReuniaoEquipeId)
                .OrderByDescending(x => x.DataCadastro)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<SubstitutoExterno>> GetSubstitutosExternosByOcorrenciaIdAsync(Guid ocorrenciaReuniaoEquipeId, CancellationToken cancellationToken = default)
        {
            return await _context.SubstitutosExternos
                .AsNoTracking()
                .Include(x => x.OcorrenciaReuniaoEquipe)
                .Include(x => x.AssociadoTitular)
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
    }
}
