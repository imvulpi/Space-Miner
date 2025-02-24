using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.logging;
using SpaceMiner.src.code.components.experiments.testing.scripts.MenusTest;
using SpaceMiner.src.code.components.processing.data.game.save;
using SpaceMiner.src.code.components.processing.data.settings.game;
using SpaceMiner.src.code.components.processing.ui.menu;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using SpaceMiner.src.code.components.processing.ui.menus._base.menu_manager_pro;
using System;

public partial class MainMenuController : Control, IMenuInject
{
    public IMenuManager MenuManager { get; set; }
    public IMenu Menu { get; set; }
    [Export] public Button PlayButton {  get; set; }
    [Export] public PackedScene GameLoaderMenuScene { get; set; }
    [Export] public Button ResumeButton { get; set; }
    [Export] public Label LastPlayed { get; set; }
    [Export] public Button NewGameButton { get; set; }
    [Export] public PackedScene NewSaveMenuScene { get; set; }
    [Export] public Button QuitButton { get; set; }
    private IMenu NewSaveMenu { get; set; }
    private IMenu GameLoaderMenu { get; set; }
    public override void _EnterTree()
    {
        MenuManager = new MenuManager()
        {
            ConnectNode = this
        };
        Node newMenuNode = NewSaveMenuScene.Instantiate();
        Menu newSaveMenu = new Menu()
        {
            ConnectToNode = this,
            MenuNode = newMenuNode,
        };
        NewSaveMenu = newSaveMenu;
        MenuManager.ConnectMenu(newSaveMenu);

        Menu gameLoaderMenu = new Menu()
        {
            ConnectToNode = this,
            MenuNode = GameLoaderMenuScene.Instantiate()
        };
        GameLoaderMenu = gameLoaderMenu;
        MenuManager.ConnectMenu(gameLoaderMenu);
    }

    public override void _Ready()
	{
        PlayButton.Pressed += PlayButton_Pressed;
        ResumeButton.Pressed += ResumeButton_Pressed;
        NewGameButton.Pressed += NewGameButton_Pressed;
        QuitButton.Pressed += QuitButton_Pressed;

        string latestSave = new GameSaveHelper().GetLatestSaveName();
        LastPlayed.Text = latestSave;
    }

    private void QuitButton_Pressed()
    {
        GetTree().Quit();
    }

    private void NewGameButton_Pressed()
    {
        NewSaveMenu.Open();
    }

    private void ResumeButton_Pressed()
    {
        string latestSave = new GameSaveHelper().GetLatestSaveName();
        if (latestSave != "")
        {
            GameSaveSettings settings = new GameSaveSettings()
            {
                SaveName = latestSave,
            };

            new GameSaveLoader(settings).Load(GetTree());
        }
        else
        {
            Logger.Log(PrettyInfoType.GeneralInfo, "LatestSave", "No latest save found.");
        }
    }

    private void PlayButton_Pressed()
    {
        GameLoaderMenu.Open();
    }

    public override void _Process(double delta)
	{
        if (Input.IsActionJustPressed("Esc"))
        {
            MenuManager.CurrentMenu?.Close();
        }
    }
}
