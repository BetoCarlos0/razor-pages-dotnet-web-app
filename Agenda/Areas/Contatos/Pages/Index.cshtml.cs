using Agenda.Areas.Contatos.Models;
using Agenda.Areas.Contatos.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Agenda.Areas.Contatos.Pages
{
    public class IndexModel : PageModel
    {
        private const int QNT_PAGINA = 3;
        private readonly IContatoService _contatoService;

        public IEnumerable<Contato> Contatos { get; set; } = new List<Contato>();
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public IndexModel(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }
        public async Task OnGetAsync([FromQuery] int paginaAtual = 1, CancellationToken cancellationToken = default)
        {
            PaginaAtual = paginaAtual;
            var qntContatos = await _contatoService.ContarContatos(cancellationToken).ConfigureAwait(false);
            TotalPaginas = qntContatos / QNT_PAGINA;
            Contatos = await _contatoService.ObterTodos(paginaAtual, QNT_PAGINA, cancellationToken);
        }
        public async Task<IActionResult> OnPostFavoriteAsync([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var result = await _contatoService.AlternarFavorito(id, cancellationToken).ConfigureAwait(false);
            if (!result)
            {
                return RedirectToPage("/Erro");
            }
            return RedirectToPage("Index");
        }
    }
}
