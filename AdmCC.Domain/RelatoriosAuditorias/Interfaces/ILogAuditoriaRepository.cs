using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.RelatoriosAuditorias.Interfaces
{
    public interface ILogAuditoriaRepository
    {
        System.Threading.Tasks.Task<AdmCC.Domain.RelatoriosAuditorias.Entities.LogAuditoria?> GetByIdAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.RelatoriosAuditorias.Entities.LogAuditoria>> GetByEntidadeAsync(string entidade, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AddAsync(AdmCC.Domain.RelatoriosAuditorias.Entities.LogAuditoria logAuditoria, System.Threading.CancellationToken cancellationToken = default);
    }
}
