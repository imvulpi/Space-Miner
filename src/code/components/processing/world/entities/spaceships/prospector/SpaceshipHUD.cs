using Godot;
using System;

public partial class SpaceshipHUD : CanvasLayer
{
	[Export] public Label HintLabel { get; set; }
    [Export] public Label LabelRight { get; set; }
    [Export] public Label CargoCapacity { get; set; }
    [Export] public Label Balance { get; set; }
    [Export] public Button CargoButton { get; set; }
}
