using AdmCC.Domain.RelatoriosAuditorias.Interfaces;
using AdmCC.Domain.Seguro.Entities;
using AdmCC.Domain.Seguro.Enums;
using AdmCC.Domain.Seguro.Interfaces;

namespace AdmCC.InfraData.Services
{
    public class SeguroService
    {
        private readonly ISeguroAssociadoRepository _seguroAssociadoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SeguroService(
            ISeguroAssociadoRepository seguroAssociadoRepository,
            IUnitOfWork unitOfWork)
        {
            _seguroAssociadoRepository = seguroAssociadoRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<SeguroAssociado?> ObterSeguroPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _seguroAssociadoRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<SeguroAssociado?> ObterSeguroPorAssociadoIdAsync(Guid associadoId, CancellationToken cancellationToken = default)
        {
            if (associadoId == Guid.Empty)
            {
                throw new ArgumentException("O identificador do associado deve ser informado.", nameof(associadoId));
            }

            return _seguroAssociadoRepository.GetByAssociadoIdAsync(associadoId, cancellationToken);
        }

        public Task<IReadOnlyCollection<SeguroAssociado>> ListarSegurosAsync(CancellationToken cancellationToken = default)
        {
            return _seguroAssociadoRepository.GetAllAsync(cancellationToken);
        }

        public async Task<SeguroAssociado> CriarSeguroAsync(SeguroAssociado seguroAssociado, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(seguroAssociado);

            await ValidarSeguroAsync(seguroAssociado, isEdicao: false, cancellationToken);

            if (seguroAssociado.Id == Guid.Empty)
            {
                seguroAssociado.Id = Guid.NewGuid();
            }

            if (seguroAssociado.DataCadastro == default)
            {
                seguroAssociado.DataCadastro = DateTime.UtcNow;
            }

            await _seguroAssociadoRepository.AddAsync(seguroAssociado, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return seguroAssociado;
        }

        public async Task<SeguroAssociado> AtualizarSeguroAsync(SeguroAssociado seguroAssociado, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(seguroAssociado);

            if (seguroAssociado.Id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do seguro deve ser informado para atualizacao.", nameof(seguroAssociado));
            }

            var existente = await _seguroAssociadoRepository.GetByIdAsync(seguroAssociado.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Seguro do associado nao encontrado para atualizacao.");

            var seguroAtualizado = AplicarDadosSeguro(seguroAssociado, existente);

            await ValidarSeguroAsync(seguroAtualizado, isEdicao: true, cancellationToken);

            await _seguroAssociadoRepository.UpdateAsync(seguroAtualizado, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return seguroAtualizado;
        }

        public async Task ExcluirSeguroAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do seguro deve ser informado.", nameof(id));
            }

            var seguro = await _seguroAssociadoRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Seguro do associado nao encontrado para exclusao.");

            if (seguro.SolicitacoesAlteracaoBeneficiario.Any(x => x.StatusSolicitacaoBeneficiario == StatusSolicitacaoBeneficiario.EmAnalise))
            {
                throw new InvalidOperationException("Nao e permitido excluir seguro com solicitacoes de alteracao em analise.");
            }

            await _seguroAssociadoRepository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<BeneficiarioSeguro>> ListarBeneficiariosAsync(Guid seguroAssociadoId, CancellationToken cancellationToken = default)
        {
            var seguro = await ObterSeguroExistenteAsync(seguroAssociadoId, cancellationToken);
            return seguro.Beneficiarios.OrderBy(x => x.NomeCompleto).ToArray();
        }

        public async Task<BeneficiarioSeguro> AdicionarBeneficiarioAsync(Guid seguroAssociadoId, BeneficiarioSeguro beneficiario, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(beneficiario);

            var seguro = await ObterSeguroExistenteAsync(seguroAssociadoId, cancellationToken);

            if (beneficiario.Id == Guid.Empty)
            {
                beneficiario.Id = Guid.NewGuid();
            }

            beneficiario.SeguroAssociadoId = seguro.Id;
            seguro.Beneficiarios.Add(beneficiario);

            ValidarBeneficiarios(seguro);

            await _seguroAssociadoRepository.UpdateAsync(seguro, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return beneficiario;
        }

        public async Task<BeneficiarioSeguro> AtualizarBeneficiarioAsync(
            Guid seguroAssociadoId,
            Guid beneficiarioId,
            BeneficiarioSeguro beneficiarioAtualizado,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(beneficiarioAtualizado);

            var seguro = await ObterSeguroExistenteAsync(seguroAssociadoId, cancellationToken);
            var beneficiario = seguro.Beneficiarios.FirstOrDefault(x => x.Id == beneficiarioId)
                ?? throw new KeyNotFoundException("Beneficiario nao encontrado para atualizacao.");

            beneficiario.NomeCompleto = beneficiarioAtualizado.NomeCompleto;
            beneficiario.Cpf = beneficiarioAtualizado.Cpf;
            beneficiario.GrauParentesco = beneficiarioAtualizado.GrauParentesco;
            beneficiario.Percentual = beneficiarioAtualizado.Percentual;
            beneficiario.Telefone = beneficiarioAtualizado.Telefone;

            ValidarBeneficiarios(seguro);

            await _seguroAssociadoRepository.UpdateAsync(seguro, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return beneficiario;
        }

        public async Task RemoverBeneficiarioAsync(Guid seguroAssociadoId, Guid beneficiarioId, CancellationToken cancellationToken = default)
        {
            var seguro = await ObterSeguroExistenteAsync(seguroAssociadoId, cancellationToken);
            var beneficiario = seguro.Beneficiarios.FirstOrDefault(x => x.Id == beneficiarioId)
                ?? throw new KeyNotFoundException("Beneficiario nao encontrado para exclusao.");

            seguro.Beneficiarios.Remove(beneficiario);

            ValidarBeneficiarios(seguro);

            await _seguroAssociadoRepository.UpdateAsync(seguro, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<ContatoEmergencia?> ObterContatoEmergenciaAsync(Guid seguroAssociadoId, CancellationToken cancellationToken = default)
        {
            var seguro = await ObterSeguroExistenteAsync(seguroAssociadoId, cancellationToken);
            return seguro.ContatoEmergencia;
        }

        public async Task<ContatoEmergencia> DefinirContatoEmergenciaAsync(
            Guid seguroAssociadoId,
            ContatoEmergencia contatoEmergencia,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(contatoEmergencia);

            var seguro = await ObterSeguroExistenteAsync(seguroAssociadoId, cancellationToken);

            if (seguro.ContatoEmergencia is null)
            {
                contatoEmergencia.Id = contatoEmergencia.Id == Guid.Empty ? Guid.NewGuid() : contatoEmergencia.Id;
                contatoEmergencia.SeguroAssociadoId = seguro.Id;
                seguro.ContatoEmergencia = contatoEmergencia;
            }
            else
            {
                seguro.ContatoEmergencia.NomeCompleto = contatoEmergencia.NomeCompleto;
                seguro.ContatoEmergencia.TelefonePrincipal = contatoEmergencia.TelefonePrincipal;
                seguro.ContatoEmergencia.TelefoneSecundario = contatoEmergencia.TelefoneSecundario;
            }

            ValidarContatoEmergencia(seguro.ContatoEmergencia);

            await _seguroAssociadoRepository.UpdateAsync(seguro, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return seguro.ContatoEmergencia!;
        }

        public async Task<IReadOnlyCollection<SolicitacaoAlteracaoBeneficiario>> ListarSolicitacoesAlteracaoBeneficiarioAsync(
            Guid seguroAssociadoId,
            CancellationToken cancellationToken = default)
        {
            var seguro = await ObterSeguroExistenteAsync(seguroAssociadoId, cancellationToken);
            return seguro.SolicitacoesAlteracaoBeneficiario
                .OrderByDescending(x => x.DataSolicitacao)
                .ToArray();
        }

        public async Task<ConsentimentoLgpdSeguro> RegistrarConsentimentoLgpdAsync(
            Guid seguroAssociadoId,
            ConsentimentoLgpdSeguro consentimentoLgpdSeguro,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(consentimentoLgpdSeguro);

            var seguro = await ObterSeguroExistenteAsync(seguroAssociadoId, cancellationToken);

            if (seguro.ConsentimentoLgpd is null)
            {
                consentimentoLgpdSeguro.Id = consentimentoLgpdSeguro.Id == Guid.Empty ? Guid.NewGuid() : consentimentoLgpdSeguro.Id;
                consentimentoLgpdSeguro.SeguroAssociadoId = seguro.Id;
                seguro.ConsentimentoLgpd = consentimentoLgpdSeguro;
            }
            else
            {
                seguro.ConsentimentoLgpd.Aceito = consentimentoLgpdSeguro.Aceito;
                seguro.ConsentimentoLgpd.DataAceite = consentimentoLgpdSeguro.DataAceite;
                seguro.ConsentimentoLgpd.TextoConsentimento = consentimentoLgpdSeguro.TextoConsentimento;
            }

            ValidarConsentimento(seguro.ConsentimentoLgpd);

            await _seguroAssociadoRepository.UpdateAsync(seguro, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return seguro.ConsentimentoLgpd!;
        }

        public async Task<SolicitacaoAlteracaoBeneficiario> SolicitarAlteracaoBeneficiariosAsync(
            Guid seguroAssociadoId,
            string? observacaoSolicitante,
            CancellationToken cancellationToken = default)
        {
            if (seguroAssociadoId == Guid.Empty)
            {
                throw new ArgumentException("O identificador do seguro deve ser informado.", nameof(seguroAssociadoId));
            }

            var seguro = await _seguroAssociadoRepository.GetByIdAsync(seguroAssociadoId, cancellationToken)
                ?? throw new KeyNotFoundException("Seguro do associado nao encontrado para solicitacao.");

            var solicitacao = new SolicitacaoAlteracaoBeneficiario
            {
                Id = Guid.NewGuid(),
                SeguroAssociadoId = seguroAssociadoId,
                DataSolicitacao = DateTime.UtcNow,
                ObservacaoSolicitante = observacaoSolicitante,
                StatusSolicitacaoBeneficiario = StatusSolicitacaoBeneficiario.Solicitada
            };

            seguro.SolicitacoesAlteracaoBeneficiario.Add(solicitacao);

            await _seguroAssociadoRepository.UpdateAsync(seguro, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return solicitacao;
        }

        private async Task ValidarSeguroAsync(SeguroAssociado seguroAssociado, bool isEdicao, CancellationToken cancellationToken)
        {
            if (seguroAssociado.AssociadoId == Guid.Empty)
            {
                throw new ArgumentException("O associado do seguro deve ser informado.", nameof(seguroAssociado));
            }

            if (string.IsNullOrWhiteSpace(seguroAssociado.Profissao))
            {
                throw new ArgumentException("A profissao do associado deve ser informada no seguro.", nameof(seguroAssociado));
            }

            ValidarBeneficiarios(seguroAssociado);
            ValidarContatoEmergencia(seguroAssociado.ContatoEmergencia);
            ValidarConsentimento(seguroAssociado.ConsentimentoLgpd);

            var seguroExistente = await _seguroAssociadoRepository.GetByAssociadoIdAsync(seguroAssociado.AssociadoId, cancellationToken);
            if (seguroExistente is not null && (!isEdicao || seguroExistente.Id != seguroAssociado.Id))
            {
                throw new InvalidOperationException("Ja existe um seguro cadastrado para o associado informado.");
            }
        }

        private async Task<SeguroAssociado> ObterSeguroExistenteAsync(Guid seguroAssociadoId, CancellationToken cancellationToken)
        {
            if (seguroAssociadoId == Guid.Empty)
            {
                throw new ArgumentException("O identificador do seguro deve ser informado.", nameof(seguroAssociadoId));
            }

            return await _seguroAssociadoRepository.GetByIdAsync(seguroAssociadoId, cancellationToken)
                ?? throw new KeyNotFoundException("Seguro do associado nao encontrado.");
        }

        private static SeguroAssociado AplicarDadosSeguro(SeguroAssociado origem, SeguroAssociado destino)
        {
            destino.AssociadoId = origem.AssociadoId;
            destino.EstadoCivil = origem.EstadoCivil;
            destino.Profissao = origem.Profissao;
            destino.DataCadastro = destino.DataCadastro;
            destino.Beneficiarios = origem.Beneficiarios;
            destino.ContatoEmergencia = origem.ContatoEmergencia;
            destino.ConsentimentoLgpd = origem.ConsentimentoLgpd;
            destino.SolicitacoesAlteracaoBeneficiario = origem.SolicitacoesAlteracaoBeneficiario;

            return destino;
        }

        private static void ValidarBeneficiarios(SeguroAssociado seguroAssociado)
        {
            if (!seguroAssociado.Beneficiarios.Any())
            {
                throw new InvalidOperationException("O seguro deve possuir ao menos um beneficiario.");
            }

            if (seguroAssociado.Beneficiarios.Any(x =>
                string.IsNullOrWhiteSpace(x.NomeCompleto) ||
                string.IsNullOrWhiteSpace(x.Cpf) ||
                string.IsNullOrWhiteSpace(x.GrauParentesco) ||
                string.IsNullOrWhiteSpace(x.Telefone)))
            {
                throw new InvalidOperationException("Todos os beneficiarios devem possuir dados obrigatorios preenchidos.");
            }

            var percentualTotal = seguroAssociado.Beneficiarios.Sum(x => x.Percentual);
            if (percentualTotal <= 0 || percentualTotal > 100)
            {
                throw new InvalidOperationException("A soma dos percentuais dos beneficiarios deve ser maior que zero e no maximo 100.");
            }
        }

        private static void ValidarContatoEmergencia(ContatoEmergencia? contatoEmergencia)
        {
            if (contatoEmergencia is not null &&
                (string.IsNullOrWhiteSpace(contatoEmergencia.NomeCompleto) ||
                 string.IsNullOrWhiteSpace(contatoEmergencia.TelefonePrincipal)))
            {
                throw new InvalidOperationException("Contato de emergencia informado deve possuir nome e telefone principal.");
            }
        }

        private static void ValidarConsentimento(ConsentimentoLgpdSeguro? consentimento)
        {
            if (consentimento is null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(consentimento.TextoConsentimento))
            {
                throw new InvalidOperationException("O texto do consentimento LGPD deve ser informado.");
            }

            if (consentimento.Aceito && !consentimento.DataAceite.HasValue)
            {
                throw new InvalidOperationException("Consentimento LGPD aceito deve possuir data de aceite.");
            }
        }
    }
}
