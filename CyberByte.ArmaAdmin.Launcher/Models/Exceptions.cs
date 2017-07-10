using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CyberByte.ArmaAdmin.Launcher.Models
{
    class RequestException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public RequestException() : base()

        {

        }

        /// <summary>
        /// Argument constructor
        /// </summary>
        /// <param name="message">This is the description of the exception</param>
        public RequestException(String message) : base(message)

        {

        }

        /// <summary>
        /// Argument constructor with inner exception
        /// </summary>
        /// <param name="message">This is the description of the exception</param>
        /// <param name="innerException">Inner exception</param>
        public RequestException(String message, Exception innerException) : base(message, innerException)

        {

        }

        /// <summary>
        /// Argument constructor with serialization support
        /// </summary>
        /// <param name="info">Instance of SerializationInfo</param>
        /// <param name="context">Instance of StreamingContext</param>
        protected RequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }

    class DownloadException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DownloadException() : base()

        {

        }

        /// <summary>
        /// Argument constructor
        /// </summary>
        /// <param name="message">This is the description of the exception</param>
        public DownloadException(String message) : base(message)

        {

        }

        /// <summary>
        /// Argument constructor with inner exception
        /// </summary>
        /// <param name="message">This is the description of the exception</param>
        /// <param name="innerException">Inner exception</param>
        public DownloadException(String message, Exception innerException) : base(message, innerException)

        {

        }

        /// <summary>
        /// Argument constructor with serialization support
        /// </summary>
        /// <param name="info">Instance of SerializationInfo</param>
        /// <param name="context">Instance of StreamingContext</param>
        protected DownloadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }

    class CredentialsException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public CredentialsException() : base()

        {

        }

        /// <summary>
        /// Argument constructor
        /// </summary>
        /// <param name="message">This is the description of the exception</param>
        public CredentialsException(String message) : base(message)

        {

        }

        /// <summary>
        /// Argument constructor with inner exception
        /// </summary>
        /// <param name="message">This is the description of the exception</param>
        /// <param name="innerException">Inner exception</param>
        public CredentialsException(String message, Exception innerException) : base(message, innerException)

        {

        }

        /// <summary>
        /// Argument constructor with serialization support
        /// </summary>
        /// <param name="info">Instance of SerializationInfo</param>
        /// <param name="context">Instance of StreamingContext</param>
        protected CredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }

    class BadResponseStatusException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public BadResponseStatusException() : base()

        {

        }

        /// <summary>
        /// Argument constructor
        /// </summary>
        /// <param name="message">This is the description of the exception</param>
        public BadResponseStatusException(String message) : base(message)

        {

        }

        /// <summary>
        /// Argument constructor with inner exception
        /// </summary>
        /// <param name="message">This is the description of the exception</param>
        /// <param name="innerException">Inner exception</param>
        public BadResponseStatusException(String message, Exception innerException) : base(message, innerException)

        {

        }

        /// <summary>
        /// Argument constructor with serialization support
        /// </summary>
        /// <param name="info">Instance of SerializationInfo</param>
        /// <param name="context">Instance of StreamingContext</param>
        protected BadResponseStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
