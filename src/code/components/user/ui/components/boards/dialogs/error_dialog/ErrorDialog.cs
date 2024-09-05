using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.logging;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;
using System;

public partial class ErrorDialog : CanvasLayer
{
    public ErrorDialog() { }
    public ErrorDialog(string errorMessage, string errorType, Exception exception) { 
        ErrorMessage = errorMessage;
        ErrorType = errorType;
        Exception = exception;
    }
	[Export] private RichTextLabel ErrorTypeLabel {  get; set; }
	[Export] private RichTextLabel ErrorMessageLabel { get; set; }
	[Export] private Button OkButton { get; set; }
	[Export] public Button Report { get; set; }

	public string ErrorMessage = "No message included";
	public string ErrorType = "No type included";
    public Exception Exception = null;
    public override void _Ready()
    {
        GetTree().Paused = true;
        ErrorTypeLabel.Text = $"[center]{ErrorType}[center]";
		ErrorMessageLabel.Text = $"[center]{ErrorMessage}[center]";
        Report.Pressed += ReportButton_Pressed;
        OkButton.Pressed += OkButton_Pressed;
	}

    private void OkButton_Pressed()
    {
        GetTree().Paused = false;
        GD.Print("a");
        GetParent().RemoveChild(this);
    }

    private void ReportButton_Pressed()
    {
        try
        {
            GetTree().Paused = true;
            ReportErrorDialog reportMenu = ResourceLoader.Load<PackedScene>(InternalPaths.ERROR_REPORT_DIALOG).Instantiate<ReportErrorDialog>();
            AddChild(reportMenu);
        }catch(Exception ex)
        {
            ErrorTypeLabel.Text = ex.GetType().ToString();
            ErrorMessageLabel.Text = $"Opening report menu failed - {ex.Message} | Please report manually :/";
            PrettyLogger.Log(PrettyErrorType.Critical, "ReportMenuFail", ErrorMessageLabel.Text);
        }
    }
}
