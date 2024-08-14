using Godot;
using GruInject.API.Nodes;

namespace GruInject.API.Initializators
{
    public interface IInstanceInitializator
    {
        void InitializeGruInstance(GruNode gruNode);
        void InitializeGruInstance(GruNode2D gruNode);
        void InitializeGruInstance(GruNode3D gruNode);
        void InitializeGruInstance(GruControl gruNode);
        void InitializeNodeInstance<T>(T node) where T : Node;
    }
}