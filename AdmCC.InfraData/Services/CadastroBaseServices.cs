using AdmCC.Domain.CadastroBase.Entities;
using AdmCC.Domain.CadastroBase.Enums;
using AdmCC.Domain.CadastroBase.Interfaces;
using AdmCC.Domain.RelatoriosAuditorias.Interfaces;

namespace AdmCC.InfraData.Services
{
    // Empresa e endereco entram aqui como dependencias obrigatorias do agregado Associado.
    public class CadastroBaseService
    {
        private readonly IAssociadoRepository _associadoRepository;
        private readonly IAnuidadeRepository _anuidadeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CadastroBaseService(
            IAssociadoRepository associadoRepository,
            IAnuidadeRepository anuidadeRepository,
            IUnitOfWork unitOfWork)
        {
            _associadoRepository = associadoRepository;
            _anuidadeRepository = anuidadeRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Associado?> ObterAssociadoPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _associadoRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<IReadOnlyCollection<Associado>> ListarAssociadosAsync(CancellationToken cancellationToken = default)
        {
            return _associadoRepository.GetAllAsync(cancellationToken);
        }

        public Task<IReadOnlyCollection<Associado>> ListarAssociadosPorEquipeAsync(Guid equipeId, CancellationToken cancellationToken = default)
        {
            if (equipeId == Guid.Empty)
            {
                throw new ArgumentException("O identificador da equipe atual deve ser informado.", nameof(equipeId));
            }

            return _associadoRepository.GetByEquipeAtualIdAsync(equipeId, cancellationToken);
        }

        public async Task<Associado> CriarAssociadoAsync(Associado associado, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(associado);

            await ValidarAssociadoAsync(associado, isEdicao: false, cancellationToken);

            if (associado.Id == Guid.Empty)
            {
                associado.Id = Guid.NewGuid();
            }

            if (associado.DataCadastro == default)
            {
                associado.DataCadastro = DateTime.UtcNow;
            }

            await _associadoRepository.AddAsync(associado, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return associado;
        }

        public async Task<Associado> AtualizarAssociadoAsync(Associado associado, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(associado);

            if (associado.Id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do associado deve ser informado para atualizacao.", nameof(associado));
            }

            var existente = await _associadoRepository.GetByIdAsync(associado.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Associado nao encontrado para atualizacao.");

            var associadoAtualizado = AplicarDadosAssociado(associado, existente);

            await ValidarAssociadoAsync(associadoAtualizado, isEdicao: true, cancellationToken);

            await _associadoRepository.UpdateAsync(associadoAtualizado, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return associadoAtualizado;
        }

        public async Task<Associado> AlterarStatusAssociadoAsync(
            Guid associadoId,
            StatusAssociado novoStatus,
            Guid usuarioResponsavelId,
            string? motivo,
            CancellationToken cancellationToken = default)
        {
            if (associadoId == Guid.Empty)
            {
                throw new ArgumentException("O identificador do associado deve ser informado.", nameof(associadoId));
            }

            if (usuarioResponsavelId == Guid.Empty)
            {
                throw new ArgumentException("O usuario responsavel pela alteracao deve ser informado.", nameof(usuarioResponsavelId));
            }

            if (!Enum.IsDefined(novoStatus))
            {
                throw new ArgumentException("O novo status informado e invalido.", nameof(novoStatus));
            }

            var associado = await _associadoRepository.GetByIdAsync(associadoId, cancellationToken)
                ?? throw new KeyNotFoundException("Associado nao encontrado para alteracao de status.");

            if (associado.StatusAssociado == novoStatus)
            {
                throw new InvalidOperationException("O associado ja se encontra no status informado.");
            }

            associado.HistoricoStatus.Add(new HistoricoAssociado
            {
                Id = Guid.NewGuid(),
                AssociadoId = associado.Id,
                StatusAnterior = associado.StatusAssociado,
                StatusNovo = novoStatus,
                DataAlteracao = DateTime.UtcNow,
                Motivo = string.IsNullOrWhiteSpace(motivo) ? null : motivo.Trim(),
                UsuarioResponsavelId = usuarioResponsavelId
            });

            associado.StatusAssociado = novoStatus;

            await _associadoRepository.UpdateAsync(associado, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return associado;
        }

        public async Task ExcluirAssociadoAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do associado deve ser informado.", nameof(id));
            }

            var associado = await _associadoRepository.GetByIdAsync(id, cancellationToken);

            if (associado is null)
            {
                throw new KeyNotFoundException("Associado nao encontrado para exclusao.");
            }

            await _associadoRepository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public Task<Anuidade?> ObterAnuidadePorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _anuidadeRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<IReadOnlyCollection<Anuidade>> ListarAnuidadesAsync(CancellationToken cancellationToken = default)
        {
            return _anuidadeRepository.GetAllAsync(cancellationToken);
        }

        public async Task<Anuidade> CriarAnuidadeAsync(Anuidade anuidade, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(anuidade);

            ValidarAnuidade(anuidade);

            if (anuidade.Id == Guid.Empty)
            {
                anuidade.Id = Guid.NewGuid();
            }

            await _anuidadeRepository.AddAsync(anuidade, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return anuidade;
        }

        public async Task<Anuidade> AtualizarAnuidadeAsync(Anuidade anuidade, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(anuidade);

            if (anuidade.Id == Guid.Empty)
            {
                throw new ArgumentException("O identificador da anuidade deve ser informado para atualizacao.", nameof(anuidade));
            }

            var existente = await _anuidadeRepository.GetByIdAsync(anuidade.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Anuidade nao encontrada para atualizacao.");

            ValidarAnuidade(anuidade);

            if (anuidade.DataPagamentoPrimeiraAnuidade == default)
            {
                anuidade.DataPagamentoPrimeiraAnuidade = existente.DataPagamentoPrimeiraAnuidade;
            }

            if (anuidade.DataUltimaRenovacao == default)
            {
                anuidade.DataUltimaRenovacao = existente.DataUltimaRenovacao;
            }

            await _anuidadeRepository.UpdateAsync(anuidade, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return anuidade;
        }

        public async Task ExcluirAnuidadeAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador da anuidade deve ser informado.", nameof(id));
            }

            var anuidade = await _anuidadeRepository.GetByIdAsync(id, cancellationToken);

            if (anuidade is null)
            {
                throw new KeyNotFoundException("Anuidade nao encontrada para exclusao.");
            }

            await _anuidadeRepository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<Anuidade> RegistrarPagamentoAnuidadeAsync(Guid anuidadeId, DateTime dataPagamento, CancellationToken cancellationToken = default)
        {
            if (anuidadeId == Guid.Empty)
            {
                throw new ArgumentException("O identificador da anuidade deve ser informado.", nameof(anuidadeId));
            }

            if (dataPagamento == default)
            {
                throw new ArgumentException("A data de pagamento deve ser informada.", nameof(dataPagamento));
            }

            if (dataPagamento.Date > DateTime.UtcNow.Date)
            {
                throw new InvalidOperationException("Nao e permitido registrar pagamento de anuidade com data futura.");
            }

            var anuidade = await _anuidadeRepository.GetByIdAsync(anuidadeId, cancellationToken)
                ?? throw new KeyNotFoundException("Anuidade nao encontrada para registrar pagamento.");

            if (anuidade.DataPagamentoPrimeiraAnuidade is null)
            {
                anuidade.DataPagamentoPrimeiraAnuidade = dataPagamento;
            }

            anuidade.DataUltimaRenovacao = dataPagamento;
            anuidade.StatusAnuidade = StatusAnuidade.Pago;

            await _anuidadeRepository.UpdateAsync(anuidade, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return anuidade;
        }

        private async Task ValidarAssociadoAsync(Associado associado, bool isEdicao, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(associado.NomeCompleto))
            {
                throw new ArgumentException("O nome completo do associado deve ser informado.", nameof(associado));
            }

            if (string.IsNullOrWhiteSpace(associado.Cpf))
            {
                throw new ArgumentException("O CPF do associado deve ser informado.", nameof(associado));
            }

            if (string.IsNullOrWhiteSpace(associado.EmailPrincipal))
            {
                throw new ArgumentException("O e-mail principal do associado deve ser informado.", nameof(associado));
            }

            if (string.IsNullOrWhiteSpace(associado.TelefoneWhatsappPrincipal))
            {
                throw new ArgumentException("O telefone principal do associado deve ser informado.", nameof(associado));
            }

            if (associado.DataNascimento == default)
            {
                throw new ArgumentException("A data de nascimento do associado deve ser informada.", nameof(associado));
            }

            if (associado.DataNascimento.Date >= DateTime.UtcNow.Date)
            {
                throw new InvalidOperationException("A data de nascimento do associado deve ser anterior a data atual.");
            }

            if (associado.DataIngresso == default)
            {
                throw new ArgumentException("A data de ingresso do associado deve ser informada.", nameof(associado));
            }

            if (associado.DataIngresso.Date > DateTime.UtcNow.Date)
            {
                throw new InvalidOperationException("A data de ingresso do associado nao pode estar no futuro.");
            }

            if (!Enum.IsDefined(associado.StatusAssociado))
            {
                throw new ArgumentException("O status do associado informado e invalido.", nameof(associado));
            }

            if (associado.PadrinhoId == Guid.Empty)
            {
                throw new ArgumentException("O padrinho do associado deve ser informado.", nameof(associado));
            }

            if (associado.PadrinhoId == associado.Id)
            {
                throw new InvalidOperationException("O associado nao pode ser padrinho de si mesmo.");
            }

            if (associado.EnderecoId == Guid.Empty ||
                associado.EmpresaId == Guid.Empty ||
                associado.AnuidadeId == Guid.Empty ||
                associado.EquipeOrigemId == Guid.Empty ||
                associado.EquipeAtualId == Guid.Empty ||
                associado.ClusterId == Guid.Empty ||
                associado.AtuacaoEspecificaId == Guid.Empty)
            {
                throw new ArgumentException("Todos os relacionamentos obrigatorios do associado devem ser informados.", nameof(associado));
            }

            var associadoPorCpf = await _associadoRepository.GetByCpfAsync(associado.Cpf, cancellationToken);
            if (associadoPorCpf is not null && (!isEdicao || associadoPorCpf.Id != associado.Id))
            {
                throw new InvalidOperationException("Ja existe um associado cadastrado com o CPF informado.");
            }

            var associadoPorEmail = await _associadoRepository.GetByEmailAsync(associado.EmailPrincipal, cancellationToken);
            if (associadoPorEmail is not null && (!isEdicao || associadoPorEmail.Id != associado.Id))
            {
                throw new InvalidOperationException("Ja existe um associado cadastrado com o e-mail informado.");
            }
        }

        private static Associado AplicarDadosAssociado(Associado origem, Associado destino)
        {
            destino.NomeCompleto = origem.NomeCompleto;
            destino.Cpf = origem.Cpf;
            destino.DataNascimento = origem.DataNascimento;
            destino.PermitirExibirAniversario = origem.PermitirExibirAniversario;
            destino.EmailPrincipal = origem.EmailPrincipal;
            destino.TelefoneWhatsappPrincipal = origem.TelefoneWhatsappPrincipal;
            destino.DataIngresso = origem.DataIngresso;
            destino.StatusAssociado = origem.StatusAssociado;
            destino.EnderecoId = origem.EnderecoId;
            destino.EmpresaId = origem.EmpresaId;
            destino.AnuidadeId = origem.AnuidadeId;
            destino.PadrinhoId = origem.PadrinhoId;
            destino.EquipeOrigemId = origem.EquipeOrigemId;
            destino.EquipeAtualId = origem.EquipeAtualId;
            destino.ClusterId = origem.ClusterId;
            destino.AtuacaoEspecificaId = origem.AtuacaoEspecificaId;
            destino.DataCadastro = destino.DataCadastro == default ? DateTime.UtcNow : destino.DataCadastro;

            return destino;
        }

        private static void ValidarAnuidade(Anuidade anuidade)
        {
            if (anuidade.VencimentoAtual == default)
            {
                throw new ArgumentException("O vencimento atual da anuidade deve ser informado.", nameof(anuidade));
            }

            if (anuidade.StatusAnuidade == StatusAnuidade.Pago && anuidade.DataPagamentoPrimeiraAnuidade is null)
            {
                throw new InvalidOperationException("Nao e permitido marcar anuidade como paga sem data de pagamento.");
            }

            if (anuidade.DataUltimaRenovacao.HasValue &&
                anuidade.DataPagamentoPrimeiraAnuidade.HasValue &&
                anuidade.DataUltimaRenovacao.Value < anuidade.DataPagamentoPrimeiraAnuidade.Value)
            {
                throw new InvalidOperationException("A ultima renovacao nao pode ser anterior ao primeiro pagamento.");
            }
        }
    }
}
