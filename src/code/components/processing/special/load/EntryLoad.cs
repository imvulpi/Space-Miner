using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.processing.special.load.checkers;

public partial class EntryLoad : Node
{
    public override void _Ready()
	{
        GD.Print(new PrettyInfo(PrettyInfoType.Broadcast, "Entry", "Entry process started."));
        new UserSettingChecker().Check();

		GD.Print(new PrettyInfo(PrettyInfoType.Broadcast, "Entry", "Entry process ended."));
    }

    public override void _Process(double delta)
	{
	}
}
