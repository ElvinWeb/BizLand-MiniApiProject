using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Core.Entites
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime DeletedDate { get; set; } = DateTime.UtcNow.AddHours(4);
    }
}
