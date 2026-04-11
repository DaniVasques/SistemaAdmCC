using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.Estrategia.Interfaces
{
    public interface IAtuacaoEspecificaRepository
    {
        System.Threading.Tasks.Task<AdmCC.Domain.Estrategia.Entities.AtuacaoEspecifica?> GetByIdAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.Estrategia.Entities.AtuacaoEspecifica>> GetAllAsync(System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.Estrategia.Entities.AtuacaoEspecifica>> GetByClusterIdAsync(Guid clusterId, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<bool> ExistsByNomeAsync(Guid clusterId, string nome, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AddAsync(AdmCC.Domain.Estrategia.Entities.AtuacaoEspecifica atuacaoEspecifica, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task UpdateAsync(AdmCC.Domain.Estrategia.Entities.AtuacaoEspecifica atuacaoEspecifica, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task DeleteAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
    }
}
