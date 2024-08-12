using System;
using System.Collections.Generic;
using Godot;
using GruInject.API.Attributes;

namespace GruInject.Start
{
    /// <summary>
    /// Main GruInject node, could be added in autoloads in godot for comfort
    /// </summary>
    public partial class GruInjectStart : Node
    {
        private API.GruInject _gruInject;

        public override void _EnterTree()
        {
            _gruInject = new API.GruInject(
                new List<Type>() {typeof(AutoSpawnAttribute)},
                new List<Type>() {typeof(InjectAttribute)});
            _gruInject.Start();
            base._EnterTree();
        }

        public override void _ExitTree()
        {
            _gruInject?.Stop();
            base._ExitTree();
        }
    }
}