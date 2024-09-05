using Godot;
using SpaceMiner.src.code.components.commons.errors.logging;
using SpaceMiner.src.code.components.commons.errors.report;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using System;
using System.IO;

namespace SpaceMiner.src.code.components.commons.errors.exceptions.handlers
{
    public class ExceptionHandler
    {
        /// <summary>
        /// Handles General Exceptions and some Game Exceptions - logs them and optionally shows a dialog
        /// </summary>
        /// <param name="showToUser">Whether a error dialog should appear to the user.</param>
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

        /// <summary>
        /// Handles Game Exceptions - logs them or and optionally shows a dialog
        /// </summary>
        /// <param name="showToUser">Whether a error dialog should appear to the user.</param>
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
