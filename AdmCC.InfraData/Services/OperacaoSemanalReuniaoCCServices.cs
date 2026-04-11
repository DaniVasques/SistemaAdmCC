using AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities;
using AdmCC.Domain.OperacaoSemanalReuniaoCC.Enums;
using AdmCC.Domain.OperacaoSemanalReuniaoCC.Interfaces;
using AdmCC.Domain.RelatoriosAuditorias.Interfaces;

namespace AdmCC.InfraData.Services
{
    public class OperacaoSemanalReuniaoCCService
    {
        private readonly ICicloSemanalRepository _cicloSemanalRepository;
        private readonly IReuniaoCCRepository _reuniaoCCRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OperacaoSemanalReuniaoCCService(
            ICicloSemanalRepository cicloSemanalRepository,
            IReuniaoCCRepository reuniaoCCRepository,
            IUnitOfWork unitOfWork)
        {
            _cicloSemanalRepository = cicloSemanalRepository;
            _reuniaoCCRepository = reuniaoCCRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<CicloSemanal?> ObterCicloPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _cicloSemanalRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<CicloSemanal?> ObterCicloAtivoAsync(CancellationToken cancellationToken = default)
        {
            return _cicloSemanalRepository.GetAtivoAsync(cancellationToken);
        }

        public Task<IReadOnlyCollection<CicloSemanal>> ListarCiclosAsync(CancellationToken cancellationToken = default)
        {
            return _cicloSemanalRepository.GetAllAsync(cancellationToken);
        }

        public async Task<CicloSemanal> CriarCicloAsync(CicloSemanal cicloSemanal, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(cicloSemanal);

            await ValidarCicloAsync(cicloSemanal, isEdicao: false, cancellationToken);

            if (cicloSemanal.Id == Guid.Empty)
            {
                cicloSemanal.Id = Guid.NewGuid();
            }

            if (cicloSemanal.DataCadastro == default)
            {
                cicloSemanal.DataCadastro = DateTime.UtcNow;
            }

            if (cicloSemanal.Ativo)
            {
                var cicloAtivo = await _cicloSemanalRepository.GetAtivoAsync(cancellationToken);
                if (cicloAtivo is not null && cicloAtivo.Id != cicloSemanal.Id)
                {
                    throw new InvalidOperationException("Ja existe um ciclo semanal ativo. Inative o atual antes de criar outro ativo.");
                }
            }

            await _cicloSemanalRepository.AddAsync(cicloSemanal, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return cicloSemanal;
        }

        public async Task<CicloSemanal> AtualizarCicloAsync(CicloSemanal cicloSemanal, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(cicloSemanal);

            if (cicloSemanal.Id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do ciclo deve ser informado para atualizacao.", nameof(cicloSemanal));
            }

            var existente = await _cicloSemanalRepository.GetByIdAsync(cicloSemanal.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Ciclo semanal nao encontrado para atualizacao.");

            await ValidarCicloAsync(cicloSemanal, isEdicao: true, cancellationToken);

            if (cicloSemanal.Ativo)
            {
                var cicloAtivo = await _cicloSemanalRepository.GetAtivoAsync(cancellationToken);
                if (cicloAtivo is not null && cicloAtivo.Id != cicloSemanal.Id)
                {
                    throw new InvalidOperationException("Ja existe outro ciclo semanal ativo no sistema.");
                }
            }

            cicloSemanal.DataCadastro = existente.DataCadastro;
            cicloSemanal.ReunioesCC = existente.ReunioesCC;

            await _cicloSemanalRepository.UpdateAsync(cicloSemanal, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return cicloSemanal;
        }

        public async Task AtivarCicloAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do ciclo deve ser informado.", nameof(id));
            }

            var ciclo = await _cicloSemanalRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Ciclo semanal nao encontrado para ativacao.");

            var cicloAtivo = await _cicloSemanalRepository.GetAtivoAsync(cancellationToken);
            if (cicloAtivo is not null && cicloAtivo.Id != id)
            {
                cicloAtivo.Ativo = false;
                await _cicloSemanalRepository.UpdateAsync(cicloAtivo, cancellationToken);
            }

            ciclo.Ativo = true;
            await _cicloSemanalRepository.UpdateAsync(ciclo, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public Task<ReuniaoCC?> ObterReuniaoPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _reuniaoCCRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<IReadOnlyCollection<ReuniaoCC>> ListarReunioesPorCicloAsync(Guid cicloSemanalId, CancellationToken cancellationToken = default)
        {
            if (cicloSemanalId == Guid.Empty)
            {
                throw new ArgumentException("O identificador do ciclo deve ser informado.", nameof(cicloSemanalId));
            }

            return _reuniaoCCRepository.GetByCicloSemanalIdAsync(cicloSemanalId, cancellationToken);
        }

        public Task<IReadOnlyCollection<ReuniaoCC>> ListarReunioesPendentesPorAssociadoAsync(Guid associadoId, CancellationToken cancellationToken = default)
        {
            if (associadoId == Guid.Empty)
            {
                throw new ArgumentException("O identificador do associado deve ser informado.", nameof(associadoId));
            }

            return _reuniaoCCRepository.GetPendentesByAssociadoIdAsync(associadoId, cancellationToken);
        }

        public async Task<ReuniaoCC> CriarReuniaoAsync(ReuniaoCC reuniaoCC, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(reuniaoCC);

            await ValidarReuniaoAsync(reuniaoCC, cancellationToken);

            if (reuniaoCC.Id == Guid.Empty)
            {
                reuniaoCC.Id = Guid.NewGuid();
            }

            if (reuniaoCC.DataCadastro == default)
            {
                reuniaoCC.DataCadastro = DateTime.UtcNow;
            }

            await _reuniaoCCRepository.AddAsync(reuniaoCC, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return reuniaoCC;
        }

        public async Task<ReuniaoCC> AtualizarReuniaoAsync(ReuniaoCC reuniaoCC, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(reuniaoCC);

            if (reuniaoCC.Id == Guid.Empty)
            {
                throw new ArgumentException("O identificador da reuniao deve ser informado para atualizacao.", nameof(reuniaoCC));
            }

            var existente = await _reuniaoCCRepository.GetByIdAsync(reuniaoCC.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Reuniao CC nao encontrada para atualizacao.");

            await ValidarReuniaoAsync(reuniaoCC, cancellationToken);

            reuniaoCC.DataCadastro = existente.DataCadastro;
            reuniaoCC.Validacoes = existente.Validacoes;

            await _reuniaoCCRepository.UpdateAsync(reuniaoCC, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return reuniaoCC;
        }

        public async Task AtualizarStatusReuniaoAsync(Guid id, StatusReuniaoCC statusReuniaoCC, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador da reuniao deve ser informado.", nameof(id));
            }

            var reuniao = await _reuniaoCCRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Reuniao CC nao encontrada para atualizacao de status.");

            if (statusReuniaoCC == StatusReuniaoCC.Validada && !reuniao.Validacoes.Any())
            {
                throw new InvalidOperationException("Nao e permitido marcar a reuniao como validada sem validacoes registradas.");
            }

            reuniao.StatusReuniaoCC = statusReuniaoCC;
            await _reuniaoCCRepository.UpdateAsync(reuniao, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task ExcluirReuniaoAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador da reuniao deve ser informado.", nameof(id));
            }

            var reuniao = await _reuniaoCCRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Reuniao CC nao encontrada para exclusao.");

            if (reuniao.Validacoes.Any())
            {
                throw new InvalidOperationException("Nao e permitido excluir reuniao que ja possui validacoes registradas.");
            }

            await _reuniaoCCRepository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task ValidarCicloAsync(CicloSemanal cicloSemanal, bool isEdicao, CancellationToken cancellationToken)
        {
            if (cicloSemanal.DataInicio == default || cicloSemanal.DataEncerramento == default)
            {
                throw new ArgumentException("As datas de inicio e encerramento do ciclo devem ser informadas.", nameof(cicloSemanal));
            }

            if (cicloSemanal.DataEncerramento < cicloSemanal.DataInicio)
            {
                throw new InvalidOperationException("A data de encerramento do ciclo nao pode ser anterior a data de inicio.");
            }

            var cicloMesmoPeriodo = await _cicloSemanalRepository.GetByPeriodoAsync(cicloSemanal.DataInicio, cicloSemanal.DataEncerramento, cancellationToken);
            if (cicloMesmoPeriodo is not null && (!isEdicao || cicloMesmoPeriodo.Id != cicloSemanal.Id))
            {
                throw new InvalidOperationException("Ja existe um ciclo cadastrado para o mesmo periodo.");
            }

            if (cicloSemanal.MesReferencia < 1 || cicloSemanal.MesReferencia > 12)
            {
                throw new InvalidOperationException("O mes de referencia do ciclo deve estar entre 1 e 12.");
            }

            if (cicloSemanal.AnoReferencia < 2000)
            {
                throw new InvalidOperationException("O ano de referencia do ciclo e invalido.");
            }
        }

        private async Task ValidarReuniaoAsync(ReuniaoCC reuniaoCC, CancellationToken cancellationToken)
        {
            if (reuniaoCC.CicloSemanalId == Guid.Empty ||
                reuniaoCC.AssociadoOrigemId == Guid.Empty ||
                reuniaoCC.AssociadoDestinoId == Guid.Empty)
            {
                throw new ArgumentException("O ciclo e os associados da reuniao devem ser informados.", nameof(reuniaoCC));
            }

            if (reuniaoCC.AssociadoOrigemId == reuniaoCC.AssociadoDestinoId)
            {
                throw new InvalidOperationException("O associado de origem nao pode ser o mesmo associado de destino.");
            }

            if (reuniaoCC.DataAgendada == default)
            {
                throw new ArgumentException("A data agendada da reuniao deve ser informada.", nameof(reuniaoCC));
            }

            var ciclo = await _cicloSemanalRepository.GetByIdAsync(reuniaoCC.CicloSemanalId, cancellationToken)
                ?? throw new InvalidOperationException("O ciclo semanal informado para a reuniao nao existe.");

            if (reuniaoCC.DataAgendada < ciclo.DataInicio || reuniaoCC.DataAgendada > ciclo.DataEncerramento)
            {
                throw new InvalidOperationException("A data agendada da reuniao deve estar dentro do periodo do ciclo semanal.");
            }

            if (reuniaoCC.TipoReuniaoCC == TipoReuniaoCC.Online && string.IsNullOrWhiteSpace(reuniaoCC.LinkReuniaoOnline))
            {
                throw new InvalidOperationException("Reunioes online devem informar o link da reuniao.");
            }

            if (reuniaoCC.TipoReuniaoCC == TipoReuniaoCC.Presencial && string.IsNullOrWhiteSpace(reuniaoCC.LocalReuniao))
            {
                throw new InvalidOperationException("Reunioes presenciais devem informar o local da reuniao.");
            }
        }
    }
}
