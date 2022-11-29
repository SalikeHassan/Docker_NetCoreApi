using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Domain.Entities
{
    public class BlogAuthorXref
    {
        public Author Author { get; set; }

        public int AuthorId { get; set; }

        public Blog Blog { get; set; }

        public int BlogId { get; set; }
    }
}
