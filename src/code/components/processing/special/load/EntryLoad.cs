using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.processing.special.load.checkers;

public partial class EntryLoad : Node
{
    [Export] public PackedScene MainMenuScene { get; set; }
    public override void _Ready()
	{
        GD.Print(new PrettyInfo(PrettyInfoType.Broadcast, "Entry", "Entry process started."));

        // Checking
        new EssentialDirectoryChecker().Check();
        new UserSettingChecker().Check();

        // Loading
        CallDeferred("LoadMainMenu");
        GD.Print(new PrettyInfo(PrettyInfoType.Broadcast, "Entry", "Entry process ended."));
        QueueFree();
    }

    private void LoadMainMenu()
    {
        GetTree().Root.AddChild(MainMenuScene.Instantiate());
    }

    public override void _Process(double delta)
	{
	}
}
