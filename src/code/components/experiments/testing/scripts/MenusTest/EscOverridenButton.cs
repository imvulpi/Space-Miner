using Godot;
using SpaceMiner.src.code.components.processing.ui.menu;
using System;

public partial class EscOverridenButton : Button
{
    public MenuManager MenuManager { get; set; }
    public DefaultMenu Menu { get; set; }
    [Export] public PackedScene EscOverridenMenu { get; set; }
    public override void _Ready()
    {
        MenuManager = GetTree().Root.GetNode<MenuControllerTest>("MenuControllerTest").MenuManager;
        Pressed += EscOverridenButton_Pressed;
        Menu = new DefaultMenu()
        {
            MenuNode = EscOverridenMenu.Instantiate(),
            ConnectToNode = GetTree().Root,
        };

        EscOverridenControl escControl = (EscOverridenControl)Menu.MenuNode;
        escControl.menu = Menu;
    }

    private void EscOverridenButton_Pressed()
    {
        MenuManager.RegisterMenu(Menu);
        Menu.Open(Menu.ConnectToNode);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
