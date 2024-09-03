using Godot;
using SpaceMiner.src.code.components.user.ui.components.boards;
using System;

public partial class ErrorDialog : CanvasLayer, IConfirmationDialog
{
	[Export] private Label ErrorTypeLabel {  get; set; }
	[Export] private RichTextLabel ErrorMessageLabel { get; set; }
	[Export] private Button OkButton { get; set; }
	public string ErrorMessage = "";
	public string ErrorType = "";
    public event Action<bool> Decision;
    public override void _Ready()
	{
		ErrorTypeLabel.Text = ErrorType;
		ErrorMessageLabel.Text = ErrorMessage;
        OkButton.Pressed += OkButton_Pressed;
	}

    private void OkButton_Pressed()
    {
		Decision.Invoke(true);
    }
}
