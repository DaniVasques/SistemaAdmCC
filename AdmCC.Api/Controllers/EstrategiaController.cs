using AdmCC.Api.Models.Requests.Estrategia;
using AdmCC.Api.Models.Responses;
using AdmCC.Api.Models.Responses.Estrategia;
using AdmCC.Domain.Estrategia.Entities;
using AdmCC.InfraData.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdmCC.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class EstrategiaController : ControllerBase
    {
        private readonly EstrategiaService _service;

        public EstrategiaController(EstrategiaService service)
        {
            _service = service;
        }

        [HttpPost("api/clusters")]
        public async Task<ActionResult<ClusterResponse>> CriarClusterAsync([FromBody] CriarClusterRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var cluster = await _service.CriarClusterAsync(new Cluster { Nome = request.Nome.Trim(), Ativo = request.Ativo }, cancellationToken);
                return CreatedAtAction(nameof(ObterClusterPorIdAsync), new { id = cluster.Id }, Mapear(cluster));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("api/clusters")]
        public async Task<ActionResult<IReadOnlyCollection<ClusterResponse>>> ListarClustersAsync(CancellationToken cancellationToken)
        {
            var clusters = await _service.ListarClustersAsync(cancellationToken);
            return Ok(clusters.Select(Mapear).ToArray());
        }

        [HttpGet("api/clusters/{id:guid}")]
        public async Task<ActionResult<ClusterResponse>> ObterClusterPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var cluster = await _service.ObterClusterPorIdAsync(id, cancellationToken);
            return cluster is null ? NotFound(CriarMensagemErro("Cluster nao encontrado.")) : Ok(Mapear(cluster));
        }

        [HttpPut("api/clusters/{id:guid}")]
        public async Task<ActionResult<ClusterResponse>> AtualizarClusterAsync(Guid id, [FromBody] AtualizarClusterRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var cluster = await _service.AtualizarClusterAsync(new Cluster { Id = id, Nome = request.Nome.Trim(), Ativo = request.Ativo }, cancellationToken);
                return Ok(Mapear(cluster));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpDelete("api/clusters/{id:guid}")]
        public async Task<ActionResult<MensagemResponse>> ExcluirClusterAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _service.ExcluirClusterAsync(id, cancellationToken);
                return Ok(new MensagemResponse { Sucesso = true, Mensagem = "Cluster excluido com sucesso." });
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPost("api/atuacoes-especificas")]
        public async Task<ActionResult<AtuacaoEspecificaResponse>> CriarAtuacaoAsync([FromBody] CriarAtuacaoEspecificaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var atuacao = await _service.CriarAtuacaoAsync(new AtuacaoEspecifica { ClusterId = request.ClusterId, Nome = request.Nome.Trim(), Ativo = request.Ativo }, cancellationToken);
                return CreatedAtAction(nameof(ObterAtuacaoPorIdAsync), new { id = atuacao.Id }, Mapear(atuacao));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("api/atuacoes-especificas")]
        public async Task<ActionResult<IReadOnlyCollection<AtuacaoEspecificaResponse>>> ListarAtuacoesAsync(CancellationToken cancellationToken)
        {
            var atuacoes = await _service.ListarAtuacoesAsync(cancellationToken);
            return Ok(atuacoes.Select(Mapear).ToArray());
        }

        [HttpGet("api/atuacoes-especificas/{id:guid}")]
        public async Task<ActionResult<AtuacaoEspecificaResponse>> ObterAtuacaoPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var atuacao = await _service.ObterAtuacaoPorIdAsync(id, cancellationToken);
            return atuacao is null ? NotFound(CriarMensagemErro("Atuacao especifica nao encontrada.")) : Ok(Mapear(atuacao));
        }

        [HttpPut("api/atuacoes-especificas/{id:guid}")]
        public async Task<ActionResult<AtuacaoEspecificaResponse>> AtualizarAtuacaoAsync(Guid id, [FromBody] AtualizarAtuacaoEspecificaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var atuacao = await _service.AtualizarAtuacaoAsync(new AtuacaoEspecifica { Id = id, ClusterId = request.ClusterId, Nome = request.Nome.Trim(), Ativo = request.Ativo }, cancellationToken);
                return Ok(Mapear(atuacao));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpDelete("api/atuacoes-especificas/{id:guid}")]
        public async Task<ActionResult<MensagemResponse>> ExcluirAtuacaoAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _service.ExcluirAtuacaoAsync(id, cancellationToken);
                return Ok(new MensagemResponse { Sucesso = true, Mensagem = "Atuacao especifica excluida com sucesso." });
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPost("api/grupamentos-estrategicos")]
        public async Task<ActionResult<GrupamentoEstrategicoResponse>> CriarGrupamentoAsync([FromBody] CriarGrupamentoEstrategicoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var grupamento = await _service.CriarGrupamentoAsync(new GrupamentoEstrategico { Nome = request.Nome.Trim(), Sigla = request.Sigla.Trim(), Ativo = request.Ativo }, cancellationToken);
                return CreatedAtAction(nameof(ObterGrupamentoPorIdAsync), new { id = grupamento.Id }, Mapear(grupamento));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("api/grupamentos-estrategicos")]
        public async Task<ActionResult<IReadOnlyCollection<GrupamentoEstrategicoResponse>>> ListarGrupamentosAsync(CancellationToken cancellationToken)
        {
            var grupamentos = await _service.ListarGrupamentosAsync(cancellationToken);
            return Ok(grupamentos.Select(Mapear).ToArray());
        }

        [HttpGet("api/grupamentos-estrategicos/{id:guid}")]
        public async Task<ActionResult<GrupamentoEstrategicoResponse>> ObterGrupamentoPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var grupamento = await _service.ObterGrupamentoPorIdAsync(id, cancellationToken);
            return grupamento is null ? NotFound(CriarMensagemErro("Grupamento estrategico nao encontrado.")) : Ok(Mapear(grupamento));
        }

        [HttpPut("api/grupamentos-estrategicos/{id:guid}")]
        public async Task<ActionResult<GrupamentoEstrategicoResponse>> AtualizarGrupamentoAsync(Guid id, [FromBody] AtualizarGrupamentoEstrategicoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var grupamento = await _service.AtualizarGrupamentoAsync(new GrupamentoEstrategico { Id = id, Nome = request.Nome.Trim(), Sigla = request.Sigla.Trim(), Ativo = request.Ativo }, cancellationToken);
                return Ok(Mapear(grupamento));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpDelete("api/grupamentos-estrategicos/{id:guid}")]
        public async Task<ActionResult<MensagemResponse>> ExcluirGrupamentoAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _service.ExcluirGrupamentoAsync(id, cancellationToken);
                return Ok(new MensagemResponse { Sucesso = true, Mensagem = "Grupamento estrategico excluido com sucesso." });
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        private static ClusterResponse Mapear(Cluster cluster) => new() { Id = cluster.Id, Nome = cluster.Nome, Ativo = cluster.Ativo };
        private static AtuacaoEspecificaResponse Mapear(AtuacaoEspecifica atuacao) => new() { Id = atuacao.Id, ClusterId = atuacao.ClusterId, Nome = atuacao.Nome, Ativo = atuacao.Ativo };
        private static GrupamentoEstrategicoResponse Mapear(GrupamentoEstrategico grupamento) => new() { Id = grupamento.Id, Nome = grupamento.Nome, Sigla = grupamento.Sigla, Ativo = grupamento.Ativo };

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
