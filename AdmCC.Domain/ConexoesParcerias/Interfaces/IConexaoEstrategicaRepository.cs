using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.ConexoesParcerias.Interfaces
{
    public interface IConexaoEstrategicaRepository
    {
        System.Threading.Tasks.Task<AdmCC.Domain.ConexoesParcerias.Entities.ConexaoEstrategica?> GetByIdAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.ConexoesParcerias.Entities.ConexaoEstrategica>> GetAllAsync(System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.ConexoesParcerias.Entities.ConexaoEstrategica>> GetByAssociadoOrigemIdAsync(Guid associadoOrigemId, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.ConexoesParcerias.Entities.ConexaoEstrategica>> GetByAssociadoDestinoIdAsync(Guid associadoDestinoId, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AddAsync(AdmCC.Domain.ConexoesParcerias.Entities.ConexaoEstrategica conexaoEstrategica, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task UpdateAsync(AdmCC.Domain.ConexoesParcerias.Entities.ConexaoEstrategica conexaoEstrategica, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task DeleteAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
    }
}
