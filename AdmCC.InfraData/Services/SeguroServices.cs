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

            await ValidarSeguroAsync(seguroAssociado, isEdicao: true, cancellationToken);

            seguroAssociado.DataCadastro = existente.DataCadastro;

            await _seguroAssociadoRepository.UpdateAsync(seguroAssociado, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return seguroAssociado;
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

            if (seguroAssociado.ContatoEmergencia is not null &&
                (string.IsNullOrWhiteSpace(seguroAssociado.ContatoEmergencia.NomeCompleto) ||
                 string.IsNullOrWhiteSpace(seguroAssociado.ContatoEmergencia.TelefonePrincipal)))
            {
                throw new InvalidOperationException("Contato de emergencia informado deve possuir nome e telefone principal.");
            }

            if (seguroAssociado.ConsentimentoLgpd is not null)
            {
                if (string.IsNullOrWhiteSpace(seguroAssociado.ConsentimentoLgpd.TextoConsentimento))
                {
                    throw new InvalidOperationException("O texto do consentimento LGPD deve ser informado.");
                }

                if (seguroAssociado.ConsentimentoLgpd.Aceito && !seguroAssociado.ConsentimentoLgpd.DataAceite.HasValue)
                {
                    throw new InvalidOperationException("Consentimento LGPD aceito deve possuir data de aceite.");
                }
            }

            var seguroExistente = await _seguroAssociadoRepository.GetByAssociadoIdAsync(seguroAssociado.AssociadoId, cancellationToken);
            if (seguroExistente is not null && (!isEdicao || seguroExistente.Id != seguroAssociado.Id))
            {
                throw new InvalidOperationException("Ja existe um seguro cadastrado para o associado informado.");
            }
        }
    }
}
