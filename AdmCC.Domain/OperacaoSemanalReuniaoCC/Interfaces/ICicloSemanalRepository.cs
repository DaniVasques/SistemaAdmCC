using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.OperacaoSemanalReuniaoCC.Interfaces
{
    public interface ICicloSemanalRepository
    {
        System.Threading.Tasks.Task<AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.CicloSemanal?> GetByIdAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.CicloSemanal?> GetAtivoAsync(System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.CicloSemanal?> GetByPeriodoAsync(DateTime dataInicio, DateTime dataEncerramento, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.CicloSemanal>> GetAllAsync(System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.RegistroEducacional?> GetRegistroEducacionalByIdAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.RegistroEducacional>> GetRegistrosEducacionaisAsync(System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AddRegistroEducacionalAsync(AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.RegistroEducacional registroEducacional, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task UpdateRegistroEducacionalAsync(AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.RegistroEducacional registroEducacional, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task DeleteRegistroEducacionalAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.ParametroPontuacaoEducacional?> GetParametroPontuacaoEducacionalByIdAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.ParametroPontuacaoEducacional>> GetParametrosPontuacaoEducacionalAsync(System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task UpdateParametroPontuacaoEducacionalAsync(AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.ParametroPontuacaoEducacional parametroPontuacaoEducacional, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AddAsync(AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.CicloSemanal cicloSemanal, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task UpdateAsync(AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities.CicloSemanal cicloSemanal, System.Threading.CancellationToken cancellationToken = default);
    }
}
