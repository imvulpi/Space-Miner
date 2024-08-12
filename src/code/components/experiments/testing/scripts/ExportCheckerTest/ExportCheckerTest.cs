using Godot;
using SpaceMiner.src.components.commons.godot.exports;
using System;

public partial class ExportCheckerTest : Node
{
	[Export]
	public string PlayerName { get; set; }
	[Export]
	public int Level { get; set; }
	[Export]
	public Node Node { get; set; }
	public override void _Ready()
	{
		ExportChecker checker = new ExportChecker();
		checker.CheckNotSetExportsNoSource(Node, PlayerName, Level);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
