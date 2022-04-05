using System.Collections.Generic;
using Models;
using MongoDB.Driver;
using ProjMongoDBApi.Utils;

namespace ProjMongoDBApi.Services
{
    public class TicketService

    {
        private readonly IMongoCollection<Ticket> _ticket;        

        public TicketService(IProjMongoDBApiSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _ticket = database.GetCollection<Ticket>(settings.TicketCollectionName);
        }

        public List<Ticket> Get() =>
            _ticket.Find(ticket => true).ToList();

        public Ticket Get(string id) =>
            _ticket.Find<Ticket>(ticket => ticket.Id == id).FirstOrDefault();

        public Ticket Create(Ticket ticket)
        {
            _ticket.InsertOne(ticket);
            return ticket;
        }

        public void Update(string id, Ticket ticketIn) =>
            _ticket.ReplaceOne(ticket => ticket.Id == id, ticketIn);

        public void Remove(Ticket ticketIn) =>
            _ticket.DeleteOne(ticket => ticket.Id == ticketIn.Id);

        public void Remove(string id) =>
            _ticket.DeleteOne(ticket => ticket.Id == id);
    }
}
