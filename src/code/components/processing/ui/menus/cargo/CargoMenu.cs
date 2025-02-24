using Godot;
using Godot.Collections;
using SpaceMiner.src.code.components.user.entities.spaceships;
using System;
using System.Collections.Generic;

public partial class CargoMenu : Control
{
	public List<Cargo> CargoList;
	[Export] public Control ConnectNode { get; set; }
	[Export] public CargoDataRow DataRow { get; set; }
	[Export] public Label MiddleLabel { get; set; }
	public event EventHandler CloseMenu;
	private int RowCount = 0;
	public void Initialize()
	{
		Clear();

        if (CargoList.Count <= 0)
		{
			MiddleLabel.Text = "No cargo!";
			MiddleLabel.Visible = true;
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
	private void Clear()
	{
		RowCount = 0;
		MiddleLabel.Visible = false;
		Array<Node> children = ConnectNode.GetChildren();
		foreach (var child in children)
		{
			if (child != DataRow)
			{
				ConnectNode.RemoveChild(child);
			}
		}
	}
}
