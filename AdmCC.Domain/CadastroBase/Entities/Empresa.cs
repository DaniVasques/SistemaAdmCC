using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.CadastroBase.Entities
{
    public class Empresa
    {
        public Guid Id { get; set; }

        public string Cnpj { get; set; } = string.Empty;
        public string RazaoSocial { get; set; } = string.Empty;

        public Guid EnderecoComercialId { get; set; }
        public Endereco EnderecoComercial { get; set; } = null!;
    }
}
