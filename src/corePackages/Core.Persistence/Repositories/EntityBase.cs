using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Repositories
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
        }

        public int Id { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool isDeleted { get; set; }
    }
}
