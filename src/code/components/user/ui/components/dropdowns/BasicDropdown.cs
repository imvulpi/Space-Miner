using Godot;
using System;

public partial class BasicDropdown : Button
{
    [Export] public ItemList List {  get; set; }
	public override void _Ready()
	{
        Pressed += BasicDropdown_Pressed;
        List.ItemSelected += List_ItemSelected;
    }

    private void List_ItemSelected(long index)
    {
        GD.Print("List Item Selected");
        Text = List.GetItemText((int)index);
        EmitSignal("renamed");
        List.Visible = false;
    }

    private void BasicDropdown_Pressed()
    {
        GD.Print("Pressed");
        if(List.Visible) {
            List.Visible = false;
            return;
        }
        List.Visible = true;
    }
    public override void _Process(double delta)
	{
	}
}
