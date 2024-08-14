using Godot;
using GruInject.API.Attributes;

namespace GruInject.Example;

[AutoSpawn]
public class DIAutoSpawnExample
{
    public DIAutoSpawnExample()
    {
        GD.Print("MyService i was auto spawned!");
    }
}