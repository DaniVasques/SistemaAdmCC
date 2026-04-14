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

            var cicloAtualizado = AplicarDadosCiclo(cicloSemanal, existente);

            await ValidarCicloAsync(cicloAtualizado, isEdicao: true, cancellationToken);

            if (cicloAtualizado.Ativo)
            {
                var cicloAtivo = await _cicloSemanalRepository.GetAtivoAsync(cancellationToken);
                if (cicloAtivo is not null && cicloAtivo.Id != cicloAtualizado.Id)
                {
                    throw new InvalidOperationException("Ja existe outro ciclo semanal ativo no sistema.");
                }
            }

            await _cicloSemanalRepository.UpdateAsync(cicloAtualizado, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return cicloAtualizado;
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

        public Task<IReadOnlyCollection<ReuniaoCC>> ListarReunioesAsync(CancellationToken cancellationToken = default)
        {
            return _reuniaoCCRepository.GetAllAsync(cancellationToken);
        }

        public Task<IReadOnlyCollection<ValidacaoReuniaoCC>> ListarValidacoesPorReuniaoAsync(Guid reuniaoId, CancellationToken cancellationToken = default)
        {
            if (reuniaoId == Guid.Empty)
            {
                throw new ArgumentException("O identificador da reuniao deve ser informado.", nameof(reuniaoId));
            }

            return _reuniaoCCRepository.GetValidacoesByReuniaoIdAsync(reuniaoId, cancellationToken);
        }

        public async Task<ValidacaoReuniaoCC> CriarValidacaoAsync(Guid reuniaoId, ValidacaoReuniaoCC validacaoReuniaoCC, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(validacaoReuniaoCC);

            if (reuniaoId == Guid.Empty)
            {
                throw new ArgumentException("O identificador da reuniao deve ser informado.", nameof(reuniaoId));
            }

            var reuniao = await _reuniaoCCRepository.GetByIdAsync(reuniaoId, cancellationToken)
                ?? throw new KeyNotFoundException("Reuniao CC nao encontrada para registrar validacao.");

            validacaoReuniaoCC.ReuniaoCCId = reuniaoId;

            ValidarValidacao(validacaoReuniaoCC, reuniao);

            if (validacaoReuniaoCC.Id == Guid.Empty)
            {
                validacaoReuniaoCC.Id = Guid.NewGuid();
            }

            if (validacaoReuniaoCC.DataValidacao == default)
            {
                validacaoReuniaoCC.DataValidacao = DateTime.UtcNow;
            }

            await _reuniaoCCRepository.AddValidacaoAsync(validacaoReuniaoCC, cancellationToken);

            if (reuniao.StatusReuniaoCC == StatusReuniaoCC.Pendente)
            {
                reuniao.StatusReuniaoCC = StatusReuniaoCC.ParcialmenteValidada;
                await _reuniaoCCRepository.UpdateAsync(reuniao, cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return validacaoReuniaoCC;
        }

        public Task<IReadOnlyCollection<ProspectReuniaoCC>> ListarProspectsPorReuniaoAsync(Guid reuniaoId, CancellationToken cancellationToken = default)
        {
            if (reuniaoId == Guid.Empty)
            {
                throw new ArgumentException("O identificador da reuniao deve ser informado.", nameof(reuniaoId));
            }

            return _reuniaoCCRepository.GetProspectsByReuniaoIdAsync(reuniaoId, cancellationToken);
        }

        public async Task<ProspectReuniaoCC> CriarProspectAsync(Guid reuniaoId, ProspectReuniaoCC prospectReuniaoCC, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(prospectReuniaoCC);

            if (reuniaoId == Guid.Empty)
            {
                throw new ArgumentException("O identificador da reuniao deve ser informado.", nameof(reuniaoId));
            }

            var reuniao = await _reuniaoCCRepository.GetByIdAsync(reuniaoId, cancellationToken)
                ?? throw new KeyNotFoundException("Reuniao CC nao encontrada para registrar prospect.");

            var validacao = reuniao.Validacoes.FirstOrDefault(x => x.Id == prospectReuniaoCC.ValidacaoReuniaoCCId);
            if (validacao is null)
            {
                throw new InvalidOperationException("A validacao informada nao pertence a reuniao CC.");
            }

            if (validacao.NaoEncontrouProspect)
            {
                throw new InvalidOperationException("Nao e permitido adicionar prospect em validacao marcada sem prospect.");
            }

            ValidarProspect(prospectReuniaoCC);

            if (prospectReuniaoCC.Id == Guid.Empty)
            {
                prospectReuniaoCC.Id = Guid.NewGuid();
            }

            await _reuniaoCCRepository.AddProspectAsync(prospectReuniaoCC, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return prospectReuniaoCC;
        }

        public Task<IReadOnlyCollection<ReuniaoCC>> ListarReunioesPendentesPorAssociadoAsync(Guid associadoId, CancellationToken cancellationToken = default)
        {
            if (associadoId == Guid.Empty)
            {
                throw new ArgumentException("O identificador do associado deve ser informado.", nameof(associadoId));
            }

            return _reuniaoCCRepository.GetPendentesByAssociadoIdAsync(associadoId, cancellationToken);
        }

        public Task<RegistroEducacional?> ObterRegistroEducacionalPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do registro educacional deve ser informado.", nameof(id));
            }

            return _cicloSemanalRepository.GetRegistroEducacionalByIdAsync(id, cancellationToken);
        }

        public Task<IReadOnlyCollection<RegistroEducacional>> ListarRegistrosEducacionaisAsync(CancellationToken cancellationToken = default)
        {
            return _cicloSemanalRepository.GetRegistrosEducacionaisAsync(cancellationToken);
        }

        public async Task<RegistroEducacional> CriarRegistroEducacionalAsync(RegistroEducacional registroEducacional, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(registroEducacional);

            await AplicarEValidarRegistroEducacionalAsync(registroEducacional, cancellationToken);

            if (registroEducacional.Id == Guid.Empty)
            {
                registroEducacional.Id = Guid.NewGuid();
            }

            await _cicloSemanalRepository.AddRegistroEducacionalAsync(registroEducacional, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return registroEducacional;
        }

        public async Task<RegistroEducacional> AtualizarRegistroEducacionalAsync(RegistroEducacional registroEducacional, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(registroEducacional);

            if (registroEducacional.Id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do registro educacional deve ser informado para atualizacao.", nameof(registroEducacional));
            }

            var existente = await _cicloSemanalRepository.GetRegistroEducacionalByIdAsync(registroEducacional.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Registro educacional nao encontrado para atualizacao.");

            AplicarDadosRegistroEducacional(registroEducacional, existente);
            await AplicarEValidarRegistroEducacionalAsync(existente, cancellationToken);

            await _cicloSemanalRepository.UpdateRegistroEducacionalAsync(existente, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return existente;
        }

        public async Task ExcluirRegistroEducacionalAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do registro educacional deve ser informado.", nameof(id));
            }

            var existente = await _cicloSemanalRepository.GetRegistroEducacionalByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Registro educacional nao encontrado para exclusao.");

            await _cicloSemanalRepository.DeleteRegistroEducacionalAsync(existente.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public Task<IReadOnlyCollection<ParametroPontuacaoEducacional>> ListarParametrosPontuacaoEducacionalAsync(CancellationToken cancellationToken = default)
        {
            return _cicloSemanalRepository.GetParametrosPontuacaoEducacionalAsync(cancellationToken);
        }

        public Task<ParametroPontuacaoEducacional?> ObterParametroPontuacaoEducacionalPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do parametro deve ser informado.", nameof(id));
            }

            return _cicloSemanalRepository.GetParametroPontuacaoEducacionalByIdAsync(id, cancellationToken);
        }

        public async Task<ParametroPontuacaoEducacional> AtualizarParametroPontuacaoEducacionalAsync(ParametroPontuacaoEducacional parametroPontuacaoEducacional, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(parametroPontuacaoEducacional);

            if (parametroPontuacaoEducacional.Id == Guid.Empty)
            {
                throw new ArgumentException("O identificador do parametro deve ser informado para atualizacao.", nameof(parametroPontuacaoEducacional));
            }

            var existente = await _cicloSemanalRepository.GetParametroPontuacaoEducacionalByIdAsync(parametroPontuacaoEducacional.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Parametro de pontuacao educacional nao encontrado para atualizacao.");

            AplicarDadosParametroPontuacaoEducacional(parametroPontuacaoEducacional, existente);
            ValidarParametroPontuacaoEducacional(existente);

            await _cicloSemanalRepository.UpdateParametroPontuacaoEducacionalAsync(existente, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return existente;
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

            var reuniaoAtualizada = AplicarDadosReuniao(reuniaoCC, existente);

            await ValidarReuniaoAsync(reuniaoAtualizada, cancellationToken);

            await _reuniaoCCRepository.UpdateAsync(reuniaoAtualizada, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return reuniaoAtualizada;
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

        private static void ValidarValidacao(ValidacaoReuniaoCC validacaoReuniaoCC, ReuniaoCC reuniao)
        {
            if (validacaoReuniaoCC.AssociadoId == Guid.Empty)
            {
                throw new ArgumentException("O associado da validacao deve ser informado.", nameof(validacaoReuniaoCC));
            }

            var participanteValido =
                validacaoReuniaoCC.AssociadoId == reuniao.AssociadoOrigemId ||
                validacaoReuniaoCC.AssociadoId == reuniao.AssociadoDestinoId;

            if (!participanteValido)
            {
                throw new InvalidOperationException("A validacao da reuniao deve ser registrada por um dos associados participantes.");
            }

            if (reuniao.Validacoes.Any(x => x.AssociadoId == validacaoReuniaoCC.AssociadoId))
            {
                throw new InvalidOperationException("Ja existe validacao registrada para esse associado na reuniao.");
            }

            if (validacaoReuniaoCC.PontosGerados < 0)
            {
                throw new InvalidOperationException("A validacao nao pode gerar pontuacao negativa.");
            }
        }

        private static void ValidarProspect(ProspectReuniaoCC prospectReuniaoCC)
        {
            if (prospectReuniaoCC.ValidacaoReuniaoCCId == Guid.Empty)
            {
                throw new ArgumentException("A validacao vinculada ao prospect deve ser informada.", nameof(prospectReuniaoCC));
            }

            if (string.IsNullOrWhiteSpace(prospectReuniaoCC.NomeProspect))
            {
                throw new ArgumentException("O nome do prospect deve ser informado.", nameof(prospectReuniaoCC));
            }

            if (string.IsNullOrWhiteSpace(prospectReuniaoCC.NomeEmpresa))
            {
                throw new ArgumentException("O nome da empresa do prospect deve ser informado.", nameof(prospectReuniaoCC));
            }
        }

        private async Task AplicarEValidarRegistroEducacionalAsync(RegistroEducacional registroEducacional, CancellationToken cancellationToken)
        {
            if (registroEducacional.AssociadoId == Guid.Empty || registroEducacional.ParametroPontuacaoEducacionalId == Guid.Empty)
            {
                throw new ArgumentException("O associado e o parametro de pontuacao educacional devem ser informados.", nameof(registroEducacional));
            }

            if (string.IsNullOrWhiteSpace(registroEducacional.Titulo))
            {
                throw new ArgumentException("O titulo do registro educacional deve ser informado.", nameof(registroEducacional));
            }

            if (registroEducacional.DataOcorrencia == default)
            {
                throw new ArgumentException("A data de ocorrencia do registro educacional deve ser informada.", nameof(registroEducacional));
            }

            var parametro = await _cicloSemanalRepository.GetParametroPontuacaoEducacionalByIdAsync(registroEducacional.ParametroPontuacaoEducacionalId, cancellationToken)
                ?? throw new InvalidOperationException("O parametro de pontuacao educacional informado nao existe.");

            if (!parametro.Ativo)
            {
                throw new InvalidOperationException("Nao e permitido usar parametro de pontuacao educacional inativo.");
            }

            registroEducacional.TipoPontuacaoEducacional = parametro.TipoPontuacaoEducacional;
            registroEducacional.Pontos = parametro.Pontos;

            if (registroEducacional.Validado)
            {
                registroEducacional.DataValidacao ??= DateTime.UtcNow;
            }
            else
            {
                registroEducacional.DataValidacao = null;
            }
        }

        private static void ValidarParametroPontuacaoEducacional(ParametroPontuacaoEducacional parametroPontuacaoEducacional)
        {
            if (string.IsNullOrWhiteSpace(parametroPontuacaoEducacional.Nome))
            {
                throw new ArgumentException("O nome do parametro de pontuacao educacional deve ser informado.", nameof(parametroPontuacaoEducacional));
            }

            if (parametroPontuacaoEducacional.Pontos <= 0)
            {
                throw new InvalidOperationException("O parametro de pontuacao educacional deve possuir pontos positivos.");
            }
        }

        private static CicloSemanal AplicarDadosCiclo(CicloSemanal origem, CicloSemanal destino)
        {
            destino.DataInicio = origem.DataInicio;
            destino.DataEncerramento = origem.DataEncerramento;
            destino.MesReferencia = origem.MesReferencia;
            destino.AnoReferencia = origem.AnoReferencia;
            destino.Ativo = origem.Ativo;
            return destino;
        }

        private static ReuniaoCC AplicarDadosReuniao(ReuniaoCC origem, ReuniaoCC destino)
        {
            destino.CicloSemanalId = origem.CicloSemanalId;
            destino.AssociadoOrigemId = origem.AssociadoOrigemId;
            destino.AssociadoDestinoId = origem.AssociadoDestinoId;
            destino.TipoReuniaoCC = origem.TipoReuniaoCC;
            destino.LocalReuniao = origem.LocalReuniao;
            destino.LinkReuniaoOnline = origem.LinkReuniaoOnline;
            destino.DataAgendada = origem.DataAgendada;
            destino.StatusReuniaoCC = origem.StatusReuniaoCC;
            return destino;
        }

        private static void AplicarDadosRegistroEducacional(RegistroEducacional origem, RegistroEducacional destino)
        {
            destino.AssociadoId = origem.AssociadoId;
            destino.ParametroPontuacaoEducacionalId = origem.ParametroPontuacaoEducacionalId;
            destino.Titulo = origem.Titulo;
            destino.CodigoExterno = origem.CodigoExterno;
            destino.DataOcorrencia = origem.DataOcorrencia;
            destino.Validado = origem.Validado;
            destino.DataValidacao = origem.DataValidacao;
        }

        private static void AplicarDadosParametroPontuacaoEducacional(ParametroPontuacaoEducacional origem, ParametroPontuacaoEducacional destino)
        {
            destino.Nome = origem.Nome;
            destino.TipoPontuacaoEducacional = origem.TipoPontuacaoEducacional;
            destino.Pontos = origem.Pontos;
            destino.Ativo = origem.Ativo;
        }
    }
}
