using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.RelatoriosAuditorias.Interfaces
{
    public interface IRelatorioService
    {
        System.Threading.Tasks.Task<byte[]> GerarRelatorioAssociadosEquipeAsync(Guid equipeId, DateTime dataReferencia, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<byte[]> GerarRelatorioIndicadoresSemanaisAsync(Guid equipeId, Guid cicloSemanalId, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<byte[]> GerarRelatorioConexoesAsync(DateTime dataInicio, DateTime dataFim, System.Threading.CancellationToken cancellationToken = default);
    }
}
