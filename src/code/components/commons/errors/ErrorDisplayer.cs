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

        public static void ShowErrorDialog(string message, string type)
        {
            if (ErrorHolder == null)
            {
                throw new GameException(PrettyErrorType.Critical, "ErrorDialog", "Exceptions controller is not set");
            }

            Node node = ResourceLoader.Load<PackedScene>(InternalPaths.ERROR_DIALOG).Instantiate();
            if (node is ErrorDialog errorDialog)
            {
                errorDialog.ErrorMessage = message;
                errorDialog.ErrorType = type;
                errorDialog.Decision += (bool obj) =>
                {
                    ErrorHolder.RemoveChild(errorDialog);
                };
                ErrorHolder.AddChild(errorDialog);
            }
            else
            {
                PrettyLogger.Log(PrettyErrorType.Critical, "ErrorDialog", $"{InternalPaths.ERROR_DIALOG} Node needs to be a dialog");
            }
        }
    }
}
