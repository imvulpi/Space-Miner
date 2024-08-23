using System;
using System.Collections.Generic;
using Godot;
using GruInject.API.Attributes;

namespace GruInject.API.Start
{
    // AutoLoad this node or make sure it remains in the current scene. 
    public partial class GruInjectStart : Node
    {
        private GruInject _gruInject;

        public override void _EnterTree()
        {
            _gruInject = new API.GruInject(
                new List<Type>() {typeof(AutoSpawnAttribute)},
                new List<Type>() {typeof(InjectAttribute)});
            _gruInject.Start();//Comment me if comment below is uncommented
            //_gruInject.Start(false, false); //Uncomment me to allow unregistered instances.
            //if you want to track circular dependencies set first parameter in Start to true.
            base._EnterTree();
        }

        public override void _ExitTree()
        {
            _gruInject?.Stop();
            base._ExitTree();
        }
    }
}