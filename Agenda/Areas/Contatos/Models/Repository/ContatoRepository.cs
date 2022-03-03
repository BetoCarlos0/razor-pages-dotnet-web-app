using Agenda.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Areas.Contatos.Models.Repository
{
    public class ContatoRepository : GenericRepository<Contato>
    {
        public ContatoRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
