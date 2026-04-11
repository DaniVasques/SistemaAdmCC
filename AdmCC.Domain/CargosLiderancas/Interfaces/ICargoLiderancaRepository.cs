using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.CargosLiderancas.Interfaces
{
    public interface ICargoLiderancaRepository
    {
        System.Threading.Tasks.Task<AdmCC.Domain.CargosLiderancas.Entities.CargoLideranca?> GetByIdAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.CargosLiderancas.Entities.CargoLideranca>> GetAllAsync(System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.CargosLiderancas.Entities.CargoLideranca>> GetAtivosAsync(System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AddAsync(AdmCC.Domain.CargosLiderancas.Entities.CargoLideranca cargoLideranca, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task UpdateAsync(AdmCC.Domain.CargosLiderancas.Entities.CargoLideranca cargoLideranca, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task DeleteAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
    }
}
