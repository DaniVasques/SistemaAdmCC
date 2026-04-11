using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.CadastroBase.Interfaces
{
    public interface IAnuidadeRepository
    {
        Task<AdmCC.Domain.CadastroBase.Entities.Anuidade?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<AdmCC.Domain.CadastroBase.Entities.Anuidade>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(AdmCC.Domain.CadastroBase.Entities.Anuidade anuidade, CancellationToken cancellationToken = default);
        Task UpdateAsync(AdmCC.Domain.CadastroBase.Entities.Anuidade anuidade, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
