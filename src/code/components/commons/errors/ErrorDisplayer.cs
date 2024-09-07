using Godot;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.errors.logging;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;

namespace SpaceMiner.src.code.components.commons.errors
{
    public class ErrorDisplayer
    {
        public static ErrorHolder ErrorHolder { get; private set; }
        public static void Init(SceneTree tree)
        {   
            ErrorHolder = tree.Root.GetNode<ErrorHolder>(ErrorHolder.ErrorHolderNodeName);
        }

        /// <summary>
        /// Shows an error dialog by creating it and appending to an exceptions holder (autoload)
        /// </summary>
        /// <param name="message">Message to appear in the error dialog</param>
        /// <param name="type">Type to appear in the error dialog</param>
        /// <returns>Created ErrorDialog</returns>
        /// <exception cref="GameException"></exception>
        public static ErrorDialogBox ShowErrorDialog(string message, string type)
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
                PrettyLogger.Log(PrettyErrorType.Critical, "ErrorDialog", $"{InternalPaths.ERROR_DIALOG} Node needs to be a dialog");
                return null;
            }
        }
    }
}
