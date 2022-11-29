using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Domain.Entities
{
    public class Blog : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public ICollection<Author> Authors { get; set; }

        public ICollection<ReaderComment> ReaderComments { get; set; }

        public ICollection<BlogAuthorXref> BlogAuthorXrefs { get; set; }

        public ReaderReaction ReaderReaction { get; set; }
    }
}
