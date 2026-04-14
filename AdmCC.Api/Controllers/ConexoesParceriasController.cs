using AdmCC.Api.Models.Requests.ConexoesParcerias;
using AdmCC.Api.Models.Responses;
using AdmCC.Api.Models.Responses.ConexoesParcerias;
using AdmCC.Domain.ConexoesParcerias.Entities;
using AdmCC.InfraData.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdmCC.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class ConexoesParceriasController : ControllerBase
    {
        private readonly ConexoesParceriasService _service;

        public ConexoesParceriasController(ConexoesParceriasService service)
        {
            _service = service;
        }

        [HttpPost("api/conexoes")]
        [ProducesResponseType(typeof(ConexaoEstrategicaResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ConexaoEstrategicaResponse>> CriarConexaoAsync(
            [FromBody] CriarConexaoEstrategicaRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var conexao = await _service.CriarConexaoAsync(MapearConexao(request), cancellationToken);
                return CreatedAtAction(nameof(ObterConexaoPorIdAsync), new { id = conexao.Id }, MapearConexaoResponse(conexao));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("api/conexoes")]
        [ProducesResponseType(typeof(IReadOnlyCollection<ConexaoEstrategicaResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<ConexaoEstrategicaResponse>>> ListarConexoesAsync(CancellationToken cancellationToken)
        {
            var conexoes = await _service.ListarConexoesAsync(cancellationToken);
            return Ok(conexoes.Select(MapearConexaoResponse).ToArray());
        }

        [HttpGet("api/conexoes/{id:guid}")]
        [ProducesResponseType(typeof(ConexaoEstrategicaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ConexaoEstrategicaResponse>> ObterConexaoPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var conexao = await _service.ObterConexaoPorIdAsync(id, cancellationToken);
            if (conexao is null)
            {
                return NotFound(CriarMensagemErro("Conexao estrategica nao encontrada."));
            }

            return Ok(MapearConexaoResponse(conexao));
        }

        [HttpPut("api/conexoes/{id:guid}")]
        [ProducesResponseType(typeof(ConexaoEstrategicaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ConexaoEstrategicaResponse>> AtualizarConexaoAsync(
            Guid id,
            [FromBody] AtualizarConexaoEstrategicaRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var conexao = await _service.AtualizarConexaoAsync(MapearConexao(id, request), cancellationToken);
                return Ok(MapearConexaoResponse(conexao));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpDelete("api/conexoes/{id:guid}")]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MensagemResponse>> ExcluirConexaoAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _service.ExcluirConexaoAsync(id, cancellationToken);
                return Ok(new MensagemResponse { Sucesso = true, Mensagem = "Conexao excluida com sucesso." });
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPatch("api/conexoes/{id:guid}/validacao")]
        [ProducesResponseType(typeof(NegocioRecebidoValidacaoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NegocioRecebidoValidacaoResponse>> ValidarNegocioRecebidoAsync(
            Guid id,
            [FromBody] ValidarNegocioRecebidoRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var validacao = await _service.ValidarNegocioRecebidoAsync(id, MapearValidacao(id, request), cancellationToken);
                return Ok(MapearValidacaoResponse(validacao));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPost("api/parcerias")]
        [ProducesResponseType(typeof(ParceriaAssociadoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ParceriaAssociadoResponse>> CriarParceriaAsync(
            [FromBody] CriarParceriaAssociadoRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var parceria = await _service.CriarParceriaAsync(MapearParceria(request), cancellationToken);
                return CreatedAtAction(nameof(ObterParceriaPorIdAsync), new { id = parceria.Id }, MapearParceriaResponse(parceria));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("api/parcerias")]
        [ProducesResponseType(typeof(IReadOnlyCollection<ParceriaAssociadoResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<ParceriaAssociadoResponse>>> ListarParceriasAsync(CancellationToken cancellationToken)
        {
            var parcerias = await _service.ListarParceriasAsync(cancellationToken);
            return Ok(parcerias.Select(MapearParceriaResponse).ToArray());
        }

        [HttpGet("api/parcerias/{id:guid}")]
        [ProducesResponseType(typeof(ParceriaAssociadoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ParceriaAssociadoResponse>> ObterParceriaPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var parceria = await _service.ObterParceriaPorIdAsync(id, cancellationToken);
            if (parceria is null)
            {
                return NotFound(CriarMensagemErro("Parceria nao encontrada."));
            }

            return Ok(MapearParceriaResponse(parceria));
        }

        [HttpDelete("api/parcerias/{id:guid}")]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MensagemResponse>> ExcluirParceriaAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _service.EncerrarParceriaAsync(id, cancellationToken);
                return Ok(new MensagemResponse { Sucesso = true, Mensagem = "Parceria encerrada com sucesso." });
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        private static ConexaoEstrategica MapearConexao(CriarConexaoEstrategicaRequest request)
        {
            return new ConexaoEstrategica
            {
                AssociadoOrigemId = request.AssociadoOrigemId,
                AssociadoDestinoId = request.AssociadoDestinoId,
                NomeContatoOuEmpresa = request.NomeContatoOuEmpresa.Trim(),
                TelefoneContato = request.TelefoneContato.Trim(),
                Complemento = string.IsNullOrWhiteSpace(request.Complemento) ? null : request.Complemento.Trim(),
                TipoDeConexao = request.TipoDeConexao,
                StatusConexao = request.StatusConexao
            };
        }

        private static ConexaoEstrategica MapearConexao(Guid id, AtualizarConexaoEstrategicaRequest request)
        {
            return new ConexaoEstrategica
            {
                Id = id,
                AssociadoOrigemId = request.AssociadoOrigemId,
                AssociadoDestinoId = request.AssociadoDestinoId,
                NomeContatoOuEmpresa = request.NomeContatoOuEmpresa.Trim(),
                TelefoneContato = request.TelefoneContato.Trim(),
                Complemento = string.IsNullOrWhiteSpace(request.Complemento) ? null : request.Complemento.Trim(),
                TipoDeConexao = request.TipoDeConexao,
                StatusConexao = request.StatusConexao,
                Excluida = request.Excluida
            };
        }

        private static NegocioRecebidoValidacao MapearValidacao(Guid conexaoId, ValidarNegocioRecebidoRequest request)
        {
            return new NegocioRecebidoValidacao
            {
                ConexaoEstrategicaId = conexaoId,
                AssociadoReceptorId = request.AssociadoReceptorId,
                StatusConexao = request.StatusConexao,
                MotivoNegocioNaoFechado = request.MotivoNegocioNaoFechado,
                ValorNegocioFechado = request.ValorNegocioFechado,
                DataValidacao = request.DataValidacao,
                PrazoEstourado = request.PrazoEstourado,
                DataPrazoEstourado = request.DataPrazoEstourado
            };
        }

        private static ParceriaAssociado MapearParceria(CriarParceriaAssociadoRequest request)
        {
            return new ParceriaAssociado
            {
                AssociadoOrigemId = request.AssociadoOrigemId,
                AssociadoDestinoId = request.AssociadoDestinoId,
                DataParceria = request.DataParceria ?? default,
                Ativa = request.Ativa
            };
        }

        private static ConexaoEstrategicaResponse MapearConexaoResponse(ConexaoEstrategica conexao)
        {
            return new ConexaoEstrategicaResponse
            {
                Id = conexao.Id,
                AssociadoOrigemId = conexao.AssociadoOrigemId,
                AssociadoDestinoId = conexao.AssociadoDestinoId,
                NomeContatoOuEmpresa = conexao.NomeContatoOuEmpresa,
                TelefoneContato = conexao.TelefoneContato,
                Complemento = conexao.Complemento,
                TipoDeConexao = conexao.TipoDeConexao,
                StatusConexao = conexao.StatusConexao,
                DataEnvio = conexao.DataEnvio,
                Excluida = conexao.Excluida,
                ValidacaoRecebimento = conexao.ValidacaoRecebimento is null ? null : MapearValidacaoResponse(conexao.ValidacaoRecebimento)
            };
        }

        private static NegocioRecebidoValidacaoResponse MapearValidacaoResponse(NegocioRecebidoValidacao validacao)
        {
            return new NegocioRecebidoValidacaoResponse
            {
                Id = validacao.Id,
                ConexaoEstrategicaId = validacao.ConexaoEstrategicaId,
                AssociadoReceptorId = validacao.AssociadoReceptorId,
                StatusConexao = validacao.StatusConexao,
                MotivoNegocioNaoFechado = validacao.MotivoNegocioNaoFechado,
                ValorNegocioFechado = validacao.ValorNegocioFechado,
                DataValidacao = validacao.DataValidacao,
                PrazoEstourado = validacao.PrazoEstourado,
                DataPrazoEstourado = validacao.DataPrazoEstourado
            };
        }

        private static ParceriaAssociadoResponse MapearParceriaResponse(ParceriaAssociado parceria)
        {
            return new ParceriaAssociadoResponse
            {
                Id = parceria.Id,
                AssociadoOrigemId = parceria.AssociadoOrigemId,
                AssociadoDestinoId = parceria.AssociadoDestinoId,
                DataParceria = parceria.DataParceria,
                Ativa = parceria.Ativa
            };
        }

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

        private static MensagemResponse CriarMensagemErro(string mensagem)
        {
            return new MensagemResponse
            {
                Sucesso = false,
                Mensagem = mensagem
            };
        }
    }
}
