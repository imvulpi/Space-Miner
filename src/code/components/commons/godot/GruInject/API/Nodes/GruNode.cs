using Godot;
using GruInject.API.Attributes;

namespace GruInject.API.Nodes
{
    public partial class GruNode: Node
    {
        private bool _wasInitialized = false;
        public override void _EnterTree()
        {
            if (!_wasInitialized)
            {
                _wasInitialized = true;
                InstanceInitializator.CurrentInstanceInitializator.InitializeGruInstance(this);
            }
            base._EnterTree();
        }
    }
}