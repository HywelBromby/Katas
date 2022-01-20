using BasicCrudAPIKata.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicCrudAPIKata
{
    public interface ITicketRepository
    {
        Task Add(Ticket ticket);
        Task Delete(int id);
        Task<IEnumerable<Ticket>> GetAll();
        Task<Ticket> Get(int id);
        Task Update(int id, Ticket newValue);
    }
}