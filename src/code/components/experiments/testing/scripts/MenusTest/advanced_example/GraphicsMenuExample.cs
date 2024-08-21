using Godot;
using SpaceMiner.src.code.components.experiments.testing.scripts.MenusTest;
using SpaceMiner.src.code.components.processing.ui.menu;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using System;

public partial class GraphicsMenuExample : Control, IMenuContainer
{
    public IMenuManager MenuManager { get; set; }
    public IMenu Menu { get; set; }
	[Export] public Button SpecialButton { get; set; }
    [Export] public PackedScene MenuScene { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() 
	{
        SpecialButton.Pressed += SpecialButton_Pressed;
	}

    private void SpecialButton_Pressed()
    {
        DefaultMenu defaultMenu = new DefaultMenu()
        {
            MenuNode = MenuScene.Instantiate(),
            ConnectToNode = GetTree().Root
        };
        MenuManager.RegisterMenu(defaultMenu);
        defaultMenu.Open(defaultMenu.ConnectToNode);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
