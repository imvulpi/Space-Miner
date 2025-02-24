using Godot;
using SpaceMiner.src.code.components.user.ui.components.boards.dialog_boxes;
using System;

namespace SpaceMiner.src.code.components.user.ui.components.boards
{
    public partial class BinaryDecisionDialogBox : Control, IBinaryDecisionDialogBox
    {
        public event Action<bool> Decision;
        [Export] public BaseButton YesButton { get; set; }
        [Export] public BaseButton NoButton { get; set; }
        [Export] public Label TitleLabel {  get; set; }
        [Export] public Label MessageLabel { get; set; }
        public string SaveName { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public override void _Ready()
        {
            YesButton.Pressed += ConfirmButton_Pressed;
            NoButton.Pressed += CancelButton_Pressed;
            TitleLabel.Text = Title;
            MessageLabel.Text = Message;
        }

        private void CancelButton_Pressed()
        {
            Decision?.Invoke(false);
        }

        private void ConfirmButton_Pressed()
        {
            Decision?.Invoke(true);
        }
    }
}
