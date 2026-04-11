using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.ConexoesParcerias.Interfaces
{
    public interface IParceriaAssociadoRepository
    {
        System.Threading.Tasks.Task<AdmCC.Domain.ConexoesParcerias.Entities.ParceriaAssociado?> GetByIdAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.ConexoesParcerias.Entities.ParceriaAssociado>> GetAllAsync(System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.ConexoesParcerias.Entities.ParceriaAssociado>> GetAtivasByAssociadoIdAsync(Guid associadoId, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AddAsync(AdmCC.Domain.ConexoesParcerias.Entities.ParceriaAssociado parceriaAssociado, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task UpdateAsync(AdmCC.Domain.ConexoesParcerias.Entities.ParceriaAssociado parceriaAssociado, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task DeleteAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
    }
}
