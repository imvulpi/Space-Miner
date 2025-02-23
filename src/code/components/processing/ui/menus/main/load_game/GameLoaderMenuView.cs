using Godot;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.exceptions.handlers;
using SpaceMiner.src.code.components.processing.data.game.save;
using SpaceMiner.src.code.components.user.ui.components.boards;
using SpaceMiner.src.code.components.user.ui.components.other;
using System;
using System.Collections.Generic;
using SpaceMiner.src.code.components.processing.data.settings.game;
using SpaceMiner.src.code.components.user.ui.components.other.game_save_item;

namespace SpaceMiner.src.code.components.processing.ui.menus.main.load_game
{
    public class GameLoaderMenuView
    {
        public GameLoaderMenuView(Control menu, Control savesWrapper, Control savesHolder, PackedScene saveItemTemplate, PackedScene confirmationMenuTemplate, Func<string[]> getAllSavesPaths)
        {
            Menu = menu;
            SavesWrapper = savesWrapper;
            SavesHolder = savesHolder;
            SaveItemTemplate = saveItemTemplate;
            ConfirmationMenuTemplate = confirmationMenuTemplate;
            GetAllSavesPaths = getAllSavesPaths;
            Initialize();
        }
        public GameLoaderMenuView(Control menu, Control savesWrapper, Control savesHolder, PackedScene saveItemTemplate, PackedScene confirmationMenuTemplate)
        {
            Menu = menu;
            SavesWrapper = savesWrapper;
            SavesHolder = savesHolder;
            SaveItemTemplate = saveItemTemplate;
            ConfirmationMenuTemplate = confirmationMenuTemplate;
        }

        public Control Menu { get; set; }
        public Control SavesWrapper { get; set; }
        public Control SavesHolder { get; set; }
        public PackedScene SaveItemTemplate { get; set; }
        public PackedScene ConfirmationMenuTemplate { get; set; }

        public event EventHandler<IGameSaveItem> LoadSave;
        public event EventHandler<IGameSaveItem> DeleteSave;
        public Func<string[]> GetAllSavesPaths;

        private int saveCount = 0;
        private int cumulativeHeight = 0;
        private readonly List<IGameSaveItem> saveItems = new();
        public void Initialize()
        {
            try
            {
                string[] savesDirsPaths = GetAllSavesPaths?.Invoke();
                CreateAllSaveItems(savesDirsPaths);
            }
            catch (Exception ex)
            {
                GD.Print(ex);
                ExceptionHandler.HandleException(ex, true);
            }
        }

        private void CreateAllSaveItems(string[] savesDirsPaths)
        {
            foreach (string savePath in savesDirsPaths)
            {
                string[] pathSplit = savePath.Split(new char[] { '/', '\\' });
                string saveName = pathSplit[pathSplit.Length - 1];
                CreateSaveItem(saveName, savePath);
            }
        }

        private void CreateSaveItem(string saveName, string savePath)
        {
            IGameSaveItem saveItem = SaveItemTemplate.Instantiate<IGameSaveItem>();
            Control saveNode = saveItem.SaveItemNode;
            saveNode.Name = saveName;
            saveItem.SaveName = saveName;
            saveItem.NameLabel.Text = saveName;
            saveItem.PathLabel.Text = $"./saves/{saveName}";
            saveItem.FullPath = savePath;
            PositioningAddSave(saveItem);
            saveItem.DeleteSaveEvent += DeleteSaveEvent;
            saveItem.LoadSaveEvent += LoadSaveEvent;

            saveCount += 1;
            cumulativeHeight = (int)saveNode.Position.Y;
            PositioningSetHolderHeight();
        }

        private void LoadSaveEvent(object obj)
        {
            Node LoadConfirmationMenu = ConfirmationMenuTemplate.Instantiate();

            if (LoadConfirmationMenu is BinaryDecisionDialogBox confirmationDialog && obj is IGameSaveItem saveObj)
            {
                SetupLoadActions(confirmationDialog, saveObj);
            }
            else
            {
                throw new GameException(PrettyErrorType.Invalid, "LoadConfirmationMenu", "This Menu should be of SaveConfirmationDialog type (Report this)");
            }
        }

        private void SetupLoadActions(BinaryDecisionDialogBox dialog, IGameSaveItem saveItem)
        {
            dialog.SaveName = saveItem.SaveName;
            dialog.Title = "Load save?";
            dialog.Message = $"Do you want to load: {saveItem.NameLabel.Text} Save?";
            Menu.AddChild(dialog);

            dialog.Decision += (bool shouldLoad) =>
            {
                if (shouldLoad) LoadSave?.Invoke(this, saveItem);
                else
                {
                    Menu.RemoveChild(dialog);
                }
            };
        }

        private void DeleteSaveEvent(object obj)
        {
            Node DeleteConfirmationMenu = ConfirmationMenuTemplate.Instantiate();
            if (DeleteConfirmationMenu is BinaryDecisionDialogBox confirmationMenu && obj is IGameSaveItem saveObj)
            {
                SetupDeleteActions(confirmationMenu, saveObj);
            }
            else
            {
                throw new GameException(PrettyErrorType.Invalid, "DeleteConfirmationMenu", "This Menu should be of SaveConfirmationDialog type (Report this)");
            }
        }

        private void SetupDeleteActions(BinaryDecisionDialogBox dialog, IGameSaveItem saveItem)
        {
            dialog.SaveName = saveItem.SaveName;
            dialog.Title = "Delete save?";
            dialog.Message = $"Do you wish to delete: {saveItem.NameLabel.Text} Save?\nThis action can't be reversed";
            Menu.AddChild(dialog);
            dialog.Decision += (bool shouldDelete) =>
            {
                if (shouldDelete)
                {
                    DeleteSave?.Invoke(this, saveItem);
                    try
                    {
                        Menu.RemoveChild(dialog);
                        cumulativeHeight -= (int)saveItem.SaveItemNode.Size.Y;
                        SavesHolder.RemoveChild(saveItem.SaveItemNode);
                        saveItems.Remove(saveItem);
                        RepositionNodes();
                        PositioningSetHolderHeight();
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.HandleException(ex, true);
                    }
                }
                else
                {
                    Menu.RemoveChild(dialog);
                }

            };
        }

        private void PositioningAddSave(IGameSaveItem saveItem)
        {
            Control saveNode = saveItem.SaveItemNode;
            SavesHolder.AddChild(saveNode);
            saveItems.Add(saveItem);
            saveNode.Position = new Vector2(saveNode.Position.X, saveNode.Size.Y * saveCount);
            saveNode.SetSize(new Vector2(SavesHolder.Size.X, saveNode.Size.Y));
        }

        private void RepositionNodes()
        {
            int nodeCount = 0;
            foreach (var saveItem in saveItems)
            {
                Control saveNode = saveItem.SaveItemNode;
                saveNode.Position = new Vector2(saveNode.Position.X, saveNode.Size.Y * nodeCount);
                nodeCount++;
            }
        }

        private void PositioningSetHolderHeight()
        {
            if (cumulativeHeight > SavesHolder.Size.Y)
            {
                SavesWrapper.SetSize(new Vector2(SavesWrapper.Size.X, cumulativeHeight));
                SavesHolder.SetSize(new Vector2(SavesHolder.Size.X, cumulativeHeight));
            }
            else if (SavesHolder.Size.Y > cumulativeHeight)
            {
                SavesWrapper.SetSize(new Vector2(SavesWrapper.Size.X, cumulativeHeight));
                SavesHolder.SetSize(new Vector2(SavesHolder.Size.X, cumulativeHeight));
            }
        }
    }
}
