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
                GD.PushError("Menu was not registered in a MenuManager | can't Close() ");
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
                GD.PushError("Menu was not registered in a MenuManager | can't Delete()");
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
                if(Manager == null)
                {
                    GD.PushError("Menu was not registered in a MenuManager | can't Open()");
                }
                else
                {
                    GD.PushError("Menu connecting node was not provided | can't Open()");
                }
            }
        }
    }
}
