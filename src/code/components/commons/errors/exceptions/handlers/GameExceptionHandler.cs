using Godot;
using SpaceMiner.src.code.components.commons.errors.logging;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;
using System;

namespace SpaceMiner.src.code.components.commons.errors.exceptions.handlers
{
    public class GameExceptionHandler
    {
        private static ExceptionsController ExceptionsController;
        public static void Init(SceneTree tree)
        {
            ExceptionsController = tree.Root.GetNode<ExceptionsController>("ExceptionsController");
        }

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
                    ShowErrorDialog(exception.Message, exception.GetType().ToString());
                }
            }
        }

        public static void HandleException(GameException exception, bool showToUser = true)
        {
            if (exception.ErrorType != null)
            {
                PrettyLogger.Log((PrettyErrorType)exception.ErrorType, exception.Cause, exception.Message);
                if (showToUser) ShowErrorDialog(exception.Message, exception.ErrorType.ToString());
            }
            else
            {
                PrettyLogger.Log(PrettyLogType.Error, exception.CustomError, exception.Cause, exception.Message);
                if(showToUser) ShowErrorDialog(exception.Message, exception.CustomError);
            }
        }

        private static void ShowErrorDialog(string message, string type)
        {
            Node node = ResourceLoader.Load<PackedScene>(InternalPaths.ERROR_DIALOG).Instantiate();
            if(ExceptionsController == null)
            {
                throw new GameException(PrettyErrorType.Critical, "ErrorDialog", "Exceptions controller is not set");
            }

            if (node is ErrorDialog errorDialog)
            {
                errorDialog.ErrorMessage = message;
                errorDialog.ErrorType = type;
                errorDialog.Decision += (bool obj) =>
                {
                    ExceptionsController.RemoveChild(errorDialog);
                };
                ExceptionsController.AddChild(errorDialog);
            }
            else
            {
                PrettyLogger.Log(PrettyErrorType.Critical, "ErrorDialog", "Node needs to be a dialog");
            }
        }
    }
}
