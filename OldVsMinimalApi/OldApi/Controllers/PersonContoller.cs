using Contracts.Models;
using Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace OldApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _service;

        public PersonController(IPersonService service)
        {
            _service = service; 
        }

        [HttpGet("")]
        public IResult GetAllPeople()
        {
            var result = _service.GetAll();
            return result.Any() ? Results.Ok(result) : Results.NoContent();
        }

        [HttpGet("{id}")]
        public IResult GetPerson(Guid id)
        {
            var person = _service.Get(id);
            return person is { } ? Results.Ok(person) : Results.NotFound();
        }

        [HttpPost("")]
        public IResult AddOrUpdatePerson([FromBody] Person person) => Results.Ok(_service.AddOrUpdate(person));
    }
}