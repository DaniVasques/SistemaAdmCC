using AdmCC.Domain.CadastroBase.Entities;
using AdmCC.Domain.CadastroBase.Interfaces;
using AdmCC.InfraData.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdmCC.InfraData.Repositories
{
    public class AnuidadeRepository : IAnuidadeRepository
    {
        private readonly AdmCCContext _context;

        public AnuidadeRepository(AdmCCContext context)
        {
            _context = context;
        }

        public Task<Anuidade?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _context.Anuidades
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<Anuidade>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Anuidades
                .AsNoTracking()
                .OrderBy(x => x.VencimentoAtual)
                .ToListAsync(cancellationToken);
        }

        public Task AddAsync(Anuidade anuidade, CancellationToken cancellationToken = default)
        {
            return _context.Anuidades.AddAsync(anuidade, cancellationToken).AsTask();
        }

        public Task UpdateAsync(Anuidade anuidade, CancellationToken cancellationToken = default)
        {
            _context.Anuidades.Update(anuidade);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var anuidade = await _context.Anuidades
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (anuidade is null)
            {
                return;
            }

            _context.Anuidades.Remove(anuidade);
        }
    }

    public class AssociadoRepository : IAssociadoRepository
    {
        private readonly AdmCCContext _context;

        public AssociadoRepository(AdmCCContext context)
        {
            _context = context;
        }

        public Task<Associado?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return BuildAssociadoQuery(trackChanges: false)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task<Associado?> GetByCpfAsync(string cpf, CancellationToken cancellationToken = default)
        {
            return BuildAssociadoQuery(trackChanges: false)
                .FirstOrDefaultAsync(x => x.Cpf == cpf, cancellationToken);
        }

        public Task<Associado?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return BuildAssociadoQuery(trackChanges: false)
                .FirstOrDefaultAsync(x => x.EmailPrincipal == email, cancellationToken);
        }

        public async Task<IReadOnlyCollection<Associado>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await BuildAssociadoQuery(trackChanges: false)
                .OrderBy(x => x.NomeCompleto)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<Associado>> GetByEquipeAtualIdAsync(Guid equipeId, CancellationToken cancellationToken = default)
        {
            return await BuildAssociadoQuery(trackChanges: false)
                .Where(x => x.EquipeAtualId == equipeId)
                .OrderBy(x => x.NomeCompleto)
                .ToListAsync(cancellationToken);
        }

        public Task<bool> ExistsByCpfAsync(string cpf, CancellationToken cancellationToken = default)
        {
            return _context.Associados.AnyAsync(x => x.Cpf == cpf, cancellationToken);
        }

        public Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return _context.Associados.AnyAsync(x => x.EmailPrincipal == email, cancellationToken);
        }

        public Task AddAsync(Associado associado, CancellationToken cancellationToken = default)
        {
            return _context.Associados.AddAsync(associado, cancellationToken).AsTask();
        }

        public Task UpdateAsync(Associado associado, CancellationToken cancellationToken = default)
        {
            _context.Associados.Update(associado);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var associado = await _context.Associados
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (associado is null)
            {
                return;
            }

            _context.Associados.Remove(associado);
        }

        private IQueryable<Associado> BuildAssociadoQuery(bool trackChanges)
        {
            var query = _context.Associados
                .Include(x => x.Endereco)
                .Include(x => x.Empresa)
                    .ThenInclude(x => x.EnderecoComercial)
                .Include(x => x.Anuidade)
                .Include(x => x.Padrinho)
                .Include(x => x.EquipeOrigem)
                .Include(x => x.Cluster)
                .Include(x => x.AtuacaoEspecifica)
                .Include(x => x.HistoricoStatus)
                .Include(x => x.AssociadosGrupamentos)
                    .ThenInclude(x => x.GrupamentoEstrategico)
                .AsQueryable();

            return trackChanges ? query : query.AsNoTracking();
        }
    }
}
