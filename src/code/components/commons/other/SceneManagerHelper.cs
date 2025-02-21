using Godot;
using Godot.Collections;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.commons.other
{
    class SceneManagerHelper
    {
        public SceneManager FindSceneManagerMeta(Node inSceneNode)
        {
            Array<Node> nodes = inSceneNode.GetTree().Root.GetChildren();
            foreach(Node node in nodes){
                Variant metaIsManager = node.GetMeta("SceneManager");
                if(metaIsManager.VariantType == Variant.Type.Bool)
                {
                    // Other nodes should not have that meta
                    if(node is SceneManager sceneManager)
                    {
                        return sceneManager;
                    }
                    else
                    {
                        throw new GameException(PrettyErrorType.Invalid, "Node with SceneManager bool metadata is not a SceneManager", "");
                    }
                }
            }
            return null;
        }

        public SceneManager FindSceneManager(Node inSceneNode)
        {
            Array<Node> nodes = inSceneNode.GetTree().Root.GetChildren();
            foreach (Node node in nodes)
            {
                if(node is SceneManager sceneManager)
                {
                    return sceneManager;
                }
            }
            return null;
        }
    }
}
