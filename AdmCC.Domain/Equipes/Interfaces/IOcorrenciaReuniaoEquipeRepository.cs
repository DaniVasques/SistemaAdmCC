using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.Equipes.Interfaces
{
    public interface IOcorrenciaReuniaoEquipeRepository
    {
        System.Threading.Tasks.Task<AdmCC.Domain.Equipes.Entities.OcorrenciaReuniaoEquipe?> GetByIdAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<AdmCC.Domain.Equipes.Entities.OcorrenciaReuniaoEquipe?> GetByEquipeEDataAsync(Guid equipeId, DateTime dataReuniao, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.Equipes.Entities.OcorrenciaReuniaoEquipe>> GetByEquipeIdAsync(Guid equipeId, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AddAsync(AdmCC.Domain.Equipes.Entities.OcorrenciaReuniaoEquipe ocorrenciaReuniaoEquipe, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task UpdateAsync(AdmCC.Domain.Equipes.Entities.OcorrenciaReuniaoEquipe ocorrenciaReuniaoEquipe, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task DeleteAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
    }
}
