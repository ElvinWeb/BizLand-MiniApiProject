using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.DTO.PortfolioDtos
{
    public class PortfolioGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProjectUrl { get; set; }
        public string Client { get; set; }
        public string ProjectDate { get; set; }
        public string Category { get; set; }
        public string ImgUrl { get; set; }  
    }
}
