using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using ProjMongoDBApi.Services;
using ProjMongoDBApi.Utils;
using Xunit;

namespace ProjTestTicket
{
    public class UnitTest1
    {
        public TicketService InitializeDataBase()
        {
            var settings = new ProjMongoDBApiSettings();
            TicketService ticketService = new(settings);
            return ticketService;
        }

       

        

        [Fact]
        public void GetAll()
        {
            var ticketService = InitializeDataBase();
            IEnumerable<Ticket> tickets = ticketService.Get();
            Assert.Equal(3, tickets.Count());

        }

        

        [Fact]
        public void GetId()
        {
            var ticketService = InitializeDataBase();
            var ticket = ticketService.Get("624f30cbca7c6d0eb25ec3f6");
            if (ticket == null) ticket = new Ticket();
            Assert.Equal("624f30cbca7c6d0eb25ec3f6", ticket.Id);
        }
        [Fact]
        public void Create()
        {
            var ticketService = InitializeDataBase();
            Ticket newTicket = new Ticket()
            {
                LoginUser = "Jose Costa",                
                Amount = 566556

            };
            ticketService.Create(newTicket);
            var ticket = ticketService.Get(newTicket.Id);
            Assert.Equal("Jose Costa", ticket.LoginUser);
        }

        [Fact]
        public void Delete()
        {
            var ticketService = InitializeDataBase();

            var ticket = ticketService.Get("624f30cbca7c6d0eb25ec3f6");
            ticketService.Remove(ticket.Id);
            ticket = ticketService.Get("624f30cbca7c6d0eb25ec3f6");
            Assert.Null(ticket);

        }

        [Fact]
        public void Update()
        {
            var ticketService = InitializeDataBase();
            Ticket newTicket = new Ticket()
            {


                LoginUser = "Jose Costa",
                Amount = 566556

            };

            var ticket = ticketService.Get("624f30cbca7c6d0eb25ec3f6");
            newTicket.Id = ticket.Id;
            ticketService.Update(ticket.Id, newTicket);
            var ticketReturn = ticketService.Get("624f30cbca7c6d0eb25ec3f6");
            Assert.Equal("Costa", ticketReturn.LoginUser);

        }
    }
}
