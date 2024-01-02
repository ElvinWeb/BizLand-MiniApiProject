using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Core.Entites
{
    public class Profession : BaseEntity
    {
        public string Name { get; set; }
        public List<Worker> Workers { get; set; }
    }
}
