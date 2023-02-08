using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientFakeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        public readonly static IList<Person> _values = new List<Person>
            {
                new Person(1,"Aytaç",30),
                new Person(2,"Hazal",26),
                new Person(3,"Hande",8),
                new Person(4,"Berrin",15),

            };
        public PersonController()
        {

        }

        [Route("getAllPersons")]
        [HttpGet]
        public async Task<IActionResult> GetAllPersons()
        {
            return Ok(_values);
        }

        [Route("getPerson")]
        [HttpGet]
        public async Task<IActionResult> GetPerson(string name)
        {
            return Ok(_values.FirstOrDefault(x => x.Name == name));
        }

        [Route("addPerson")]
        [HttpPost]
        public async Task<IActionResult> AddPerson([FromBody]AddPersonRequest person)
        {
            int maxId = _values.MaxBy(x => x.Id).Id + 1;
            _values.Add(new Person(maxId, person.Name, person.Age));
            return Ok();
        }

        [Route("updatePerson")]
        [HttpPut]
        public async Task<IActionResult> UpdatePerson([FromBody]UpdatePersonRequest request)
        {
            var person = _values.FirstOrDefault(x => x.Id == request.Id);

            if(person == null)
            {
                return NotFound();
            }

            person.Name = request.Name;
            person.Age = request.Age;
            return Ok();
             

        }

    }
}
