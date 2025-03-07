using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.errors.logging;
using SpaceMiner.src.code.components.processing.special.load.checkers;
using SpaceMiner.src.code.components.processing.special.load.other;
using System.Reflection.Metadata.Ecma335;

public partial class EntryLoad : Node
{
    [Export] public PackedScene MainMenuScene { get; set; }
    [Export] public SceneManager SceneManager { get; set; }
    public override void _Ready()
	{
        // Very early loading
        Logger.Init();
        Logger.Log(PrettyInfoType.GeneralInfo, "Entry", "Entry process started.");
        TemporaryCleaner.ClearTemp();
        Checking();
        Loading();

        Logger.Log(PrettyInfoType.GeneralInfo, "Entry", "Entry process ended.");
    }

    private void Checking()
    {
        new EssentialDirectoryChecker().Check();
        new UserSettingChecker().Check();
    }

    private void Loading()
    {
        CallDeferred("LoadMainMenu");
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(MainMenuScene);
        SceneManager.RemoveChild(this);
        QueueFree();
    }

    public override void _Process(double delta)
	{
	}
}
