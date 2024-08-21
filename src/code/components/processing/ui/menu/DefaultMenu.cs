using Godot;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using System;

namespace SpaceMiner.src.code.components.processing.ui.menu
{
    public class DefaultMenu : IMenu
    {
        public Node MenuNode { get; set; }
        public Node ConnectToNode { get; set; }
        public IMenuManager Manager { get; set; }
        public Func<IMenuManager, bool> EscActionDelegate { get; set; }

        public DefaultMenu()
        {
            EscActionDelegate = EscAction;
        }

        public void Close()
        {
            if (Manager != null)
            {
                Manager.UnregisterMenu(this);
                ConnectToNode.RemoveChild(MenuNode);
            }
            else
            {
                GD.PushError("Menu was not registered");
            }
        }

        public void Delete()
        {
            if (Manager != null)
            {
                Manager.UnregisterMenu(this);
            }
            else
            {
                GD.PushError("Menu was not registered");
            }

            ConnectToNode.RemoveChild(MenuNode);
            MenuNode.Free();
        }

        public bool EscAction(IMenuManager manager)
        {
            if (manager != null)
            {
                Close();
                return true;
            }
            return false;
        }

        public void Open(Node connectNode)
        {
            if(Manager != null && connectNode != null)
            {
                connectNode.AddChild(MenuNode);
            }
            else
            {
                GD.PushError("Menu not registered or connect node is null");
            }
        }
    }
}
