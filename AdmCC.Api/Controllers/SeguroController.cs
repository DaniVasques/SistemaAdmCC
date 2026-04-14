using AdmCC.Api.Models.Requests.Seguro;
using AdmCC.Api.Models.Responses;
using AdmCC.Api.Models.Responses.Seguro;
using AdmCC.Domain.Seguro.Entities;
using AdmCC.InfraData.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdmCC.Api.Controllers
{
    [ApiController]
    [Route("api/seguros")]
    [Produces("application/json")]
    public class SeguroController : ControllerBase
    {
        private readonly SeguroService _seguroService;

        public SeguroController(SeguroService seguroService)
        {
            _seguroService = seguroService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SeguroAssociadoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SeguroAssociadoResponse>> CriarAsync([FromBody] CriarSeguroAssociadoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var seguro = await _seguroService.CriarSeguroAsync(MapearSeguro(request), cancellationToken);
                return CreatedAtAction(nameof(ObterPorIdAsync), new { id = seguro.Id }, MapearSeguroResponse(seguro));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<SeguroAssociadoResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<SeguroAssociadoResponse>>> ListarAsync(CancellationToken cancellationToken)
        {
            var seguros = await _seguroService.ListarSegurosAsync(cancellationToken);
            return Ok(seguros.Select(MapearSeguroResponse).ToArray());
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(SeguroAssociadoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SeguroAssociadoResponse>> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var seguro = await _seguroService.ObterSeguroPorIdAsync(id, cancellationToken);
            if (seguro is null)
            {
                return NotFound(CriarMensagemErro("Seguro do associado nao encontrado."));
            }

            return Ok(MapearSeguroResponse(seguro));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(SeguroAssociadoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SeguroAssociadoResponse>> AtualizarAsync(Guid id, [FromBody] AtualizarSeguroAssociadoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var seguro = await _seguroService.AtualizarSeguroAsync(MapearSeguro(id, request), cancellationToken);
                return Ok(MapearSeguroResponse(seguro));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MensagemResponse>> ExcluirAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _seguroService.ExcluirSeguroAsync(id, cancellationToken);
                return Ok(new MensagemResponse { Sucesso = true, Mensagem = "Seguro excluido com sucesso." });
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPost("{id:guid}/beneficiarios")]
        [ProducesResponseType(typeof(BeneficiarioSeguroResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BeneficiarioSeguroResponse>> AdicionarBeneficiarioAsync(Guid id, [FromBody] CriarBeneficiarioSeguroRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var beneficiario = await _seguroService.AdicionarBeneficiarioAsync(id, MapearBeneficiario(request), cancellationToken);
                return CreatedAtAction(nameof(ListarBeneficiariosAsync), new { id }, MapearBeneficiarioResponse(beneficiario));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("{id:guid}/beneficiarios")]
        [ProducesResponseType(typeof(IReadOnlyCollection<BeneficiarioSeguroResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<BeneficiarioSeguroResponse>>> ListarBeneficiariosAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var beneficiarios = await _seguroService.ListarBeneficiariosAsync(id, cancellationToken);
                return Ok(beneficiarios.Select(MapearBeneficiarioResponse).ToArray());
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPut("{id:guid}/beneficiarios/{beneficiarioId:guid}")]
        [ProducesResponseType(typeof(BeneficiarioSeguroResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BeneficiarioSeguroResponse>> AtualizarBeneficiarioAsync(
            Guid id,
            Guid beneficiarioId,
            [FromBody] AtualizarBeneficiarioSeguroRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var beneficiario = await _seguroService.AtualizarBeneficiarioAsync(id, beneficiarioId, MapearBeneficiario(request), cancellationToken);
                return Ok(MapearBeneficiarioResponse(beneficiario));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpDelete("{id:guid}/beneficiarios/{beneficiarioId:guid}")]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MensagemResponse>> RemoverBeneficiarioAsync(Guid id, Guid beneficiarioId, CancellationToken cancellationToken)
        {
            try
            {
                await _seguroService.RemoverBeneficiarioAsync(id, beneficiarioId, cancellationToken);
                return Ok(new MensagemResponse { Sucesso = true, Mensagem = "Beneficiario removido com sucesso." });
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPost("{id:guid}/contato-emergencia")]
        [ProducesResponseType(typeof(ContatoEmergenciaResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ContatoEmergenciaResponse>> CriarContatoEmergenciaAsync(Guid id, [FromBody] CriarContatoEmergenciaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var contato = await _seguroService.DefinirContatoEmergenciaAsync(id, MapearContato(request), cancellationToken);
                return CreatedAtAction(nameof(ObterContatoEmergenciaAsync), new { id }, MapearContatoResponse(contato));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("{id:guid}/contato-emergencia")]
        [ProducesResponseType(typeof(ContatoEmergenciaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContatoEmergenciaResponse>> ObterContatoEmergenciaAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var contato = await _seguroService.ObterContatoEmergenciaAsync(id, cancellationToken);
                if (contato is null)
                {
                    return NotFound(CriarMensagemErro("Contato de emergencia nao encontrado."));
                }

                return Ok(MapearContatoResponse(contato));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPut("{id:guid}/contato-emergencia")]
        [ProducesResponseType(typeof(ContatoEmergenciaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContatoEmergenciaResponse>> AtualizarContatoEmergenciaAsync(Guid id, [FromBody] AtualizarContatoEmergenciaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var contato = await _seguroService.DefinirContatoEmergenciaAsync(id, MapearContato(request), cancellationToken);
                return Ok(MapearContatoResponse(contato));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPost("{id:guid}/solicitacoes-alteracao-beneficiario")]
        [ProducesResponseType(typeof(SolicitacaoAlteracaoBeneficiarioResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SolicitacaoAlteracaoBeneficiarioResponse>> CriarSolicitacaoAsync(Guid id, [FromBody] CriarSolicitacaoAlteracaoBeneficiarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var solicitacao = await _seguroService.SolicitarAlteracaoBeneficiariosAsync(id, request.ObservacaoSolicitante, cancellationToken);
                return CreatedAtAction(nameof(ListarSolicitacoesAsync), new { id }, MapearSolicitacaoResponse(solicitacao));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("{id:guid}/solicitacoes-alteracao-beneficiario")]
        [ProducesResponseType(typeof(IReadOnlyCollection<SolicitacaoAlteracaoBeneficiarioResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyCollection<SolicitacaoAlteracaoBeneficiarioResponse>>> ListarSolicitacoesAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var solicitacoes = await _seguroService.ListarSolicitacoesAlteracaoBeneficiarioAsync(id, cancellationToken);
                return Ok(solicitacoes.Select(MapearSolicitacaoResponse).ToArray());
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPost("{id:guid}/consentimento-lgpd")]
        [ProducesResponseType(typeof(ConsentimentoLgpdSeguroResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ConsentimentoLgpdSeguroResponse>> RegistrarConsentimentoAsync(Guid id, [FromBody] RegistrarConsentimentoLgpdSeguroRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var consentimento = await _seguroService.RegistrarConsentimentoLgpdAsync(id, MapearConsentimento(request), cancellationToken);
                return CreatedAtAction(nameof(ObterPorIdAsync), new { id }, MapearConsentimentoResponse(consentimento));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        private static SeguroAssociado MapearSeguro(CriarSeguroAssociadoRequest request)
        {
            return new SeguroAssociado
            {
                AssociadoId = request.AssociadoId,
                EstadoCivil = request.EstadoCivil,
                Profissao = request.Profissao.Trim(),
                Beneficiarios = request.Beneficiarios.Select(MapearBeneficiario).ToList(),
                ContatoEmergencia = request.ContatoEmergencia is null ? null : MapearContato(request.ContatoEmergencia),
                ConsentimentoLgpd = request.ConsentimentoLgpd is null ? null : MapearConsentimento(request.ConsentimentoLgpd)
            };
        }

        private static SeguroAssociado MapearSeguro(Guid id, AtualizarSeguroAssociadoRequest request)
        {
            return new SeguroAssociado
            {
                Id = id,
                AssociadoId = request.AssociadoId,
                EstadoCivil = request.EstadoCivil,
                Profissao = request.Profissao.Trim(),
                Beneficiarios = request.Beneficiarios.Select(MapearBeneficiario).ToList(),
                ContatoEmergencia = request.ContatoEmergencia is null ? null : MapearContato(request.ContatoEmergencia),
                ConsentimentoLgpd = request.ConsentimentoLgpd is null ? null : MapearConsentimento(request.ConsentimentoLgpd)
            };
        }

        private static BeneficiarioSeguro MapearBeneficiario(CriarBeneficiarioSeguroRequest request)
        {
            return new BeneficiarioSeguro
            {
                NomeCompleto = request.NomeCompleto.Trim(),
                Cpf = request.Cpf.Trim(),
                GrauParentesco = request.GrauParentesco.Trim(),
                Percentual = request.Percentual,
                Telefone = request.Telefone.Trim()
            };
        }

        private static BeneficiarioSeguro MapearBeneficiario(AtualizarBeneficiarioSeguroRequest request)
        {
            return new BeneficiarioSeguro
            {
                NomeCompleto = request.NomeCompleto.Trim(),
                Cpf = request.Cpf.Trim(),
                GrauParentesco = request.GrauParentesco.Trim(),
                Percentual = request.Percentual,
                Telefone = request.Telefone.Trim()
            };
        }

        private static ContatoEmergencia MapearContato(CriarContatoEmergenciaRequest request)
        {
            return new ContatoEmergencia
            {
                NomeCompleto = request.NomeCompleto.Trim(),
                TelefonePrincipal = request.TelefonePrincipal.Trim(),
                TelefoneSecundario = string.IsNullOrWhiteSpace(request.TelefoneSecundario) ? null : request.TelefoneSecundario.Trim()
            };
        }

        private static ContatoEmergencia MapearContato(AtualizarContatoEmergenciaRequest request)
        {
            return new ContatoEmergencia
            {
                NomeCompleto = request.NomeCompleto.Trim(),
                TelefonePrincipal = request.TelefonePrincipal.Trim(),
                TelefoneSecundario = string.IsNullOrWhiteSpace(request.TelefoneSecundario) ? null : request.TelefoneSecundario.Trim()
            };
        }

        private static ConsentimentoLgpdSeguro MapearConsentimento(RegistrarConsentimentoLgpdSeguroRequest request)
        {
            return new ConsentimentoLgpdSeguro
            {
                Aceito = request.Aceito,
                DataAceite = request.DataAceite,
                TextoConsentimento = request.TextoConsentimento.Trim()
            };
        }

        private static SeguroAssociadoResponse MapearSeguroResponse(SeguroAssociado seguro)
        {
            return new SeguroAssociadoResponse
            {
                Id = seguro.Id,
                AssociadoId = seguro.AssociadoId,
                EstadoCivil = seguro.EstadoCivil,
                Profissao = seguro.Profissao,
                DataCadastro = seguro.DataCadastro,
                Beneficiarios = seguro.Beneficiarios.Select(MapearBeneficiarioResponse).ToArray(),
                ContatoEmergencia = seguro.ContatoEmergencia is null ? null : MapearContatoResponse(seguro.ContatoEmergencia),
                ConsentimentoLgpd = seguro.ConsentimentoLgpd is null ? null : MapearConsentimentoResponse(seguro.ConsentimentoLgpd),
                SolicitacoesAlteracaoBeneficiario = seguro.SolicitacoesAlteracaoBeneficiario.Select(MapearSolicitacaoResponse).ToArray()
            };
        }

        private static BeneficiarioSeguroResponse MapearBeneficiarioResponse(BeneficiarioSeguro beneficiario)
        {
            return new BeneficiarioSeguroResponse
            {
                Id = beneficiario.Id,
                SeguroAssociadoId = beneficiario.SeguroAssociadoId,
                NomeCompleto = beneficiario.NomeCompleto,
                Cpf = beneficiario.Cpf,
                GrauParentesco = beneficiario.GrauParentesco,
                Percentual = beneficiario.Percentual,
                Telefone = beneficiario.Telefone
            };
        }

        private static ContatoEmergenciaResponse MapearContatoResponse(ContatoEmergencia contato)
        {
            return new ContatoEmergenciaResponse
            {
                Id = contato.Id,
                SeguroAssociadoId = contato.SeguroAssociadoId,
                NomeCompleto = contato.NomeCompleto,
                TelefonePrincipal = contato.TelefonePrincipal,
                TelefoneSecundario = contato.TelefoneSecundario
            };
        }

        private static ConsentimentoLgpdSeguroResponse MapearConsentimentoResponse(ConsentimentoLgpdSeguro consentimento)
        {
            return new ConsentimentoLgpdSeguroResponse
            {
                Id = consentimento.Id,
                SeguroAssociadoId = consentimento.SeguroAssociadoId,
                Aceito = consentimento.Aceito,
                DataAceite = consentimento.DataAceite,
                TextoConsentimento = consentimento.TextoConsentimento
            };
        }

        private static SolicitacaoAlteracaoBeneficiarioResponse MapearSolicitacaoResponse(SolicitacaoAlteracaoBeneficiario solicitacao)
        {
            return new SolicitacaoAlteracaoBeneficiarioResponse
            {
                Id = solicitacao.Id,
                SeguroAssociadoId = solicitacao.SeguroAssociadoId,
                DataSolicitacao = solicitacao.DataSolicitacao,
                StatusSolicitacaoBeneficiario = solicitacao.StatusSolicitacaoBeneficiario,
                ObservacaoSolicitante = solicitacao.ObservacaoSolicitante,
                ObservacaoAnalise = solicitacao.ObservacaoAnalise,
                DataConclusao = solicitacao.DataConclusao
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
