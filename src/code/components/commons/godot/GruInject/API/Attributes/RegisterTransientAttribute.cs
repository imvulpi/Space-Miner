using System;

namespace GruInject.API.Attributes
{
    /// <summary>
    /// Classes and Struct with this attribute will have instance created per request of instance.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class RegisterTransientAttribute : Attribute{}
}