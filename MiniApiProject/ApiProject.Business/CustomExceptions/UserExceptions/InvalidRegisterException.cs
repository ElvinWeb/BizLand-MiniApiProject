using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.CustomExceptions.UserExceptions
{
    public class InvalidRegisterException : Exception
    {
        public InvalidRegisterException()
        {
        }

        public InvalidRegisterException(string? message) : base(message)
        {
        }

        
    }
}
