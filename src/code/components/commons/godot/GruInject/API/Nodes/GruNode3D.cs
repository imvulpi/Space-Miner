﻿using Godot;
using GruInject.API.Initializators;

namespace GruInject.API.Nodes
{
    public partial class GruNode3D : Node3D
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