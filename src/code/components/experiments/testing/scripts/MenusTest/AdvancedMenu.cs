using Godot;
using SpaceMiner.src.code.components.experiments.testing.scripts.MenusTest;
using SpaceMiner.src.code.components.processing.ui.menu;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using System;

public partial class AdvancedMenu : Control, IMenuContainer
{
	[Export] public PackedScene MenuScene;
	[Export] public Button OpenMenuButton;
    public IMenuManager MenuManager { get; set; }
    public IMenu Menu { get; set; }

    public override void _Ready()
	{
        OpenMenuButton.Pressed += OpenMenuButton_Pressed; 
	}

    private void OpenMenuButton_Pressed()
    {

        if (MenuManager != null)
        {
            DefaultMenu menu = new DefaultMenu()
            {
                MenuNode = MenuScene.Instantiate(),
                ConnectToNode = GetTree().Root
            };
            MenuManager.RegisterMenu(menu);
            menu.Open(menu.ConnectToNode);
        }
        else
        {
            GD.Print("No menumanager");
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
