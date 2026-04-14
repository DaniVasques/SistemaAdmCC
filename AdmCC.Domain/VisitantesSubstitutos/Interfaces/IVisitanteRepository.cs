using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.VisitantesSubstitutos.Interfaces
{
    public interface IVisitanteRepository
    {
        System.Threading.Tasks.Task<AdmCC.Domain.VisitantesSubstitutos.Entities.VisitanteExterno?> GetVisitanteExternoByIdAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.VisitantesSubstitutos.Entities.VisitanteExterno>> GetVisitantesExternosAsync(System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AddVisitanteExternoAsync(AdmCC.Domain.VisitantesSubstitutos.Entities.VisitanteExterno visitanteExterno, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task UpdateVisitanteExternoAsync(AdmCC.Domain.VisitantesSubstitutos.Entities.VisitanteExterno visitanteExterno, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task DeleteVisitanteExternoAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);

        System.Threading.Tasks.Task<AdmCC.Domain.VisitantesSubstitutos.Entities.VisitaInterna?> GetVisitaInternaByIdAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.VisitantesSubstitutos.Entities.VisitaInterna>> GetVisitasInternasAsync(System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AddVisitaInternaAsync(AdmCC.Domain.VisitantesSubstitutos.Entities.VisitaInterna visitaInterna, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task UpdateVisitaInternaAsync(AdmCC.Domain.VisitantesSubstitutos.Entities.VisitaInterna visitaInterna, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task DeleteVisitaInternaAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);

        System.Threading.Tasks.Task<AdmCC.Domain.VisitantesSubstitutos.Entities.SubstitutoAssociado?> GetSubstitutoAssociadoByIdAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.VisitantesSubstitutos.Entities.SubstitutoAssociado>> GetSubstitutosAssociadosAsync(System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AddSubstitutoAssociadoAsync(AdmCC.Domain.VisitantesSubstitutos.Entities.SubstitutoAssociado substitutoAssociado, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task UpdateSubstitutoAssociadoAsync(AdmCC.Domain.VisitantesSubstitutos.Entities.SubstitutoAssociado substitutoAssociado, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task DeleteSubstitutoAssociadoAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);

        System.Threading.Tasks.Task<AdmCC.Domain.VisitantesSubstitutos.Entities.SubstitutoExterno?> GetSubstitutoExternoByIdAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.VisitantesSubstitutos.Entities.SubstitutoExterno>> GetSubstitutosExternosAsync(System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AddSubstitutoExternoAsync(AdmCC.Domain.VisitantesSubstitutos.Entities.SubstitutoExterno substitutoExterno, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task UpdateSubstitutoExternoAsync(AdmCC.Domain.VisitantesSubstitutos.Entities.SubstitutoExterno substitutoExterno, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task DeleteSubstitutoExternoAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);

        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.VisitantesSubstitutos.Entities.VisitanteExterno>> GetVisitantesExternosByOcorrenciaIdAsync(Guid ocorrenciaReuniaoEquipeId, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.VisitantesSubstitutos.Entities.VisitaInterna>> GetVisitasInternasByOcorrenciaIdAsync(Guid ocorrenciaReuniaoEquipeId, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.VisitantesSubstitutos.Entities.SubstitutoAssociado>> GetSubstitutosAssociadosByOcorrenciaIdAsync(Guid ocorrenciaReuniaoEquipeId, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.VisitantesSubstitutos.Entities.SubstitutoExterno>> GetSubstitutosExternosByOcorrenciaIdAsync(Guid ocorrenciaReuniaoEquipeId, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AtualizarStatusValidacaoAsync(Guid registroId, string tipoRegistro, AdmCC.Domain.VisitantesSubstitutos.Enums.StatusValidacaoPresenca statusValidacaoPresenca, System.Threading.CancellationToken cancellationToken = default);
    }
}
