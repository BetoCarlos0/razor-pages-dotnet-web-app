using Agenda.Areas.Contatos.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Agenda.Areas.Contatos.Services
{
    public interface IContatoService
    {
        Task<bool> Adicionar(Contato contato, CancellationToken cancellationToken);
        Task<bool> Alterar(Contato contato, CancellationToken cancellationToken);
        Task<IEnumerable<Contato>> ObterTodos(CancellationToken cancellationToken);
        Task<bool> ApagarContato(Guid id, CancellationToken cancellationToken);
        Task<Contato> ObterPorId(Guid id, CancellationToken cancellationToken);
    }
}
