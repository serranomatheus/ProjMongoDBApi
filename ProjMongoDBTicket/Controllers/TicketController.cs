using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using ProjMongoDBApi.Services;
using ProjMongoDBTicket.Services;

namespace ProjMongoDBTicket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;
        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public ActionResult<List<Ticket>> Get() =>
            _ticketService.Get();


        [HttpGet("{id:length(24)}", Name = "GetTicket")]
        public ActionResult<Ticket> Get(string id)
        {
            var ticket = _ticketService.Get(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>> Create(Ticket ticket)
        {
            try
            {
                HttpClient ApiConnection = new HttpClient();
                HttpResponseMessage passenger = await ApiConnection.GetAsync("https://localhost:44300/api/Passenger/Search?cpf=" + ticket.Passenger.Cpf);

                string responseBody = await passenger.Content.ReadAsStringAsync();
                var passengerCpf = JsonConvert.DeserializeObject<Passenger>(responseBody);
                if (passengerCpf.Cpf == null)
                    return NotFound("Passenger  not found");
                ticket.Passenger = passengerCpf;

                

                HttpResponseMessage basePrice = await ApiConnection.GetAsync("https://localhost:44359/api/BasePrice/Search?origin=" + ticket.Flight.Origin.CodeIata +"&destination=" + ticket.Flight.Destination.CodeIata);

                responseBody = await basePrice.Content.ReadAsStringAsync();
                var basePriceObject = JsonConvert.DeserializeObject<BasePrice>(responseBody);
                if (basePriceObject.Destination.CodeIata == null || basePriceObject.Origin.CodeIata == null)
                    return NotFound("BasePrice not found");
                ticket.BasePrice = basePriceObject;

                ticket.Amount = (ticket.BasePrice.Value + ticket.FlightClass.Value) - (((ticket.BasePrice.Value + ticket.FlightClass.Value)*ticket.Promotion)/100);
               
                
                HttpResponseMessage flight = await ApiConnection.GetAsync("https://localhost:44314/api/Flights/Search?origin="+ ticket.Flight.Origin.CodeIata + "&destination="+ticket.Flight.Destination.CodeIata);
                responseBody = await flight.Content.ReadAsStringAsync();
                var flightObject = JsonConvert.DeserializeObject<Flight>(responseBody);
                if (flightObject.Destination.CodeIata == null || flightObject.Origin.CodeIata == null)
                    return NotFound("Flight not found");
                ticket.Flight = flightObject;


            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }
            var responseGetLogin = await GetLoginUser.GetLogin(ticket);

            if (responseGetLogin.Sucess == true)
            {
                _ticketService.Create(ticket);
                return CreatedAtRoute("GetTicket", new { id = ticket.Id.ToString() }, ticket);
            }
            else
            {
                return GetResponse(responseGetLogin);
            }          

           
        }
        private ActionResult GetResponse(BaseResponse baseResponse)
        {
            if (baseResponse.Sucess == true)
            {
                return Ok(baseResponse.Result);
            }
            return BadRequest(baseResponse.Error);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Ticket ticketIn)
        {
            var ticket = _ticketService.Get(id);

            if (ticket == null)
            {
                return NotFound();
            }

            _ticketService.Update(id, ticketIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var ticket = _ticketService.Get(id);

            if (ticket == null)
            {
                return NotFound();
            }

            _ticketService.Remove(ticket.Id);

            return NoContent();
        }
    }
}
