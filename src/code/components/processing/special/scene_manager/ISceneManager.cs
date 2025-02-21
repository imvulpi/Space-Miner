using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.special.scene_manager
{
    public interface ISceneManager
    {
        public IList<SubScene> ScenesRoot { get; set; }
        public Node LoadScene(PackedScene scenePacked);
        public void UnloadScenes(PackedScene scenePacked);
        public Node LoadScene(Node sceneNode);
        public void UnloadScenes(Node sceneNode);
        public Node LoadScene(string scenePath);
        public void UnloadScenes(string scenePath);
        public Node LoadScene(SubScene subScene);
        public void UnloadScene(SubScene subScene);
        public Node SwitchScene(PackedScene scenePacked);
        public Node SwitchScene(Node sceneNode);
        public Node SwitchScene(SubScene subScene);
    }
}
