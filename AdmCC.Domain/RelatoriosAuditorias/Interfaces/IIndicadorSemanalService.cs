using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.RelatoriosAuditorias.Interfaces
{
    public interface IIndicadorSemanalService
    {
        System.Threading.Tasks.Task<int> CalcularPontuacaoEquipeAsync(Guid equipeId, DateTime dataReferencia, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<int> CalcularTotalVisitantesValidadosAsync(Guid equipeId, DateTime dataInicio, DateTime dataFim, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<int> CalcularTotalReunioesCcValidadasAsync(Guid associadoId, DateTime dataInicio, DateTime dataFim, System.Threading.CancellationToken cancellationToken = default);
    }
}
