using Godot;
using SpaceMiner.src.code.components.user.entities.spaceships;
using System;
using System.Collections.Generic;

public partial class CargoMenu : Control
{
	public List<Cargo> CargoList;
	[Export] public Control ConnectNode { get; set; }
	[Export] public CargoDataRow DataRow { get; set; }
	[Export] public Button Back { get; set; }
	public event EventHandler CloseMenu;
	private int RowCount = 0;
	public void Initialize()
	{
		Back.Pressed += () =>
		{
			CloseMenu?.Invoke(this, EventArgs.Empty);
		};

		if (CargoList.Count <= 0)
		{
			CargoDataRow newDataRow = DataRow.Duplicate() as CargoDataRow;
			newDataRow.Position = new Vector2(newDataRow.Position.X, newDataRow.Size.Y);
			newDataRow.NameLabel.Text = "NO DATA";
			newDataRow.AmountLabel.Text = "";
			newDataRow.WeightLabel.Text = "";
			newDataRow.Visible = true;
			ConnectNode.AddChild(newDataRow);
		}
		else
		{
			foreach (Cargo cargo in CargoList)
			{
				RowCount++;
				CargoDataRow newDataRow = DataRow.Duplicate() as CargoDataRow;
				newDataRow.Position = new Vector2(newDataRow.Position.X, newDataRow.Size.Y * RowCount);
				newDataRow.NameLabel.Text = $"{cargo.CargoType.ToString()}";
				newDataRow.AmountLabel.Text = $"Amount: {cargo.Amount}";
				newDataRow.WeightLabel.Text = $"Weight: {cargo.Weight}";
				newDataRow.Visible = true;
				ConnectNode.AddChild(newDataRow);
			}
		}
	}

    public override void _PhysicsProcess(double delta)
    {
		if (Input.IsActionJustPressed("Esc"))
		{
			CloseMenu?.Invoke(this, EventArgs.Empty);
		}
    }
}
