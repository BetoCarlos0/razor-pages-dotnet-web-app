using Agenda.Areas.Contatos.Models;
using Agenda.Areas.Contatos.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Agenda.Areas.Contatos.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IContatoService _contatoService;

        [BindProperty]
        public Contato Contato { get; set; } = new Contato();

        public CreateModel(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }
        public async Task OnGetAsync([FromRoute] Guid? id, CancellationToken cancellationToken)
        {
            if (id.HasValue)
            {
                Contato = await _contatoService.ObterPorId(id.Value, cancellationToken).ConfigureAwait(false);
            }

        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
           
            var result = await _contatoService.Adicionar(Contato, cancellationToken).ConfigureAwait(false);
            if(!result)
            {
                return RedirectToPage("/Error");
            }

            return RedirectToPage("Index");
        }
        public async Task<IActionResult> OnPostUpdateAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _contatoService.Alterar(Contato, cancellationToken).ConfigureAwait(false);
            if (!result)
            {
                return RedirectToPage("/Error");
            }

            return RedirectToPage("Index");
        }
    }
}
