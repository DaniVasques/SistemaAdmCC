using AdmCC.Domain.CargosLiderancas.Entities;
using AdmCC.Domain.CargosLiderancas.Interfaces;
using AdmCC.InfraData.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdmCC.InfraData.Repositories
{
    public class CargoLiderancaRepository : ICargoLiderancaRepository
    {
        private readonly AdmCCContext _context;

        public CargoLiderancaRepository(AdmCCContext context)
        {
            _context = context;
        }

        public Task<CargoLideranca?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return BuildCargoQuery(trackChanges: false)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<CargoLideranca>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await BuildCargoQuery(trackChanges: false)
                .OrderBy(x => x.Nome)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<CargoLideranca>> GetAtivosAsync(CancellationToken cancellationToken = default)
        {
            return await BuildCargoQuery(trackChanges: false)
                .Where(x => x.Ativo)
                .OrderBy(x => x.Nome)
                .ToListAsync(cancellationToken);
        }

        public Task AddAsync(CargoLideranca cargoLideranca, CancellationToken cancellationToken = default)
        {
            return _context.CargosLideranca.AddAsync(cargoLideranca, cancellationToken).AsTask();
        }

        public Task UpdateAsync(CargoLideranca cargoLideranca, CancellationToken cancellationToken = default)
        {
            _context.CargosLideranca.Update(cargoLideranca);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var cargo = await _context.CargosLideranca
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (cargo is null)
            {
                return;
            }

            _context.CargosLideranca.Remove(cargo);
        }

        private IQueryable<CargoLideranca> BuildCargoQuery(bool trackChanges)
        {
            var query = _context.CargosLideranca
                .Include(x => x.AssociadosCargos)
                .Include(x => x.EquipesCargosAtivos)
                .Include(x => x.Designacoes)
                .AsQueryable();

            return trackChanges ? query : query.AsNoTracking();
        }
    }
}
