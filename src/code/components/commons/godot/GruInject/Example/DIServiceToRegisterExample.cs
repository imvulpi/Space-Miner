using Godot;
using GruInject.API.Attributes;

namespace GruInject.Example;

[RegisterTransient]
public class DIServiceToRegisterExample
{
    public DIServiceToRegisterExample()
    {
        GD.Print("AJ. I should be created each time i am requested");
    }
}