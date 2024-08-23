using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace SpaceMiner.src.code.components.commons.godot.scenes
{
    public class SceneHelper
    {
        public void ChangeScene(string scenePath, SceneTree sceneTree)
        {
            sceneTree.ChangeSceneToFile(scenePath);
        }
        public void ChangeScene(PackedScene packedScene, SceneTree sceneTree)
        {
            sceneTree.ChangeSceneToPacked(packedScene);
        }
        public Node GetNodeFromScene(string scenePath)
        {
            PackedScene scene = (PackedScene)ResourceLoader.Load(scenePath);
            Node newSceneInstance = scene.Instantiate();
            return newSceneInstance;
        }
    }
}