using Godot;
using SpaceMiner.src.code.components.experiments.testing.scripts.MenusTest;
using SpaceMiner.src.code.components.processing.ui.menu;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using System;

public partial class MainAdvancedMenuExample : Control, IMenuContainer
{
    public IMenuManager MenuManager { get; set; }
    public IMenu Menu { get; set; }
	[Export] public Button GraphicsButton {  get; set; }
	[Export] public Button AudioButton { get; set; }
	[Export] public Button MiscButton {  get; set; }
    [Export] public PackedScene GraphicsScene { get; set; }
    [Export] public PackedScene AudioScene { get; set; }
    [Export] public PackedScene MiscScene { get; set; }
    public override void _Ready()
	{
        GraphicsButton.Pressed += GraphicsButton_Pressed;
        AudioButton.Pressed += AudioButton_Pressed;
        MiscButton.Pressed += MiscButton_Pressed;
	}

    private void MiscButton_Pressed()
    {
        SetMenu(MiscScene);
    }

    private void AudioButton_Pressed()
    {
        SetMenu(AudioScene);
    }

    private void GraphicsButton_Pressed()
    {
        SetMenu(GraphicsScene);
    }

    private void SetMenu(PackedScene scene)
    {
        DefaultMenu menu = GetMenu(scene);
        if(menu.MenuNode is IMenuContainer menuContainer)
        {
            menuContainer.Menu = menu;
            menuContainer.MenuManager = MenuManager;
        }
        MenuManager.RegisterMenu(menu);
        menu.Open(menu.ConnectToNode);
    }

    private DefaultMenu GetMenu(PackedScene scene)
    {
        DefaultMenu menu = new DefaultMenu()
        {
            MenuNode = scene.Instantiate(),
            ConnectToNode = GetTree().Root,
        };
        return menu;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
