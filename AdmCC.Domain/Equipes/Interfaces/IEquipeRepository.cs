using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.Equipes.Interfaces
{
    public interface IEquipeRepository
    {
        System.Threading.Tasks.Task<AdmCC.Domain.Equipes.Entities.Equipe?> GetByIdAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.Equipes.Entities.Equipe>> GetAllAsync(System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.Equipes.Entities.Equipe>> GetAtivasAsync(System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AddAsync(AdmCC.Domain.Equipes.Entities.Equipe equipe, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task UpdateAsync(AdmCC.Domain.Equipes.Entities.Equipe equipe, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task DeleteAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
    }
}
