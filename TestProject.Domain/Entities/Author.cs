using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Common.Enum;

namespace TestProject.Domain.Entities
{
    public class Author : Entity
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public ICollection<BlogAuthorXref> BlogAuthorXrefs { get; set; }
    }
}
