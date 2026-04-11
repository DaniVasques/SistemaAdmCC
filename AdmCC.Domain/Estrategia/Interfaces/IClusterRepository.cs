using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.Estrategia.Interfaces
{
    public interface IClusterRepository
    {
        System.Threading.Tasks.Task<AdmCC.Domain.Estrategia.Entities.Cluster?> GetByIdAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.Estrategia.Entities.Cluster>> GetAllAsync(System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<bool> ExistsByNomeAsync(string nome, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AddAsync(AdmCC.Domain.Estrategia.Entities.Cluster cluster, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task UpdateAsync(AdmCC.Domain.Estrategia.Entities.Cluster cluster, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task DeleteAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
    }
}
