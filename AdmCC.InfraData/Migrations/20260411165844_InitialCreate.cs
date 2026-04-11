using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdmCC.InfraData.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anuidades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataPagamentoPrimeiraAnuidade = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusAnuidade = table.Column<int>(type: "int", nullable: false),
                    VencimentoAtual = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUltimaRenovacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anuidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CargosLideranca",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClassificacaoCargo = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargosLideranca", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CiclosSemanais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataEncerramento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MesReferencia = table.Column<int>(type: "int", nullable: false),
                    AnoReferencia = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CiclosSemanais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clusters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clusters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    PermitirExibicaoNaRede = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrupamentosEstrategicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sigla = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupamentosEstrategicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogsAuditoria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Entidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Acao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioResponsavelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DadosAnterioresJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DadosNovosJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogsAuditoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificacoesInternas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioDestinoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Mensagem = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Lida = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataLeitura = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificacoesInternas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParametrosPontuacaoEducacional",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TipoPontuacaoEducacional = table.Column<int>(type: "int", nullable: false),
                    Pontos = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametrosPontuacaoEducacional", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParametrosPontuacaoEquipe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantidadeMinimaAssociados = table.Column<int>(type: "int", nullable: false),
                    QuantidadeMaximaAssociados = table.Column<int>(type: "int", nullable: true),
                    Pontos = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametrosPontuacaoEquipe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AtuacoesEspecificas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    ClusterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtuacoesEspecificas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AtuacoesEspecificas_Clusters_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Clusters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    RazaoSocial = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EnderecoComercialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empresas_Enderecos_EnderecoComercialId",
                        column: x => x.EnderecoComercialId,
                        principalTable: "Enderecos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Equipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeEquipe = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    DataInicioFormacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataPrevisaoLancamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataEfetivaLancamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusEquipe = table.Column<int>(type: "int", nullable: false),
                    DiaReuniaoEquipe = table.Column<int>(type: "int", nullable: false),
                    HorarioReuniao = table.Column<TimeOnly>(type: "time", nullable: false),
                    ModeloReuniaoDeEquipe = table.Column<int>(type: "int", nullable: false),
                    LocalReuniaoPresencialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkReuniaoOnline = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NumeroComponentesAtivos = table.Column<int>(type: "int", nullable: false),
                    PontuacaoMensalAtual = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipes_Enderecos_LocalReuniaoPresencialId",
                        column: x => x.LocalReuniaoPresencialId,
                        principalTable: "Enderecos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiretoriasEquipesVinculos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoVinculo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Indeterminado = table.Column<bool>(type: "bit", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiretoriasEquipesVinculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiretoriasEquipesVinculos_Equipes_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EquipesCargosAtivos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CargoLiderancaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipesCargosAtivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipesCargosAtivos_CargosLideranca_CargoLiderancaId",
                        column: x => x.CargoLiderancaId,
                        principalTable: "CargosLideranca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EquipesCargosAtivos_Equipes_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EquipesOrigem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoOrigem = table.Column<int>(type: "int", nullable: false),
                    EquipeOrigemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipesOrigem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipesOrigem_Equipes_EquipeOrigemId",
                        column: x => x.EquipeOrigemId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OcorrenciasReunioesEquipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataReuniao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumeroOcorrenciaNoMes = table.Column<int>(type: "int", nullable: false),
                    EhPresencial = table.Column<bool>(type: "bit", nullable: false),
                    Realizada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcorrenciasReunioesEquipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OcorrenciasReunioesEquipes_Equipes_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Associados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PermitirExibirAniversario = table.Column<bool>(type: "bit", nullable: false),
                    EmailPrincipal = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TelefoneWhatsappPrincipal = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DataIngresso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusAssociado = table.Column<int>(type: "int", nullable: false),
                    EnderecoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnuidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PadrinhoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipeOrigemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipeAtualId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClusterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AtuacaoEspecificaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Associados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Associados_Anuidades_AnuidadeId",
                        column: x => x.AnuidadeId,
                        principalTable: "Anuidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Associados_Associados_PadrinhoId",
                        column: x => x.PadrinhoId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Associados_AtuacoesEspecificas_AtuacaoEspecificaId",
                        column: x => x.AtuacaoEspecificaId,
                        principalTable: "AtuacoesEspecificas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Associados_Clusters_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Clusters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Associados_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Associados_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Enderecos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Associados_EquipesOrigem_EquipeOrigemId",
                        column: x => x.EquipeOrigemId,
                        principalTable: "EquipesOrigem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Associados_Equipes_EquipeAtualId",
                        column: x => x.EquipeAtualId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssociadosCargosLideranca",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CargoLiderancaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociadosCargosLideranca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssociadosCargosLideranca_Associados_AssociadoId",
                        column: x => x.AssociadoId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssociadosCargosLideranca_CargosLideranca_CargoLiderancaId",
                        column: x => x.CargoLiderancaId,
                        principalTable: "CargosLideranca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssociadosGrupamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrupamentoEstrategicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociadosGrupamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssociadosGrupamentos_Associados_AssociadoId",
                        column: x => x.AssociadoId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssociadosGrupamentos_GrupamentosEstrategicos_GrupamentoEstrategicoId",
                        column: x => x.GrupamentoEstrategicoId,
                        principalTable: "GrupamentosEstrategicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConexoesEstrategicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoOrigemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoDestinoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeContatoOuEmpresa = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TelefoneContato = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TipoDeConexao = table.Column<int>(type: "int", nullable: false),
                    StatusConexao = table.Column<int>(type: "int", nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Excluida = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConexoesEstrategicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConexoesEstrategicas_Associados_AssociadoDestinoId",
                        column: x => x.AssociadoDestinoId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConexoesEstrategicas_Associados_AssociadoOrigemId",
                        column: x => x.AssociadoOrigemId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DesignacoesLiderancaEquipe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CargoLiderancaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignacoesLiderancaEquipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignacoesLiderancaEquipe_Associados_AssociadoId",
                        column: x => x.AssociadoId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DesignacoesLiderancaEquipe_CargosLideranca_CargoLiderancaId",
                        column: x => x.CargoLiderancaId,
                        principalTable: "CargosLideranca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DesignacoesLiderancaEquipe_Equipes_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoricosAssociado",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusAnterior = table.Column<int>(type: "int", nullable: false),
                    StatusNovo = table.Column<int>(type: "int", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UsuarioResponsavelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosAssociado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricosAssociado_Associados_AssociadoId",
                        column: x => x.AssociadoId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParceriasAssociados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoOrigemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoDestinoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataParceria = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParceriasAssociados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParceriasAssociados_Associados_AssociadoDestinoId",
                        column: x => x.AssociadoDestinoId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParceriasAssociados_Associados_AssociadoOrigemId",
                        column: x => x.AssociadoOrigemId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerfisAssociados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FotoProfissionalUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DescricaoProfissional = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    NomeEmpresaExibicao = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LogomarcaEmpresaUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Site = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    EmailPublico = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PerfilPublicado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfisAssociados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerfisAssociados_Associados_AssociadoId",
                        column: x => x.AssociadoId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PresencasReunioesEquipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OcorrenciaReuniaoEquipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Presente = table.Column<bool>(type: "bit", nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresencasReunioesEquipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PresencasReunioesEquipes_Associados_AssociadoId",
                        column: x => x.AssociadoId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PresencasReunioesEquipes_OcorrenciasReunioesEquipes_OcorrenciaReuniaoEquipeId",
                        column: x => x.OcorrenciaReuniaoEquipeId,
                        principalTable: "OcorrenciasReunioesEquipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosEducacionais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParametroPontuacaoEducacionalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoPontuacaoEducacional = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CodigoExterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Pontos = table.Column<int>(type: "int", nullable: false),
                    DataOcorrencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Validado = table.Column<bool>(type: "bit", nullable: false),
                    DataValidacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosEducacionais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosEducacionais_Associados_AssociadoId",
                        column: x => x.AssociadoId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegistrosEducacionais_ParametrosPontuacaoEducacional_ParametroPontuacaoEducacionalId",
                        column: x => x.ParametroPontuacaoEducacionalId,
                        principalTable: "ParametrosPontuacaoEducacional",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReunioesCC",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CicloSemanalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoOrigemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoDestinoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoReuniaoCC = table.Column<int>(type: "int", nullable: false),
                    LocalReuniao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LinkReuniaoOnline = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DataAgendada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusReuniaoCC = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReunioesCC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReunioesCC_Associados_AssociadoDestinoId",
                        column: x => x.AssociadoDestinoId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReunioesCC_Associados_AssociadoOrigemId",
                        column: x => x.AssociadoOrigemId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReunioesCC_CiclosSemanais_CicloSemanalId",
                        column: x => x.CicloSemanalId,
                        principalTable: "CiclosSemanais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SegurosAssociados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstadoCivil = table.Column<int>(type: "int", nullable: false),
                    Profissao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SegurosAssociados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SegurosAssociados_Associados_AssociadoId",
                        column: x => x.AssociadoId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubstitutosAssociados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OcorrenciaReuniaoEquipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoTitularId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoSubstitutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusValidacaoPresenca = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataValidacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubstitutosAssociados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubstitutosAssociados_Associados_AssociadoSubstitutoId",
                        column: x => x.AssociadoSubstitutoId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubstitutosAssociados_Associados_AssociadoTitularId",
                        column: x => x.AssociadoTitularId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubstitutosAssociados_OcorrenciasReunioesEquipes_OcorrenciaReuniaoEquipeId",
                        column: x => x.OcorrenciaReuniaoEquipeId,
                        principalTable: "OcorrenciasReunioesEquipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubstitutosExternos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OcorrenciaReuniaoEquipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoTitularId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoPessoa = table.Column<int>(type: "int", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TelefonePrincipal = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Cpf = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    NomeEmpresa = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    StatusValidacaoPresenca = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataValidacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubstitutosExternos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubstitutosExternos_Associados_AssociadoTitularId",
                        column: x => x.AssociadoTitularId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubstitutosExternos_OcorrenciasReunioesEquipes_OcorrenciaReuniaoEquipeId",
                        column: x => x.OcorrenciaReuniaoEquipeId,
                        principalTable: "OcorrenciasReunioesEquipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VisitantesExternos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OcorrenciaReuniaoEquipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoResponsavelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoPessoa = table.Column<int>(type: "int", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TelefonePrincipal = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Cpf = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    NomeEmpresa = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    StatusValidacaoPresenca = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataValidacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitantesExternos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitantesExternos_Associados_AssociadoResponsavelId",
                        column: x => x.AssociadoResponsavelId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitantesExternos_OcorrenciasReunioesEquipes_OcorrenciaReuniaoEquipeId",
                        column: x => x.OcorrenciaReuniaoEquipeId,
                        principalTable: "OcorrenciasReunioesEquipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VisitasInternas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OcorrenciaReuniaoEquipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoVisitanteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusValidacaoPresenca = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataValidacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitasInternas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitasInternas_Associados_AssociadoVisitanteId",
                        column: x => x.AssociadoVisitanteId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitasInternas_OcorrenciasReunioesEquipes_OcorrenciaReuniaoEquipeId",
                        column: x => x.OcorrenciaReuniaoEquipeId,
                        principalTable: "OcorrenciasReunioesEquipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NegociosRecebidosValidacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConexaoEstrategicaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoReceptorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusConexao = table.Column<int>(type: "int", nullable: false),
                    MotivoNegocioNaoFechado = table.Column<int>(type: "int", nullable: true),
                    ValorNegocioFechado = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    DataValidacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrazoEstourado = table.Column<bool>(type: "bit", nullable: false),
                    DataPrazoEstourado = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NegociosRecebidosValidacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NegociosRecebidosValidacoes_Associados_AssociadoReceptorId",
                        column: x => x.AssociadoReceptorId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NegociosRecebidosValidacoes_ConexoesEstrategicas_ConexaoEstrategicaId",
                        column: x => x.ConexaoEstrategicaId,
                        principalTable: "ConexoesEstrategicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MidiasAssociados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerfilAssociadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeMidia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OrdemExibicao = table.Column<int>(type: "int", nullable: false),
                    Ativa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MidiasAssociados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MidiasAssociados_PerfisAssociados_PerfilAssociadoId",
                        column: x => x.PerfilAssociadoId,
                        principalTable: "PerfisAssociados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValidacoesReunioesCC",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReuniaoCCId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataValidacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NaoEncontrouProspect = table.Column<bool>(type: "bit", nullable: false),
                    PontosGerados = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidacoesReunioesCC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValidacoesReunioesCC_Associados_AssociadoId",
                        column: x => x.AssociadoId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ValidacoesReunioesCC_ReunioesCC_ReuniaoCCId",
                        column: x => x.ReuniaoCCId,
                        principalTable: "ReunioesCC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BeneficiariosSeguro",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeguroAssociadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    GrauParentesco = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Percentual = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeneficiariosSeguro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BeneficiariosSeguro_SegurosAssociados_SeguroAssociadoId",
                        column: x => x.SeguroAssociadoId,
                        principalTable: "SegurosAssociados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsentimentosLgpdSeguro",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeguroAssociadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Aceito = table.Column<bool>(type: "bit", nullable: false),
                    DataAceite = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TextoConsentimento = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsentimentosLgpdSeguro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsentimentosLgpdSeguro_SegurosAssociados_SeguroAssociadoId",
                        column: x => x.SeguroAssociadoId,
                        principalTable: "SegurosAssociados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContatosEmergencia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeguroAssociadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TelefonePrincipal = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TelefoneSecundario = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContatosEmergencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContatosEmergencia_SegurosAssociados_SeguroAssociadoId",
                        column: x => x.SeguroAssociadoId,
                        principalTable: "SegurosAssociados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitacoesAlteracaoBeneficiario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeguroAssociadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataSolicitacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusSolicitacaoBeneficiario = table.Column<int>(type: "int", nullable: false),
                    ObservacaoSolicitante = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ObservacaoAnalise = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DataConclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacoesAlteracaoBeneficiario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitacoesAlteracaoBeneficiario_SegurosAssociados_SeguroAssociadoId",
                        column: x => x.SeguroAssociadoId,
                        principalTable: "SegurosAssociados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProspectsReunioesCC",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValidacaoReuniaoCCId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeProspect = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NomeEmpresa = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CompartilhouApenasContato = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProspectsReunioesCC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProspectsReunioesCC_ValidacoesReunioesCC_ValidacaoReuniaoCCId",
                        column: x => x.ValidacaoReuniaoCCId,
                        principalTable: "ValidacoesReunioesCC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Associados_AnuidadeId",
                table: "Associados",
                column: "AnuidadeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Associados_AtuacaoEspecificaId",
                table: "Associados",
                column: "AtuacaoEspecificaId");

            migrationBuilder.CreateIndex(
                name: "IX_Associados_ClusterId",
                table: "Associados",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_Associados_Cpf",
                table: "Associados",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Associados_EmailPrincipal",
                table: "Associados",
                column: "EmailPrincipal",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Associados_EmpresaId",
                table: "Associados",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Associados_EnderecoId",
                table: "Associados",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Associados_EquipeAtualId",
                table: "Associados",
                column: "EquipeAtualId");

            migrationBuilder.CreateIndex(
                name: "IX_Associados_EquipeOrigemId",
                table: "Associados",
                column: "EquipeOrigemId");

            migrationBuilder.CreateIndex(
                name: "IX_Associados_PadrinhoId",
                table: "Associados",
                column: "PadrinhoId");

            migrationBuilder.CreateIndex(
                name: "IX_AssociadosCargosLideranca_AssociadoId_CargoLiderancaId_DataInicio",
                table: "AssociadosCargosLideranca",
                columns: new[] { "AssociadoId", "CargoLiderancaId", "DataInicio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssociadosCargosLideranca_CargoLiderancaId",
                table: "AssociadosCargosLideranca",
                column: "CargoLiderancaId");

            migrationBuilder.CreateIndex(
                name: "IX_AssociadosGrupamentos_AssociadoId_GrupamentoEstrategicoId",
                table: "AssociadosGrupamentos",
                columns: new[] { "AssociadoId", "GrupamentoEstrategicoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssociadosGrupamentos_GrupamentoEstrategicoId",
                table: "AssociadosGrupamentos",
                column: "GrupamentoEstrategicoId");

            migrationBuilder.CreateIndex(
                name: "IX_AtuacoesEspecificas_ClusterId_Nome",
                table: "AtuacoesEspecificas",
                columns: new[] { "ClusterId", "Nome" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BeneficiariosSeguro_SeguroAssociadoId",
                table: "BeneficiariosSeguro",
                column: "SeguroAssociadoId");

            migrationBuilder.CreateIndex(
                name: "IX_CargosLideranca_Nome",
                table: "CargosLideranca",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CiclosSemanais_DataInicio_DataEncerramento",
                table: "CiclosSemanais",
                columns: new[] { "DataInicio", "DataEncerramento" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clusters_Nome",
                table: "Clusters",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConexoesEstrategicas_AssociadoDestinoId",
                table: "ConexoesEstrategicas",
                column: "AssociadoDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConexoesEstrategicas_AssociadoOrigemId",
                table: "ConexoesEstrategicas",
                column: "AssociadoOrigemId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsentimentosLgpdSeguro_SeguroAssociadoId",
                table: "ConsentimentosLgpdSeguro",
                column: "SeguroAssociadoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContatosEmergencia_SeguroAssociadoId",
                table: "ContatosEmergencia",
                column: "SeguroAssociadoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DesignacoesLiderancaEquipe_AssociadoId",
                table: "DesignacoesLiderancaEquipe",
                column: "AssociadoId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignacoesLiderancaEquipe_CargoLiderancaId",
                table: "DesignacoesLiderancaEquipe",
                column: "CargoLiderancaId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignacoesLiderancaEquipe_EquipeId_CargoLiderancaId_AssociadoId_DataInicio",
                table: "DesignacoesLiderancaEquipe",
                columns: new[] { "EquipeId", "CargoLiderancaId", "AssociadoId", "DataInicio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiretoriasEquipesVinculos_EquipeId",
                table: "DiretoriasEquipesVinculos",
                column: "EquipeId");

            migrationBuilder.CreateIndex(
                name: "IX_DiretoriasEquipesVinculos_UsuarioId",
                table: "DiretoriasEquipesVinculos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_Cnpj",
                table: "Empresas",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_EnderecoComercialId",
                table: "Empresas",
                column: "EnderecoComercialId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipes_LocalReuniaoPresencialId",
                table: "Equipes",
                column: "LocalReuniaoPresencialId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipes_NomeEquipe",
                table: "Equipes",
                column: "NomeEquipe",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipesCargosAtivos_CargoLiderancaId",
                table: "EquipesCargosAtivos",
                column: "CargoLiderancaId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipesCargosAtivos_EquipeId_CargoLiderancaId",
                table: "EquipesCargosAtivos",
                columns: new[] { "EquipeId", "CargoLiderancaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipesOrigem_EquipeOrigemId",
                table: "EquipesOrigem",
                column: "EquipeOrigemId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupamentosEstrategicos_Sigla",
                table: "GrupamentosEstrategicos",
                column: "Sigla",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosAssociado_AssociadoId",
                table: "HistoricosAssociado",
                column: "AssociadoId");

            migrationBuilder.CreateIndex(
                name: "IX_LogsAuditoria_Entidade_EntidadeId",
                table: "LogsAuditoria",
                columns: new[] { "Entidade", "EntidadeId" });

            migrationBuilder.CreateIndex(
                name: "IX_LogsAuditoria_UsuarioResponsavelId",
                table: "LogsAuditoria",
                column: "UsuarioResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_MidiasAssociados_PerfilAssociadoId_OrdemExibicao",
                table: "MidiasAssociados",
                columns: new[] { "PerfilAssociadoId", "OrdemExibicao" });

            migrationBuilder.CreateIndex(
                name: "IX_NegociosRecebidosValidacoes_AssociadoReceptorId",
                table: "NegociosRecebidosValidacoes",
                column: "AssociadoReceptorId");

            migrationBuilder.CreateIndex(
                name: "IX_NegociosRecebidosValidacoes_ConexaoEstrategicaId",
                table: "NegociosRecebidosValidacoes",
                column: "ConexaoEstrategicaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificacoesInternas_Lida",
                table: "NotificacoesInternas",
                column: "Lida");

            migrationBuilder.CreateIndex(
                name: "IX_NotificacoesInternas_UsuarioDestinoId",
                table: "NotificacoesInternas",
                column: "UsuarioDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_OcorrenciasReunioesEquipes_EquipeId_DataReuniao",
                table: "OcorrenciasReunioesEquipes",
                columns: new[] { "EquipeId", "DataReuniao" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParametrosPontuacaoEquipe_QuantidadeMinimaAssociados_QuantidadeMaximaAssociados",
                table: "ParametrosPontuacaoEquipe",
                columns: new[] { "QuantidadeMinimaAssociados", "QuantidadeMaximaAssociados" },
                unique: true,
                filter: "[QuantidadeMaximaAssociados] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ParceriasAssociados_AssociadoDestinoId",
                table: "ParceriasAssociados",
                column: "AssociadoDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_ParceriasAssociados_AssociadoOrigemId_AssociadoDestinoId",
                table: "ParceriasAssociados",
                columns: new[] { "AssociadoOrigemId", "AssociadoDestinoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerfisAssociados_AssociadoId",
                table: "PerfisAssociados",
                column: "AssociadoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PresencasReunioesEquipes_AssociadoId",
                table: "PresencasReunioesEquipes",
                column: "AssociadoId");

            migrationBuilder.CreateIndex(
                name: "IX_PresencasReunioesEquipes_OcorrenciaReuniaoEquipeId_AssociadoId",
                table: "PresencasReunioesEquipes",
                columns: new[] { "OcorrenciaReuniaoEquipeId", "AssociadoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProspectsReunioesCC_ValidacaoReuniaoCCId",
                table: "ProspectsReunioesCC",
                column: "ValidacaoReuniaoCCId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosEducacionais_AssociadoId",
                table: "RegistrosEducacionais",
                column: "AssociadoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosEducacionais_ParametroPontuacaoEducacionalId",
                table: "RegistrosEducacionais",
                column: "ParametroPontuacaoEducacionalId");

            migrationBuilder.CreateIndex(
                name: "IX_ReunioesCC_AssociadoDestinoId",
                table: "ReunioesCC",
                column: "AssociadoDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_ReunioesCC_AssociadoOrigemId",
                table: "ReunioesCC",
                column: "AssociadoOrigemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReunioesCC_CicloSemanalId_AssociadoOrigemId_AssociadoDestinoId",
                table: "ReunioesCC",
                columns: new[] { "CicloSemanalId", "AssociadoOrigemId", "AssociadoDestinoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SegurosAssociados_AssociadoId",
                table: "SegurosAssociados",
                column: "AssociadoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacoesAlteracaoBeneficiario_SeguroAssociadoId",
                table: "SolicitacoesAlteracaoBeneficiario",
                column: "SeguroAssociadoId");

            migrationBuilder.CreateIndex(
                name: "IX_SubstitutosAssociados_AssociadoSubstitutoId",
                table: "SubstitutosAssociados",
                column: "AssociadoSubstitutoId");

            migrationBuilder.CreateIndex(
                name: "IX_SubstitutosAssociados_AssociadoTitularId",
                table: "SubstitutosAssociados",
                column: "AssociadoTitularId");

            migrationBuilder.CreateIndex(
                name: "IX_SubstitutosAssociados_OcorrenciaReuniaoEquipeId",
                table: "SubstitutosAssociados",
                column: "OcorrenciaReuniaoEquipeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubstitutosExternos_AssociadoTitularId",
                table: "SubstitutosExternos",
                column: "AssociadoTitularId");

            migrationBuilder.CreateIndex(
                name: "IX_SubstitutosExternos_Cpf",
                table: "SubstitutosExternos",
                column: "Cpf");

            migrationBuilder.CreateIndex(
                name: "IX_SubstitutosExternos_OcorrenciaReuniaoEquipeId",
                table: "SubstitutosExternos",
                column: "OcorrenciaReuniaoEquipeId");

            migrationBuilder.CreateIndex(
                name: "IX_ValidacoesReunioesCC_AssociadoId",
                table: "ValidacoesReunioesCC",
                column: "AssociadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ValidacoesReunioesCC_ReuniaoCCId_AssociadoId",
                table: "ValidacoesReunioesCC",
                columns: new[] { "ReuniaoCCId", "AssociadoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VisitantesExternos_AssociadoResponsavelId",
                table: "VisitantesExternos",
                column: "AssociadoResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitantesExternos_Cpf",
                table: "VisitantesExternos",
                column: "Cpf");

            migrationBuilder.CreateIndex(
                name: "IX_VisitantesExternos_OcorrenciaReuniaoEquipeId",
                table: "VisitantesExternos",
                column: "OcorrenciaReuniaoEquipeId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitasInternas_AssociadoVisitanteId",
                table: "VisitasInternas",
                column: "AssociadoVisitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitasInternas_OcorrenciaReuniaoEquipeId",
                table: "VisitasInternas",
                column: "OcorrenciaReuniaoEquipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssociadosCargosLideranca");

            migrationBuilder.DropTable(
                name: "AssociadosGrupamentos");

            migrationBuilder.DropTable(
                name: "BeneficiariosSeguro");

            migrationBuilder.DropTable(
                name: "ConsentimentosLgpdSeguro");

            migrationBuilder.DropTable(
                name: "ContatosEmergencia");

            migrationBuilder.DropTable(
                name: "DesignacoesLiderancaEquipe");

            migrationBuilder.DropTable(
                name: "DiretoriasEquipesVinculos");

            migrationBuilder.DropTable(
                name: "EquipesCargosAtivos");

            migrationBuilder.DropTable(
                name: "HistoricosAssociado");

            migrationBuilder.DropTable(
                name: "LogsAuditoria");

            migrationBuilder.DropTable(
                name: "MidiasAssociados");

            migrationBuilder.DropTable(
                name: "NegociosRecebidosValidacoes");

            migrationBuilder.DropTable(
                name: "NotificacoesInternas");

            migrationBuilder.DropTable(
                name: "ParametrosPontuacaoEquipe");

            migrationBuilder.DropTable(
                name: "ParceriasAssociados");

            migrationBuilder.DropTable(
                name: "PresencasReunioesEquipes");

            migrationBuilder.DropTable(
                name: "ProspectsReunioesCC");

            migrationBuilder.DropTable(
                name: "RegistrosEducacionais");

            migrationBuilder.DropTable(
                name: "SolicitacoesAlteracaoBeneficiario");

            migrationBuilder.DropTable(
                name: "SubstitutosAssociados");

            migrationBuilder.DropTable(
                name: "SubstitutosExternos");

            migrationBuilder.DropTable(
                name: "VisitantesExternos");

            migrationBuilder.DropTable(
                name: "VisitasInternas");

            migrationBuilder.DropTable(
                name: "GrupamentosEstrategicos");

            migrationBuilder.DropTable(
                name: "CargosLideranca");

            migrationBuilder.DropTable(
                name: "PerfisAssociados");

            migrationBuilder.DropTable(
                name: "ConexoesEstrategicas");

            migrationBuilder.DropTable(
                name: "ValidacoesReunioesCC");

            migrationBuilder.DropTable(
                name: "ParametrosPontuacaoEducacional");

            migrationBuilder.DropTable(
                name: "SegurosAssociados");

            migrationBuilder.DropTable(
                name: "OcorrenciasReunioesEquipes");

            migrationBuilder.DropTable(
                name: "ReunioesCC");

            migrationBuilder.DropTable(
                name: "Associados");

            migrationBuilder.DropTable(
                name: "CiclosSemanais");

            migrationBuilder.DropTable(
                name: "Anuidades");

            migrationBuilder.DropTable(
                name: "AtuacoesEspecificas");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "EquipesOrigem");

            migrationBuilder.DropTable(
                name: "Clusters");

            migrationBuilder.DropTable(
                name: "Equipes");

            migrationBuilder.DropTable(
                name: "Enderecos");
        }
    }
}
