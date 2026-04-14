using AdmCC.Domain.RelatoriosAuditorias.Interfaces;
using AdmCC.Domain.VisitantesSubstitutos.Entities;
using AdmCC.Domain.VisitantesSubstitutos.Enums;
using AdmCC.Domain.VisitantesSubstitutos.Interfaces;

namespace AdmCC.InfraData.Services
{
    public class VisitantesSubstitutosService
    {
        private readonly IVisitanteRepository _visitanteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VisitantesSubstitutosService(
            IVisitanteRepository visitanteRepository,
            IUnitOfWork unitOfWork)
        {
            _visitanteRepository = visitanteRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<IReadOnlyCollection<VisitanteExterno>> ListarVisitantesExternosAsync(CancellationToken cancellationToken = default)
        {
            return _visitanteRepository.GetVisitantesExternosAsync(cancellationToken);
        }

        public Task<VisitanteExterno?> ObterVisitanteExternoPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do visitante externo deve ser informado.", nameof(id));
            }

            return _visitanteRepository.GetVisitanteExternoByIdAsync(id, cancellationToken);
        }

        public Task<IReadOnlyCollection<VisitanteExterno>> ListarVisitantesExternosPorOcorrenciaAsync(Guid ocorrenciaReuniaoEquipeId, CancellationToken cancellationToken = default)
        {
            if (ocorrenciaReuniaoEquipeId == Guid.Empty)
            {
                throw new ArgumentException("A ocorrencia da reuniao deve ser informada.", nameof(ocorrenciaReuniaoEquipeId));
            }

            return _visitanteRepository.GetVisitantesExternosByOcorrenciaIdAsync(ocorrenciaReuniaoEquipeId, cancellationToken);
        }

        public async Task<VisitanteExterno> CriarVisitanteExternoAsync(VisitanteExterno visitanteExterno, CancellationToken cancellationToken = default)
        {
            return await RegistrarVisitanteExternoAsync(visitanteExterno, cancellationToken);
        }

        public Task<IReadOnlyCollection<VisitaInterna>> ListarVisitasInternasPorOcorrenciaAsync(Guid ocorrenciaReuniaoEquipeId, CancellationToken cancellationToken = default)
        {
            if (ocorrenciaReuniaoEquipeId == Guid.Empty)
            {
                throw new ArgumentException("A ocorrencia da reuniao deve ser informada.", nameof(ocorrenciaReuniaoEquipeId));
            }

            return _visitanteRepository.GetVisitasInternasByOcorrenciaIdAsync(ocorrenciaReuniaoEquipeId, cancellationToken);
        }

        public Task<IReadOnlyCollection<VisitaInterna>> ListarVisitasInternasAsync(CancellationToken cancellationToken = default)
        {
            return _visitanteRepository.GetVisitasInternasAsync(cancellationToken);
        }

        public Task<VisitaInterna?> ObterVisitaInternaPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador da visita interna deve ser informado.", nameof(id));
            }

            return _visitanteRepository.GetVisitaInternaByIdAsync(id, cancellationToken);
        }

        public async Task<VisitaInterna> CriarVisitaInternaAsync(VisitaInterna visitaInterna, CancellationToken cancellationToken = default)
        {
            return await RegistrarVisitaInternaAsync(visitaInterna, cancellationToken);
        }

        public Task<IReadOnlyCollection<SubstitutoAssociado>> ListarSubstitutosAssociadosPorOcorrenciaAsync(Guid ocorrenciaReuniaoEquipeId, CancellationToken cancellationToken = default)
        {
            if (ocorrenciaReuniaoEquipeId == Guid.Empty)
            {
                throw new ArgumentException("A ocorrencia da reuniao deve ser informada.", nameof(ocorrenciaReuniaoEquipeId));
            }

            return _visitanteRepository.GetSubstitutosAssociadosByOcorrenciaIdAsync(ocorrenciaReuniaoEquipeId, cancellationToken);
        }

        public Task<IReadOnlyCollection<SubstitutoAssociado>> ListarSubstitutosAssociadosAsync(CancellationToken cancellationToken = default)
        {
            return _visitanteRepository.GetSubstitutosAssociadosAsync(cancellationToken);
        }

        public Task<SubstitutoAssociado?> ObterSubstitutoAssociadoPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do substituto associado deve ser informado.", nameof(id));
            }

            return _visitanteRepository.GetSubstitutoAssociadoByIdAsync(id, cancellationToken);
        }

        public async Task<SubstitutoAssociado> CriarSubstitutoAssociadoAsync(SubstitutoAssociado substitutoAssociado, CancellationToken cancellationToken = default)
        {
            return await RegistrarSubstitutoAssociadoAsync(substitutoAssociado, cancellationToken);
        }

        public Task<IReadOnlyCollection<SubstitutoExterno>> ListarSubstitutosExternosPorOcorrenciaAsync(Guid ocorrenciaReuniaoEquipeId, CancellationToken cancellationToken = default)
        {
            if (ocorrenciaReuniaoEquipeId == Guid.Empty)
            {
                throw new ArgumentException("A ocorrencia da reuniao deve ser informada.", nameof(ocorrenciaReuniaoEquipeId));
            }

            return _visitanteRepository.GetSubstitutosExternosByOcorrenciaIdAsync(ocorrenciaReuniaoEquipeId, cancellationToken);
        }

        public Task<IReadOnlyCollection<SubstitutoExterno>> ListarSubstitutosExternosAsync(CancellationToken cancellationToken = default)
        {
            return _visitanteRepository.GetSubstitutosExternosAsync(cancellationToken);
        }

        public Task<SubstitutoExterno?> ObterSubstitutoExternoPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do substituto externo deve ser informado.", nameof(id));
            }

            return _visitanteRepository.GetSubstitutoExternoByIdAsync(id, cancellationToken);
        }

        public async Task<SubstitutoExterno> CriarSubstitutoExternoAsync(SubstitutoExterno substitutoExterno, CancellationToken cancellationToken = default)
        {
            return await RegistrarSubstitutoExternoAsync(substitutoExterno, cancellationToken);
        }

        public async Task<VisitanteExterno> RegistrarVisitanteExternoAsync(VisitanteExterno visitanteExterno, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(visitanteExterno);

            ValidarVisitanteExterno(visitanteExterno);

            if (visitanteExterno.Id == Guid.Empty)
            {
                visitanteExterno.Id = Guid.NewGuid();
            }

            if (visitanteExterno.DataCadastro == default)
            {
                visitanteExterno.DataCadastro = DateTime.UtcNow;
            }

            await _visitanteRepository.AddVisitanteExternoAsync(visitanteExterno, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return visitanteExterno;
        }

        public async Task<VisitanteExterno> AtualizarVisitanteExternoAsync(VisitanteExterno visitanteExterno, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(visitanteExterno);

            var existente = await _visitanteRepository.GetVisitanteExternoByIdAsync(visitanteExterno.Id, cancellationToken);
            if (existente is null)
            {
                throw new KeyNotFoundException("Visitante externo nao encontrado.");
            }

            AplicarDadosVisitanteExterno(existente, visitanteExterno);
            ValidarVisitanteExterno(existente);

            await _visitanteRepository.UpdateVisitanteExternoAsync(existente, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return existente;
        }

        public async Task ExcluirVisitanteExternoAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do visitante externo deve ser informado.", nameof(id));
            }

            var existente = await _visitanteRepository.GetVisitanteExternoByIdAsync(id, cancellationToken);
            if (existente is null)
            {
                throw new KeyNotFoundException("Visitante externo nao encontrado.");
            }

            await _visitanteRepository.DeleteVisitanteExternoAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<VisitaInterna> RegistrarVisitaInternaAsync(VisitaInterna visitaInterna, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(visitaInterna);

            ValidarVisitaInterna(visitaInterna);

            if (visitaInterna.Id == Guid.Empty)
            {
                visitaInterna.Id = Guid.NewGuid();
            }

            if (visitaInterna.DataCadastro == default)
            {
                visitaInterna.DataCadastro = DateTime.UtcNow;
            }

            await _visitanteRepository.AddVisitaInternaAsync(visitaInterna, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return visitaInterna;
        }

        public async Task<VisitaInterna> AtualizarVisitaInternaAsync(VisitaInterna visitaInterna, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(visitaInterna);

            var existente = await _visitanteRepository.GetVisitaInternaByIdAsync(visitaInterna.Id, cancellationToken);
            if (existente is null)
            {
                throw new KeyNotFoundException("Visita interna nao encontrada.");
            }

            AplicarDadosVisitaInterna(existente, visitaInterna);
            ValidarVisitaInterna(existente);

            await _visitanteRepository.UpdateVisitaInternaAsync(existente, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return existente;
        }

        public async Task ExcluirVisitaInternaAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador da visita interna deve ser informado.", nameof(id));
            }

            var existente = await _visitanteRepository.GetVisitaInternaByIdAsync(id, cancellationToken);
            if (existente is null)
            {
                throw new KeyNotFoundException("Visita interna nao encontrada.");
            }

            await _visitanteRepository.DeleteVisitaInternaAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<SubstitutoAssociado> RegistrarSubstitutoAssociadoAsync(SubstitutoAssociado substitutoAssociado, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(substitutoAssociado);

            ValidarSubstitutoAssociado(substitutoAssociado);

            if (substitutoAssociado.Id == Guid.Empty)
            {
                substitutoAssociado.Id = Guid.NewGuid();
            }

            if (substitutoAssociado.DataCadastro == default)
            {
                substitutoAssociado.DataCadastro = DateTime.UtcNow;
            }

            await _visitanteRepository.AddSubstitutoAssociadoAsync(substitutoAssociado, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return substitutoAssociado;
        }

        public async Task<SubstitutoAssociado> AtualizarSubstitutoAssociadoAsync(SubstitutoAssociado substitutoAssociado, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(substitutoAssociado);

            var existente = await _visitanteRepository.GetSubstitutoAssociadoByIdAsync(substitutoAssociado.Id, cancellationToken);
            if (existente is null)
            {
                throw new KeyNotFoundException("Substituto associado nao encontrado.");
            }

            AplicarDadosSubstitutoAssociado(existente, substitutoAssociado);
            ValidarSubstitutoAssociado(existente);

            await _visitanteRepository.UpdateSubstitutoAssociadoAsync(existente, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return existente;
        }

        public async Task ExcluirSubstitutoAssociadoAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do substituto associado deve ser informado.", nameof(id));
            }

            var existente = await _visitanteRepository.GetSubstitutoAssociadoByIdAsync(id, cancellationToken);
            if (existente is null)
            {
                throw new KeyNotFoundException("Substituto associado nao encontrado.");
            }

            await _visitanteRepository.DeleteSubstitutoAssociadoAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<SubstitutoExterno> RegistrarSubstitutoExternoAsync(SubstitutoExterno substitutoExterno, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(substitutoExterno);

            ValidarSubstitutoExterno(substitutoExterno);

            if (substitutoExterno.Id == Guid.Empty)
            {
                substitutoExterno.Id = Guid.NewGuid();
            }

            if (substitutoExterno.DataCadastro == default)
            {
                substitutoExterno.DataCadastro = DateTime.UtcNow;
            }

            await _visitanteRepository.AddSubstitutoExternoAsync(substitutoExterno, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return substitutoExterno;
        }

        public async Task<SubstitutoExterno> AtualizarSubstitutoExternoAsync(SubstitutoExterno substitutoExterno, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(substitutoExterno);

            var existente = await _visitanteRepository.GetSubstitutoExternoByIdAsync(substitutoExterno.Id, cancellationToken);
            if (existente is null)
            {
                throw new KeyNotFoundException("Substituto externo nao encontrado.");
            }

            AplicarDadosSubstitutoExterno(existente, substitutoExterno);
            ValidarSubstitutoExterno(existente);

            await _visitanteRepository.UpdateSubstitutoExternoAsync(existente, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return existente;
        }

        public async Task ExcluirSubstitutoExternoAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do substituto externo deve ser informado.", nameof(id));
            }

            var existente = await _visitanteRepository.GetSubstitutoExternoByIdAsync(id, cancellationToken);
            if (existente is null)
            {
                throw new KeyNotFoundException("Substituto externo nao encontrado.");
            }

            await _visitanteRepository.DeleteSubstitutoExternoAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task AtualizarStatusValidacaoAsync(Guid registroId, string tipoRegistro, StatusValidacaoPresenca statusValidacaoPresenca, CancellationToken cancellationToken = default)
        {
            if (registroId == Guid.Empty)
            {
                throw new ArgumentException("O identificador do registro deve ser informado.", nameof(registroId));
            }

            if (string.IsNullOrWhiteSpace(tipoRegistro))
            {
                throw new ArgumentException("O tipo do registro deve ser informado.", nameof(tipoRegistro));
            }

            await _visitanteRepository.AtualizarStatusValidacaoAsync(registroId, tipoRegistro, statusValidacaoPresenca, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private static void AplicarDadosVisitanteExterno(VisitanteExterno destino, VisitanteExterno origem)
        {
            destino.OcorrenciaReuniaoEquipeId = origem.OcorrenciaReuniaoEquipeId;
            destino.AssociadoResponsavelId = origem.AssociadoResponsavelId;
            destino.TipoPessoa = origem.TipoPessoa;
            destino.NomeCompleto = origem.NomeCompleto;
            destino.TelefonePrincipal = origem.TelefonePrincipal;
            destino.Email = origem.Email;
            destino.Cpf = origem.Cpf;
            destino.NomeEmpresa = origem.NomeEmpresa;
        }

        private static void AplicarDadosVisitaInterna(VisitaInterna destino, VisitaInterna origem)
        {
            destino.OcorrenciaReuniaoEquipeId = origem.OcorrenciaReuniaoEquipeId;
            destino.AssociadoVisitanteId = origem.AssociadoVisitanteId;
        }

        private static void AplicarDadosSubstitutoAssociado(SubstitutoAssociado destino, SubstitutoAssociado origem)
        {
            destino.OcorrenciaReuniaoEquipeId = origem.OcorrenciaReuniaoEquipeId;
            destino.AssociadoTitularId = origem.AssociadoTitularId;
            destino.AssociadoSubstitutoId = origem.AssociadoSubstitutoId;
        }

        private static void AplicarDadosSubstitutoExterno(SubstitutoExterno destino, SubstitutoExterno origem)
        {
            destino.OcorrenciaReuniaoEquipeId = origem.OcorrenciaReuniaoEquipeId;
            destino.AssociadoTitularId = origem.AssociadoTitularId;
            destino.TipoPessoa = origem.TipoPessoa;
            destino.NomeCompleto = origem.NomeCompleto;
            destino.TelefonePrincipal = origem.TelefonePrincipal;
            destino.Email = origem.Email;
            destino.Cpf = origem.Cpf;
            destino.NomeEmpresa = origem.NomeEmpresa;
        }

        private static void ValidarVisitanteExterno(VisitanteExterno visitanteExterno)
        {
            if (visitanteExterno.OcorrenciaReuniaoEquipeId == Guid.Empty || visitanteExterno.AssociadoResponsavelId == Guid.Empty)
            {
                throw new ArgumentException("A ocorrencia da reuniao e o associado responsavel devem ser informados.", nameof(visitanteExterno));
            }

            if (string.IsNullOrWhiteSpace(visitanteExterno.NomeCompleto) || string.IsNullOrWhiteSpace(visitanteExterno.TelefonePrincipal))
            {
                throw new ArgumentException("Nome completo e telefone principal do visitante externo devem ser informados.", nameof(visitanteExterno));
            }

            if (visitanteExterno.TipoPessoa == TipoPessoa.Empresa && string.IsNullOrWhiteSpace(visitanteExterno.NomeEmpresa))
            {
                throw new InvalidOperationException("Visitante externo do tipo empresa deve informar o nome da empresa.");
            }
        }

        private static void ValidarVisitaInterna(VisitaInterna visitaInterna)
        {
            if (visitaInterna.OcorrenciaReuniaoEquipeId == Guid.Empty || visitaInterna.AssociadoVisitanteId == Guid.Empty)
            {
                throw new ArgumentException("A ocorrencia da reuniao e o associado visitante devem ser informados.", nameof(visitaInterna));
            }
        }

        private static void ValidarSubstitutoAssociado(SubstitutoAssociado substitutoAssociado)
        {
            if (substitutoAssociado.OcorrenciaReuniaoEquipeId == Guid.Empty ||
                substitutoAssociado.AssociadoTitularId == Guid.Empty ||
                substitutoAssociado.AssociadoSubstitutoId == Guid.Empty)
            {
                throw new ArgumentException("A ocorrencia da reuniao, o associado titular e o associado substituto devem ser informados.", nameof(substitutoAssociado));
            }

            if (substitutoAssociado.AssociadoTitularId == substitutoAssociado.AssociadoSubstitutoId)
            {
                throw new InvalidOperationException("O associado substituto nao pode ser o mesmo associado titular.");
            }
        }

        private static void ValidarSubstitutoExterno(SubstitutoExterno substitutoExterno)
        {
            if (substitutoExterno.OcorrenciaReuniaoEquipeId == Guid.Empty || substitutoExterno.AssociadoTitularId == Guid.Empty)
            {
                throw new ArgumentException("A ocorrencia da reuniao e o associado titular devem ser informados.", nameof(substitutoExterno));
            }

            if (string.IsNullOrWhiteSpace(substitutoExterno.NomeCompleto) || string.IsNullOrWhiteSpace(substitutoExterno.TelefonePrincipal))
            {
                throw new ArgumentException("Nome completo e telefone principal do substituto externo devem ser informados.", nameof(substitutoExterno));
            }

            if (substitutoExterno.TipoPessoa == TipoPessoa.Empresa && string.IsNullOrWhiteSpace(substitutoExterno.NomeEmpresa))
            {
                throw new InvalidOperationException("Substituto externo do tipo empresa deve informar o nome da empresa.");
            }
        }
    }
}
