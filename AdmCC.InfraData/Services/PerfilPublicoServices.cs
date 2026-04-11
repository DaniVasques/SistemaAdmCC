using AdmCC.Domain.PerfilPublico.Entities;
using AdmCC.Domain.PerfilPublico.Interfaces;
using AdmCC.Domain.RelatoriosAuditorias.Interfaces;

namespace AdmCC.InfraData.Services
{
    public class PerfilPublicoService
    {
        private readonly IPerfilAssociadoRepository _perfilAssociadoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PerfilPublicoService(
            IPerfilAssociadoRepository perfilAssociadoRepository,
            IUnitOfWork unitOfWork)
        {
            _perfilAssociadoRepository = perfilAssociadoRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<PerfilAssociado?> ObterPerfilPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _perfilAssociadoRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<PerfilAssociado?> ObterPerfilPorAssociadoIdAsync(Guid associadoId, CancellationToken cancellationToken = default)
        {
            if (associadoId == Guid.Empty)
            {
                throw new ArgumentException("O identificador do associado deve ser informado.", nameof(associadoId));
            }

            return _perfilAssociadoRepository.GetByAssociadoIdAsync(associadoId, cancellationToken);
        }

        public async Task<PerfilAssociado> CriarPerfilAsync(PerfilAssociado perfilAssociado, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(perfilAssociado);

            await ValidarPerfilAsync(perfilAssociado, isEdicao: false, cancellationToken);

            if (perfilAssociado.Id == Guid.Empty)
            {
                perfilAssociado.Id = Guid.NewGuid();
            }

            await _perfilAssociadoRepository.AddAsync(perfilAssociado, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return perfilAssociado;
        }

        public async Task<PerfilAssociado> AtualizarPerfilAsync(PerfilAssociado perfilAssociado, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(perfilAssociado);

            if (perfilAssociado.Id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do perfil deve ser informado para atualizacao.", nameof(perfilAssociado));
            }

            var existente = await _perfilAssociadoRepository.GetByIdAsync(perfilAssociado.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Perfil publico nao encontrado para atualizacao.");

            await ValidarPerfilAsync(perfilAssociado, isEdicao: true, cancellationToken);

            perfilAssociado.Midias = perfilAssociado.Midias
                .OrderBy(x => x.OrdemExibicao)
                .ToList();

            if (!perfilAssociado.Midias.Any())
            {
                perfilAssociado.Midias = existente.Midias;
            }

            await _perfilAssociadoRepository.UpdateAsync(perfilAssociado, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return perfilAssociado;
        }

        public async Task PublicarPerfilAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do perfil deve ser informado.", nameof(id));
            }

            var perfil = await _perfilAssociadoRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Perfil publico nao encontrado para publicacao.");

            await ValidarPerfilParaPublicacaoAsync(perfil);

            perfil.PerfilPublicado = true;
            await _perfilAssociadoRepository.UpdateAsync(perfil, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DespublicarPerfilAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do perfil deve ser informado.", nameof(id));
            }

            var perfil = await _perfilAssociadoRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Perfil publico nao encontrado para despublicacao.");

            perfil.PerfilPublicado = false;
            await _perfilAssociadoRepository.UpdateAsync(perfil, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task ValidarPerfilAsync(PerfilAssociado perfilAssociado, bool isEdicao, CancellationToken cancellationToken)
        {
            if (perfilAssociado.AssociadoId == Guid.Empty)
            {
                throw new ArgumentException("O associado do perfil deve ser informado.", nameof(perfilAssociado));
            }

            if (string.IsNullOrWhiteSpace(perfilAssociado.DescricaoProfissional))
            {
                throw new ArgumentException("A descricao profissional do perfil deve ser informada.", nameof(perfilAssociado));
            }

            if (perfilAssociado.Midias.Any(x => string.IsNullOrWhiteSpace(x.NomeMidia) || string.IsNullOrWhiteSpace(x.Url)))
            {
                throw new InvalidOperationException("Todas as midias do perfil devem possuir nome e URL.");
            }

            var perfilExistente = await _perfilAssociadoRepository.GetByAssociadoIdAsync(perfilAssociado.AssociadoId, cancellationToken);
            if (perfilExistente is not null && (!isEdicao || perfilExistente.Id != perfilAssociado.Id))
            {
                throw new InvalidOperationException("Ja existe um perfil publico cadastrado para esse associado.");
            }
        }

        private static Task ValidarPerfilParaPublicacaoAsync(PerfilAssociado perfilAssociado)
        {
            if (string.IsNullOrWhiteSpace(perfilAssociado.FotoProfissionalUrl))
            {
                throw new InvalidOperationException("Nao e permitido publicar perfil sem foto profissional.");
            }

            if (string.IsNullOrWhiteSpace(perfilAssociado.DescricaoProfissional))
            {
                throw new InvalidOperationException("Nao e permitido publicar perfil sem descricao profissional.");
            }

            return Task.CompletedTask;
        }
    }
}
