using Godot;
using Godot.Collections;
using SpaceMiner.src.code.components.processing.special.scene_manager;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class SceneManager : Node, ISceneManager
{
    public IList<SubScene> ScenesRoot { get; set; } = new List<SubScene>();
    public event EventHandler<SubScene> SceneLoaded;
    public event EventHandler<SubScene> SceneUnloaded;

    public Node LoadScene(PackedScene scenePacked)
    {
        Node sceneNode = scenePacked.Instantiate();
        SubScene subScene = new(sceneNode, scenePacked.ResourcePath);
        AddChild(subScene.Node);
        ScenesRoot.Add(subScene);
        SceneLoaded?.Invoke(this, subScene);
        return sceneNode;
    }

    public Node LoadScene(Node sceneNode)
    {
        if (sceneNode == null) return null;
        SubScene subScene = new(sceneNode);
        AddChild(subScene.Node);
        ScenesRoot.Add(subScene);
        SceneLoaded?.Invoke(this, subScene);
        return sceneNode;
    }

    public Node LoadScene(string scenePath)
    {
        Node node = ResourceLoader.Load<PackedScene>(scenePath).Instantiate();
        SubScene subScene = new(node, scenePath);
        AddChild(subScene.Node);
        ScenesRoot.Add(subScene);
        SceneLoaded?.Invoke(this, subScene);
        return subScene.Node;
    }

    public Node LoadScene(SubScene subScene)
    {
        AddChild(subScene.Node);
        ScenesRoot.Add(subScene);
        SceneLoaded?.Invoke(this, subScene);
        return subScene.Node;
    }

    public Node SwitchScene(PackedScene scenePacked)
    {
        Node node = scenePacked.Instantiate();
        if (node != null)
        {
            SubScene subScene = new(node, scenePacked.ResourcePath);
            return SwitchScene(subScene);
        }
        return null;
    }

    public Node SwitchScene(Node sceneNode)
    {
        if (sceneNode != null)
        {
            SubScene subScene = new(sceneNode);
            return SwitchScene(subScene);
        }
        return null;
    }

    public Node SwitchScene(SubScene subScene)
    {
        if(subScene != null && subScene.Node != null)
        {
            RemoveAllScenes();
            LoadScene(subScene);
            return subScene.Node;
        }
        return null;
    }

    public void UnloadScene(SubScene subScene)
    {
        RemoveChild(subScene.Node);
        ScenesRoot.Remove(subScene);
        SceneUnloaded?.Invoke(this, subScene);
    }

    public void UnloadScenes(PackedScene scenePacked)
    {
        for (int i = 0; i < ScenesRoot.Count; i++)
        {
            SubScene subScene = ScenesRoot[i];
            if (subScene.ResourcePath == scenePacked.ResourcePath)
            {
                RemoveChild(subScene.Node);
                ScenesRoot.Remove(subScene);
                SceneUnloaded?.Invoke(this, subScene);
                i++;
            }
        }
    }

    public void UnloadScenes(Node sceneNode)
    {
        if (sceneNode == null) return;
        for (int i = 0; i < ScenesRoot.Count; i++)
        {
            SubScene subScene = ScenesRoot[i];
            if (subScene.Node == sceneNode)
            {
                RemoveChild(subScene.Node);
                ScenesRoot.Remove(subScene);
                SceneUnloaded?.Invoke(this, subScene);
                i--;
            }
        }
    }

    public void UnloadScenes(string scenePath)
    {
        for (int i = 0; i < ScenesRoot.Count; i++)
        {
            SubScene subScene = ScenesRoot[i];
            if (subScene.ResourcePath == scenePath)
            {
                RemoveChild(subScene.Node);
                ScenesRoot.Remove(subScene);
                SceneUnloaded?.Invoke(this, subScene);
                i--;
            }
        }
    }

    private void RemoveAllScenes()
    {
        ScenesRoot.Clear();
        Array<Node> children = GetChildren();
        foreach(var child in children)
        {
            RemoveChild(child);
        }
    }
}
