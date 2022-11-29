using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Domain.Entities
{
    public class ReaderComment : Entity
    {
        public string Comment { get; set; }

        public Blog Blog { get; set; }

        public int BlogId { get; set; }
    }
}
