using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.DTO.WorkerDtos
{
    public class WorkerGetDto
    {
        public string FullName { get; set; }
        public string MediaUrl { get; set; }
        public string Profession { get; set; }
        public string ImgUrl { get; set; }
    }
}
