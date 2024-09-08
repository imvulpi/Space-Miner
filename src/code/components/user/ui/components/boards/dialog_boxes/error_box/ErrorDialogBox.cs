using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.logging;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;
using SpaceMiner.src.code.components.user.ui.components.boards.dialogs.error_dialog;
using System;
public partial class ErrorDialogBox : CanvasLayer, IErrorDialogBox
{
	[Export] private RichTextLabel ErrorTypeLabel {  get; set; }
	[Export] private RichTextLabel ErrorMessageLabel { get; set; }
    [Export] public BaseButton ReportButton { get; set; }
    [Export] public BaseButton CancelButton { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public override void _Ready()
    {
        GetTree().Paused = true;
        ErrorTypeLabel.Text = $"[center]{Title}[center]";
		ErrorMessageLabel.Text = $"[center]{Message}[center]";
        ReportButton.Pressed += ReportButton_Pressed;
        CancelButton.Pressed += CancelButton_Pressed;
	}

    private void CancelButton_Pressed()
    {
        GetTree().Paused = false;
        GetParent().RemoveChild(this);
    }

    private void ReportButton_Pressed()
    {
        try
        {
            GetTree().Paused = true;
            ReportErrorDialogBox reportMenu = ResourceLoader.Load<PackedScene>(InternalPaths.ERROR_REPORT_DIALOG).Instantiate<ReportErrorDialogBox>();
            reportMenu.parentErrorDialogBox = this;
            AddChild(reportMenu);
        }catch(Exception ex)
        {
            ErrorTypeLabel.Text = $"[center]{ex.GetType()}[center]";
            ErrorMessageLabel.Text = $"Opening report menu failed - {ex.Message} | Please report manually :/";
            Logger.Log(PrettyErrorType.Critical, "ReportMenuFail", ErrorMessageLabel.Text);
        }
    }
}
