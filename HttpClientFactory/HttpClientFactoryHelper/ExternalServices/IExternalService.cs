using HttpClientFactoryHelper.Request;
using HttpClientFactoryHelper.Response;

namespace HttpClientFactoryHelper.ExternalServices
{
    public interface IExternalService
    {
        Task<List<GetPersonResponse>> GetAllPerson();
        Task<GetPersonResponse> GetPerson(GetPersonRequest request);
        Task AddPerson(AddPersonRequest request);
        Task UpdatePerson(UpdatePersonRequest request);
    }
}
