using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Octopus.Client.Exceptions
{
    /// <summary>
    /// An exception thrown when there was a problem with the request (usually a HTTP 400). 
    /// </summary>
    public class OctopusValidationException : OctopusException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OctopusValidationException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errors">The errors.</param>
        public OctopusValidationException(string message, ICollection<string> errors)
            :this((int)System.Net.HttpStatusCode.BadRequest, message, errors)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OctopusValidationException" /> class.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code.</param>
        /// <param name="message">The message.</param>
        /// <param name="errors">The errors.</param>
        public OctopusValidationException(int httpStatusCode, string message, ICollection<string> errors)
            : base(httpStatusCode, message + Environment.NewLine + Environment.NewLine + string.Join(Environment.NewLine, errors.Select(e => " - " + e)) + Environment.NewLine)
        {
            ErrorMessage = message;
            Errors = errors.ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets the error message that was returned by the Octopus Server.
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Gets a list of problems with the request that was returned by the Octopus Server.
        /// </summary>
        public ReadOnlyCollection<String> Errors { get; private set; }
    }
}