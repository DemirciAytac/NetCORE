using HttpClientFactoryHelper.Constant;
using HttpClientFactoryHelper.Extensions;
using HttpClientFactoryHelper.Request;
using HttpClientFactoryHelper.Response;

namespace HttpClientFactoryHelper.ExternalServices
{
    public class ExternalService : IExternalService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ExternalService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async  Task AddPerson(AddPersonRequest request)
        {
             await _httpClientFactory.PostRequest<AddPersonRequest, string>(NamedHttpClients.ExternalFakeAPIClient, "addPerson", request);
        }

        public async Task<List<GetPersonResponse>> GetAllPerson()
        {
            return  await _httpClientFactory.GetRequest<string, List<GetPersonResponse>> (NamedHttpClients.ExternalFakeAPIClient, "getAllPersons", null);
           
        }

        public async Task<GetPersonResponse> GetPerson(GetPersonRequest request)
        {
            return await _httpClientFactory.GetRequest<GetPersonRequest, GetPersonResponse>(NamedHttpClients.ExternalFakeAPIClient, "getPerson", request);
           
        }

        public async Task UpdatePerson(UpdatePersonRequest request)
        {
             await _httpClientFactory.PutRequest<UpdatePersonRequest, string>(NamedHttpClients.ExternalFakeAPIClient, "updatePerson", request);
        }
    }
}
