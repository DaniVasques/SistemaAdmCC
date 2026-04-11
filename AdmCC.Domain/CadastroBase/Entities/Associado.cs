using AdmCC.Domain.CadastroBase.Enums;
using AdmCC.Domain.Estrategia.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.CadastroBase.Entities
{
    public class Associado
    {
        public Guid Id { get; set; }= Guid.NewGuid();

        public string NomeCompleto { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public bool PermitirExibirAniversario { get; set; }

        public string EmailPrincipal { get; set; } = string.Empty;
        public string TelefoneWhatsappPrincipal { get; set; } = string.Empty;

        public DateTime DataIngresso { get; set; }
        public DateTime DataCadastro { get; set; }

        public StatusAssociado StatusAssociado { get; set; }

        public Guid EnderecoId { get; set; }
        public Endereco Endereco { get; set; } = null!;

        public Guid EmpresaId { get; set; }
        public Empresa Empresa { get; set; } = null!;

        public Guid AnuidadeId { get; set; }
        public Anuidade Anuidade { get; set; } = null!;

        public Guid PadrinhoId { get; set; }
        public Associado? Padrinho { get; set; }
        public ICollection<Associado> Indicados { get; set; } = new List<Associado>();

        public Guid EquipeOrigemId { get; set; }
        public EquipeOrigem EquipeOrigem { get; set; } = null!;

        public Guid EquipeAtualId { get; set; }

        public Guid ClusterId { get; set; }
        public Cluster Cluster { get; set; } = null!;

        public Guid AtuacaoEspecificaId { get; set; }
        public AtuacaoEspecifica AtuacaoEspecifica { get; set; } = null!;

        public ICollection<HistoricoAssociado> HistoricoStatus { get; set; }
            = new List<HistoricoAssociado>();

        public ICollection<AssociadoGrupamento> AssociadosGrupamentos { get; set; }
            = new List<AssociadoGrupamento>();
    }
}

