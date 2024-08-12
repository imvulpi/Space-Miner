using System;
using System.Collections.Generic;
using GruInject.API.Attributes;
using GruInject.Core.Injection;

namespace GruInject.API
{
    public static class GruInjStatic
    {
        public static readonly ServiceLocator ServiceLocator = 
            new ServiceLocator(
                new List<Type>() 
                    {typeof(InjectAttribute)}, 
                false, true);
        
        public static T Get<T>()
        {
            return ServiceLocator.GetInstance<T>();
        }
    }
}