using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.Exceptions
{
    public class ApplicationBusinessException : Exception
    {
        public ApplicationBusinessException()
        { }

        public ApplicationBusinessException(string message)
            : base(message)
        { }

        public ApplicationBusinessException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
