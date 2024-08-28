using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.experiments.testing.scripts.MenusTest;
using SpaceMiner.src.code.components.processing.ui.menu;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using System;

public partial class MainMenuController : Control, IMenuContainer
{
    public IMenuManager MenuManager { get; set; }
    public IMenu Menu { get; set; }
    [Export] public Button PlayButton {  get; set; }
    [Export] public PackedScene GameLoaderMenu { get; set; }
    [Export] public Button ResumeButton { get; set; }
    [Export] public Button NewGameButton { get; set; }
    [Export] public PackedScene NewSaveMenu { get; set; }
    [Export] public Button QuitButton { get; set; }
    public override void _EnterTree()
    {
        MenuManager = new MenuManager();
        MenuManager.MainMenuAction = (IMenuManager manager) => {}; // Can later ask whether player wants to quit.
    }

    public override void _Ready()
	{
        PlayButton.Pressed += PlayButton_Pressed;
        ResumeButton.Pressed += ResumeButton_Pressed;
        NewGameButton.Pressed += NewGameButton_Pressed;
        QuitButton.Pressed += QuitButton_Pressed;
	}

    private void QuitButton_Pressed()
    {
        GetTree().Quit();
    }

    private void NewGameButton_Pressed()
    {
        Node newMenuNode = NewSaveMenu.Instantiate();
        DefaultMenu menu = new DefaultMenu()
        {
            ConnectToNode = this,
            MenuNode = newMenuNode,
        };
        if(newMenuNode is IMenuContainer container)
        {
            container.Menu = menu;
            container.MenuManager = MenuManager;
        }
        MenuManager.RegisterMenu(menu);
        menu.Open();
    }

    private void ResumeButton_Pressed()
    {
        // TODO: Load most recently played save
        throw new NotImplementedException();
    }

    private void PlayButton_Pressed()
    {
        DefaultMenu menu = new DefaultMenu()
        {
            ConnectToNode = this,
            MenuNode = GameLoaderMenu.Instantiate()
        };
        MenuManager.RegisterMenu(menu);
        menu.Open();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
        MenuManager.ManageMenus();
    }
}
