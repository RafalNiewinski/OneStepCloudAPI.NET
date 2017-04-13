using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.Exceptions
{
    public static class ExceptionHelper
    {
        public static Exception DetermineByHttpCode(HttpStatusCode code, string message = "")
        {
            if ((int)code == 401)
                return new NotAuthorizedException();
            else if ((int)code == 404)
                return new ResourceNotFoundException();
            else
                return new ServerErrorException(code, $"[{code}] {message}");
        }
    }
}
