using Godot;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.errors.logging;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;
using SpaceMiner.src.code.components.user.ui.components.boards.dialogs.error_dialog;

namespace SpaceMiner.src.code.components.commons.errors.displayer
{
    public class ErrorDialogBoxBasicDisplayer : IErrorDialogBoxDisplayer
    {
        public Node ErrorHolder { get; set; }
        
        /// <summary>
        /// Shows an error dialog by creating it and appending to an exceptions holder (autoload)
        /// </summary>
        /// <param name="message">Message to appear in the error dialog</param>
        /// <param name="type">Type to appear in the error dialog</param>
        /// <returns>Created ErrorDialog</returns>
        /// <exception cref="GameException"></exception>
        public IErrorDialogBox Display(string message, string type)
        {
            if (ErrorHolder == null)
            {
                throw new GameException(PrettyErrorType.Critical, "ErrorDialog", "Exceptions holder is not set");
            }

            Node node = ResourceLoader.Load<PackedScene>(InternalPaths.ERROR_DIALOG).Instantiate();
            if (node is ErrorDialogBox errorDialog)
            {
                errorDialog.Message = message;
                errorDialog.Title = type;
                ErrorHolder.AddChild(errorDialog);
                return errorDialog;
            }
            else
            {
                Logger.Log(PrettyErrorType.Critical, "ErrorDialog", $"{InternalPaths.ERROR_DIALOG} Node needs to be a dialog");
                return null;
            }
        }
    }
}
