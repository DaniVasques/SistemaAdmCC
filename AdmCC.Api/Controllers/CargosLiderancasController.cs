using AdmCC.Api.Models.Requests.CargosLiderancas;
using AdmCC.Api.Models.Responses;
using AdmCC.Api.Models.Responses.CargosLiderancas;
using AdmCC.Domain.CargosLiderancas.Entities;
using AdmCC.InfraData.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdmCC.Api.Controllers
{
    // Vinculos de lideranca permanecem como evolucao futura do modulo.
    [ApiController]
    [Route("api/cargos-lideranca")]
    [Produces("application/json")]
    public class CargosLiderancasController : ControllerBase
    {
        private readonly CargosLiderancasService _service;

        public CargosLiderancasController(CargosLiderancasService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<CargoLiderancaResponse>> CriarAsync([FromBody] CriarCargoLiderancaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var cargo = await _service.CriarCargoAsync(new CargoLideranca
                {
                    Nome = request.Nome.Trim(),
                    ClassificacaoCargo = request.ClassificacaoCargo,
                    Ativo = request.Ativo
                }, cancellationToken);

                return CreatedAtAction(nameof(ObterPorIdAsync), new { id = cargo.Id }, Mapear(cargo));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<CargoLiderancaResponse>>> ListarAsync(CancellationToken cancellationToken)
        {
            var cargos = await _service.ListarCargosAsync(cancellationToken);
            return Ok(cargos.Select(Mapear).ToArray());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CargoLiderancaResponse>> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var cargo = await _service.ObterCargoPorIdAsync(id, cancellationToken);
            return cargo is null ? NotFound(CriarMensagemErro("Cargo de lideranca nao encontrado.")) : Ok(Mapear(cargo));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CargoLiderancaResponse>> AtualizarAsync(Guid id, [FromBody] AtualizarCargoLiderancaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var cargo = await _service.AtualizarCargoAsync(new CargoLideranca
                {
                    Id = id,
                    Nome = request.Nome.Trim(),
                    ClassificacaoCargo = request.ClassificacaoCargo,
                    Ativo = request.Ativo
                }, cancellationToken);

                return Ok(Mapear(cargo));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<MensagemResponse>> ExcluirAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _service.ExcluirCargoAsync(id, cancellationToken);
                return Ok(new MensagemResponse { Sucesso = true, Mensagem = "Cargo de lideranca excluido com sucesso." });
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        private static CargoLiderancaResponse Mapear(CargoLideranca cargo) => new()
        {
            Id = cargo.Id,
            Nome = cargo.Nome,
            ClassificacaoCargo = cargo.ClassificacaoCargo,
            Ativo = cargo.Ativo,
            DataCadastro = cargo.DataCadastro
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
