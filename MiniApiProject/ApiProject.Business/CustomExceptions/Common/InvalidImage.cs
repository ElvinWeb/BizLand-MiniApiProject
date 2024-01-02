﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.CustomExceptions.Common
{
    public class InvalidImage : Exception
    {
        public InvalidImage()
        {
        }

        public InvalidImage(string? message) : base(message)
        {
        }

     
    }
}
