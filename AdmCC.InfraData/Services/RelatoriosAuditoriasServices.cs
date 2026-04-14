using AdmCC.Domain.RelatoriosAuditorias.Entities;
using AdmCC.Domain.RelatoriosAuditorias.Interfaces;

namespace AdmCC.InfraData.Services
{
    // Escopo atual do modulo restrito a auditoria/logs.
    public class RelatoriosAuditoriasService
    {
        private readonly ILogAuditoriaRepository _logAuditoriaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RelatoriosAuditoriasService(
            ILogAuditoriaRepository logAuditoriaRepository,
            IUnitOfWork unitOfWork)
        {
            _logAuditoriaRepository = logAuditoriaRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<LogAuditoria?> ObterLogPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _logAuditoriaRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<IReadOnlyCollection<LogAuditoria>> ListarLogsPorEntidadeAsync(string entidade, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(entidade))
            {
                throw new ArgumentException("A entidade deve ser informada para consulta de logs.", nameof(entidade));
            }

            return _logAuditoriaRepository.GetByEntidadeAsync(entidade, cancellationToken);
        }

        public async Task<LogAuditoria> RegistrarLogAsync(LogAuditoria logAuditoria, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(logAuditoria);

            if (string.IsNullOrWhiteSpace(logAuditoria.Entidade))
            {
                throw new ArgumentException("A entidade do log deve ser informada.", nameof(logAuditoria));
            }

            if (string.IsNullOrWhiteSpace(logAuditoria.Acao))
            {
                throw new ArgumentException("A acao do log deve ser informada.", nameof(logAuditoria));
            }

            if (logAuditoria.EntidadeId == Guid.Empty || logAuditoria.UsuarioResponsavelId == Guid.Empty)
            {
                throw new ArgumentException("A entidade e o usuario responsavel do log devem ser informados.", nameof(logAuditoria));
            }

            if (logAuditoria.Id == Guid.Empty)
            {
                logAuditoria.Id = Guid.NewGuid();
            }

            if (logAuditoria.DataHora == default)
            {
                logAuditoria.DataHora = DateTime.UtcNow;
            }

            await _logAuditoriaRepository.AddAsync(logAuditoria, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return logAuditoria;
        }
    }
}
