using System;
using System.Runtime.Serialization;

namespace FunkyTasks {
    [Serializable]
    public class PredicateFailedException : Exception {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:PredicateFailedException" /> class
        /// </summary>
        public PredicateFailedException() {}

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:PredicateFailedException" /> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String" /> that describes the exception. </param>
        public PredicateFailedException(string message) : base(message) {}

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:PredicateFailedException" /> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String" /> that describes the exception. </param>
        /// <param name="inner">The exception that is the cause of the current exception. </param>
        public PredicateFailedException(string message, Exception inner) : base(message, inner) {}

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:PredicateFailedException" /> class
        /// </summary>
        /// <param name="context">The contextual information about the source or destination.</param>
        /// <param name="info">The object that holds the serialized object data.</param>
        protected PredicateFailedException(
            SerializationInfo info,
            StreamingContext context) : base(
                info,
                context) {}
    }
}