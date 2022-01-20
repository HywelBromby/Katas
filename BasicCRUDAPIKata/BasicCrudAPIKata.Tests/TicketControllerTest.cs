using BasicCrudAPIKata.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicCrudAPIKata.Tests
{
    [TestClass]
    public class TicketControllerTest
    {

        TicketController _IUT;

        [TestInitialize]
        public void Setup()
        {
            _IUT = new TicketController(new FakeITicketRepository());

        }

        [TestMethod]
        public async Task Given_ATicketIsAdded_ThenTheListCountShouldIncrease()
        {
            //The List Should Initialy be empty
            var tickets = await _IUT.GetAll();
            Assert.AreEqual(0,tickets.Count());


            var initialTicket = new Ticket { PersonId = 3, Content = "TheFirstAdded" };
            await _IUT.Post(initialTicket);
            var ticketsAfterAdd = await _IUT.GetAll();
            Assert.AreEqual(1, ticketsAfterAdd.Count());
        }

        [TestMethod]
        public async Task Given_ATicketIsAdded_ThenDeletedTheListCountShouldIncreaseThenDecrease()
        {
            //The List Should Initialy be empty
            var tickets = await _IUT.GetAll();
            Assert.AreEqual(0, tickets.Count());


            var initialTicket = new Ticket { Id=7, PersonId = 3, Content = "TheFirstAdded" };
            await _IUT.Post(initialTicket);
            var ticketsAfterAdd = await _IUT.GetAll();
            Assert.AreEqual(1, ticketsAfterAdd.Count());


            await _IUT.Delete(7);
            Assert.AreEqual(0, ticketsAfterAdd.Count());

        }


        [DataTestMethod]
        [DataRow(9,1,"abc")]
        [DataRow(10,2, "def")]
        [DataRow(11,3, "ghi")]
        public async Task Given_ATicketIsAdded_ThenTheValuesShouldBeSavedCorectly(int ticketId,int personId, string content)
        {
            //Used to add a ticket with an explicit Id (should use a guid and pass it in.....)
            var ticketToAdd = JsonConvert.DeserializeObject<Ticket>("{'PersonId':" + personId + ", 'Content':'" + content + "', 'Id':" + ticketId + " }");
                       
            await _IUT.Post(ticketToAdd);

            var actual =await  _IUT.Get(ticketId);

            Assert.AreEqual(ticketToAdd.Id, actual.Id);
            Assert.AreEqual(ticketToAdd.Content, actual.Content);
            Assert.AreEqual(ticketToAdd.PersonId, actual.PersonId);            
        }


        [DataTestMethod]
        [DataRow(1, "abc")]
        [DataRow(2, "def")]
        [DataRow(3, "ghi")]
        public async Task Given_ATicketIsUpdated_ThenTheValuesShouldBeSavedCorectly(int personId, string content)
        {

            var ticketToAdd = new Ticket
            {
                Id = 1,
                Content = "Test",
                PersonId = 2
            };

            await _IUT.Post(ticketToAdd);



            //Used to add a ticket with an explicit Id (should use a guid and pass it in.....)
            var ticketToUpdate = JsonConvert.DeserializeObject<Ticket>("{'PersonId':" + personId + ", 'Content':'" + content + "', 'Id':" + ticketToAdd.Id + " }");

            await _IUT.Put(ticketToAdd.Id,ticketToAdd);

            var actual = await _IUT.Get(ticketToAdd.Id);

             Assert.AreEqual(ticketToAdd.Content, actual.Content);
            Assert.AreEqual(ticketToAdd.PersonId, actual.PersonId);
        }


        private class FakeITicketRepository : ITicketRepository
        {
            List<Ticket> _ticketList = new List<Ticket>();
            public async Task Add(Ticket ticket)
            {
                _ticketList.Add(ticket);
            }

            public async Task Delete(int id)
            {
                _ticketList.Remove(_ticketList.First(i => i.Id == id));
            }

            public async Task<Ticket> Get(int id)
            {
                return _ticketList.FirstOrDefault<Ticket>(i => i.Id == id);
            }

            public async Task<IEnumerable<Ticket>> GetAll()
            {
                return _ticketList;
            }

            public async Task Update(int id, Ticket newValue)
            {
                var ticket = _ticketList.First<Ticket>(i => i.Id == id);
                ticket.PersonId = newValue.Id;
                ticket.Content = newValue.Content;
                
            }
        }

    }
}
