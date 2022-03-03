using Agenda.Areas.Contatos.Models;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Shared.Data
{
    public class AgendaDbContext : DbContext
    {
        public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base(options)
        {

        }
        public DbSet<Contato> Contatos { get; set; }
    }
}
