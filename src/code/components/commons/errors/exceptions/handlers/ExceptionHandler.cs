using Godot;
using SpaceMiner.src.code.components.commons.errors.logging;
using System;

namespace SpaceMiner.src.code.components.commons.errors.exceptions.handlers
{
    public class ExceptionHandler
    {
        private static Exception LastException { get; set; }

        /// <summary>
        /// Handles General Exceptions and some Game Exceptions - logs them and optionally shows a dialog
        /// </summary>
        /// <param name="showToUser">Whether a error dialog should appear to the user.</param>
        public static void HandleException(Exception exception, bool showToUser = false)
        {
            try
            {
                if (exception is GameException gameException)
                {
                    HandleException(gameException, showToUser);
                }
                else
                {
                    if (CheckLastException(exception, showToUser)) return;
                    Logger.Log(PrettyLogType.Error, exception.GetType().ToString(), $"Exception", $"{exception.Message}");
                    if (showToUser)
                    {
                        ErrorDisplayer.ShowErrorDialog(exception.Message, exception.GetType().ToString());
                    }
                }
            }
            catch(Exception e)
            {
                Logger.CriticalLog(PrettyErrorType.Critical, $"{e.GetType()}", $"The exception handler had an internal exception | {e}", e.Message);
                ErrorDisplayer.ShowErrorDialog($"Critical error: {e.Message} - Please report this to the developers.", e.GetType().ToString());
                throw;
            }
        }

        /// <summary>
        /// Handles Game Exceptions - logs them or and optionally shows a dialog
        /// </summary>
        /// <param name="showToUser">Whether a error dialog should appear to the user.</param>
        public static void HandleException(GameException exception, bool showToUser = false)
        {
            try
            {
                if (CheckLastException(exception, showToUser)) return;

                if (exception.ErrorType != null)
                {
                    Logger.Log((PrettyErrorType)exception.ErrorType, exception.Cause, exception.Message);
                    if (showToUser) ErrorDisplayer.ShowErrorDialog(exception.Message, exception.ErrorType.ToString());
                }
                else
                {
                    Logger.Log(PrettyLogType.Error, exception.CustomError, exception.Cause, exception.Message);
                    if (showToUser) ErrorDisplayer.ShowErrorDialog(exception.Message, exception.CustomError);
                }
            }
            catch (Exception e)
            {
                Logger.CriticalLog(PrettyErrorType.Critical, $"{e.GetType()}", $"The exception handler had an internal exception | {e}", e.Message);
                ErrorDisplayer.ShowErrorDialog($"Critical error: {e.Message} - Please report this to the developers.", e.GetType().ToString());
                throw;
            }
        }

        private static bool CheckLastException(Exception ex, bool showToUser)
        {
            if (LastException != null && ex.StackTrace == LastException.StackTrace)
            {
                Logger.Log(PrettyErrorType.Critical, $"RepeatingError", $"Errors are from the same source, futher playing might be impossible.");
                if (showToUser) ErrorDisplayer.ShowErrorDialog($"Errors are from the same source, futher playing might be impossible\nTRY restarting the game, if this continues then please report this to the developers.", "Repeating Errors");
                return true;
            }
            LastException = ex;
            return false;
        }
    }
}
