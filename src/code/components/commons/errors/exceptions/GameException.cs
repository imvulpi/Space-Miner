using System;
using System.Diagnostics;

#nullable enable
namespace SpaceMiner.src.code.components.commons.errors.exceptions
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class GameException : Exception
    {
        public string? Description { get; private set; }
        public string? DevMessage { get; private set; }
        public string? Cause { get; private set; }
        public PrettyErrorType? ErrorType { get; private set; }
        public string? CustomError { get; private set; }
        public GameException() : base() {}
        public GameException(string message) : base(message) { 
            Description = message;
        }
        public GameException(string message, Exception inner) : base(message, inner) {
            Description = message;
            ErrorType = PrettyErrorType.GeneralError;
        }
        public GameException(PrettyErrorType errorType, string cause, string message) : base($"[{errorType}]: {message}") { 
            Cause = cause;
            Description = message;
            ErrorType = errorType;
        }
        public GameException(PrettyErrorType errorType, string cause, string message, Exception inner) : base($"[{errorType}]: {message}", inner) {
            Cause = cause;
            Description = message;
            ErrorType = errorType;
        }
        public GameException(string customError, string cause, string message, Exception inner) : base($"[{customError}]: {message}", inner)
        {
            Cause = cause;
            Description = message;
            CustomError = customError;
        }

        public GameException(PrettyErrorType errorType, string cause, string message, string devMessage) : base($"[{errorType}]: {message}")
        {
            Cause = cause;
            Description = message;
            ErrorType = errorType;
            DevMessage = devMessage;
        }
        public GameException(PrettyErrorType errorType, string cause, string message, string devMessage, Exception inner) : base($"[{errorType}]: {message}", inner)
        {
            Cause = cause;
            Description = message;
            ErrorType = errorType;
            DevMessage = devMessage;
        }
        public GameException(string customError, string cause, string message, string devMessage, Exception inner) : base($"[{customError}]: {message}", inner)
        {
            Cause = cause;
            Description = message;
            CustomError = customError;
            DevMessage = devMessage;
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
