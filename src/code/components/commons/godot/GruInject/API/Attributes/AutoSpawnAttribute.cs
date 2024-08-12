using System;

namespace GruInject.API.Attributes
{
    /// <summary>
    /// Classes with this attribute will be auto created on start of Service allocation (it also include inheritors of class with this attribute).
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoSpawnAttribute : RegisterSingletonAttribute {    }
}