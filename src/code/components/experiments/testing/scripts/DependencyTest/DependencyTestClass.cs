using Godot;
using GruInject.API.Attributes;
using System;


/// <summary>
/// Simple class for dependency test of GruInject. The Name value should be 'space'
/// </summary>
[RegisterSingleton]
public class DependencyTestClass
{
    public string Name { get; set; }
    public DependencyTestClass()
    {
        Name = "space";
    }
}
