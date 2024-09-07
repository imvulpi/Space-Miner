using Godot;
using SpaceMiner.src.code.components.user.ui.components.boards.dialog_boxes;
using System;

public partial class ConfirmationDialog : Control, IConfirmationDialog
{
    public event Action<bool> Decision;
	[Export] public Button ConfirmButton {  get; set; }
	[Export] public Button CancelButton { get; set; }
    public override void _Ready()
	{
        ConfirmButton.Pressed += ConfirmButton_Pressed;
        CancelButton.Pressed += CancelButton_Pressed;
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
