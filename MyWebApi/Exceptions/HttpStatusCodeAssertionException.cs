﻿namespace MyWebApi.Exceptions
{
    using System;

    /// <summary>
    /// Exception for invalid status code result when expecting certain HTTP status code.
    /// </summary>
    public class HttpStatusCodeAssertionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the HttpStatusCodeAssertionException class.
        /// </summary>
        /// <param name="message">Message for System.Exception class.</param>
        public HttpStatusCodeAssertionException(string message)
            : base(message)
        {
        }
    }
}
