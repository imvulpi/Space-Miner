using Godot;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.user.ui.components.boards;
using SpaceMiner.src.code.components.user.ui.components.other.game_save_item;
using System;
using SpaceMiner.src.code.components.commons.errors.exceptions.handlers;
using SpaceMiner.src.code.components.processing.data.game.save;
using SpaceMiner.src.code.components.processing.data.settings.game;

namespace SpaceMiner.src.code.components.processing.ui.menus.main.load_game
{
    public class LoadSaveEventHandler
    {
        public LoadSaveEventHandler(PackedScene loadConfirmationMenu, Node connectNode) {
            LoadConfirmationMenu = loadConfirmationMenu;
            ConnectNode = connectNode;
        }

        public PackedScene LoadConfirmationMenu { get; set; }
        public Node ConnectNode { get; set; }

        public void LoadSaveEvent(object obj)
        {
            Node loadConfirmationMenu = LoadConfirmationMenu.Instantiate();

            if (loadConfirmationMenu is BinaryDecisionDialogBox confirmationDialog && obj is IGameSaveItem saveObj)
            {
                SetupConfirmationDialog(confirmationDialog, saveObj);
                HandleDecision(confirmationDialog, saveObj);
            }
            else
            {
                throw new GameException(PrettyErrorType.Invalid, "LoadConfirmationMenu", "This Menu should be of SaveConfirmationDialog type and object of IGameSaveItem (Report this)");
            }
        }

        private void SetupConfirmationDialog(BinaryDecisionDialogBox dialog, IGameSaveItem saveItem)
        {
            dialog.SaveName = saveItem.NameLabel.Text;
            dialog.Title = "Load save?";
            dialog.Message = $"Do you want to load: {saveItem.NameLabel.Text} Save?";
            ConnectNode.AddChild(dialog);
        }

        private void HandleDecision(BinaryDecisionDialogBox dialog, IGameSaveItem saveItem)
        {
            dialog.Decision += (bool shouldLoad) =>
            {
                if (shouldLoad)
                {
                    GameSaveSettings settings = new GameSaveSettings()
                    {
                        SaveName = saveItem.NameLabel.Text,
                    };

                    try
                    {
                        GameSaveManager gameSaveManager = new GameSaveManager(settings);
                        gameSaveManager.Load(ConnectNode.GetTree());
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.HandleException(ex, true);
                    }
                }
                else
                {
                    ConnectNode.RemoveChild(dialog);
                    dialog.QueueFree();
                }
            };
        }
    }
}
