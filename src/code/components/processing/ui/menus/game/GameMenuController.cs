    using Godot;
using SpaceMiner.src.code.components.experiments.testing.scripts.MenusTest;
using SpaceMiner.src.code.components.processing.ui.menu;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;

public partial class GameMenuController : Node2D, IMenuContainer
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
        DefaultMenu settingMenu = new()
        {
            ConnectToNode = Menu.ConnectToNode,
            MenuNode = SettingsScene.Instantiate(),
        };

        MenuManager.RegisterMenu(settingMenu);
        if(settingMenu.MenuNode is IMenuContainer container)
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
}
