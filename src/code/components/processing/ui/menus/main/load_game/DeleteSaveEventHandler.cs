﻿﻿using Godot;
using SpaceMiner.src.code.components.commons.errors.exceptions;
﻿using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.user.ui.components.boards;
using SpaceMiner.src.code.components.user.ui.components.other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceMiner.src.code.components.commons.errors.exceptions.handlers;
using SpaceMiner.src.code.components.processing.data.game.save;

namespace SpaceMiner.src.code.components.processing.ui.menus.main.load_game
{
    public class DeleteSaveEventHandler
    {
        public DeleteSaveEventHandler(PackedScene deleteSaveMenu, Node connectNode)
        {
            DeleteSaveMenu = deleteSaveMenu;
            ConnectNode = connectNode;


        }

        public PackedScene DeleteSaveMenu { get; set; }
        public Node ConnectNode { get; set; }
        public Action<GameSaveItem> deleteSaveItem { get; set; }

        private GameSaveHelper saveHelper = new GameSaveHelper();

        /// <summary>
        /// Checks and handles decision process of save deletion.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Save item to be deleted</returns>
        /// <exception cref="GameException"></exception>
        public GameSaveItem DeleteSaveEvent(object obj)
        {
            Node deleteConfirmationMenu = DeleteSaveMenu.Instantiate();
            if (deleteConfirmationMenu is BinaryDecisionDialogBox confirmationMenu && obj is GameSaveItem saveObj)
            {
                SetupConfirmationDialog(confirmationMenu, saveObj);
                return HandleDecision(confirmationMenu, saveObj);
            }
            else
            {
                GD.Print("err");
                throw new GameException(PrettyErrorType.Invalid, "DeleteConfirmationMenu", "This Menu should be of SaveConfirmationDialog type (Report this)");
            }
        }

        private void SetupConfirmationDialog(BinaryDecisionDialogBox dialog, GameSaveItem saveItem)
        {
            dialog.SaveName = saveItem.NameLabel.Text;
            dialog.Title = "Delete save?";
            dialog.Message = $"Do you wish to delete: {saveItem.NameLabel.Text} Save?\nThis action can't be reversed";
            ConnectNode.AddChild(dialog);
        }

        private GameSaveItem HandleDecision(BinaryDecisionDialogBox dialog, GameSaveItem saveItem)
        {
            bool shouldDelete = false;
            dialog.Decision += (bool decision) =>
            {
                shouldDelete = decision;
            };

            if (shouldDelete)
            {
                try
                {
                    if (shouldDelete)
                    {
                        ConnectNode.RemoveChild(dialog);
                        saveItem.QueueFree();
                        dialog.QueueFree();
                        return saveItem;
                        try
                        {
                            ConnectNode.RemoveChild(dialog);
                            dialog.QueueFree();
                            deleteSaveItem.Invoke(saveItem);
                        }
                        catch (Exception ex)
                        {
                            ExceptionHandler.HandleException(ex, true);
                        }
                    }
                }
                catch (Exception ex) {
                    ExceptionHandler.HandleException(ex, true);
                    ConnectNode.RemoveChild(dialog);
                    dialog.QueueFree();
                }
            }
            else
            {
                ConnectNode.RemoveChild(dialog);
                dialog.QueueFree();
                return null;
                {
                    ConnectNode.RemoveChild(dialog);
                    dialog.QueueFree();
                    return null;
                }
            }
            return null;
        }
    }
}