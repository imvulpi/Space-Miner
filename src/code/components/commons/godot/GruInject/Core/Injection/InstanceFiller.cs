using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GruInject.Core.Injection
{
    public class InstanceFiller : IDisposable
    {
        private AttributeCollector _attributeCollector = new();
        private List<Type> _injectAttributes;

        public InstanceFiller(List<Type> injectAttributes)
        {
            _injectAttributes = injectAttributes;
        }
        
        public void FillInstance<T>(T instance, Func<Type,object> instanceProvider)
        {
            foreach (var attribute in _injectAttributes)
            {
                var fields = _attributeCollector.GetFields(attribute, instance.GetType());
                foreach (var fieldInfo in fields)
                {
                    fieldInfo.SetValue(instance, instanceProvider(fieldInfo.FieldType));
                }

                var properties = _attributeCollector.GetProperties(attribute, instance.GetType());
                foreach (var propertyInfo in properties)
                {
                    propertyInfo.SetValue(instance, instanceProvider(propertyInfo.PropertyType));
                }
            }
        }

        public void InitializeInstanceCtor(Type type, object instance, Func<Type,object> instanceProvider)
        {
            var ctor = type.GetConstructors()
                           .Where(c => c.IsPublic)
                           .OrderByDescending(c => c.GetParameters().Length)
                           .FirstOrDefault()
                       ?? throw new InvalidOperationException($"No suitable constructor found on type '{type}'");

            
            var injectionServices = ctor.GetParameters()
                .Select(p => instanceProvider(p.ParameterType))
                .ToArray();
            
            ctor.Invoke(instance, injectionServices);
        }

        public void InitializeMethods<T>(T instance, Func<Type,object> instanceProvider)
        {
            foreach (var attribute in _injectAttributes)
            {
                var methods = _attributeCollector.GetMethods(attribute, instance.GetType());
                foreach (var methodInfo in methods)
                {
                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    var injectionServices = parameters
                        .Select(p => instanceProvider(p.ParameterType))
                        .ToArray();
                    methodInfo.Invoke(instance, injectionServices);
                }

                var methodsWithAttributeOnParam =
                    _attributeCollector.GetMethodsWithAttributeOnParam(attribute, instance.GetType());
                
                foreach (var methodInfo in methodsWithAttributeOnParam)
                {
                    if (!methodInfo.IsDefined(attribute, false))
                    {
                        ParameterInfo[] parameters = methodInfo.GetParameters();
                        var injectionServices = parameters
                            .Select(p => instanceProvider(p.ParameterType))
                            .ToArray();
                        methodInfo.Invoke(instance, injectionServices);
                    }
                }
            }
        }

        public void Dispose()
        {
        }
    }
}