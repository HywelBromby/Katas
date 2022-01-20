using BasicCrudAPIKata.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BasicCrudAPIKata
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
               

        // GET: api/<TicketController>
        [HttpGet]
        public async Task<IEnumerable<Ticket>> GetAll()
        {
            return await _ticketRepository.GetAll();
        }

        // GET api/<TicketController>/5
        [HttpGet("{id}")]
        public async Task<Ticket> Get(int id)
        {
            return await _ticketRepository.Get(id);
        }

        // POST api/<TicketController>
        [HttpPost]
        public async Task Post([FromBody] Ticket ticket)
        {
            await _ticketRepository.Add(ticket);
        }

        // PUT api/<TicketController>/5
        [HttpPut("{id}")]
        public async Task Put(int id,[FromBody] Ticket newValue)
        {
            await _ticketRepository.Update(id, newValue);
        }

        // DELETE api/<TicketController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _ticketRepository.Delete(id);
        }
    }
}
