using Godot;
using Hardware.Info;
using SpaceMiner.src.code.components.user.blocks.structures.orbital_materials;
using SpaceMiner.src.code.components.user.blocks.structures.stellaf_forge;
using SpaceMiner.src.code.components.user.entities.spaceships;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class StellarShopMenu : Control
{
    [Export] public StellarDataRow Row { get; set; }
	[Export] public Control ConnectNode { get; set; }
    [Export] public Label MiddleLabel { get; set; }
	public Dictionary<string, float> Products { get; set; } 
    public Func<string, int> GetAmountOwned { get; set; }
    public Action<string, int> PurchaseConfirmed;
    private int rowNumber = 0;
    public override void _Ready()
	{
        if (Products != null && Products.Count > 0)
        {
            foreach ((string type, float price) in Products)
            {
                StellarDataRow newRow = Row.Duplicate() as StellarDataRow;
                newRow.ItemLabel.Text = type.Replace("_", " ");
                newRow.BuyPrice.Text = $"{price}$";
                newRow.UserOwns.Text = $"{GetAmountOwned(type)}";
                newRow.BuyButton.Pressed += () =>
                {
                     PurchaseConfirmed.Invoke(type, 1);
                    newRow.UserOwns.Text = $"{GetAmountOwned(type)}";
                };
                newRow.Visible = true;
                newRow.Position = new Vector2(newRow.Position.X, 10 + rowNumber * newRow.Size.Y);
                rowNumber++;
                ConnectNode.AddChild(newRow);
            }
        }
        else
        {
            ShopDataRow newRow = Row.Duplicate() as ShopDataRow;
            newRow.ItemLabel.Text = $"Out of stock sorry!";
            ConnectNode.AddChild(newRow);
        }
    }
    public void ShowError(string message)
    {
        MiddleLabel.Text = message;
        MiddleLabel.Visible = true;
        Timer time = new()
        {
            WaitTime = 1,
            Autostart = true,
            OneShot = true
        };
        AddChild(time);
        time.Timeout += () =>
        {
            MiddleLabel.Visible = false;
            MiddleLabel.Text = "";
            RemoveChild(time);
            time.Dispose();
        };
    }
}
