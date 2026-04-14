using AdmCC.Domain.Equipes.Entities;
using AdmCC.Domain.Equipes.Enums;
using AdmCC.Domain.Equipes.Interfaces;
using AdmCC.Domain.RelatoriosAuditorias.Interfaces;

namespace AdmCC.InfraData.Services
{
    // O fluxo publico atual do modulo usa a pontuacao mensal no agregado Equipe.
    public class EquipesService
    {
        private readonly IEquipeRepository _equipeRepository;
        private readonly IOcorrenciaReuniaoEquipeRepository _ocorrenciaReuniaoEquipeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EquipesService(
            IEquipeRepository equipeRepository,
            IOcorrenciaReuniaoEquipeRepository ocorrenciaReuniaoEquipeRepository,
            IUnitOfWork unitOfWork)
        {
            _equipeRepository = equipeRepository;
            _ocorrenciaReuniaoEquipeRepository = ocorrenciaReuniaoEquipeRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Equipe?> ObterEquipePorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _equipeRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<IReadOnlyCollection<Equipe>> ListarEquipesAsync(CancellationToken cancellationToken = default)
        {
            return _equipeRepository.GetAllAsync(cancellationToken);
        }

        public Task<IReadOnlyCollection<Equipe>> ListarEquipesAtivasAsync(CancellationToken cancellationToken = default)
        {
            return _equipeRepository.GetAtivasAsync(cancellationToken);
        }

        public async Task<Equipe> CriarEquipeAsync(Equipe equipe, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(equipe);

            await ValidarEquipeAsync(equipe, isEdicao: false, cancellationToken);

            if (equipe.Id == Guid.Empty)
            {
                equipe.Id = Guid.NewGuid();
            }

            await _equipeRepository.AddAsync(equipe, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return equipe;
        }

        public async Task<Equipe> AtualizarEquipeAsync(Equipe equipe, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(equipe);

            if (equipe.Id == Guid.Empty)
            {
                throw new ArgumentException("O identificador da equipe deve ser informado para atualizacao.", nameof(equipe));
            }

            var existente = await _equipeRepository.GetByIdAsync(equipe.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Equipe nao encontrada para atualizacao.");

            var equipeAtualizada = AplicarDadosEquipe(equipe, existente);

            await ValidarEquipeAsync(equipeAtualizada, isEdicao: true, cancellationToken);

            await _equipeRepository.UpdateAsync(equipeAtualizada, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return equipeAtualizada;
        }

        public async Task ExcluirEquipeAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador da equipe deve ser informado.", nameof(id));
            }

            var equipe = await _equipeRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Equipe nao encontrada para exclusao.");

            if (equipe.OcorrenciasReuniao.Any())
            {
                throw new InvalidOperationException("Nao e permitido excluir equipe que ja possui ocorrencias de reuniao registradas.");
            }

            await _equipeRepository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public Task<OcorrenciaReuniaoEquipe?> ObterOcorrenciaPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _ocorrenciaReuniaoEquipeRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<IReadOnlyCollection<OcorrenciaReuniaoEquipe>> ListarOcorrenciasPorEquipeAsync(Guid equipeId, CancellationToken cancellationToken = default)
        {
            if (equipeId == Guid.Empty)
            {
                throw new ArgumentException("O identificador da equipe deve ser informado.", nameof(equipeId));
            }

            return _ocorrenciaReuniaoEquipeRepository.GetByEquipeIdAsync(equipeId, cancellationToken);
        }

        public async Task<OcorrenciaReuniaoEquipe> CriarOcorrenciaAsync(OcorrenciaReuniaoEquipe ocorrenciaReuniaoEquipe, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(ocorrenciaReuniaoEquipe);

            await ValidarOcorrenciaAsync(ocorrenciaReuniaoEquipe, isEdicao: false, cancellationToken);

            if (ocorrenciaReuniaoEquipe.Id == Guid.Empty)
            {
                ocorrenciaReuniaoEquipe.Id = Guid.NewGuid();
            }

            await _ocorrenciaReuniaoEquipeRepository.AddAsync(ocorrenciaReuniaoEquipe, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ocorrenciaReuniaoEquipe;
        }

        public async Task<OcorrenciaReuniaoEquipe> AtualizarOcorrenciaAsync(OcorrenciaReuniaoEquipe ocorrenciaReuniaoEquipe, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(ocorrenciaReuniaoEquipe);

            if (ocorrenciaReuniaoEquipe.Id == Guid.Empty)
            {
                throw new ArgumentException("O identificador da ocorrencia deve ser informado para atualizacao.", nameof(ocorrenciaReuniaoEquipe));
            }

            var existente = await _ocorrenciaReuniaoEquipeRepository.GetByIdAsync(ocorrenciaReuniaoEquipe.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Ocorrencia de reuniao nao encontrada para atualizacao.");

            var ocorrenciaAtualizada = AplicarDadosOcorrencia(ocorrenciaReuniaoEquipe, existente);

            await ValidarOcorrenciaAsync(ocorrenciaAtualizada, isEdicao: true, cancellationToken);

            await _ocorrenciaReuniaoEquipeRepository.UpdateAsync(ocorrenciaAtualizada, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ocorrenciaAtualizada;
        }

        public async Task<PresencaReuniaoEquipe> RegistrarPresencaAsync(
            Guid ocorrenciaId,
            PresencaReuniaoEquipe presencaReuniaoEquipe,
            CancellationToken cancellationToken = default)
        {
            if (ocorrenciaId == Guid.Empty)
            {
                throw new ArgumentException("O identificador da ocorrencia deve ser informado.", nameof(ocorrenciaId));
            }

            ArgumentNullException.ThrowIfNull(presencaReuniaoEquipe);

            var ocorrencia = await _ocorrenciaReuniaoEquipeRepository.GetByIdAsync(ocorrenciaId, cancellationToken)
                ?? throw new KeyNotFoundException("Ocorrencia de reuniao nao encontrada para registrar presenca.");

            ValidarPresenca(ocorrencia, presencaReuniaoEquipe);

            if (presencaReuniaoEquipe.Id == Guid.Empty)
            {
                presencaReuniaoEquipe.Id = Guid.NewGuid();
            }

            presencaReuniaoEquipe.OcorrenciaReuniaoEquipeId = ocorrencia.Id;
            presencaReuniaoEquipe.DataRegistro = presencaReuniaoEquipe.DataRegistro == default
                ? DateTime.UtcNow
                : presencaReuniaoEquipe.DataRegistro;

            ocorrencia.Presencas.Add(presencaReuniaoEquipe);

            await _ocorrenciaReuniaoEquipeRepository.UpdateAsync(ocorrencia, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return presencaReuniaoEquipe;
        }

        public async Task MarcarOcorrenciaComoRealizadaAsync(Guid ocorrenciaId, CancellationToken cancellationToken = default)
        {
            if (ocorrenciaId == Guid.Empty)
            {
                throw new ArgumentException("O identificador da ocorrencia deve ser informado.", nameof(ocorrenciaId));
            }

            var ocorrencia = await _ocorrenciaReuniaoEquipeRepository.GetByIdAsync(ocorrenciaId, cancellationToken)
                ?? throw new KeyNotFoundException("Ocorrencia de reuniao nao encontrada.");

            ocorrencia.Realizada = true;

            await _ocorrenciaReuniaoEquipeRepository.UpdateAsync(ocorrencia, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task ExcluirOcorrenciaAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador da ocorrencia deve ser informado.", nameof(id));
            }

            var ocorrencia = await _ocorrenciaReuniaoEquipeRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Ocorrencia de reuniao nao encontrada para exclusao.");

            if (ocorrencia.Presencas.Any())
            {
                throw new InvalidOperationException("Nao e permitido excluir ocorrencia que ja possui presencas registradas.");
            }

            await _ocorrenciaReuniaoEquipeRepository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task ValidarEquipeAsync(Equipe equipe, bool isEdicao, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(equipe.NomeEquipe))
            {
                throw new ArgumentException("O nome da equipe deve ser informado.", nameof(equipe));
            }

            if (equipe.LocalReuniaoPresencialId == Guid.Empty)
            {
                throw new ArgumentException("O local da reuniao da equipe deve ser informado.", nameof(equipe));
            }

            if (equipe.DataInicioFormacao == default || equipe.DataPrevisaoLancamento == default)
            {
                throw new ArgumentException("As datas de formacao e previsao de lancamento devem ser informadas.", nameof(equipe));
            }

            if (equipe.DataPrevisaoLancamento < equipe.DataInicioFormacao)
            {
                throw new InvalidOperationException("A previsao de lancamento nao pode ser anterior ao inicio da formacao.");
            }

            if (equipe.DataEfetivaLancamento.HasValue && equipe.DataEfetivaLancamento.Value < equipe.DataInicioFormacao)
            {
                throw new InvalidOperationException("O lancamento efetivo nao pode ser anterior ao inicio da formacao.");
            }

            if (equipe.ModeloReuniaoDeEquipe == ModeloReuniaoDeEquipe.HibridoPrimeiraETerceiraPresencialDemaisOnline &&
                string.IsNullOrWhiteSpace(equipe.LinkReuniaoOnline))
            {
                throw new InvalidOperationException("Equipes online devem informar o link da reuniao.");
            }

            if (equipe.NumeroComponentesAtivos < 0 || equipe.PontuacaoMensalAtual < 0)
            {
                throw new InvalidOperationException("Os totais da equipe nao podem ser negativos.");
            }

            var equipes = await _equipeRepository.GetAllAsync(cancellationToken);
            var duplicada = equipes.FirstOrDefault(x =>
                string.Equals(x.NomeEquipe, equipe.NomeEquipe, StringComparison.OrdinalIgnoreCase) &&
                (!isEdicao || x.Id != equipe.Id));

            if (duplicada is not null)
            {
                throw new InvalidOperationException("Ja existe uma equipe cadastrada com esse nome.");
            }
        }

        private async Task ValidarOcorrenciaAsync(OcorrenciaReuniaoEquipe ocorrenciaReuniaoEquipe, bool isEdicao, CancellationToken cancellationToken)
        {
            if (ocorrenciaReuniaoEquipe.EquipeId == Guid.Empty)
            {
                throw new ArgumentException("A equipe da ocorrencia deve ser informada.", nameof(ocorrenciaReuniaoEquipe));
            }

            if (ocorrenciaReuniaoEquipe.DataReuniao == default)
            {
                throw new ArgumentException("A data da reuniao deve ser informada.", nameof(ocorrenciaReuniaoEquipe));
            }

            if (ocorrenciaReuniaoEquipe.NumeroOcorrenciaNoMes <= 0)
            {
                throw new InvalidOperationException("O numero da ocorrencia no mes deve ser maior que zero.");
            }

            var ocorrenciaExistente = await _ocorrenciaReuniaoEquipeRepository.GetByEquipeEDataAsync(
                ocorrenciaReuniaoEquipe.EquipeId,
                ocorrenciaReuniaoEquipe.DataReuniao,
                cancellationToken);

            if (ocorrenciaExistente is not null && (!isEdicao || ocorrenciaExistente.Id != ocorrenciaReuniaoEquipe.Id))
            {
                throw new InvalidOperationException("Ja existe uma ocorrencia cadastrada para a equipe na data informada.");
            }
        }

        private static void ValidarPresenca(OcorrenciaReuniaoEquipe ocorrencia, PresencaReuniaoEquipe presencaReuniaoEquipe)
        {
            if (presencaReuniaoEquipe.AssociadoId == Guid.Empty)
            {
                throw new ArgumentException("O associado da presenca deve ser informado.", nameof(presencaReuniaoEquipe));
            }

            if (presencaReuniaoEquipe.DataRegistro != default && presencaReuniaoEquipe.DataRegistro > DateTime.UtcNow)
            {
                throw new InvalidOperationException("A data de registro da presenca nao pode estar no futuro.");
            }

            var presencaJaRegistrada = ocorrencia.Presencas.Any(x => x.AssociadoId == presencaReuniaoEquipe.AssociadoId);
            if (presencaJaRegistrada)
            {
                throw new InvalidOperationException("Ja existe presenca registrada para esse associado nesta ocorrencia.");
            }
        }

        private static Equipe AplicarDadosEquipe(Equipe origem, Equipe destino)
        {
            destino.NomeEquipe = origem.NomeEquipe;
            destino.DataInicioFormacao = origem.DataInicioFormacao;
            destino.DataPrevisaoLancamento = origem.DataPrevisaoLancamento;
            destino.DataEfetivaLancamento = origem.DataEfetivaLancamento;
            destino.StatusEquipe = origem.StatusEquipe;
            destino.DiaReuniaoEquipe = origem.DiaReuniaoEquipe;
            destino.HorarioReuniao = origem.HorarioReuniao;
            destino.ModeloReuniaoDeEquipe = origem.ModeloReuniaoDeEquipe;
            destino.LocalReuniaoPresencialId = origem.LocalReuniaoPresencialId;
            destino.LinkReuniaoOnline = origem.LinkReuniaoOnline;
            destino.NumeroComponentesAtivos = origem.NumeroComponentesAtivos;
            destino.PontuacaoMensalAtual = origem.PontuacaoMensalAtual;

            return destino;
        }

        private static OcorrenciaReuniaoEquipe AplicarDadosOcorrencia(OcorrenciaReuniaoEquipe origem, OcorrenciaReuniaoEquipe destino)
        {
            destino.EquipeId = origem.EquipeId;
            destino.DataReuniao = origem.DataReuniao;
            destino.NumeroOcorrenciaNoMes = origem.NumeroOcorrenciaNoMes;
            destino.EhPresencial = origem.EhPresencial;
            destino.Realizada = origem.Realizada;

            return destino;
        }
    }
}
