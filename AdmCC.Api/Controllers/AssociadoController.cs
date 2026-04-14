using AdmCC.Api.Models.Requests.CadastroBase;
using AdmCC.Api.Models.Responses;
using AdmCC.Api.Models.Responses.CadastroBase;
using AdmCC.Domain.CadastroBase.Entities;
using AdmCC.InfraData.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdmCC.Api.Controllers
{
    [ApiController]
    [Route("api/associados")]
    [Produces("application/json")]
    public class AssociadoController : ControllerBase
    {
        private readonly CadastroBaseService _cadastroBaseService;

        public AssociadoController(CadastroBaseService cadastroBaseService)
        {
            _cadastroBaseService = cadastroBaseService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AssociadoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AssociadoResponse>> CriarAsync(
            [FromBody] CriarAssociadoRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var associado = await _cadastroBaseService.CriarAssociadoAsync(MapearAssociado(request), cancellationToken);
                return CreatedAtAction(nameof(ObterPorIdAsync), new { id = associado.Id }, MapearResponse(associado));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<AssociadoResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<AssociadoResponse>>> ListarAsync(CancellationToken cancellationToken)
        {
            var associados = await _cadastroBaseService.ListarAssociadosAsync(cancellationToken);
            return Ok(associados.Select(MapearResponse).ToArray());
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(AssociadoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AssociadoResponse>> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var associado = await _cadastroBaseService.ObterAssociadoPorIdAsync(id, cancellationToken);

            if (associado is null)
            {
                return NotFound(new MensagemResponse
                {
                    Sucesso = false,
                    Mensagem = "Associado nao encontrado."
                });
            }

            return Ok(MapearResponse(associado));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(AssociadoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AssociadoResponse>> AtualizarAsync(
            Guid id,
            [FromBody] AtualizarAssociadoRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var associado = await _cadastroBaseService.AtualizarAssociadoAsync(MapearAssociado(id, request), cancellationToken);
                return Ok(MapearResponse(associado));
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
                await _cadastroBaseService.ExcluirAssociadoAsync(id, cancellationToken);
                return Ok(new MensagemResponse
                {
                    Sucesso = true,
                    Mensagem = "Associado excluido com sucesso."
                });
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPatch("{id:guid}/status")]
        [ProducesResponseType(typeof(AssociadoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MensagemResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AssociadoResponse>> AlterarStatusAsync(
            Guid id,
            [FromBody] AlterarStatusAssociadoRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var associado = await _cadastroBaseService.AlterarStatusAssociadoAsync(
                    id,
                    request.NovoStatus,
                    request.UsuarioResponsavelId,
                    request.Motivo,
                    cancellationToken);

                return Ok(MapearResponse(associado));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
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

        private static Associado MapearAssociado(CriarAssociadoRequest request)
        {
            return new Associado
            {
                NomeCompleto = request.NomeCompleto.Trim(),
                Cpf = request.Cpf.Trim(),
                DataNascimento = request.DataNascimento,
                PermitirExibirAniversario = request.PermitirExibirAniversario,
                EmailPrincipal = request.EmailPrincipal.Trim(),
                TelefoneWhatsappPrincipal = request.TelefoneWhatsappPrincipal.Trim(),
                DataIngresso = request.DataIngresso,
                StatusAssociado = request.StatusAssociado,
                EnderecoId = request.EnderecoId,
                EmpresaId = request.EmpresaId,
                AnuidadeId = request.AnuidadeId,
                PadrinhoId = request.PadrinhoId,
                EquipeOrigemId = request.EquipeOrigemId,
                EquipeAtualId = request.EquipeAtualId,
                ClusterId = request.ClusterId,
                AtuacaoEspecificaId = request.AtuacaoEspecificaId
            };
        }

        private static Associado MapearAssociado(Guid id, AtualizarAssociadoRequest request)
        {
            return new Associado
            {
                Id = id,
                NomeCompleto = request.NomeCompleto.Trim(),
                Cpf = request.Cpf.Trim(),
                DataNascimento = request.DataNascimento,
                PermitirExibirAniversario = request.PermitirExibirAniversario,
                EmailPrincipal = request.EmailPrincipal.Trim(),
                TelefoneWhatsappPrincipal = request.TelefoneWhatsappPrincipal.Trim(),
                DataIngresso = request.DataIngresso,
                StatusAssociado = request.StatusAssociado,
                EnderecoId = request.EnderecoId,
                EmpresaId = request.EmpresaId,
                AnuidadeId = request.AnuidadeId,
                PadrinhoId = request.PadrinhoId,
                EquipeOrigemId = request.EquipeOrigemId,
                EquipeAtualId = request.EquipeAtualId,
                ClusterId = request.ClusterId,
                AtuacaoEspecificaId = request.AtuacaoEspecificaId
            };
        }

        private static AssociadoResponse MapearResponse(Associado associado)
        {
            return new AssociadoResponse
            {
                Id = associado.Id,
                NomeCompleto = associado.NomeCompleto,
                Cpf = associado.Cpf,
                DataNascimento = associado.DataNascimento,
                PermitirExibirAniversario = associado.PermitirExibirAniversario,
                EmailPrincipal = associado.EmailPrincipal,
                TelefoneWhatsappPrincipal = associado.TelefoneWhatsappPrincipal,
                DataIngresso = associado.DataIngresso,
                DataCadastro = associado.DataCadastro,
                StatusAssociado = associado.StatusAssociado,
                EnderecoId = associado.EnderecoId,
                EmpresaId = associado.EmpresaId,
                AnuidadeId = associado.AnuidadeId,
                PadrinhoId = associado.PadrinhoId,
                EquipeOrigemId = associado.EquipeOrigemId,
                EquipeAtualId = associado.EquipeAtualId,
                ClusterId = associado.ClusterId,
                ClusterNome = associado.Cluster?.Nome,
                AtuacaoEspecificaId = associado.AtuacaoEspecificaId,
                AtuacaoEspecificaNome = associado.AtuacaoEspecifica?.Nome,
                PadrinhoNome = associado.Padrinho?.NomeCompleto,
                Grupamentos = associado.AssociadosGrupamentos
                    .Select(x => x.GrupamentoEstrategico?.Nome ?? string.Empty)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToArray()
            };
        }
    }
}
