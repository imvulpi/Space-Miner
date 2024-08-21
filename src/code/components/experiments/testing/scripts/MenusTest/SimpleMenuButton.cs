using Godot;
using GruInject.API.Attributes;
using GruInject.API.Initializators;
using SpaceMiner.src.code.components.commons.godot.scenes;
using SpaceMiner.src.code.components.processing.ui.menu;
using System;

public partial class SimpleMenuButton : Button
{
    public MenuManager MenuManager { get; set; }
    [Export] PackedScene SimpleMenu { get; set; }
    public override void _Ready()
	{
        // Gets the MenuManager through the node containing it.
        MenuManager = GetTree().Root.GetNode<MenuControllerTest>("MenuControllerTest").MenuManager;
        Pressed += NextMenuButton_Pressed;
	}

    private void NextMenuButton_Pressed()
    {
        if (MenuManager == null)
        {
            GD.PushError("Menu manager not set");
        }
        else
        {
            DefaultMenu menu = new()
            {
                MenuNode = SimpleMenu.Instantiate(),
                ConnectToNode = GetTree().Root.GetNode("MenuControllerTest")
            };
            MenuManager.RegisterMenu(menu);
            menu.Open(GetTree().Root.GetNode("MenuControllerTest"));
        }
    }
    public override void _Process(double delta){}
}
