using Godot;
using SpaceMiner.src.code.components.experiments.testing.scripts.MenusTest;
using SpaceMiner.src.code.components.processing.ui.menu;
using System;

public partial class PassingMenuButton : Button
{
    public MenuManager MenuManager { get; set; }
    [Export] PackedScene MenuContainerScene { get; set; }
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
            Node menuNode = MenuContainerScene.Instantiate();
            IMenuContainer container = (IMenuContainer)menuNode;

            DefaultMenu menu = new()
            {
                MenuNode = menuNode,
                ConnectToNode = GetTree().Root.GetNode("MenuControllerTest")
            };
            container.Menu = menu;
            container.MenuManager = MenuManager;
            MenuManager.RegisterMenu(menu);
            menu.Open(GetTree().Root.GetNode("MenuControllerTest"));
        }
    }
    public override void _Process(double delta) { }
}
