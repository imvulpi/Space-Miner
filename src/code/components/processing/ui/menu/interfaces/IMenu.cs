using Godot;
using System;

namespace SpaceMiner.src.code.components.processing.ui.menu.interfaces
{
    public interface IMenu
    {
        void Open(Node connectNode);
        void Close();
        void Delete();
        bool EscAction(IMenuManager manager);
        Node MenuNode { get; set; }
        Node ConnectToNode { get; set; }
        Func<IMenuManager, bool> EscActionDelegate { get; set; }
        IMenuManager Manager { get; set; }
        // Menu controller
    }
}