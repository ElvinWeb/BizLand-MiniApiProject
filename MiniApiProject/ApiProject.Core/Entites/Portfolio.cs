using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Core.Entites
{ 
    public class Portfolio : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProjectUrl { get; set; }
        public string Client { get; set; }
        public DateTime ProjectDate { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public List<Image> Images { get; set; }
        [NotMapped]
        public List<IFormFile> PortfolioImages { get; set; }
    }
}
