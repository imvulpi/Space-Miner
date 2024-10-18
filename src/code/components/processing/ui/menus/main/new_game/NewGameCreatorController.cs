using Godot;
using SpaceMiner.src.code.components.commons.errors.exceptions.handlers;
using SpaceMiner.src.code.components.commons.godot.project_settings.game.world;
using SpaceMiner.src.code.components.experiments.testing.scripts.MenusTest;
using SpaceMiner.src.code.components.processing.data.game.save;
using SpaceMiner.src.code.components.processing.data.settings.game;
using SpaceMiner.src.code.components.processing.data.settings.game.couplers;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using System;

public partial class NewGameCreatorController : Control, IMenuContainer
{
    private readonly GameSaveSettings gameSettings = new();
    private readonly GameSaveHelper gameHelper = new();
    [Export] public LineEdit SaveName { get; set; }
	[Export] public ItemList WorldType { get; set; }
	[Export] public ItemList Difficulties { get; set; }
    [Export] public Button WorldTypeButton { get; set; }
    [Export] public Button DifficultiesButton { get; set; }
	[Export] public Button CreateButton { get; set; }
    public IMenuManager MenuManager { get; set; }
    public IMenu Menu { get; set; }

    public override void _Ready()
	{
        SaveName.TextChanged += SaveName_TextChanged;
        WorldType.ItemSelected += WorldType_ItemSelected;
        Difficulties.ItemSelected += Difficulties_ItemSelected;
        CreateButton.Pressed += CreateButton_Pressed;
	}

    private void CreateButton_Pressed()
    {
        try
        {
            if (IsCreateable()) {
                GameSettingCoupler coupler = new();
                GameSaveManager gameSaveManager = new(gameSettings);
                gameHelper.CreateSave(gameSettings);
                coupler.Save(gameSettings);
                gameSaveManager.Load(GetTree());
                Menu.Close();
            }
        }
        catch(Exception ex)
        {
            ExceptionHandler.HandleException(ex, true);
        }
    }

    private void Difficulties_ItemSelected(long index)
    {
        gameSettings.GameDifficulty = (GameDifficulty)index;
        CreateButton.Disabled = !IsCreateable();
        RenderingServer.CanvasItemSetModulate(DifficultiesButton.GetCanvasItem(), Colors.White);
    }

    private void WorldType_ItemSelected(long index)
    {
        gameSettings.WorldType = (WorldType)index;
        CreateButton.Disabled = !IsCreateable();
        RenderingServer.CanvasItemSetModulate(WorldTypeButton.GetCanvasItem(), Colors.White);
    }

    private void SaveName_TextChanged(string newText)
    {
        if (new GameSaveHelper().CheckSaveNameAvailability(newText))
        {
            RenderingServer.CanvasItemSetModulate(SaveName.GetCanvasItem(), Colors.White);
            gameSettings.SaveName = SaveName.Text;
            CreateButton.Disabled = !IsCreateable();
        }
        else
        {
            RenderingServer.CanvasItemSetModulate(SaveName.GetCanvasItem(), Colors.Red);
            CreateButton.Disabled = true;
        }
    }

    private bool IsCreateable()
    {
        bool nameAvailable = gameHelper.CheckSaveNameAvailability(SaveName.Text);
        if (nameAvailable && WorldType.IsAnythingSelected() && Difficulties.IsAnythingSelected())
        {
            return true;
        }
        else
        {
            if (!nameAvailable)
            {
                RenderingServer.CanvasItemSetModulate(SaveName.GetCanvasItem(), Colors.Red);
            }

            if (!WorldType.IsAnythingSelected())
            {
                RenderingServer.CanvasItemSetModulate(WorldTypeButton.GetCanvasItem(), Colors.Red);
            }

            if (!Difficulties.IsAnythingSelected())
            {
                RenderingServer.CanvasItemSetModulate(DifficultiesButton.GetCanvasItem(), Colors.Red);
            }
        }
        return false;
    }

    public override void _Process(double delta) {}
}
