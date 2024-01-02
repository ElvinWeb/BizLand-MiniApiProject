using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Core.Entites
{ 
    public class Portfolio : BaseEntity
    {
        public string Title { get; set; }
        public string DetailUrl { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public List<Image> Images { get; set; }
    }
}
