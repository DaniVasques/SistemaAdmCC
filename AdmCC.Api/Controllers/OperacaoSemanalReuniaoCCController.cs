using AdmCC.Api.Models.Requests.OperacaoSemanalReuniaoCC;
using AdmCC.Api.Models.Responses;
using AdmCC.Api.Models.Responses.OperacaoSemanalReuniaoCC;
using AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities;
using AdmCC.InfraData.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdmCC.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class OperacaoSemanalReuniaoCCController : ControllerBase
    {
        private readonly OperacaoSemanalReuniaoCCService _service;

        public OperacaoSemanalReuniaoCCController(OperacaoSemanalReuniaoCCService service)
        {
            _service = service;
        }

        [HttpPost("api/ciclos-semanais")]
        public async Task<ActionResult<CicloSemanalResponse>> CriarCicloAsync([FromBody] CriarCicloSemanalRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var ciclo = await _service.CriarCicloAsync(MapearCiclo(request), cancellationToken);
                return CreatedAtAction(nameof(ObterCicloPorIdAsync), new { id = ciclo.Id }, Mapear(ciclo));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("api/ciclos-semanais")]
        public async Task<ActionResult<IReadOnlyCollection<CicloSemanalResponse>>> ListarCiclosAsync(CancellationToken cancellationToken)
        {
            var ciclos = await _service.ListarCiclosAsync(cancellationToken);
            return Ok(ciclos.Select(Mapear).ToArray());
        }

        [HttpGet("api/ciclos-semanais/{id:guid}")]
        public async Task<ActionResult<CicloSemanalResponse>> ObterCicloPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var ciclo = await _service.ObterCicloPorIdAsync(id, cancellationToken);
            return ciclo is null ? NotFound(CriarMensagemErro("Ciclo semanal nao encontrado.")) : Ok(Mapear(ciclo));
        }

        [HttpPut("api/ciclos-semanais/{id:guid}")]
        public async Task<ActionResult<CicloSemanalResponse>> AtualizarCicloAsync(Guid id, [FromBody] AtualizarCicloSemanalRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var ciclo = await _service.AtualizarCicloAsync(MapearCiclo(id, request), cancellationToken);
                return Ok(Mapear(ciclo));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPost("api/reunioes-cc")]
        public async Task<ActionResult<ReuniaoCCResponse>> CriarReuniaoAsync([FromBody] CriarReuniaoCCRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var reuniao = await _service.CriarReuniaoAsync(MapearReuniao(request), cancellationToken);
                return CreatedAtAction(nameof(ObterReuniaoPorIdAsync), new { id = reuniao.Id }, Mapear(reuniao));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("api/reunioes-cc")]
        public async Task<ActionResult<IReadOnlyCollection<ReuniaoCCResponse>>> ListarReunioesAsync(CancellationToken cancellationToken)
        {
            var reunioes = await _service.ListarReunioesAsync(cancellationToken);
            return Ok(reunioes.Select(Mapear).ToArray());
        }

        [HttpGet("api/reunioes-cc/{id:guid}")]
        public async Task<ActionResult<ReuniaoCCResponse>> ObterReuniaoPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var reuniao = await _service.ObterReuniaoPorIdAsync(id, cancellationToken);
            return reuniao is null ? NotFound(CriarMensagemErro("Reuniao CC nao encontrada.")) : Ok(Mapear(reuniao));
        }

        [HttpPut("api/reunioes-cc/{id:guid}")]
        public async Task<ActionResult<ReuniaoCCResponse>> AtualizarReuniaoAsync(Guid id, [FromBody] AtualizarReuniaoCCRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var reuniao = await _service.AtualizarReuniaoAsync(MapearReuniao(id, request), cancellationToken);
                return Ok(Mapear(reuniao));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpDelete("api/reunioes-cc/{id:guid}")]
        public async Task<ActionResult<MensagemResponse>> ExcluirReuniaoAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _service.ExcluirReuniaoAsync(id, cancellationToken);
                return Ok(new MensagemResponse { Sucesso = true, Mensagem = "Reuniao CC excluida com sucesso." });
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPost("api/reunioes-cc/{id:guid}/validacoes")]
        [ProducesResponseType(typeof(ValidacaoReuniaoCCResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ValidacaoReuniaoCCResponse>> CriarValidacaoAsync(Guid id, [FromBody] CriarValidacaoReuniaoCCRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validacao = await _service.CriarValidacaoAsync(id, MapearValidacao(id, request), cancellationToken);
                return CreatedAtAction(nameof(ListarValidacoesAsync), new { id }, Mapear(validacao));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("api/reunioes-cc/{id:guid}/validacoes")]
        [ProducesResponseType(typeof(IReadOnlyCollection<ValidacaoReuniaoCCResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyCollection<ValidacaoReuniaoCCResponse>>> ListarValidacoesAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var validacoes = await _service.ListarValidacoesPorReuniaoAsync(id, cancellationToken);
                return Ok(validacoes.Select(Mapear).ToArray());
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPost("api/reunioes-cc/{id:guid}/prospects")]
        [ProducesResponseType(typeof(ProspectReuniaoCCResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProspectReuniaoCCResponse>> CriarProspectAsync(Guid id, [FromBody] CriarProspectReuniaoCCRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var prospect = await _service.CriarProspectAsync(id, MapearProspect(request), cancellationToken);
                return CreatedAtAction(nameof(ListarProspectsAsync), new { id }, Mapear(prospect));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("api/reunioes-cc/{id:guid}/prospects")]
        [ProducesResponseType(typeof(IReadOnlyCollection<ProspectReuniaoCCResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyCollection<ProspectReuniaoCCResponse>>> ListarProspectsAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var prospects = await _service.ListarProspectsPorReuniaoAsync(id, cancellationToken);
                return Ok(prospects.Select(Mapear).ToArray());
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPost("api/registros-educacionais")]
        [ProducesResponseType(typeof(RegistroEducacionalResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RegistroEducacionalResponse>> CriarRegistroEducacionalAsync([FromBody] CriarRegistroEducacionalRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var registro = await _service.CriarRegistroEducacionalAsync(MapearRegistroEducacional(request), cancellationToken);
                return CreatedAtAction(nameof(ObterRegistroEducacionalPorIdAsync), new { id = registro.Id }, Mapear(registro));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("api/registros-educacionais")]
        [ProducesResponseType(typeof(IReadOnlyCollection<RegistroEducacionalResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<RegistroEducacionalResponse>>> ListarRegistrosEducacionaisAsync(CancellationToken cancellationToken)
        {
            var registros = await _service.ListarRegistrosEducacionaisAsync(cancellationToken);
            return Ok(registros.Select(Mapear).ToArray());
        }

        [HttpGet("api/registros-educacionais/{id:guid}")]
        [ProducesResponseType(typeof(RegistroEducacionalResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RegistroEducacionalResponse>> ObterRegistroEducacionalPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var registro = await _service.ObterRegistroEducacionalPorIdAsync(id, cancellationToken);
                return registro is null ? NotFound(CriarMensagemErro("Registro educacional nao encontrado.")) : Ok(Mapear(registro));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPut("api/registros-educacionais/{id:guid}")]
        [ProducesResponseType(typeof(RegistroEducacionalResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RegistroEducacionalResponse>> AtualizarRegistroEducacionalAsync(Guid id, [FromBody] AtualizarRegistroEducacionalRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var registro = await _service.AtualizarRegistroEducacionalAsync(MapearRegistroEducacional(id, request), cancellationToken);
                return Ok(Mapear(registro));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpDelete("api/registros-educacionais/{id:guid}")]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MensagemResponse>> ExcluirRegistroEducacionalAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _service.ExcluirRegistroEducacionalAsync(id, cancellationToken);
                return Ok(new MensagemResponse { Sucesso = true, Mensagem = "Registro educacional excluido com sucesso." });
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("api/parametros-pontuacao-educacional")]
        [ProducesResponseType(typeof(IReadOnlyCollection<ParametroPontuacaoEducacionalResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<ParametroPontuacaoEducacionalResponse>>> ListarParametrosPontuacaoEducacionalAsync(CancellationToken cancellationToken)
        {
            var parametros = await _service.ListarParametrosPontuacaoEducacionalAsync(cancellationToken);
            return Ok(parametros.Select(Mapear).ToArray());
        }

        [HttpGet("api/parametros-pontuacao-educacional/{id:guid}")]
        [ProducesResponseType(typeof(ParametroPontuacaoEducacionalResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ParametroPontuacaoEducacionalResponse>> ObterParametroPontuacaoEducacionalPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var parametro = await _service.ObterParametroPontuacaoEducacionalPorIdAsync(id, cancellationToken);
                return parametro is null ? NotFound(CriarMensagemErro("Parametro de pontuacao educacional nao encontrado.")) : Ok(Mapear(parametro));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPut("api/parametros-pontuacao-educacional/{id:guid}")]
        [ProducesResponseType(typeof(ParametroPontuacaoEducacionalResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ParametroPontuacaoEducacionalResponse>> AtualizarParametroPontuacaoEducacionalAsync(Guid id, [FromBody] AtualizarParametroPontuacaoEducacionalRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var parametro = await _service.AtualizarParametroPontuacaoEducacionalAsync(MapearParametroPontuacaoEducacional(id, request), cancellationToken);
                return Ok(Mapear(parametro));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        private static CicloSemanal MapearCiclo(CriarCicloSemanalRequest request) => new()
        {
            DataInicio = request.DataInicio,
            DataEncerramento = request.DataEncerramento,
            MesReferencia = request.MesReferencia,
            AnoReferencia = request.AnoReferencia,
            Ativo = request.Ativo
        };

        private static CicloSemanal MapearCiclo(Guid id, AtualizarCicloSemanalRequest request) => new()
        {
            Id = id,
            DataInicio = request.DataInicio,
            DataEncerramento = request.DataEncerramento,
            MesReferencia = request.MesReferencia,
            AnoReferencia = request.AnoReferencia,
            Ativo = request.Ativo
        };

        private static ReuniaoCC MapearReuniao(CriarReuniaoCCRequest request) => new()
        {
            CicloSemanalId = request.CicloSemanalId,
            AssociadoOrigemId = request.AssociadoOrigemId,
            AssociadoDestinoId = request.AssociadoDestinoId,
            TipoReuniaoCC = request.TipoReuniaoCC,
            LocalReuniao = string.IsNullOrWhiteSpace(request.LocalReuniao) ? null : request.LocalReuniao.Trim(),
            LinkReuniaoOnline = string.IsNullOrWhiteSpace(request.LinkReuniaoOnline) ? null : request.LinkReuniaoOnline.Trim(),
            DataAgendada = request.DataAgendada,
            StatusReuniaoCC = request.StatusReuniaoCC
        };

        private static ReuniaoCC MapearReuniao(Guid id, AtualizarReuniaoCCRequest request) => new()
        {
            Id = id,
            CicloSemanalId = request.CicloSemanalId,
            AssociadoOrigemId = request.AssociadoOrigemId,
            AssociadoDestinoId = request.AssociadoDestinoId,
            TipoReuniaoCC = request.TipoReuniaoCC,
            LocalReuniao = string.IsNullOrWhiteSpace(request.LocalReuniao) ? null : request.LocalReuniao.Trim(),
            LinkReuniaoOnline = string.IsNullOrWhiteSpace(request.LinkReuniaoOnline) ? null : request.LinkReuniaoOnline.Trim(),
            DataAgendada = request.DataAgendada,
            StatusReuniaoCC = request.StatusReuniaoCC
        };

        private static ValidacaoReuniaoCC MapearValidacao(Guid reuniaoId, CriarValidacaoReuniaoCCRequest request) => new()
        {
            ReuniaoCCId = reuniaoId,
            AssociadoId = request.AssociadoId,
            DataValidacao = request.DataValidacao ?? default,
            NaoEncontrouProspect = request.NaoEncontrouProspect,
            PontosGerados = request.PontosGerados
        };

        private static ProspectReuniaoCC MapearProspect(CriarProspectReuniaoCCRequest request) => new()
        {
            ValidacaoReuniaoCCId = request.ValidacaoReuniaoCCId,
            NomeProspect = request.NomeProspect.Trim(),
            NomeEmpresa = request.NomeEmpresa.Trim(),
            CompartilhouApenasContato = request.CompartilhouApenasContato
        };

        private static RegistroEducacional MapearRegistroEducacional(CriarRegistroEducacionalRequest request) => new()
        {
            AssociadoId = request.AssociadoId,
            ParametroPontuacaoEducacionalId = request.ParametroPontuacaoEducacionalId,
            Titulo = request.Titulo.Trim(),
            CodigoExterno = string.IsNullOrWhiteSpace(request.CodigoExterno) ? null : request.CodigoExterno.Trim(),
            DataOcorrencia = request.DataOcorrencia,
            Validado = request.Validado
        };

        private static RegistroEducacional MapearRegistroEducacional(Guid id, AtualizarRegistroEducacionalRequest request) => new()
        {
            Id = id,
            AssociadoId = request.AssociadoId,
            ParametroPontuacaoEducacionalId = request.ParametroPontuacaoEducacionalId,
            Titulo = request.Titulo.Trim(),
            CodigoExterno = string.IsNullOrWhiteSpace(request.CodigoExterno) ? null : request.CodigoExterno.Trim(),
            DataOcorrencia = request.DataOcorrencia,
            Validado = request.Validado
        };

        private static ParametroPontuacaoEducacional MapearParametroPontuacaoEducacional(Guid id, AtualizarParametroPontuacaoEducacionalRequest request) => new()
        {
            Id = id,
            Nome = request.Nome.Trim(),
            TipoPontuacaoEducacional = request.TipoPontuacaoEducacional,
            Pontos = request.Pontos,
            Ativo = request.Ativo
        };

        private static CicloSemanalResponse Mapear(CicloSemanal ciclo) => new()
        {
            Id = ciclo.Id,
            DataInicio = ciclo.DataInicio,
            DataEncerramento = ciclo.DataEncerramento,
            MesReferencia = ciclo.MesReferencia,
            AnoReferencia = ciclo.AnoReferencia,
            Ativo = ciclo.Ativo,
            DataCadastro = ciclo.DataCadastro,
            QuantidadeReunioes = ciclo.ReunioesCC.Count
        };

        private static ReuniaoCCResponse Mapear(ReuniaoCC reuniao) => new()
        {
            Id = reuniao.Id,
            CicloSemanalId = reuniao.CicloSemanalId,
            AssociadoOrigemId = reuniao.AssociadoOrigemId,
            NomeAssociadoOrigem = reuniao.AssociadoOrigem?.NomeCompleto,
            AssociadoDestinoId = reuniao.AssociadoDestinoId,
            NomeAssociadoDestino = reuniao.AssociadoDestino?.NomeCompleto,
            TipoReuniaoCC = reuniao.TipoReuniaoCC,
            LocalReuniao = reuniao.LocalReuniao,
            LinkReuniaoOnline = reuniao.LinkReuniaoOnline,
            DataAgendada = reuniao.DataAgendada,
            DataCadastro = reuniao.DataCadastro,
            StatusReuniaoCC = reuniao.StatusReuniaoCC,
            QuantidadeValidacoes = reuniao.Validacoes.Count
        };

        private static ValidacaoReuniaoCCResponse Mapear(ValidacaoReuniaoCC validacao) => new()
        {
            Id = validacao.Id,
            ReuniaoCCId = validacao.ReuniaoCCId,
            AssociadoId = validacao.AssociadoId,
            NomeAssociado = validacao.Associado?.NomeCompleto,
            DataValidacao = validacao.DataValidacao,
            NaoEncontrouProspect = validacao.NaoEncontrouProspect,
            PontosGerados = validacao.PontosGerados,
            Prospects = validacao.Prospects.Select(Mapear).ToArray()
        };

        private static ProspectReuniaoCCResponse Mapear(ProspectReuniaoCC prospect) => new()
        {
            Id = prospect.Id,
            ValidacaoReuniaoCCId = prospect.ValidacaoReuniaoCCId,
            NomeProspect = prospect.NomeProspect,
            NomeEmpresa = prospect.NomeEmpresa,
            CompartilhouApenasContato = prospect.CompartilhouApenasContato
        };

        private static RegistroEducacionalResponse Mapear(RegistroEducacional registro) => new()
        {
            Id = registro.Id,
            AssociadoId = registro.AssociadoId,
            NomeAssociado = registro.Associado?.NomeCompleto,
            ParametroPontuacaoEducacionalId = registro.ParametroPontuacaoEducacionalId,
            NomeParametro = registro.ParametroPontuacaoEducacional?.Nome,
            TipoPontuacaoEducacional = registro.TipoPontuacaoEducacional,
            Titulo = registro.Titulo,
            CodigoExterno = registro.CodigoExterno,
            Pontos = registro.Pontos,
            DataOcorrencia = registro.DataOcorrencia,
            Validado = registro.Validado,
            DataValidacao = registro.DataValidacao
        };

        private static ParametroPontuacaoEducacionalResponse Mapear(ParametroPontuacaoEducacional parametro) => new()
        {
            Id = parametro.Id,
            Nome = parametro.Nome,
            TipoPontuacaoEducacional = parametro.TipoPontuacaoEducacional,
            Pontos = parametro.Pontos,
            Ativo = parametro.Ativo,
            DataCadastro = parametro.DataCadastro
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
