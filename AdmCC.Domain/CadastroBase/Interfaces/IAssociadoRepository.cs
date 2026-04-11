using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.CadastroBase.Interfaces
{
    public interface IAssociadoRepository
    {
        System.Threading.Tasks.Task<AdmCC.Domain.CadastroBase.Entities.Associado?> GetByIdAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<AdmCC.Domain.CadastroBase.Entities.Associado?> GetByCpfAsync(string cpf, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<AdmCC.Domain.CadastroBase.Entities.Associado?> GetByEmailAsync(string email, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.CadastroBase.Entities.Associado>> GetAllAsync(System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyCollection<AdmCC.Domain.CadastroBase.Entities.Associado>> GetByEquipeAtualIdAsync(Guid equipeId, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<bool> ExistsByCpfAsync(string cpf, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task<bool> ExistsByEmailAsync(string email, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task AddAsync(AdmCC.Domain.CadastroBase.Entities.Associado associado, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task UpdateAsync(AdmCC.Domain.CadastroBase.Entities.Associado associado, System.Threading.CancellationToken cancellationToken = default);
        System.Threading.Tasks.Task DeleteAsync(Guid id, System.Threading.CancellationToken cancellationToken = default);
    }
}
