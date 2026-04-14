using AdmCC.Api.Models.Requests.VisitantesSubstitutos;
using AdmCC.Api.Models.Responses;
using AdmCC.Api.Models.Responses.VisitantesSubstitutos;
using AdmCC.Domain.VisitantesSubstitutos.Entities;
using AdmCC.InfraData.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdmCC.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class VisitantesSubstitutosController : ControllerBase
    {
        private readonly VisitantesSubstitutosService _service;

        public VisitantesSubstitutosController(VisitantesSubstitutosService service)
        {
            _service = service;
        }

        [HttpPost("api/visitantes-externos")]
        [ProducesResponseType(typeof(VisitanteExternoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VisitanteExternoResponse>> CriarVisitanteExternoAsync(
            [FromBody] CriarVisitanteExternoRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var visitante = await _service.CriarVisitanteExternoAsync(MapearVisitanteExterno(request), cancellationToken);
                return CreatedAtAction(nameof(ObterVisitanteExternoPorIdAsync), new { id = visitante.Id }, MapearVisitanteExternoResponse(visitante));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("api/visitantes-externos")]
        [ProducesResponseType(typeof(IReadOnlyCollection<VisitanteExternoResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<VisitanteExternoResponse>>> ListarVisitantesExternosAsync(CancellationToken cancellationToken)
        {
            var visitantes = await _service.ListarVisitantesExternosAsync(cancellationToken);
            return Ok(visitantes.Select(MapearVisitanteExternoResponse).ToArray());
        }

        [HttpGet("api/visitantes-externos/{id:guid}")]
        [ProducesResponseType(typeof(VisitanteExternoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VisitanteExternoResponse>> ObterVisitanteExternoPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var visitante = await _service.ObterVisitanteExternoPorIdAsync(id, cancellationToken);
                return visitante is null
                    ? NotFound(CriarMensagemErro("Visitante externo nao encontrado."))
                    : Ok(MapearVisitanteExternoResponse(visitante));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPut("api/visitantes-externos/{id:guid}")]
        [ProducesResponseType(typeof(VisitanteExternoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VisitanteExternoResponse>> AtualizarVisitanteExternoAsync(
            Guid id,
            [FromBody] AtualizarVisitanteExternoRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var visitante = await _service.AtualizarVisitanteExternoAsync(MapearVisitanteExterno(id, request), cancellationToken);
                return Ok(MapearVisitanteExternoResponse(visitante));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpDelete("api/visitantes-externos/{id:guid}")]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MensagemResponse>> ExcluirVisitanteExternoAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _service.ExcluirVisitanteExternoAsync(id, cancellationToken);
                return Ok(new MensagemResponse { Sucesso = true, Mensagem = "Visitante externo excluido com sucesso." });
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPost("api/visitas-internas")]
        [ProducesResponseType(typeof(VisitaInternaResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VisitaInternaResponse>> CriarVisitaInternaAsync(
            [FromBody] CriarVisitaInternaRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var visita = await _service.CriarVisitaInternaAsync(MapearVisitaInterna(request), cancellationToken);
                return CreatedAtAction(nameof(ObterVisitaInternaPorIdAsync), new { id = visita.Id }, MapearVisitaInternaResponse(visita));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("api/visitas-internas")]
        [ProducesResponseType(typeof(IReadOnlyCollection<VisitaInternaResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<VisitaInternaResponse>>> ListarVisitasInternasAsync(CancellationToken cancellationToken)
        {
            var visitas = await _service.ListarVisitasInternasAsync(cancellationToken);
            return Ok(visitas.Select(MapearVisitaInternaResponse).ToArray());
        }

        [HttpGet("api/visitas-internas/{id:guid}")]
        [ProducesResponseType(typeof(VisitaInternaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VisitaInternaResponse>> ObterVisitaInternaPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var visita = await _service.ObterVisitaInternaPorIdAsync(id, cancellationToken);
                return visita is null
                    ? NotFound(CriarMensagemErro("Visita interna nao encontrada."))
                    : Ok(MapearVisitaInternaResponse(visita));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPut("api/visitas-internas/{id:guid}")]
        [ProducesResponseType(typeof(VisitaInternaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VisitaInternaResponse>> AtualizarVisitaInternaAsync(
            Guid id,
            [FromBody] AtualizarVisitaInternaRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var visita = await _service.AtualizarVisitaInternaAsync(MapearVisitaInterna(id, request), cancellationToken);
                return Ok(MapearVisitaInternaResponse(visita));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpDelete("api/visitas-internas/{id:guid}")]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MensagemResponse>> ExcluirVisitaInternaAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _service.ExcluirVisitaInternaAsync(id, cancellationToken);
                return Ok(new MensagemResponse { Sucesso = true, Mensagem = "Visita interna excluida com sucesso." });
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPost("api/substitutos-associados")]
        [ProducesResponseType(typeof(SubstitutoAssociadoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubstitutoAssociadoResponse>> CriarSubstitutoAssociadoAsync(
            [FromBody] CriarSubstitutoAssociadoRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var substituto = await _service.CriarSubstitutoAssociadoAsync(MapearSubstitutoAssociado(request), cancellationToken);
                return CreatedAtAction(nameof(ObterSubstitutoAssociadoPorIdAsync), new { id = substituto.Id }, MapearSubstitutoAssociadoResponse(substituto));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("api/substitutos-associados")]
        [ProducesResponseType(typeof(IReadOnlyCollection<SubstitutoAssociadoResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<SubstitutoAssociadoResponse>>> ListarSubstitutosAssociadosAsync(CancellationToken cancellationToken)
        {
            var substitutos = await _service.ListarSubstitutosAssociadosAsync(cancellationToken);
            return Ok(substitutos.Select(MapearSubstitutoAssociadoResponse).ToArray());
        }

        [HttpGet("api/substitutos-associados/{id:guid}")]
        [ProducesResponseType(typeof(SubstitutoAssociadoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SubstitutoAssociadoResponse>> ObterSubstitutoAssociadoPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var substituto = await _service.ObterSubstitutoAssociadoPorIdAsync(id, cancellationToken);
                return substituto is null
                    ? NotFound(CriarMensagemErro("Substituto associado nao encontrado."))
                    : Ok(MapearSubstitutoAssociadoResponse(substituto));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpDelete("api/substitutos-associados/{id:guid}")]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MensagemResponse>> ExcluirSubstitutoAssociadoAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _service.ExcluirSubstitutoAssociadoAsync(id, cancellationToken);
                return Ok(new MensagemResponse { Sucesso = true, Mensagem = "Substituto associado excluido com sucesso." });
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPost("api/substitutos-externos")]
        [ProducesResponseType(typeof(SubstitutoExternoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubstitutoExternoResponse>> CriarSubstitutoExternoAsync(
            [FromBody] CriarSubstitutoExternoRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var substituto = await _service.CriarSubstitutoExternoAsync(MapearSubstitutoExterno(request), cancellationToken);
                return CreatedAtAction(nameof(ObterSubstitutoExternoPorIdAsync), new { id = substituto.Id }, MapearSubstitutoExternoResponse(substituto));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("api/substitutos-externos")]
        [ProducesResponseType(typeof(IReadOnlyCollection<SubstitutoExternoResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<SubstitutoExternoResponse>>> ListarSubstitutosExternosAsync(CancellationToken cancellationToken)
        {
            var substitutos = await _service.ListarSubstitutosExternosAsync(cancellationToken);
            return Ok(substitutos.Select(MapearSubstitutoExternoResponse).ToArray());
        }

        [HttpGet("api/substitutos-externos/{id:guid}")]
        [ProducesResponseType(typeof(SubstitutoExternoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SubstitutoExternoResponse>> ObterSubstitutoExternoPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var substituto = await _service.ObterSubstitutoExternoPorIdAsync(id, cancellationToken);
                return substituto is null
                    ? NotFound(CriarMensagemErro("Substituto externo nao encontrado."))
                    : Ok(MapearSubstitutoExternoResponse(substituto));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpDelete("api/substitutos-externos/{id:guid}")]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MensagemResponse>> ExcluirSubstitutoExternoAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _service.ExcluirSubstitutoExternoAsync(id, cancellationToken);
                return Ok(new MensagemResponse { Sucesso = true, Mensagem = "Substituto externo excluido com sucesso." });
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        private static VisitanteExterno MapearVisitanteExterno(CriarVisitanteExternoRequest request)
        {
            return new VisitanteExterno
            {
                OcorrenciaReuniaoEquipeId = request.OcorrenciaReuniaoEquipeId,
                AssociadoResponsavelId = request.AssociadoResponsavelId,
                TipoPessoa = request.TipoPessoa,
                NomeCompleto = request.NomeCompleto.Trim(),
                TelefonePrincipal = request.TelefonePrincipal.Trim(),
                Email = string.IsNullOrWhiteSpace(request.Email) ? null : request.Email.Trim(),
                Cpf = string.IsNullOrWhiteSpace(request.Cpf) ? null : request.Cpf.Trim(),
                NomeEmpresa = string.IsNullOrWhiteSpace(request.NomeEmpresa) ? string.Empty : request.NomeEmpresa.Trim()
            };
        }

        private static VisitanteExterno MapearVisitanteExterno(Guid id, AtualizarVisitanteExternoRequest request)
        {
            return new VisitanteExterno
            {
                Id = id,
                OcorrenciaReuniaoEquipeId = request.OcorrenciaReuniaoEquipeId,
                AssociadoResponsavelId = request.AssociadoResponsavelId,
                TipoPessoa = request.TipoPessoa,
                NomeCompleto = request.NomeCompleto.Trim(),
                TelefonePrincipal = request.TelefonePrincipal.Trim(),
                Email = string.IsNullOrWhiteSpace(request.Email) ? null : request.Email.Trim(),
                Cpf = string.IsNullOrWhiteSpace(request.Cpf) ? null : request.Cpf.Trim(),
                NomeEmpresa = string.IsNullOrWhiteSpace(request.NomeEmpresa) ? string.Empty : request.NomeEmpresa.Trim()
            };
        }

        private static VisitaInterna MapearVisitaInterna(CriarVisitaInternaRequest request)
        {
            return new VisitaInterna
            {
                OcorrenciaReuniaoEquipeId = request.OcorrenciaReuniaoEquipeId,
                AssociadoVisitanteId = request.AssociadoVisitanteId
            };
        }

        private static VisitaInterna MapearVisitaInterna(Guid id, AtualizarVisitaInternaRequest request)
        {
            return new VisitaInterna
            {
                Id = id,
                OcorrenciaReuniaoEquipeId = request.OcorrenciaReuniaoEquipeId,
                AssociadoVisitanteId = request.AssociadoVisitanteId
            };
        }

        private static SubstitutoAssociado MapearSubstitutoAssociado(CriarSubstitutoAssociadoRequest request)
        {
            return new SubstitutoAssociado
            {
                OcorrenciaReuniaoEquipeId = request.OcorrenciaReuniaoEquipeId,
                AssociadoTitularId = request.AssociadoTitularId,
                AssociadoSubstitutoId = request.AssociadoSubstitutoId
            };
        }

        private static SubstitutoExterno MapearSubstitutoExterno(CriarSubstitutoExternoRequest request)
        {
            return new SubstitutoExterno
            {
                OcorrenciaReuniaoEquipeId = request.OcorrenciaReuniaoEquipeId,
                AssociadoTitularId = request.AssociadoTitularId,
                TipoPessoa = request.TipoPessoa,
                NomeCompleto = request.NomeCompleto.Trim(),
                TelefonePrincipal = request.TelefonePrincipal.Trim(),
                Email = string.IsNullOrWhiteSpace(request.Email) ? null : request.Email.Trim(),
                Cpf = string.IsNullOrWhiteSpace(request.Cpf) ? null : request.Cpf.Trim(),
                NomeEmpresa = string.IsNullOrWhiteSpace(request.NomeEmpresa) ? null : request.NomeEmpresa.Trim()
            };
        }

        private static VisitanteExternoResponse MapearVisitanteExternoResponse(VisitanteExterno visitante)
        {
            return new VisitanteExternoResponse
            {
                Id = visitante.Id,
                OcorrenciaReuniaoEquipeId = visitante.OcorrenciaReuniaoEquipeId,
                NomeEquipe = visitante.OcorrenciaReuniaoEquipe?.Equipe?.NomeEquipe,
                AssociadoResponsavelId = visitante.AssociadoResponsavelId,
                NomeAssociadoResponsavel = visitante.AssociadoResponsavel?.NomeCompleto,
                TipoPessoa = visitante.TipoPessoa,
                NomeCompleto = visitante.NomeCompleto,
                TelefonePrincipal = visitante.TelefonePrincipal,
                Email = visitante.Email,
                Cpf = visitante.Cpf,
                NomeEmpresa = visitante.NomeEmpresa,
                StatusValidacaoPresenca = visitante.StatusValidacaoPresenca,
                DataCadastro = visitante.DataCadastro,
                DataValidacao = visitante.DataValidacao
            };
        }

        private static VisitaInternaResponse MapearVisitaInternaResponse(VisitaInterna visita)
        {
            return new VisitaInternaResponse
            {
                Id = visita.Id,
                OcorrenciaReuniaoEquipeId = visita.OcorrenciaReuniaoEquipeId,
                NomeEquipe = visita.OcorrenciaReuniaoEquipe?.Equipe?.NomeEquipe,
                AssociadoVisitanteId = visita.AssociadoVisitanteId,
                NomeAssociadoVisitante = visita.AssociadoVisitante?.NomeCompleto,
                StatusValidacaoPresenca = visita.StatusValidacaoPresenca,
                DataCadastro = visita.DataCadastro,
                DataValidacao = visita.DataValidacao
            };
        }

        private static SubstitutoAssociadoResponse MapearSubstitutoAssociadoResponse(SubstitutoAssociado substituto)
        {
            return new SubstitutoAssociadoResponse
            {
                Id = substituto.Id,
                OcorrenciaReuniaoEquipeId = substituto.OcorrenciaReuniaoEquipeId,
                NomeEquipe = substituto.OcorrenciaReuniaoEquipe?.Equipe?.NomeEquipe,
                AssociadoTitularId = substituto.AssociadoTitularId,
                NomeAssociadoTitular = substituto.AssociadoTitular?.NomeCompleto,
                AssociadoSubstitutoId = substituto.AssociadoSubstitutoId,
                NomeAssociadoSubstituto = substituto.AssociadoSubstituto?.NomeCompleto,
                StatusValidacaoPresenca = substituto.StatusValidacaoPresenca,
                DataCadastro = substituto.DataCadastro,
                DataValidacao = substituto.DataValidacao
            };
        }

        private static SubstitutoExternoResponse MapearSubstitutoExternoResponse(SubstitutoExterno substituto)
        {
            return new SubstitutoExternoResponse
            {
                Id = substituto.Id,
                OcorrenciaReuniaoEquipeId = substituto.OcorrenciaReuniaoEquipeId,
                NomeEquipe = substituto.OcorrenciaReuniaoEquipe?.Equipe?.NomeEquipe,
                AssociadoTitularId = substituto.AssociadoTitularId,
                NomeAssociadoTitular = substituto.AssociadoTitular?.NomeCompleto,
                TipoPessoa = substituto.TipoPessoa,
                NomeCompleto = substituto.NomeCompleto,
                TelefonePrincipal = substituto.TelefonePrincipal,
                Email = substituto.Email,
                Cpf = substituto.Cpf,
                NomeEmpresa = substituto.NomeEmpresa,
                StatusValidacaoPresenca = substituto.StatusValidacaoPresenca,
                DataCadastro = substituto.DataCadastro,
                DataValidacao = substituto.DataValidacao
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
