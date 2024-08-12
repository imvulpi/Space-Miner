using System;

namespace GruInject.API.Attributes
{

    /// <summary>
    /// Classes and struct with this attribute will be created only once and single instance will be distributed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class RegisterSingletonAttribute : Attribute{}
}