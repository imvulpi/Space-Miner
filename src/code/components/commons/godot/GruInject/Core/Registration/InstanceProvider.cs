using System;
using System.Collections.Generic;
using System.Linq;
using GruInject.API.Attributes;
using GruInject.Core.Injection;

namespace GruInject.Core.Registration
{
    public class InstanceProvider : IDisposable
    {
        private readonly InstanceContainer _instanceContainer = new();
        private readonly AttributeCollector _attributeCollector = new();
        private readonly List<Type> _singleInstanceRuleDefinition = new();
        private readonly List<Type> _registeredInstances = new();
        private readonly bool _allowOnlyRegisteredInstances = false;
        private readonly InstanceCreator _instanceCreator;
        
        public InstanceProvider(bool allowOnlyRegisteredInstances, InstanceFiller instanceFiller)
        {
            _allowOnlyRegisteredInstances = allowOnlyRegisteredInstances;
            var singleInstances = _attributeCollector.GetClasses(typeof(RegisterSingletonAttribute));
            foreach (var type in singleInstances)
            {
                _singleInstanceRuleDefinition.Add(type);
                foreach (var associatedInterface in type.GetInterfaces())
                {
                    if (associatedInterface != typeof(IDisposable))
                    {
                        _singleInstanceRuleDefinition.Add(associatedInterface);
                    }
                }
            }
            var registeredInstancesFound = _attributeCollector.GetClasses(typeof(RegisterTransientAttribute));
            foreach (var type in registeredInstancesFound)
            {
                _registeredInstances.Add(type);
                foreach (var associatedInterface in type.GetInterfaces())
                {
                    if (associatedInterface != typeof(IDisposable))
                    {
                        _registeredInstances.Add(associatedInterface);
                    }
                }
            }
            _instanceCreator = new InstanceCreator(_instanceContainer, instanceFiller);

            //ToDo: There can be also factory if user want to define its own creation method.
        }
        
        public object Get(Type type)
        {
            if (_allowOnlyRegisteredInstances)
            {
                if (!_registeredInstances.Contains(type) && !_singleInstanceRuleDefinition.Contains(type))
                    throw new Exception($"Trying to create unregistered Type: {type}");
            }

            if (_singleInstanceRuleDefinition.Contains(type))
            {
                if (_instanceContainer.InitializedInstances.TryGetValue(type, out var instance))
                {
                    return instance.First();
                }
            }

            return _instanceCreator.CreateInstance(type);
        }
        
        public object CheckInstanceAvailability(Type type)
        {
            if (_singleInstanceRuleDefinition.Contains(type))
            {
                if (_instanceContainer.InitializedInstances.TryGetValue(type, out var instance))
                {
                    return instance.First();
                }
            }

            return null;
        }

        public void Dispose()
        {
            _instanceContainer.Dispose();
        }
    }
}