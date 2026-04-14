using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.OperacaoSemanalReuniaoCC.Interfaces
{
    public interface IReuniaoCCRepository
    {
        System.Threading.Tasks.Task<AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.ReuniaoCC?> GetByIdAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.ReuniaoCC>> GetAllAsync(System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.ReuniaoCC>> GetByCicloSemanalIdAsync(Guid cicloSemanalId, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.ReuniaoCC>> GetPendentesByAssociadoIdAsync(Guid associadoId, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.ValidacaoReuniaoCC>> GetValidacoesByReuniaoIdAsync(Guid reuniaoId, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AddValidacaoAsync(AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.ValidacaoReuniaoCC validacaoReuniaoCC, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.ProspectReuniaoCC>> GetProspectsByReuniaoIdAsync(Guid reuniaoId, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AddProspectAsync(AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.ProspectReuniaoCC prospectReuniaoCC, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AddAsync(AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.ReuniaoCC reuniaoCC, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task UpdateAsync(AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.ReuniaoCC reuniaoCC, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task DeleteAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
    }
}
