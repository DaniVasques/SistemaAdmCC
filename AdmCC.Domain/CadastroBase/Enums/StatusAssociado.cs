using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.CadastroBase.Enums
{
    public enum StatusAssociado
    {
        PreAtivo = 1,
        Ativo = 2,
        InativoPausaProgramada = 3,
        InativoDesistencia = 4,
        InativoFalecimento = 5,
        InativoDesligado = 6,
        InativoOutro = 7
    }
}
