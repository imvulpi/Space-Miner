using Godot;
using SpaceMiner.src.code.components.commons.errors.exceptions.handlers;
using SpaceMiner.src.code.components.commons.errors.report;
using SpaceMiner.src.code.components.commons.other.paths.other_paths;
using SpaceMiner.src.code.components.user.ui.components.boards.dialogs.error_dialog;
using System;

public partial class ReportErrorDialogBox : Control, IErrorDialogBox
{
    [Export] public BaseButton ReportButton { get; set; }
    [Export] public BaseButton CancelButton { get; set; }
    [Export] private RichTextLabel DiscordInvite { get; set; }
    public Exception Exception { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }

    public override void _Ready()
	{
        ReportButton.Pressed += ReportButton_Pressed;
        CancelButton.Pressed += CancelButton_Pressed;
	}

    private void CancelButton_Pressed()
    {
        GetTree().Paused = false;
        QueueFree();
    }

    private void ReportButton_Pressed()
    {
        Error error = OS.ShellOpen(OtherPaths.DISCORD_INVITE);
        if(error != Error.Ok)
        {
            DiscordInvite.Text = $"Could not open automatically ({error})\nDiscord invite: {OtherPaths.DISCORD_INVITE}\nCopied into your clipboard! ";
            DisplayServer.ClipboardSet($"{OtherPaths.DISCORD_INVITE}");
            DiscordInvite.Visible = true;
        }

        string errorRaport = ErrorReportCollector.GetErrorReport(Exception);
        try
        {
            ErrorReportWriter.WriteReport(errorRaport);
        }
        catch (Exception ex)
        {
            ExceptionHandler.HandleException(ex, false);
        }

        DiscordInvite.Text = $"[center]Thank you for reporting this issue!\nFor detailed steps how to report this please look at our discord!\n\n{DiscordInvite.Text}[center]";
    }
}
