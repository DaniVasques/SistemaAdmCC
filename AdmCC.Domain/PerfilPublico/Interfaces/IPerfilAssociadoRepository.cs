using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.PerfilPublico.Interfaces
{
    public interface IPerfilAssociadoRepository
    {
        System.Threading.Tasks.Task<AdmCC.Domain.PerfilPublico.Entities.PerfilAssociado?> GetByIdAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<AdmCC.Domain.PerfilPublico.Entities.PerfilAssociado?> GetByAssociadoIdAsync(Guid associadoId, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AddAsync(AdmCC.Domain.PerfilPublico.Entities.PerfilAssociado perfilAssociado, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task UpdateAsync(AdmCC.Domain.PerfilPublico.Entities.PerfilAssociado perfilAssociado, System.Threading.CancellationToken cancellationToken = default);
    }
}
