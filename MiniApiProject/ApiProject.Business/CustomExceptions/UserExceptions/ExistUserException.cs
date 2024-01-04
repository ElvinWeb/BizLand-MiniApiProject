using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.CustomExceptions.UserExceptions
{
    public class ExistUserException : Exception
    {
        public ExistUserException()
        {
        }

        public ExistUserException(string? message) : base(message)
        {
        }

    }
}
