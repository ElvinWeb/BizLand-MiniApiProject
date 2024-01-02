using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.DTO.WorkerDtos
{
    public class WorkerCreateDto
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public string MediaUrl { get; set; }
        public int ProfessionId { get; set; }
        public IFormFile ImgFile { get; set; }
    }
}
