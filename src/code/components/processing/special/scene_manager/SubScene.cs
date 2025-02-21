using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.special.scene_manager
{
    public class SubScene
    {
        public SubScene(Node node)
        {
            Node = node;
        }
        public SubScene(Node node, string resourcePath)
        {
            Node = node;
            ResourcePath = resourcePath;
        }
        public Node Node { get; set; }
        public string ResourcePath { get; set; } = null;
    }
}
