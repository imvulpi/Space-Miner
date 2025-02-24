using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.ui.hud
{
    public interface IHudController
    {
        public CanvasLayer HUDCanvasLayer { get; set; }
        public void Initialize();
        public void Display();
        public void Clear();
    }
}
