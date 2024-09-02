using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.commons.errors.logging
{
    public static class PrettyLoggerDefaults
    {
        // Generic
        private const string NO_DESCRIPTION = "";
        private const string CAUSE_PLACEHOLDER = "%cause%";

        // Error descriptions
        private const string GENERAL_ERROR_DESCRIPTION = "An ERROR has occurred!";
        private const string CRITICAL_ERROR_DESCRIPTION = "An Critical level ERROR has occured!";
        private const string OPERATION_FAILED_ERROR_DESCRIPTION = "The operation failed";
        private const string INVALID_ERROR_DESCRIPTION = "The input/state is invalid or incorrect";
        private const string RESOURCE_NOT_FOUND_ERROR_DESCRIPTION = "Resource not found";

        // Warning descriptions
        private const string CORRECTED_WARNING_DESCRIPTION = "The input/state/resource was corrected";
        private const string NOT_FOUND_WARNING_DESCRIPTION = "Resource not found";

        // Info descriptions
        private const string CHECKING_INFO_DESCRIPTION = $"Checking {CAUSE_PLACEHOLDER}";
        private const string LOADED_INFO_DESCRIPTION = $"Loaded {CAUSE_PLACEHOLDER}";
        private const string CREATED_INFO_DESCRIPTION = $"Created {CAUSE_PLACEHOLDER}";
        private const string RETRY_INFO_DESCRIPTION = $"Retrying {CAUSE_PLACEHOLDER}";
        private const string RETRY_ATTEMPTY_INFO_DESCRIPTION = $"{CAUSE_PLACEHOLDER} Repair attempt";
        private const string DEFAULTED_INFO_DESCRIPTION = $"Defaulted {CAUSE_PLACEHOLDER} value";


        /// <summary>
        /// Gets default descriptions for predefined some error types<br></br>
        /// if error does not have a description it will return ""
        /// </summary>
        public static string GetDefaultDescription(PrettyErrorType errorType)
        {
            switch (errorType)
            {
                case PrettyErrorType.GeneralError:
                    return GENERAL_ERROR_DESCRIPTION;
                case PrettyErrorType.Critical:
                    return CRITICAL_ERROR_DESCRIPTION;
                case PrettyErrorType.OperationFailed:
                    return OPERATION_FAILED_ERROR_DESCRIPTION;
                case PrettyErrorType.Invalid:
                    return INVALID_ERROR_DESCRIPTION;
                case PrettyErrorType.ResourceNotFound:
                    return RESOURCE_NOT_FOUND_ERROR_DESCRIPTION;
                default: return NO_DESCRIPTION;
            }
        }

        /// <summary>
        /// Gets default descriptions for predefined some warning types<br></br>
        /// if warning does not have a description it will return ""
        /// </summary>
        public static string GetDefaultDescription(PrettyWarningType warningType)
        {
            switch (warningType)
            {
                case PrettyWarningType.Corrected:
                    return CORRECTED_WARNING_DESCRIPTION;
                case PrettyWarningType.NotFound:
                    return NOT_FOUND_WARNING_DESCRIPTION;
                default: return NO_DESCRIPTION;
            }
        }

        /// <summary>
        /// Gets default descriptions for predefined some info types<br></br>
        /// if an info does not have a description it will return ""
        /// </summary>
        public static string GetDefaultDescription(PrettyInfoType infoType, string cause = "")
        {
            switch (infoType)
            {
                case PrettyInfoType.Checking:
                    return CHECKING_INFO_DESCRIPTION.Replace(CAUSE_PLACEHOLDER, cause);
                case PrettyInfoType.Loaded:
                    return LOADED_INFO_DESCRIPTION.Replace(CAUSE_PLACEHOLDER, cause);
                case PrettyInfoType.Created:
                    return CREATED_INFO_DESCRIPTION.Replace(CAUSE_PLACEHOLDER, cause);
                case PrettyInfoType.Retry:
                    return RETRY_INFO_DESCRIPTION.Replace(CAUSE_PLACEHOLDER, cause);
                case PrettyInfoType.RepairAttempt:
                    return RETRY_ATTEMPTY_INFO_DESCRIPTION.Replace(CAUSE_PLACEHOLDER, cause);
                case PrettyInfoType.Defaulted:
                    return DEFAULTED_INFO_DESCRIPTION.Replace(CAUSE_PLACEHOLDER, cause);
                default: return NO_DESCRIPTION;
            }
        }

        
    }
}
