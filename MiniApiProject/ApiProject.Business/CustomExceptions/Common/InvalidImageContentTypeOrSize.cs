using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.CustomExceptions.Common
{
    public class InvalidImageContentTypeOrSize : Exception
    {
        public InvalidImageContentTypeOrSize()
        {
        }

        public InvalidImageContentTypeOrSize(string? message) : base(message)
        {
        }

    }
}
