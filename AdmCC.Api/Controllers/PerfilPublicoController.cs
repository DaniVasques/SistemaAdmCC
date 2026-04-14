using AdmCC.Api.Models.Requests.PerfilPublico;
using AdmCC.Api.Models.Responses;
using AdmCC.Api.Models.Responses.PerfilPublico;
using AdmCC.Domain.PerfilPublico.Entities;
using AdmCC.InfraData.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdmCC.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class PerfilPublicoController : ControllerBase
    {
        private readonly PerfilPublicoService _service;

        public PerfilPublicoController(PerfilPublicoService service)
        {
            _service = service;
        }

        [HttpPost("api/perfis-associados")]
        public async Task<ActionResult<PerfilAssociadoResponse>> CriarPerfilAsync([FromBody] CriarPerfilAssociadoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var perfil = await _service.CriarPerfilAsync(MapearPerfil(request), cancellationToken);
                return CreatedAtAction(nameof(ObterPerfilPorIdAsync), new { id = perfil.Id }, Mapear(perfil));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpGet("api/perfis-associados")]
        public async Task<ActionResult<IReadOnlyCollection<PerfilAssociadoResponse>>> ListarPerfisAsync(CancellationToken cancellationToken)
        {
            var perfis = await _service.ListarPerfisAsync(cancellationToken);
            return Ok(perfis.Select(Mapear).ToArray());
        }

        [HttpGet("api/perfis-associados/{id:guid}")]
        public async Task<ActionResult<PerfilAssociadoResponse>> ObterPerfilPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var perfil = await _service.ObterPerfilPorIdAsync(id, cancellationToken);
            return perfil is null ? NotFound(CriarMensagemErro("Perfil associado nao encontrado.")) : Ok(Mapear(perfil));
        }

        [HttpPut("api/perfis-associados/{id:guid}")]
        public async Task<ActionResult<PerfilAssociadoResponse>> AtualizarPerfilAsync(Guid id, [FromBody] AtualizarPerfilAssociadoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var perfil = await _service.AtualizarPerfilAsync(MapearPerfil(id, request), cancellationToken);
                return Ok(Mapear(perfil));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpDelete("api/perfis-associados/{id:guid}")]
        public async Task<ActionResult<MensagemResponse>> ExcluirPerfilAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _service.ExcluirPerfilAsync(id, cancellationToken);
                return Ok(new MensagemResponse { Sucesso = true, Mensagem = "Perfil associado excluido com sucesso." });
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPost("api/midias-associados/{perfilId:guid}")]
        public async Task<ActionResult<MidiaAssociadoResponse>> CriarMidiaAsync(Guid perfilId, [FromBody] CriarMidiaAssociadoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var midia = await _service.AdicionarMidiaAsync(perfilId, MapearMidia(request), cancellationToken);
                return CreatedAtAction(nameof(ObterPerfilPorIdAsync), new { id = perfilId }, Mapear(midia));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpPut("api/midias-associados/{perfilId:guid}/{midiaId:guid}")]
        public async Task<ActionResult<MidiaAssociadoResponse>> AtualizarMidiaAsync(Guid perfilId, Guid midiaId, [FromBody] AtualizarMidiaAssociadoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var midia = await _service.AtualizarMidiaAsync(perfilId, midiaId, MapearMidia(request), cancellationToken);
                return Ok(Mapear(midia));
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        [HttpDelete("api/midias-associados/{perfilId:guid}/{midiaId:guid}")]
        public async Task<ActionResult<MensagemResponse>> ExcluirMidiaAsync(Guid perfilId, Guid midiaId, CancellationToken cancellationToken)
        {
            try
            {
                await _service.RemoverMidiaAsync(perfilId, midiaId, cancellationToken);
                return Ok(new MensagemResponse { Sucesso = true, Mensagem = "Midia removida com sucesso." });
            }
            catch (Exception ex)
            {
                return CriarRespostaErro(ex);
            }
        }

        private static PerfilAssociado MapearPerfil(CriarPerfilAssociadoRequest request) => new()
        {
            AssociadoId = request.AssociadoId,
            FotoProfissionalUrl = request.FotoProfissionalUrl.Trim(),
            DescricaoProfissional = request.DescricaoProfissional.Trim(),
            NomeEmpresaExibicao = request.NomeEmpresaExibicao?.Trim(),
            LogomarcaEmpresaUrl = request.LogomarcaEmpresaUrl?.Trim(),
            Site = request.Site?.Trim(),
            EmailPublico = request.EmailPublico?.Trim(),
            PerfilPublicado = request.PerfilPublicado,
            Midias = request.Midias.Select(MapearMidia).ToList()
        };

        private static PerfilAssociado MapearPerfil(Guid id, AtualizarPerfilAssociadoRequest request) => new()
        {
            Id = id,
            AssociadoId = request.AssociadoId,
            FotoProfissionalUrl = request.FotoProfissionalUrl.Trim(),
            DescricaoProfissional = request.DescricaoProfissional.Trim(),
            NomeEmpresaExibicao = request.NomeEmpresaExibicao?.Trim(),
            LogomarcaEmpresaUrl = request.LogomarcaEmpresaUrl?.Trim(),
            Site = request.Site?.Trim(),
            EmailPublico = request.EmailPublico?.Trim(),
            PerfilPublicado = request.PerfilPublicado,
            Midias = request.Midias.Select(MapearMidia).ToList()
        };

        private static MidiaAssociado MapearMidia(CriarMidiaAssociadoRequest request) => new()
        {
            NomeMidia = request.NomeMidia.Trim(),
            Url = request.Url.Trim(),
            OrdemExibicao = request.OrdemExibicao,
            Ativa = request.Ativa
        };

        private static MidiaAssociado MapearMidia(AtualizarMidiaAssociadoRequest request) => new()
        {
            NomeMidia = request.NomeMidia.Trim(),
            Url = request.Url.Trim(),
            OrdemExibicao = request.OrdemExibicao,
            Ativa = request.Ativa
        };

        private static PerfilAssociadoResponse Mapear(PerfilAssociado perfil) => new()
        {
            Id = perfil.Id,
            AssociadoId = perfil.AssociadoId,
            FotoProfissionalUrl = perfil.FotoProfissionalUrl,
            DescricaoProfissional = perfil.DescricaoProfissional,
            NomeEmpresaExibicao = perfil.NomeEmpresaExibicao,
            LogomarcaEmpresaUrl = perfil.LogomarcaEmpresaUrl,
            Site = perfil.Site,
            EmailPublico = perfil.EmailPublico,
            PerfilPublicado = perfil.PerfilPublicado,
            Midias = perfil.Midias.OrderBy(x => x.OrdemExibicao).Select(Mapear).ToArray()
        };

        private static MidiaAssociadoResponse Mapear(MidiaAssociado midia) => new()
        {
            Id = midia.Id,
            PerfilAssociadoId = midia.PerfilAssociadoId,
            NomeMidia = midia.NomeMidia,
            Url = midia.Url,
            OrdemExibicao = midia.OrdemExibicao,
            Ativa = midia.Ativa
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
