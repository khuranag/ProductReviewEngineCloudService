using System;
using System.Net;

namespace WebRole1.Models
{
    /// <summary>
    /// Result Status
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// The success
        /// </summary>
        Success,

        /// <summary>
        /// The failure
        /// </summary>
        Failure,

        /// <summary>
        /// The internal error
        /// </summary>
        InternalError
    }

    /// <summary>
    /// Result class
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        public Result(Status status)
        {
            this.Status = status;
        }

        /// <summary>
        /// Gets or sets the statu code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets the reason phrase.
        /// </summary>
        /// <value>
        /// The reason phrase.
        /// </value>
        public string ErrorReason { get; set; }
    }
}