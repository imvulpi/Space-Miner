using Godot;
using SpaceMiner.src.code.components.user.blocks.structures.orbital_materials;
using SpaceMiner.src.code.components.user.entities.spaceships;
using System;
using System.Collections.Generic;

public partial class ShopMenu : Control
{
	public Dictionary<CargoType, int> Prices { get; set; }
	[Export] public Button Back { get; set; }
	[Export] public ShopDataRow Row { get; set; }
	[Export] public Control ConnectNode { get; set; }
	public Func<CargoType, int> GetUserCargo;
	public Action<CargoType, int> SellConfirmed;
	public EventHandler CloseMenu;
	public int rowNumber = 0;
	public override void _Ready()
	{
		foreach((CargoType type, int price) in Prices)
		{
			ShopDataRow newRow = Row.Duplicate() as ShopDataRow;
			newRow.ItemLabel.Text = type.ToString();
			newRow.SellPrice.Text = $"{price}$";
			newRow.UserOwns.Text = $"{GetUserCargo(type)}";
			newRow.AmountAll.Pressed += () => { newRow.AmountInput.Text = $"{GetUserCargo(type)}"; };
            newRow.SellButton.Pressed += () =>
			{
				if (int.TryParse(newRow.AmountInput.Text, out int amountInt))
				{
                    SellConfirmed.Invoke(type, amountInt);
				}
				else
				{
					newRow.AmountAll.Text = $"Bad input {newRow.AmountInput.Text}";
				}
			};
			newRow.Visible = true;
			newRow.Position = new Vector2(newRow.Position.X, 10 + rowNumber * newRow.Size.Y);
			rowNumber++;
			ConnectNode.AddChild(newRow);
		}
		Back.Pressed += () =>
		{
			CloseMenu?.Invoke(this, EventArgs.Empty);
		};
	}
}
