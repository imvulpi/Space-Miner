using Godot;
using System;

public partial class SpaceshipHUD : Control
{
	[Export] public Label HintLabel { get; set; }
    [Export] public Label LabelRight { get; set; }
    [Export] public Label LabelMiddle { get; set; }
    [Export] public Label CargoCapacity { get; set; }
    [Export] public Label Balance { get; set; }

    public Func<float> GetBalance;
    public Func<float> GetCargoCurrentCapacity;
    public Func<float> GetCargoMaxCapacity;

    public void UpdatesHUD()
    {
        if(GetBalance != null)
        {
            float balance = GetBalance.Invoke();
            Balance.Text = $"{balance}$";
        }
        CargoCapacity.Text = $"{GetCargoCurrentCapacity?.Invoke()}/{GetCargoMaxCapacity?.Invoke()}";
    }

    public void UpdateBalance()
    {
        if (GetBalance != null)
        {
            float balance = GetBalance.Invoke();
            Balance.Text = $"{balance}$";
        }
    }

    public void UpdateCargo()
    {
        CargoCapacity.Text = $"{GetCargoCurrentCapacity?.Invoke()}/{GetCargoMaxCapacity?.Invoke()}";
    }
}