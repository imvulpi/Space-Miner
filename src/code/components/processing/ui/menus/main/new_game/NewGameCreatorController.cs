using Godot;
using SpaceMiner.src.code.components.commons.errors.exceptions.handlers;
using SpaceMiner.src.code.components.commons.godot.project_settings.game.world;
using SpaceMiner.src.code.components.experiments.testing.scripts.MenusTest;
using SpaceMiner.src.code.components.processing.data.game.save;
using SpaceMiner.src.code.components.processing.data.settings.game;
using SpaceMiner.src.code.components.processing.data.settings.game.couplers;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using System;

public partial class NewGameCreatorController : Control, IMenuInject
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
        SaveName.TextChanged += SaveName_TextChanged; ;
        WorldType.ItemSelected += WorldType_ItemSelected;
        Difficulties.ItemSelected += Difficulties_ItemSelected;
        CreateButton.Pressed += CreateButton_Pressed;
	}

    private void CreateButton_Pressed()
    {
        try
        {
            if (!ShouldCreateBeDisabled()) {
                GameSettingCoupler coupler = new GameSettingCoupler();
                GameSaveLoader gameSaveManager = new GameSaveLoader(gameSettings);
                gameHelper.CreateSave(gameSettings);
                coupler.Save(gameSettings);
                gameSaveManager.Load(GetTree());
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
        CreateButton.Disabled = ShouldCreateBeDisabled();
        RenderingServer.CanvasItemSetModulate(DifficultiesButton.GetCanvasItem(), new Color("#ffffff"));
    }

    private void WorldType_ItemSelected(long index)
    {
        gameSettings.WorldType = (WorldType)index;
        CreateButton.Disabled = ShouldCreateBeDisabled();
        RenderingServer.CanvasItemSetModulate(WorldTypeButton.GetCanvasItem(), new Color("#ffffff"));
    }

    private void SaveName_TextChanged(string newText)
    {
        if (new GameSaveHelper().CheckSaveNameAvailability(newText))
        {
            RenderingServer.CanvasItemSetModulate(SaveName.GetCanvasItem(), new Color("#ffffff"));
            gameSettings.SaveName = SaveName.Text;
            CreateButton.Disabled = ShouldCreateBeDisabled();
        }
        else
        {
            RenderingServer.CanvasItemSetModulate(SaveName.GetCanvasItem(), new Color("#ff7081")) ;
            CreateButton.Disabled = true;
        }
    }

    private bool ShouldCreateBeDisabled()
    {
        bool nameAvailable = gameHelper.CheckSaveNameAvailability(SaveName.Text);
        if (nameAvailable && WorldType.IsAnythingSelected() && Difficulties.IsAnythingSelected())
        {
            return false;
        }
        else
        {
            if (!nameAvailable)
            {
                RenderingServer.CanvasItemSetModulate(SaveName.GetCanvasItem(), new Color("#ff7081"));
            }

            if (!WorldType.IsAnythingSelected())
            {
                RenderingServer.CanvasItemSetModulate(WorldTypeButton.GetCanvasItem(), new Color("#ff7081"));
            }

            if (!Difficulties.IsAnythingSelected())
            {
                RenderingServer.CanvasItemSetModulate(DifficultiesButton.GetCanvasItem(), new Color("#ff7081"));
            }
        }
        return true;
    }

    public override void _Process(double delta) {}
}
