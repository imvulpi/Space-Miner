namespace SpaceMiner.src.code.components.commons.errors
{
    internal class PrettyError
    {
        public PrettyErrorType? ErrorType { get; set; }
        public string CustomErrorType { get; set; }
        public string Cause { get; set; }
        public string Description { get; set; } = string.Empty;
        public PrettyError(PrettyErrorType errorType, string errorCause, string description = "")
        {
            ErrorType = errorType;
            CustomErrorType = errorType.ToString();
            Cause = errorCause;
            Description = description;
        }

        public PrettyError(string customErrorType, string errorCause, string description = "")
        {
            CustomErrorType = customErrorType;
            Cause = errorCause;
            Description = description;
        }

        public override string ToString()
        {
            string errorMessage = $"({(ErrorType == null ? CustomErrorType : ErrorType)})<{Cause}>{(Description == "" ? "" : $": {Description}")}";
            return errorMessage;
        }
    }
}
