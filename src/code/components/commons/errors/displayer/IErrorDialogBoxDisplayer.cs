using Godot;
using SpaceMiner.src.code.components.user.ui.components.boards.dialogs.error_dialog;

namespace SpaceMiner.src.code.components.commons.errors.displayer
{
    public interface IErrorDialogBoxDisplayer
    {
        Node ErrorHolder { get; set; }
        IErrorDialogBox Display(string message, string type);
    }
}
