using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.RelatoriosAuditorias.Interfaces
{
    public interface INotificacaoService
    {
        System.Threading.Tasks.Task EnviarAsync(AdmCC.Domain.RelatoriosAuditorias.Entities.NotificacaoInterna notificacaoInterna, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task MarcarComoLidaAsync(Guid notificacaoId, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.RelatoriosAuditorias.Entities.NotificacaoInterna>> GetNaoLidasAsync(Guid usuarioDestinoId, System.Threading.CancellationToken cancellationToken = default);
    }
}
