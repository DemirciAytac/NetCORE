using AutoDependencyInjection.Domain.Common;

namespace AutoDependencyInjection.Domain.Entities
{
    public class Book:BaseModel
    {
        public string Name { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }  
    }
}
