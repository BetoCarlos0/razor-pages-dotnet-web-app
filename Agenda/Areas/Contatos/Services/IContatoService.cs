using Agenda.Areas.Contatos.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Agenda.Areas.Contatos.Services
{
    public interface IContatoService
    {
        Task<bool> Adicionar(Contato contato, CancellationToken cancellationToken);
    }
}
