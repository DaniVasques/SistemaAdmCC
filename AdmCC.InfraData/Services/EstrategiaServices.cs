using AdmCC.Domain.Estrategia.Entities;
using AdmCC.Domain.Estrategia.Interfaces;
using AdmCC.Domain.RelatoriosAuditorias.Interfaces;

namespace AdmCC.InfraData.Services
{
    // O escopo publico atual do modulo cobre catalogos estrategicos.
    public class EstrategiaService
    {
        private readonly IClusterRepository _clusterRepository;
        private readonly IAtuacaoEspecificaRepository _atuacaoEspecificaRepository;
        private readonly IGrupamentoEstrategicoRepository _grupamentoEstrategicoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EstrategiaService(
            IClusterRepository clusterRepository,
            IAtuacaoEspecificaRepository atuacaoEspecificaRepository,
            IGrupamentoEstrategicoRepository grupamentoEstrategicoRepository,
            IUnitOfWork unitOfWork)
        {
            _clusterRepository = clusterRepository;
            _atuacaoEspecificaRepository = atuacaoEspecificaRepository;
            _grupamentoEstrategicoRepository = grupamentoEstrategicoRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Cluster?> ObterClusterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _clusterRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<IReadOnlyCollection<Cluster>> ListarClustersAsync(CancellationToken cancellationToken = default)
        {
            return _clusterRepository.GetAllAsync(cancellationToken);
        }

        public async Task<Cluster> CriarClusterAsync(Cluster cluster, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(cluster);

            await ValidarClusterAsync(cluster, isEdicao: false, cancellationToken);

            if (cluster.Id == Guid.Empty)
            {
                cluster.Id = Guid.NewGuid();
            }

            await _clusterRepository.AddAsync(cluster, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return cluster;
        }

        public async Task<Cluster> AtualizarClusterAsync(Cluster cluster, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(cluster);

            if (cluster.Id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do cluster deve ser informado para atualizacao.", nameof(cluster));
            }

            var existente = await _clusterRepository.GetByIdAsync(cluster.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Cluster nao encontrado para atualizacao.");

            await ValidarClusterAsync(cluster, isEdicao: true, cancellationToken);

            cluster.AtuacoesEspecificas = existente.AtuacoesEspecificas;
            cluster.Associados = existente.Associados;

            await _clusterRepository.UpdateAsync(cluster, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return cluster;
        }

        public async Task ExcluirClusterAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do cluster deve ser informado.", nameof(id));
            }

            var cluster = await _clusterRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Cluster nao encontrado para exclusao.");

            if (cluster.AtuacoesEspecificas.Any() || cluster.Associados.Any())
            {
                throw new InvalidOperationException("Nao e permitido excluir cluster com atuacoes ou associados vinculados.");
            }

            await _clusterRepository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public Task<AtuacaoEspecifica?> ObterAtuacaoPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _atuacaoEspecificaRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<IReadOnlyCollection<AtuacaoEspecifica>> ListarAtuacoesAsync(CancellationToken cancellationToken = default)
        {
            return _atuacaoEspecificaRepository.GetAllAsync(cancellationToken);
        }

        public Task<IReadOnlyCollection<AtuacaoEspecifica>> ListarAtuacoesPorClusterAsync(Guid clusterId, CancellationToken cancellationToken = default)
        {
            if (clusterId == Guid.Empty)
            {
                throw new ArgumentException("O identificador do cluster deve ser informado.", nameof(clusterId));
            }

            return _atuacaoEspecificaRepository.GetByClusterIdAsync(clusterId, cancellationToken);
        }

        public async Task<AtuacaoEspecifica> CriarAtuacaoAsync(AtuacaoEspecifica atuacaoEspecifica, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(atuacaoEspecifica);

            await ValidarAtuacaoAsync(atuacaoEspecifica, isEdicao: false, cancellationToken);

            if (atuacaoEspecifica.Id == Guid.Empty)
            {
                atuacaoEspecifica.Id = Guid.NewGuid();
            }

            await _atuacaoEspecificaRepository.AddAsync(atuacaoEspecifica, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return atuacaoEspecifica;
        }

        public async Task<AtuacaoEspecifica> AtualizarAtuacaoAsync(AtuacaoEspecifica atuacaoEspecifica, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(atuacaoEspecifica);

            if (atuacaoEspecifica.Id == Guid.Empty)
            {
                throw new ArgumentException("O identificador da atuacao deve ser informado para atualizacao.", nameof(atuacaoEspecifica));
            }

            var existente = await _atuacaoEspecificaRepository.GetByIdAsync(atuacaoEspecifica.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Atuacao especifica nao encontrada para atualizacao.");

            await ValidarAtuacaoAsync(atuacaoEspecifica, isEdicao: true, cancellationToken);

            atuacaoEspecifica.Associados = existente.Associados;

            await _atuacaoEspecificaRepository.UpdateAsync(atuacaoEspecifica, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return atuacaoEspecifica;
        }

        public async Task ExcluirAtuacaoAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador da atuacao deve ser informado.", nameof(id));
            }

            var atuacao = await _atuacaoEspecificaRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Atuacao especifica nao encontrada para exclusao.");

            if (atuacao.Associados.Any())
            {
                throw new InvalidOperationException("Nao e permitido excluir atuacao especifica com associados vinculados.");
            }

            await _atuacaoEspecificaRepository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public Task<GrupamentoEstrategico?> ObterGrupamentoPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _grupamentoEstrategicoRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<IReadOnlyCollection<GrupamentoEstrategico>> ListarGrupamentosAsync(CancellationToken cancellationToken = default)
        {
            return _grupamentoEstrategicoRepository.GetAllAsync(cancellationToken);
        }

        public async Task<GrupamentoEstrategico> CriarGrupamentoAsync(GrupamentoEstrategico grupamentoEstrategico, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(grupamentoEstrategico);

            await ValidarGrupamentoAsync(grupamentoEstrategico, isEdicao: false, cancellationToken);

            if (grupamentoEstrategico.Id == Guid.Empty)
            {
                grupamentoEstrategico.Id = Guid.NewGuid();
            }

            await _grupamentoEstrategicoRepository.AddAsync(grupamentoEstrategico, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return grupamentoEstrategico;
        }

        public async Task<GrupamentoEstrategico> AtualizarGrupamentoAsync(GrupamentoEstrategico grupamentoEstrategico, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(grupamentoEstrategico);

            if (grupamentoEstrategico.Id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do grupamento deve ser informado para atualizacao.", nameof(grupamentoEstrategico));
            }

            var existente = await _grupamentoEstrategicoRepository.GetByIdAsync(grupamentoEstrategico.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Grupamento estrategico nao encontrado para atualizacao.");

            await ValidarGrupamentoAsync(grupamentoEstrategico, isEdicao: true, cancellationToken);

            grupamentoEstrategico.AssociadosGrupamentos = existente.AssociadosGrupamentos;

            await _grupamentoEstrategicoRepository.UpdateAsync(grupamentoEstrategico, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return grupamentoEstrategico;
        }

        public async Task ExcluirGrupamentoAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do grupamento deve ser informado.", nameof(id));
            }

            var grupamento = await _grupamentoEstrategicoRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Grupamento estrategico nao encontrado para exclusao.");

            if (grupamento.AssociadosGrupamentos.Any())
            {
                throw new InvalidOperationException("Nao e permitido excluir grupamento com associados vinculados.");
            }

            await _grupamentoEstrategicoRepository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task ValidarClusterAsync(Cluster cluster, bool isEdicao, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(cluster.Nome))
            {
                throw new ArgumentException("O nome do cluster deve ser informado.", nameof(cluster));
            }

            var nomeExiste = await _clusterRepository.ExistsByNomeAsync(cluster.Nome, cancellationToken);
            if (nomeExiste)
            {
                var clusters = await _clusterRepository.GetAllAsync(cancellationToken);
                var duplicado = clusters.Any(x =>
                    string.Equals(x.Nome, cluster.Nome, StringComparison.OrdinalIgnoreCase) &&
                    (!isEdicao || x.Id != cluster.Id));

                if (duplicado)
                {
                    throw new InvalidOperationException("Ja existe um cluster cadastrado com esse nome.");
                }
            }
        }

        private async Task ValidarAtuacaoAsync(AtuacaoEspecifica atuacaoEspecifica, bool isEdicao, CancellationToken cancellationToken)
        {
            if (atuacaoEspecifica.ClusterId == Guid.Empty)
            {
                throw new ArgumentException("O cluster da atuacao deve ser informado.", nameof(atuacaoEspecifica));
            }

            if (string.IsNullOrWhiteSpace(atuacaoEspecifica.Nome))
            {
                throw new ArgumentException("O nome da atuacao especifica deve ser informado.", nameof(atuacaoEspecifica));
            }

            var nomeExiste = await _atuacaoEspecificaRepository.ExistsByNomeAsync(atuacaoEspecifica.ClusterId, atuacaoEspecifica.Nome, cancellationToken);
            if (nomeExiste)
            {
                var atuacoes = await _atuacaoEspecificaRepository.GetByClusterIdAsync(atuacaoEspecifica.ClusterId, cancellationToken);
                var duplicada = atuacoes.Any(x =>
                    string.Equals(x.Nome, atuacaoEspecifica.Nome, StringComparison.OrdinalIgnoreCase) &&
                    (!isEdicao || x.Id != atuacaoEspecifica.Id));

                if (duplicada)
                {
                    throw new InvalidOperationException("Ja existe uma atuacao especifica com esse nome dentro do cluster.");
                }
            }
        }

        private async Task ValidarGrupamentoAsync(GrupamentoEstrategico grupamentoEstrategico, bool isEdicao, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(grupamentoEstrategico.Nome))
            {
                throw new ArgumentException("O nome do grupamento deve ser informado.", nameof(grupamentoEstrategico));
            }

            if (string.IsNullOrWhiteSpace(grupamentoEstrategico.Sigla))
            {
                throw new ArgumentException("A sigla do grupamento deve ser informada.", nameof(grupamentoEstrategico));
            }

            if (grupamentoEstrategico.Sigla.Trim().Length > 4)
            {
                throw new InvalidOperationException("A sigla do grupamento deve possuir no maximo 4 caracteres.");
            }

            var nomeExiste = await _grupamentoEstrategicoRepository.ExistsByNomeAsync(grupamentoEstrategico.Nome, cancellationToken);
            if (nomeExiste)
            {
                var grupamentos = await _grupamentoEstrategicoRepository.GetAllAsync(cancellationToken);
                var duplicado = grupamentos.Any(x =>
                    string.Equals(x.Nome, grupamentoEstrategico.Nome, StringComparison.OrdinalIgnoreCase) &&
                    (!isEdicao || x.Id != grupamentoEstrategico.Id));

                if (duplicado)
                {
                    throw new InvalidOperationException("Ja existe um grupamento estrategico com esse nome.");
                }
            }

            var siglaExiste = await _grupamentoEstrategicoRepository.ExistsBySiglaAsync(grupamentoEstrategico.Sigla, cancellationToken);
            if (siglaExiste)
            {
                var grupamentos = await _grupamentoEstrategicoRepository.GetAllAsync(cancellationToken);
                var duplicado = grupamentos.Any(x =>
                    string.Equals(x.Sigla, grupamentoEstrategico.Sigla, StringComparison.OrdinalIgnoreCase) &&
                    (!isEdicao || x.Id != grupamentoEstrategico.Id));

                if (duplicado)
                {
                    throw new InvalidOperationException("Ja existe um grupamento estrategico com essa sigla.");
                }
            }
        }
    }
}
