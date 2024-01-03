﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Core.Entites
{
    public class Image : BaseEntity
    {
        public string ImgUrl { get; set; }
        public Portfolio Portfolio { get; set; }
        public int PortfolioId { get; set; }
    }
}