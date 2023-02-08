namespace HttpClientFactoryHelper.Request
{
    public class GetPersonRequest
    {
        public GetPersonRequest(string name)
        {
            Name = name;    
        }

        public string Name { get; set; }
        
    }
}
