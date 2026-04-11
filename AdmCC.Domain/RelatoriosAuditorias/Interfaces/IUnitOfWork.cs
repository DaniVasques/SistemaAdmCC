using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.RelatoriosAuditorias.Interfaces
{
    public interface IUnitOfWork
    {
        System.Threading.Tasks.Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default);
    }
}
