using HttpClientFactoryHelper.ExternalServices;
using HttpClientFactoryHelper.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace HttpClientFactoryHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpClientConsumerController : ControllerBase
    {
        private readonly IExternalService _externalService;

        public HttpClientConsumerController(IExternalService externalService)
        {
            _externalService = externalService;
        }

        [Route("GetPerson")]
        [HttpGet]
        public async Task<IActionResult> GetPerson(string name)
        {
            var response = await _externalService.GetPerson( new GetPersonRequest(name));

            return Ok(response);

        }

        [Route("GetAllPerson")]
        [HttpGet]
        public async Task<IActionResult> GetAllPerson()
        {
            var response = await _externalService.GetAllPerson();

            return Ok(response);

        }

        [Route("AddPerson")]
        [HttpPost]
        public async Task<IActionResult> AddPerson([FromBody] AddPersonRequest person)
        {
            await _externalService.AddPerson(person);
            return Ok();
        }

        [Route("updatePerson")]
        [HttpPost]
        public async Task<IActionResult> UpdatePerson([FromBody] UpdatePersonRequest person)
        {
            await _externalService.UpdatePerson(person);
            return Ok();
        }

    }
}
