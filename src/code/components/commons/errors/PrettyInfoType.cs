using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.commons.errors
{
    public enum PrettyInfoType
    {
        /// <summary>
        /// Used to display general information or updates that are not specific to other categories.
        /// </summary>
        GeneralInfo,

        /// <summary>
        /// Indicates that an operation or task completed successfully.
        /// </summary>
        Success,

        /// <summary>
        /// Represents a status where a check or verification process is ongoing.
        /// </summary>
        Checking,

        /// <summary>
        /// Indicates that data or a resource has been successfully loaded.
        /// </summary>
        Loaded,

        /// <summary>
        /// Represents that a new entity, resource, or record has been successfully created.
        /// </summary>
        Created,

        /// <summary>
        /// Suggests that an operation is being attempted again, possibly after a failure or delay.
        /// </summary>
        Retry,

        /// <summary>
        /// Denotes an ongoing attempt to repair or fix an issue or error.
        /// </summary>
        RepairAttempt,
    }
}
