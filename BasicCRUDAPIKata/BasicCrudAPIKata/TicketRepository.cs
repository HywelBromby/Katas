using BasicCrudAPIKata.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicCrudAPIKata
{


    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationContext _applicationContext;

        public TicketRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            return await _applicationContext.Tickets.ToListAsync();
        }

        public async Task<Ticket> Get(int id)
        {
            return await _applicationContext.Tickets.FindAsync(id);
        }

        public async Task Add(Ticket ticket)
        {
            await _applicationContext.Tickets.AddAsync(ticket);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task Update(int id, Ticket newValue)
        {            
            var ticket = await _applicationContext.Tickets.FindAsync(id);

            ticket.Content = newValue.Content;
            ticket.PersonId = newValue.PersonId;

            await _applicationContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var ticket = await _applicationContext.Tickets.FindAsync(id);
            _applicationContext.Tickets.Remove(ticket);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
