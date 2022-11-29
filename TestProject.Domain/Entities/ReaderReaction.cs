using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Domain.Entities
{
    public class ReaderReaction : Entity
    {
        public int LikeCount { get; set; }

        public int DislikeCount { get; set; }

        public Blog Blog { get; set; }

        public int BlogId { get; set; }
    }
}
