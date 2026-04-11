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

        public Task<IReadOnlyCollection<VisitanteExterno>> ListarVisitantesExternosPorOcorrenciaAsync(Guid ocorrenciaReuniaoEquipeId, CancellationToken cancellationToken = default)
        {
            if (ocorrenciaReuniaoEquipeId == Guid.Empty)
            {
                throw new ArgumentException("A ocorrencia da reuniao deve ser informada.", nameof(ocorrenciaReuniaoEquipeId));
            }

            return _visitanteRepository.GetVisitantesExternosByOcorrenciaIdAsync(ocorrenciaReuniaoEquipeId, cancellationToken);
        }

        public Task<IReadOnlyCollection<VisitaInterna>> ListarVisitasInternasPorOcorrenciaAsync(Guid ocorrenciaReuniaoEquipeId, CancellationToken cancellationToken = default)
        {
            if (ocorrenciaReuniaoEquipeId == Guid.Empty)
            {
                throw new ArgumentException("A ocorrencia da reuniao deve ser informada.", nameof(ocorrenciaReuniaoEquipeId));
            }

            return _visitanteRepository.GetVisitasInternasByOcorrenciaIdAsync(ocorrenciaReuniaoEquipeId, cancellationToken);
        }

        public Task<IReadOnlyCollection<SubstitutoAssociado>> ListarSubstitutosAssociadosPorOcorrenciaAsync(Guid ocorrenciaReuniaoEquipeId, CancellationToken cancellationToken = default)
        {
            if (ocorrenciaReuniaoEquipeId == Guid.Empty)
            {
                throw new ArgumentException("A ocorrencia da reuniao deve ser informada.", nameof(ocorrenciaReuniaoEquipeId));
            }

            return _visitanteRepository.GetSubstitutosAssociadosByOcorrenciaIdAsync(ocorrenciaReuniaoEquipeId, cancellationToken);
        }

        public Task<IReadOnlyCollection<SubstitutoExterno>> ListarSubstitutosExternosPorOcorrenciaAsync(Guid ocorrenciaReuniaoEquipeId, CancellationToken cancellationToken = default)
        {
            if (ocorrenciaReuniaoEquipeId == Guid.Empty)
            {
                throw new ArgumentException("A ocorrencia da reuniao deve ser informada.", nameof(ocorrenciaReuniaoEquipeId));
            }

            return _visitanteRepository.GetSubstitutosExternosByOcorrenciaIdAsync(ocorrenciaReuniaoEquipeId, cancellationToken);
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
