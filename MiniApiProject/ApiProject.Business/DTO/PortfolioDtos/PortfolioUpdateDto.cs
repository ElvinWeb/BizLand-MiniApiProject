using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.DTO.PortfolioDtos
{
    public class PortfolioUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProjectUrl { get; set; }
        public string Client { get; set; }
        public DateTime ProjectDate { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile> PortfolioImages { get; set; }
    }
}
