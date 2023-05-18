using AutoDependencyInjection.Domain.Common;
using System;

namespace AutoDependencyInjection.Domain.Entities
{
    public class Author : BaseModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Genre { get; set; }
        public Guid BookId { get; set; }
        // One-to-many relationship with books
        public List<Book> Books { get; set; }
    }
}
