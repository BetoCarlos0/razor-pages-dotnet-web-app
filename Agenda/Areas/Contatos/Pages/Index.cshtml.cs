using Agenda.Areas.Contatos.Models;
using Agenda.Areas.Contatos.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Agenda.Areas.Contatos.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IContatoService _contatoService;

        public IEnumerable<Contato> Contatos { get; set; }
        public IndexModel(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }
        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            Contatos = await _contatoService.ObterTodos(cancellationToken);
        }
    }
}
