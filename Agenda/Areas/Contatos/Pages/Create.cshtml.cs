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
        public Contato Contato { get; set; }

        public CreateModel(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }
        public void OnGet()
        {
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
    }
}
