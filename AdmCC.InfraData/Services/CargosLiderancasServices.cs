using AdmCC.Domain.CargosLiderancas.Entities;
using AdmCC.Domain.CargosLiderancas.Interfaces;
using AdmCC.Domain.RelatoriosAuditorias.Interfaces;

namespace AdmCC.InfraData.Services
{
    // Neste momento o escopo publico do modulo cobre apenas o catalogo de cargos.
    public class CargosLiderancasService
    {
        private readonly ICargoLiderancaRepository _cargoLiderancaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CargosLiderancasService(
            ICargoLiderancaRepository cargoLiderancaRepository,
            IUnitOfWork unitOfWork)
        {
            _cargoLiderancaRepository = cargoLiderancaRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<CargoLideranca?> ObterCargoPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _cargoLiderancaRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<IReadOnlyCollection<CargoLideranca>> ListarCargosAsync(CancellationToken cancellationToken = default)
        {
            return _cargoLiderancaRepository.GetAllAsync(cancellationToken);
        }

        public Task<IReadOnlyCollection<CargoLideranca>> ListarCargosAtivosAsync(CancellationToken cancellationToken = default)
        {
            return _cargoLiderancaRepository.GetAtivosAsync(cancellationToken);
        }

        public async Task<CargoLideranca> CriarCargoAsync(CargoLideranca cargoLideranca, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(cargoLideranca);

            await ValidarCargoAsync(cargoLideranca, isEdicao: false, cancellationToken);

            if (cargoLideranca.Id == Guid.Empty)
            {
                cargoLideranca.Id = Guid.NewGuid();
            }

            if (cargoLideranca.DataCadastro == default)
            {
                cargoLideranca.DataCadastro = DateTime.UtcNow;
            }

            await _cargoLiderancaRepository.AddAsync(cargoLideranca, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return cargoLideranca;
        }

        public async Task<CargoLideranca> AtualizarCargoAsync(CargoLideranca cargoLideranca, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(cargoLideranca);

            if (cargoLideranca.Id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do cargo deve ser informado para atualizacao.", nameof(cargoLideranca));
            }

            var existente = await _cargoLiderancaRepository.GetByIdAsync(cargoLideranca.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Cargo de lideranca nao encontrado para atualizacao.");

            await ValidarCargoAsync(cargoLideranca, isEdicao: true, cancellationToken);

            cargoLideranca.DataCadastro = existente.DataCadastro == default
                ? DateTime.UtcNow
                : existente.DataCadastro;

            await _cargoLiderancaRepository.UpdateAsync(cargoLideranca, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return cargoLideranca;
        }

        public async Task InativarCargoAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do cargo deve ser informado.", nameof(id));
            }

            var cargo = await _cargoLiderancaRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Cargo de lideranca nao encontrado para inativacao.");

            if (!cargo.Ativo)
            {
                return;
            }

            cargo.Ativo = false;
            await _cargoLiderancaRepository.UpdateAsync(cargo, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task ExcluirCargoAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do cargo deve ser informado.", nameof(id));
            }

            var cargo = await _cargoLiderancaRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Cargo de lideranca nao encontrado para exclusao.");

            if (cargo.AssociadosCargos.Any() || cargo.EquipesCargosAtivos.Any() || cargo.Designacoes.Any())
            {
                throw new InvalidOperationException("Nao e permitido excluir cargo que ja possui vinculacoes ativas ou historicas.");
            }

            await _cargoLiderancaRepository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task ValidarCargoAsync(CargoLideranca cargoLideranca, bool isEdicao, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(cargoLideranca.Nome))
            {
                throw new ArgumentException("O nome do cargo deve ser informado.", nameof(cargoLideranca));
            }

            var todos = await _cargoLiderancaRepository.GetAllAsync(cancellationToken);
            var duplicado = todos.FirstOrDefault(x =>
                string.Equals(x.Nome, cargoLideranca.Nome, StringComparison.OrdinalIgnoreCase) &&
                (!isEdicao || x.Id != cargoLideranca.Id));

            if (duplicado is not null)
            {
                throw new InvalidOperationException("Ja existe um cargo de lideranca cadastrado com esse nome.");
            }
        }
    }
}
