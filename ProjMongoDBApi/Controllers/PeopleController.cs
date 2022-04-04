using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjMongoDBApi.Model;
using ProjMongoDBApi.Services;

namespace ProjMongoDBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly ClientService _clientService;
        public PeopleController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public ActionResult<List<Person>> Get() =>
            _clientService.Get();


        [HttpGet("{id:length(24)}", Name = "GetPerson")]
        public ActionResult<Person> Get(string id)
        {
            var person = _clientService.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        [HttpPost]
        public ActionResult<Person> Create(Person person)
        {
            _clientService.Create(person);

            return CreatedAtRoute("GetPerson", new { id = person.Id.ToString() }, person);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Person personIn)
        {
            var person = _clientService.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            _clientService.Update(id, personIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var person = _clientService.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            _clientService.Remove(person.Id);

            return NoContent();
        }
    }
}
