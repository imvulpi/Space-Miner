using Godot;
using System;

public partial class CargoDataRow : Control
{
	[Export] public Label NameLabel { get; set; }
	[Export] public Label AmountLabel { get; set; }
	[Export] public Label WeightLabel { get; set; }
}
