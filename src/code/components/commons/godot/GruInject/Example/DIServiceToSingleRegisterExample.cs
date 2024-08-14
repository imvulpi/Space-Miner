using Godot;
using GruInject.API.Attributes;

namespace GruInject.Example;

[RegisterSingleton]
public class DIServiceToSingleRegisterExample
{
    public DIServiceToSingleRegisterExample()
    {
        GD.Print("AJ. I should be created only once");
    }
}