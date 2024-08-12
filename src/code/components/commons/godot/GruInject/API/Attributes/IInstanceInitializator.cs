using Godot;
using GruInject.API.Nodes;
using System;

namespace GruInject.API.Attributes
{
    public interface IInstanceInitializator
    {
        void InitializeGruInstance(GruNode gruNode);
        void InitializeGruInstance(GruNode2D gruNode);
        void InitializeGruInstance(GruNode3D gruNode);
        void InitializeInstance<T>(T node) where T : Node;
    }
}