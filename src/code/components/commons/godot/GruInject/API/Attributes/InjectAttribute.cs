using System;

namespace GruInject.API.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Parameter )]
    public class InjectAttribute : Attribute
    {
        //Fields and Properties with this attribute will be injected with instances
        //if class they are part of is created with Service allocation (Either create due injection or by ServiceLocator.GetInstance
        
        //If used on method or param of a method then this method will be called when instance is created.
        //all params will be filled even if attribute is used only on one of them
    }

    //ToDO: attributes for methods (ctor)
    //ToDO: attributes for fields in methods (ctor)
    //ToDO: Attribute for LazyInitialization
}