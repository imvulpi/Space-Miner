using Godot;
using SpaceMiner.src.code.components.commons.godot.scenes;
using SpaceMiner.src.code.components.experiments.testing.scripts.MenusTest;
using SpaceMiner.src.code.components.processing.ui.menu;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using System;

public partial class MainMenuNodeController : Node2D, IMenuInject
{
	[Export] public Button ResumeButton { get; set; }
	[Export] public Button SettingsButton { get; set; }
	[Export] public Button SaveButton { get; set; }
	[Export] public Button SaveAndQuitButton { get; set; }
    [Export] public PackedScene SettingsScene { get; set; }
    public IMenuManager MenuManager { get; set; }
    public IMenu Menu { get; set; }

    public override void _Ready()
	{
        ResumeButton.Pressed += ResumeButton_Pressed;
        SettingsButton.Pressed += SettingsButton_Pressed;
        SaveButton.Pressed += SaveButton_Pressed;
        SaveAndQuitButton.Pressed += SaveAndQuitButton_Pressed;
	}

    private void SaveAndQuitButton_Pressed()
    {
        // TODO: Implement saving
        GetTree().Quit();
    }

    private void SaveButton_Pressed()
    {
        // TODO: Implement saving
    }

    private void SettingsButton_Pressed()
    {
        Menu settingMenu = new Menu()
        {
            ConnectToNode = Menu.ConnectToNode,
            MenuNode = SettingsScene.Instantiate(),
        };
        MenuManager.ConnectMenu(settingMenu);
        if(settingMenu.MenuNode is SpaceMiner.src.code.components.experiments.testing.scripts.MenusTest.IMenuInject container)
        {
            container.Menu = settingMenu;
            container.MenuManager = MenuManager;
        }
        settingMenu.Open();
    }

    private void ResumeButton_Pressed()
    {
        Menu.Close();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
