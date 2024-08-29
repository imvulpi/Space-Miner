using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.commons.errors
{
    public enum PrettyErrorType
    {
        /// <summary>
        /// Represents a general error that does not fit into more specific categories.
        /// </summary>
        GeneralError,

        /// <summary>
        /// Indicates a critical error that requires immediate attention and resolution.
        /// </summary>
        Critical,

        /// <summary>
        /// Denotes a failure that occurred during an operation or task.
        /// </summary>
        OperationFailed,

        /// <summary>
        /// Signifies that the input or state provided is invalid or incorrect.
        /// </summary>
        Invalid,

        /// <summary>
        /// Represents an error where a required resource could not be found.
        /// </summary>
        ResourceNotFound,
    }
}
