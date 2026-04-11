using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.VisitantesSubstitutos.Entities
{
    public class VisitanteExterno
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid OcorrenciaReuniaoEquipeId { get; set; }
        public AdmCC.Domain.Equipes.Entities.OcorrenciaReuniaoEquipe OcorrenciaReuniaoEquipe { get; set; } = null!;

        public Guid AssociadoResponsavelId { get; set; }
        public AdmCC.Domain.CadastroBase.Entities.Associado AssociadoResponsavel { get; set; } = null!;

        public AdmCC.Domain.VisitantesSubstitutos.Enums.TipoPessoa TipoPessoa { get; set; }
            = AdmCC.Domain.VisitantesSubstitutos.Enums.TipoPessoa.Pessoa;

        public string NomeCompleto { get; set; } = string.Empty;
        public string TelefonePrincipal { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Cpf { get; set; }
        public string NomeEmpresa { get; set; } = string.Empty;

        public AdmCC.Domain.VisitantesSubstitutos.Enums.StatusValidacaoPresenca StatusValidacaoPresenca { get; set; }
            = AdmCC.Domain.VisitantesSubstitutos.Enums.StatusValidacaoPresenca.Pendente;

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public DateTime? DataValidacao { get; set; }
    }
}
