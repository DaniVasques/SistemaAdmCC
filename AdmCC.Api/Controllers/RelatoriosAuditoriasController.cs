using AdmCC.Api.Models.Responses;
using AdmCC.Api.Models.Responses.RelatoriosAuditorias;
using AdmCC.Domain.RelatoriosAuditorias.Entities;
using AdmCC.InfraData.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdmCC.Api.Controllers
{
    // A API publica deste modulo cobre apenas consulta de logs de auditoria.
    [ApiController]
    [Route("api/logs-auditoria")]
    [Produces("application/json")]
    public class RelatoriosAuditoriasController : ControllerBase
    {
        private readonly RelatoriosAuditoriasService _service;

        public RelatoriosAuditoriasController(RelatoriosAuditoriasService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<LogAuditoriaResponse>>> ListarLogsAsync([FromQuery] string entidade, CancellationToken cancellationToken)
        {
            try
            {
                var logs = await _service.ListarLogsPorEntidadeAsync(entidade, cancellationToken);
                return Ok(logs.Select(Mapear).ToArray());
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<LogAuditoriaResponse>> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var log = await _service.ObterLogPorIdAsync(id, cancellationToken);
            return log is null ? NotFound(CriarMensagemErro("Log de auditoria nao encontrado.")) : Ok(Mapear(log));
        }

        private static LogAuditoriaResponse Mapear(LogAuditoria log) => new()
        {
            Id = log.Id,
            Entidade = log.Entidade,
            Acao = log.Acao,
            EntidadeId = log.EntidadeId,
            UsuarioResponsavelId = log.UsuarioResponsavelId,
            DataHora = log.DataHora,
            DadosAnterioresJson = log.DadosAnterioresJson,
            DadosNovosJson = log.DadosNovosJson
        };

        private ActionResult CriarRespostaErro(Exception exception)
        {
            var response = CriarMensagemErro(exception.Message);
            return exception switch
            {
                KeyNotFoundException => NotFound(response),
                ArgumentNullException => BadRequest(response),
                ArgumentException => BadRequest(response),
                InvalidOperationException => BadRequest(response),
                _ => StatusCode(StatusCodes.Status500InternalServerError, CriarMensagemErro("Ocorreu um erro inesperado ao processar a solicitacao."))
            };
        }

        private static MensagemResponse CriarMensagemErro(string mensagem) => new() { Sucesso = false, Mensagem = mensagem };
    }
}
