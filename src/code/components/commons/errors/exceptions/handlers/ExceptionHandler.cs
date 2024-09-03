using SpaceMiner.src.code.components.commons.errors.logging;
using System;

namespace SpaceMiner.src.code.components.commons.errors.exceptions.handlers
{
    public class ExceptionHandler
    {
        public static void HandleException(Exception exception, bool showToUser = true)
        {
            if(exception is GameException gameException)
            {
                HandleException(gameException, showToUser);
            }
            else
            {
                PrettyLogger.Log(PrettyLogType.Error, exception.GetType().ToString(), $"Exception", $"{exception.Message}");
                if (showToUser)
                {
                   ErrorDisplayer.ShowErrorDialog(exception.Message, exception.GetType().ToString());
                }
            }
        }

        public static void HandleException(GameException exception, bool showToUser = true)
        {
            if (exception.ErrorType != null)
            {
                PrettyLogger.Log((PrettyErrorType)exception.ErrorType, exception.Cause, exception.Message);
                if (showToUser) ErrorDisplayer.ShowErrorDialog(exception.Message, exception.ErrorType.ToString());
            }
            else
            {
                PrettyLogger.Log(PrettyLogType.Error, exception.CustomError, exception.Cause, exception.Message);
                if(showToUser) ErrorDisplayer.ShowErrorDialog(exception.Message, exception.CustomError);
            }
        }
    }
}
