using AdmCC.Domain.ConexoesParcerias.Entities;
using AdmCC.Domain.ConexoesParcerias.Enums;
using AdmCC.Domain.ConexoesParcerias.Interfaces;
using AdmCC.Domain.RelatoriosAuditorias.Interfaces;

namespace AdmCC.InfraData.Services
{
    public class ConexoesParceriasService
    {
        private readonly IConexaoEstrategicaRepository _conexaoEstrategicaRepository;
        private readonly IParceriaAssociadoRepository _parceriaAssociadoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConexoesParceriasService(
            IConexaoEstrategicaRepository conexaoEstrategicaRepository,
            IParceriaAssociadoRepository parceriaAssociadoRepository,
            IUnitOfWork unitOfWork)
        {
            _conexaoEstrategicaRepository = conexaoEstrategicaRepository;
            _parceriaAssociadoRepository = parceriaAssociadoRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<ConexaoEstrategica?> ObterConexaoPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _conexaoEstrategicaRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<IReadOnlyCollection<ConexaoEstrategica>> ListarConexoesAsync(CancellationToken cancellationToken = default)
        {
            return _conexaoEstrategicaRepository.GetAllAsync(cancellationToken);
        }

        public Task<IReadOnlyCollection<ConexaoEstrategica>> ListarConexoesPorOrigemAsync(Guid associadoOrigemId, CancellationToken cancellationToken = default)
        {
            if (associadoOrigemId == Guid.Empty)
            {
                throw new ArgumentException("O associado de origem deve ser informado.", nameof(associadoOrigemId));
            }

            return _conexaoEstrategicaRepository.GetByAssociadoOrigemIdAsync(associadoOrigemId, cancellationToken);
        }

        public Task<IReadOnlyCollection<ConexaoEstrategica>> ListarConexoesPorDestinoAsync(Guid associadoDestinoId, CancellationToken cancellationToken = default)
        {
            if (associadoDestinoId == Guid.Empty)
            {
                throw new ArgumentException("O associado de destino deve ser informado.", nameof(associadoDestinoId));
            }

            return _conexaoEstrategicaRepository.GetByAssociadoDestinoIdAsync(associadoDestinoId, cancellationToken);
        }

        public async Task<ConexaoEstrategica> CriarConexaoAsync(ConexaoEstrategica conexaoEstrategica, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(conexaoEstrategica);

            await ValidarConexaoAsync(conexaoEstrategica, cancellationToken);

            if (conexaoEstrategica.Id == Guid.Empty)
            {
                conexaoEstrategica.Id = Guid.NewGuid();
            }

            if (conexaoEstrategica.DataEnvio == default)
            {
                conexaoEstrategica.DataEnvio = DateTime.UtcNow;
            }

            await _conexaoEstrategicaRepository.AddAsync(conexaoEstrategica, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return conexaoEstrategica;
        }

        public async Task<ConexaoEstrategica> AtualizarConexaoAsync(ConexaoEstrategica conexaoEstrategica, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(conexaoEstrategica);

            if (conexaoEstrategica.Id == Guid.Empty)
            {
                throw new ArgumentException("O identificador da conexao deve ser informado para atualizacao.", nameof(conexaoEstrategica));
            }

            var existente = await _conexaoEstrategicaRepository.GetByIdAsync(conexaoEstrategica.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Conexao estrategica nao encontrada para atualizacao.");

            var conexaoAtualizada = AplicarDadosConexao(conexaoEstrategica, existente);

            await ValidarConexaoAsync(conexaoAtualizada, cancellationToken);

            await _conexaoEstrategicaRepository.UpdateAsync(conexaoAtualizada, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return conexaoAtualizada;
        }

        public async Task AtualizarStatusConexaoAsync(Guid id, StatusConexao statusConexao, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador da conexao deve ser informado.", nameof(id));
            }

            var conexao = await _conexaoEstrategicaRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Conexao estrategica nao encontrada para atualizacao de status.");

            conexao.StatusConexao = statusConexao;
            await _conexaoEstrategicaRepository.UpdateAsync(conexao, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<NegocioRecebidoValidacao> ValidarNegocioRecebidoAsync(
            Guid conexaoId,
            NegocioRecebidoValidacao validacao,
            CancellationToken cancellationToken = default)
        {
            if (conexaoId == Guid.Empty)
            {
                throw new ArgumentException("O identificador da conexao deve ser informado.", nameof(conexaoId));
            }

            ArgumentNullException.ThrowIfNull(validacao);

            var conexao = await _conexaoEstrategicaRepository.GetByIdAsync(conexaoId, cancellationToken)
                ?? throw new KeyNotFoundException("Conexao estrategica nao encontrada para validacao.");

            ValidarNegocioRecebido(validacao, conexao.AssociadoDestinoId);

            var validacaoAtual = conexao.ValidacaoRecebimento;
            if (validacaoAtual is null)
            {
                validacaoAtual = new NegocioRecebidoValidacao
                {
                    Id = validacao.Id == Guid.Empty ? Guid.NewGuid() : validacao.Id,
                    ConexaoEstrategicaId = conexao.Id
                };

                conexao.ValidacaoRecebimento = validacaoAtual;
            }

            validacaoAtual.AssociadoReceptorId = validacao.AssociadoReceptorId;
            validacaoAtual.StatusConexao = validacao.StatusConexao;
            validacaoAtual.MotivoNegocioNaoFechado = validacao.MotivoNegocioNaoFechado;
            validacaoAtual.ValorNegocioFechado = validacao.ValorNegocioFechado;
            validacaoAtual.DataValidacao = validacao.DataValidacao == default ? DateTime.UtcNow : validacao.DataValidacao;
            validacaoAtual.PrazoEstourado = validacao.PrazoEstourado;
            validacaoAtual.DataPrazoEstourado = validacao.PrazoEstourado
                ? validacao.DataPrazoEstourado ?? DateTime.UtcNow
                : null;

            conexao.StatusConexao = validacao.StatusConexao;

            await _conexaoEstrategicaRepository.UpdateAsync(conexao, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return validacaoAtual;
        }

        public async Task ExcluirConexaoAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador da conexao deve ser informado.", nameof(id));
            }

            var conexao = await _conexaoEstrategicaRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Conexao estrategica nao encontrada para exclusao.");

            if (conexao.StatusConexao == StatusConexao.Fechada)
            {
                throw new InvalidOperationException("Nao e permitido excluir conexao que ja foi marcada como fechada.");
            }

            await _conexaoEstrategicaRepository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public Task<ParceriaAssociado?> ObterParceriaPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _parceriaAssociadoRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<IReadOnlyCollection<ParceriaAssociado>> ListarParceriasAsync(CancellationToken cancellationToken = default)
        {
            return _parceriaAssociadoRepository.GetAllAsync(cancellationToken);
        }

        public Task<IReadOnlyCollection<ParceriaAssociado>> ListarParceriasAtivasPorAssociadoAsync(Guid associadoId, CancellationToken cancellationToken = default)
        {
            if (associadoId == Guid.Empty)
            {
                throw new ArgumentException("O associado deve ser informado.", nameof(associadoId));
            }

            return _parceriaAssociadoRepository.GetAtivasByAssociadoIdAsync(associadoId, cancellationToken);
        }

        public async Task<ParceriaAssociado> CriarParceriaAsync(ParceriaAssociado parceriaAssociado, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(parceriaAssociado);

            await ValidarParceriaAsync(parceriaAssociado, isEdicao: false, cancellationToken);

            if (parceriaAssociado.Id == Guid.Empty)
            {
                parceriaAssociado.Id = Guid.NewGuid();
            }

            if (parceriaAssociado.DataParceria == default)
            {
                parceriaAssociado.DataParceria = DateTime.UtcNow;
            }

            await _parceriaAssociadoRepository.AddAsync(parceriaAssociado, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return parceriaAssociado;
        }

        public async Task<ParceriaAssociado> AtualizarParceriaAsync(ParceriaAssociado parceriaAssociado, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(parceriaAssociado);

            if (parceriaAssociado.Id == Guid.Empty)
            {
                throw new ArgumentException("O identificador da parceria deve ser informado para atualizacao.", nameof(parceriaAssociado));
            }

            var existente = await _parceriaAssociadoRepository.GetByIdAsync(parceriaAssociado.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Parceria nao encontrada para atualizacao.");

            var parceriaAtualizada = AplicarDadosParceria(parceriaAssociado, existente);

            await ValidarParceriaAsync(parceriaAtualizada, isEdicao: true, cancellationToken);

            await _parceriaAssociadoRepository.UpdateAsync(parceriaAtualizada, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return parceriaAtualizada;
        }

        public async Task EncerrarParceriaAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador da parceria deve ser informado.", nameof(id));
            }

            var parceria = await _parceriaAssociadoRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Parceria nao encontrada para encerramento.");

            if (!parceria.Ativa)
            {
                return;
            }

            parceria.Ativa = false;
            await _parceriaAssociadoRepository.UpdateAsync(parceria, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task ValidarConexaoAsync(ConexaoEstrategica conexaoEstrategica, CancellationToken cancellationToken)
        {
            if (conexaoEstrategica.AssociadoOrigemId == Guid.Empty || conexaoEstrategica.AssociadoDestinoId == Guid.Empty)
            {
                throw new ArgumentException("Os associados de origem e destino devem ser informados.", nameof(conexaoEstrategica));
            }

            if (conexaoEstrategica.AssociadoOrigemId == conexaoEstrategica.AssociadoDestinoId)
            {
                throw new InvalidOperationException("O associado de origem nao pode ser o mesmo associado de destino.");
            }

            if (string.IsNullOrWhiteSpace(conexaoEstrategica.NomeContatoOuEmpresa))
            {
                throw new ArgumentException("O nome do contato ou empresa deve ser informado.", nameof(conexaoEstrategica));
            }

            if (string.IsNullOrWhiteSpace(conexaoEstrategica.TelefoneContato))
            {
                throw new ArgumentException("O telefone do contato deve ser informado.", nameof(conexaoEstrategica));
            }

            var conexoesOrigem = await _conexaoEstrategicaRepository.GetByAssociadoOrigemIdAsync(conexaoEstrategica.AssociadoOrigemId, cancellationToken);
            var duplicada = conexoesOrigem.Any(x =>
                x.AssociadoDestinoId == conexaoEstrategica.AssociadoDestinoId &&
                x.Id != conexaoEstrategica.Id &&
                !x.Excluida);

            if (duplicada)
            {
                throw new InvalidOperationException("Ja existe uma conexao estrategica ativa entre esses associados.");
            }
        }

        private async Task ValidarParceriaAsync(ParceriaAssociado parceriaAssociado, bool isEdicao, CancellationToken cancellationToken)
        {
            if (parceriaAssociado.AssociadoOrigemId == Guid.Empty || parceriaAssociado.AssociadoDestinoId == Guid.Empty)
            {
                throw new ArgumentException("Os associados de origem e destino devem ser informados.", nameof(parceriaAssociado));
            }

            if (parceriaAssociado.AssociadoOrigemId == parceriaAssociado.AssociadoDestinoId)
            {
                throw new InvalidOperationException("Nao e permitido criar parceria do associado com ele mesmo.");
            }

            if (parceriaAssociado.DataParceria != default && parceriaAssociado.DataParceria.Date > DateTime.UtcNow.Date)
            {
                throw new InvalidOperationException("A data da parceria nao pode estar no futuro.");
            }

            var parcerias = await _parceriaAssociadoRepository.GetAtivasByAssociadoIdAsync(parceriaAssociado.AssociadoOrigemId, cancellationToken);
            var duplicada = parcerias.Any(x =>
                x.Ativa &&
                ((x.AssociadoOrigemId == parceriaAssociado.AssociadoOrigemId && x.AssociadoDestinoId == parceriaAssociado.AssociadoDestinoId) ||
                 (x.AssociadoOrigemId == parceriaAssociado.AssociadoDestinoId && x.AssociadoDestinoId == parceriaAssociado.AssociadoOrigemId)) &&
                (!isEdicao || x.Id != parceriaAssociado.Id));

            if (duplicada)
            {
                throw new InvalidOperationException("Ja existe uma parceria ativa entre os associados informados.");
            }
        }

        private static void ValidarNegocioRecebido(NegocioRecebidoValidacao validacao, Guid associadoDestinoId)
        {
            if (validacao.AssociadoReceptorId == Guid.Empty)
            {
                throw new ArgumentException("O associado receptor deve ser informado.", nameof(validacao));
            }

            if (validacao.AssociadoReceptorId != associadoDestinoId)
            {
                throw new InvalidOperationException("A validacao deve ser registrada pelo associado de destino da conexao.");
            }

            if (!Enum.IsDefined(validacao.StatusConexao))
            {
                throw new ArgumentException("O status da conexao informado para validacao e invalido.", nameof(validacao));
            }

            if (validacao.StatusConexao == StatusConexao.Fechada && (!validacao.ValorNegocioFechado.HasValue || validacao.ValorNegocioFechado.Value <= 0))
            {
                throw new InvalidOperationException("Negocios fechados devem informar um valor positivo.");
            }

            if (validacao.StatusConexao == StatusConexao.NegocioNaoFechado && !validacao.MotivoNegocioNaoFechado.HasValue)
            {
                throw new InvalidOperationException("Negocios nao fechados devem informar o motivo.");
            }

            if (validacao.DataValidacao.HasValue && validacao.DataValidacao.Value > DateTime.UtcNow)
            {
                throw new InvalidOperationException("A data de validacao nao pode estar no futuro.");
            }

            if (validacao.PrazoEstourado && validacao.DataPrazoEstourado.HasValue && validacao.DataPrazoEstourado.Value > DateTime.UtcNow)
            {
                throw new InvalidOperationException("A data de prazo estourado nao pode estar no futuro.");
            }
        }

        private static ConexaoEstrategica AplicarDadosConexao(ConexaoEstrategica origem, ConexaoEstrategica destino)
        {
            destino.AssociadoOrigemId = origem.AssociadoOrigemId;
            destino.AssociadoDestinoId = origem.AssociadoDestinoId;
            destino.NomeContatoOuEmpresa = origem.NomeContatoOuEmpresa;
            destino.TelefoneContato = origem.TelefoneContato;
            destino.Complemento = origem.Complemento;
            destino.TipoDeConexao = origem.TipoDeConexao;
            destino.StatusConexao = origem.StatusConexao;
            destino.Excluida = origem.Excluida;
            destino.DataEnvio = destino.DataEnvio == default ? DateTime.UtcNow : destino.DataEnvio;

            return destino;
        }

        private static ParceriaAssociado AplicarDadosParceria(ParceriaAssociado origem, ParceriaAssociado destino)
        {
            destino.AssociadoOrigemId = origem.AssociadoOrigemId;
            destino.AssociadoDestinoId = origem.AssociadoDestinoId;
            destino.Ativa = origem.Ativa;
            destino.DataParceria = destino.DataParceria == default
                ? (origem.DataParceria == default ? DateTime.UtcNow : origem.DataParceria)
                : destino.DataParceria;

            return destino;
        }
    }
}
