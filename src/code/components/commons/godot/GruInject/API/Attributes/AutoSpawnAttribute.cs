using System;

namespace GruInject.API.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoSpawnAttribute : RegisterSingletonAttribute
    {
        //Classes with this attribute will be auto created on start of Service allocation (it also include inheritors of class with this attribute).
    }
}