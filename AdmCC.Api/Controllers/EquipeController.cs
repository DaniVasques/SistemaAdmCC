using AdmCC.Api.Models.Requests.Equipes;
using AdmCC.Api.Models.Responses;
using AdmCC.Api.Models.Responses.Equipes;
using AdmCC.Domain.Equipes.Entities;
using AdmCC.InfraData.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdmCC.Api.Controllers
{
    [ApiController]
    [Route("api/equipes")]
    [Produces("application/json")]
    public class EquipeController : ControllerBase
    {
        private readonly EquipesService _equipesService;

        public EquipeController(EquipesService equipesService)
        {
            _equipesService = equipesService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(EquipeResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EquipeResponse>> CriarAsync(
            [FromBody] CriarEquipeRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var equipe = await _equipesService.CriarEquipeAsync(MapearEquipe(request), cancellationToken);
                return CreatedAtAction(nameof(ObterPorIdAsync), new { id = equipe.Id }, MapearEquipeResponse(equipe));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<EquipeResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<EquipeResponse>>> ListarAsync(CancellationToken cancellationToken)
        {
            var equipes = await _equipesService.ListarEquipesAsync(cancellationToken);
            return Ok(equipes.Select(MapearEquipeResponse).ToArray());
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(EquipeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EquipeResponse>> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var equipe = await _equipesService.ObterEquipePorIdAsync(id, cancellationToken);

            if (equipe is null)
            {
                return NotFound(new MensagemResponse
                {
                    Sucesso = false,
                    Mensagem = "Equipe nao encontrada."
                });
            }

            return Ok(MapearEquipeResponse(equipe));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(EquipeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EquipeResponse>> AtualizarAsync(
            Guid id,
            [FromBody] AtualizarEquipeRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var equipe = await _equipesService.AtualizarEquipeAsync(MapearEquipe(id, request), cancellationToken);
                return Ok(MapearEquipeResponse(equipe));
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
                await _equipesService.ExcluirEquipeAsync(id, cancellationToken);
                return Ok(new MensagemResponse
                {
                    Sucesso = true,
                    Mensagem = "Equipe excluida com sucesso."
                });
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPost("{id:guid}/ocorrencias")]
        [ProducesResponseType(typeof(OcorrenciaReuniaoEquipeResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OcorrenciaReuniaoEquipeResponse>> CriarOcorrenciaAsync(
            Guid id,
            [FromBody] CriarOcorrenciaReuniaoEquipeRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var ocorrencia = await _equipesService.CriarOcorrenciaAsync(MapearOcorrencia(id, request), cancellationToken);
                return CreatedAtAction(nameof(ListarOcorrenciasAsync), new { id }, MapearOcorrenciaResponse(ocorrencia));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("{id:guid}/ocorrencias")]
        [ProducesResponseType(typeof(IReadOnlyCollection<OcorrenciaReuniaoEquipeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyCollection<OcorrenciaReuniaoEquipeResponse>>> ListarOcorrenciasAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            try
            {
                var ocorrencias = await _equipesService.ListarOcorrenciasPorEquipeAsync(id, cancellationToken);
                return Ok(ocorrencias.Select(MapearOcorrenciaResponse).ToArray());
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPost("ocorrencias/{id:guid}/presencas")]
        [ProducesResponseType(typeof(PresencaReuniaoEquipeResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PresencaReuniaoEquipeResponse>> RegistrarPresencaAsync(
            Guid id,
            [FromBody] RegistrarPresencaReuniaoEquipeRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var presenca = await _equipesService.RegistrarPresencaAsync(id, MapearPresenca(request), cancellationToken);
                return CreatedAtAction(nameof(ObterOcorrenciaPorIdAsync), new { id }, MapearPresencaResponse(presenca));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("ocorrencias/{id:guid}")]
        [ProducesResponseType(typeof(OcorrenciaReuniaoEquipeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OcorrenciaReuniaoEquipeResponse>> ObterOcorrenciaPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var ocorrencia = await _equipesService.ObterOcorrenciaPorIdAsync(id, cancellationToken);

            if (ocorrencia is null)
            {
                return NotFound(new MensagemResponse
                {
                    Sucesso = false,
                    Mensagem = "Ocorrencia de reuniao nao encontrada."
                });
            }

            return Ok(MapearOcorrenciaResponse(ocorrencia));
        }

        private ActionResult CriarRespostaErro(Exception exception)
        {
            var response = new MensagemResponse
            {
                Sucesso = false,
                Mensagem = exception.Message
            };

            return exception switch
            {
                KeyNotFoundException => NotFound(response),
                ArgumentNullException => BadRequest(response),
                ArgumentException => BadRequest(response),
                InvalidOperationException => BadRequest(response),
                _ => StatusCode(StatusCodes.Status500InternalServerError, new MensagemResponse
                {
                    Sucesso = false,
                    Mensagem = "Ocorreu um erro inesperado ao processar a solicitacao."
                })
            };
        }

        private static Equipe MapearEquipe(CriarEquipeRequest request)
        {
            return new Equipe
            {
                NomeEquipe = request.NomeEquipe.Trim(),
                DataInicioFormacao = request.DataInicioFormacao,
                DataPrevisaoLancamento = request.DataPrevisaoLancamento,
                DataEfetivaLancamento = request.DataEfetivaLancamento,
                StatusEquipe = request.StatusEquipe,
                DiaReuniaoEquipe = request.DiaReuniaoEquipe,
                HorarioReuniao = request.HorarioReuniao,
                ModeloReuniaoDeEquipe = request.ModeloReuniaoDeEquipe,
                LocalReuniaoPresencialId = request.LocalReuniaoPresencialId,
                LinkReuniaoOnline = request.LinkReuniaoOnline.Trim(),
                NumeroComponentesAtivos = request.NumeroComponentesAtivos,
                PontuacaoMensalAtual = request.PontuacaoMensalAtual
            };
        }

        private static Equipe MapearEquipe(Guid id, AtualizarEquipeRequest request)
        {
            return new Equipe
            {
                Id = id,
                NomeEquipe = request.NomeEquipe.Trim(),
                DataInicioFormacao = request.DataInicioFormacao,
                DataPrevisaoLancamento = request.DataPrevisaoLancamento,
                DataEfetivaLancamento = request.DataEfetivaLancamento,
                StatusEquipe = request.StatusEquipe,
                DiaReuniaoEquipe = request.DiaReuniaoEquipe,
                HorarioReuniao = request.HorarioReuniao,
                ModeloReuniaoDeEquipe = request.ModeloReuniaoDeEquipe,
                LocalReuniaoPresencialId = request.LocalReuniaoPresencialId,
                LinkReuniaoOnline = request.LinkReuniaoOnline.Trim(),
                NumeroComponentesAtivos = request.NumeroComponentesAtivos,
                PontuacaoMensalAtual = request.PontuacaoMensalAtual
            };
        }

        private static OcorrenciaReuniaoEquipe MapearOcorrencia(Guid equipeId, CriarOcorrenciaReuniaoEquipeRequest request)
        {
            return new OcorrenciaReuniaoEquipe
            {
                EquipeId = equipeId,
                DataReuniao = request.DataReuniao,
                NumeroOcorrenciaNoMes = request.NumeroOcorrenciaNoMes,
                EhPresencial = request.EhPresencial,
                Realizada = request.Realizada
            };
        }

        private static PresencaReuniaoEquipe MapearPresenca(RegistrarPresencaReuniaoEquipeRequest request)
        {
            return new PresencaReuniaoEquipe
            {
                AssociadoId = request.AssociadoId,
                Presente = request.Presente,
                DataRegistro = request.DataRegistro ?? default
            };
        }

        private static EquipeResponse MapearEquipeResponse(Equipe equipe)
        {
            return new EquipeResponse
            {
                Id = equipe.Id,
                NomeEquipe = equipe.NomeEquipe,
                DataInicioFormacao = equipe.DataInicioFormacao,
                DataPrevisaoLancamento = equipe.DataPrevisaoLancamento,
                DataEfetivaLancamento = equipe.DataEfetivaLancamento,
                StatusEquipe = equipe.StatusEquipe,
                DiaReuniaoEquipe = equipe.DiaReuniaoEquipe,
                HorarioReuniao = equipe.HorarioReuniao,
                ModeloReuniaoDeEquipe = equipe.ModeloReuniaoDeEquipe,
                LocalReuniaoPresencialId = equipe.LocalReuniaoPresencialId,
                LinkReuniaoOnline = equipe.LinkReuniaoOnline,
                NumeroComponentesAtivos = equipe.NumeroComponentesAtivos,
                PontuacaoMensalAtual = equipe.PontuacaoMensalAtual,
                QuantidadeOcorrencias = equipe.OcorrenciasReuniao.Count
            };
        }

        private static OcorrenciaReuniaoEquipeResponse MapearOcorrenciaResponse(OcorrenciaReuniaoEquipe ocorrencia)
        {
            return new OcorrenciaReuniaoEquipeResponse
            {
                Id = ocorrencia.Id,
                EquipeId = ocorrencia.EquipeId,
                NomeEquipe = ocorrencia.Equipe?.NomeEquipe,
                DataReuniao = ocorrencia.DataReuniao,
                NumeroOcorrenciaNoMes = ocorrencia.NumeroOcorrenciaNoMes,
                EhPresencial = ocorrencia.EhPresencial,
                Realizada = ocorrencia.Realizada,
                Presencas = ocorrencia.Presencas.Select(MapearPresencaResponse).ToArray()
            };
        }

        private static PresencaReuniaoEquipeResponse MapearPresencaResponse(PresencaReuniaoEquipe presenca)
        {
            return new PresencaReuniaoEquipeResponse
            {
                Id = presenca.Id,
                OcorrenciaReuniaoEquipeId = presenca.OcorrenciaReuniaoEquipeId,
                AssociadoId = presenca.AssociadoId,
                NomeAssociado = presenca.Associado?.NomeCompleto,
                Presente = presenca.Presente,
                DataRegistro = presenca.DataRegistro
            };
        }
    }
}
