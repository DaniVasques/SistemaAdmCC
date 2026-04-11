using AdmCC.Domain.CadastroBase.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.CadastroBase.Entities
{
    public class Anuidade
    {
        public Guid Id { get; set; }

        public DateTime? DataPagamentoPrimeiraAnuidade { get; set; }
        public StatusAnuidade StatusAnuidade { get; set; }= StatusAnuidade.Aguardando;

        public DateTime VencimentoAtual { get; set; }
        public DateTime? DataUltimaRenovacao { get; set; }
    }
}
