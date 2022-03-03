using Agenda.Areas.Contatos.Models;
using Agenda.Shared.Data;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Agenda.Areas.Contatos.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IGenericRepository<Contato> _contatoRepository;
        private readonly IWebHostEnvironment _env;

        public ContatoService(IGenericRepository<Contato> contatoRepository, IWebHostEnvironment env)
        {
            _contatoRepository = contatoRepository;
            _env = env;
        }
        public async Task<bool> Adicionar(Contato contato, CancellationToken cancellationToken)
        {
            contato.Id = Guid.NewGuid();
            contato.FotoUrl = Path.Combine("images", "Contatos", $"{contato.Id}-{contato.Foto.FileName}");
            var fulPath = Path.Combine(_env.WebRootPath, contato.FotoUrl);
            using var fileStream = new FileStream(contato.FotoUrl, FileMode.Create);

            await contato.Foto.CopyToAsync(fileStream, cancellationToken).ConfigureAwait(false);

            await _contatoRepository.AddAsync(contato, cancellationToken).ConfigureAwait(false);
            var result = await _contatoRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
            return result;
        }
    }
}
