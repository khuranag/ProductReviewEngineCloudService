using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebRole1.Models;

namespace WebRole1.Controllers
{
    /// <summary>
    /// Container for API Controller extensions
    /// </summary>
    public static class ApiControllerExtension
    {
        /// <summary>
        /// Examines the repository result.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="result">The result.</param>
        public static void ExamineRepositoryResult(this ApiController controller, Result result)
        {
            if (result.Status == Status.InternalError)
            {
                SendErrorResponse(result.ErrorReason, HttpStatusCode.InternalServerError);
            }
            else if (result.Status != Status.Success)
            {
                SendErrorResponse(result.ErrorReason, HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Sends the error response.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <exception cref="HttpResponseException"></exception>
        /// <exception cref="HttpResponseMessage"></exception>
        private static void SendErrorResponse(string message, HttpStatusCode statusCode)
        {
            throw new HttpResponseException(new HttpResponseMessage()
            {
                ReasonPhrase = message,
                StatusCode = statusCode
            });

        }

    }
}