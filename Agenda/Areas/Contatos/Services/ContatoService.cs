﻿using Agenda.Areas.Contatos.Models;
using Agenda.Areas.Contatos.Models.Repository;
using Agenda.Shared.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Agenda.Areas.Contatos.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IGenericRepository<Contato> _genericRepository;
        private readonly IWebHostEnvironment _env;

        public ContatoService(
            IGenericRepository<Contato> genericRepository,
            IContatoRepository contatoRepository,
            IWebHostEnvironment env)
        {
            _genericRepository = genericRepository;
            _env = env;
        }
        public async Task<bool> Adicionar(Contato contato, CancellationToken cancellationToken)
        {
            contato.Id = Guid.NewGuid();
            await UploadFoto(contato, cancellationToken).ConfigureAwait(false);

            await _genericRepository.AddAsync(contato, cancellationToken).ConfigureAwait(false);
            var result = await _genericRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
            return result;
        }

        

        public async Task<bool> Alterar(Contato contato, CancellationToken cancellationToken)
        {
            if (contato.Foto != null)
            {
                await UploadFoto(contato, cancellationToken).ConfigureAwait(false);
                _genericRepository.Update(contato);
            }
            else
            {
                _genericRepository.AtualizarContatoSemFoto(contato);
            }
            contato.DataModificacao = DateTime.Now;
            return await _genericRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<bool> ApagarContato(Guid id, CancellationToken cancellationToken)
        {
            var contato = await _genericRepository.GetByKeysAsync(cancellationToken, id).ConfigureAwait(false);
            _genericRepository.Delete(contato);
            return await _genericRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<Contato> ObterPorId(Guid id, CancellationToken cancellationToken)
        {
            return await _genericRepository.GetByKeysAsync(cancellationToken, id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Contato>> ObterTodos(CancellationToken cancellationToken)
        {
            return await _genericRepository.GetAllAsync(cancellationToken: cancellationToken, noTracking: true);
        }
        private async Task UploadFoto(Contato contato, CancellationToken cancellationToken)
        {
            if(contato.Foto != null)
            {
                contato.FotoUrl = Path.Combine("images", "Contatos", $"{contato.Id}-{contato.Foto.FileName}");
                var fulPath = Path.Combine(_env.WebRootPath, contato.FotoUrl);
                var fileStream = new FileStream(fulPath, FileMode.Create);

                await contato.Foto.CopyToAsync(fileStream, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
