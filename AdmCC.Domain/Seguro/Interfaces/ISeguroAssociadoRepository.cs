using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.Seguro.Interfaces
{
    public interface ISeguroAssociadoRepository
    {
        System.Threading.Tasks.Task<AdmCC.Domain.Seguro.Entities.SeguroAssociado?> GetByIdAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<AdmCC.Domain.Seguro.Entities.SeguroAssociado?> GetByAssociadoIdAsync(Guid associadoId, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.Seguro.Entities.SeguroAssociado>> GetAllAsync(System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AddAsync(AdmCC.Domain.Seguro.Entities.SeguroAssociado seguroAssociado, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task UpdateAsync(AdmCC.Domain.Seguro.Entities.SeguroAssociado seguroAssociado, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task DeleteAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
    }
}
