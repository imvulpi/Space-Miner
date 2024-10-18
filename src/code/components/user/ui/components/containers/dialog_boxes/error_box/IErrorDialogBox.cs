using Godot;
using SpaceMiner.src.code.components.user.ui.components.boards.dialog_boxes;

namespace SpaceMiner.src.code.components.user.ui.components.boards.dialogs.error_dialog
{
    public interface IErrorDialogBox : IDialogBox
    {
        public BaseButton ReportButton {  get; set; }
        public BaseButton CancelButton { get; set; }
    }
}
