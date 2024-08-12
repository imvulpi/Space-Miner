using System;
using System.Collections.Generic;
using GruInject.API.Attributes;
using GruInject.Core.Injection;

namespace GruInject.API
{
    public class GruInject
    {
        private List<Type> _autoSpawnAttributes;
        private List<Type> _injectAttribute;

        private ServiceLocator _serviceLocator;

        public GruInject(List<Type> autoSpawnAttributes, List<Type> injectAttribute)
        {
            _injectAttribute = injectAttribute;
            _autoSpawnAttributes = autoSpawnAttributes;
        }

        public void Start(bool enableCircularDependencyDetection = false, bool allowOnlyRegisteredInstances = true)
        {
            if (_serviceLocator != null)
            {
                throw new Exception("Service already started");
            }

            _serviceLocator = new ServiceLocator(_injectAttribute, enableCircularDependencyDetection, allowOnlyRegisteredInstances, GruInjStatic.ServiceLocator);
            InstanceInitializator.CurrentInstanceInitializator = _serviceLocator;

            AttributeCollector attributeCollector = new AttributeCollector();
            foreach (var attributeToSpawn in _autoSpawnAttributes)
            {
                IEnumerable<Type> classesToSpawn = attributeCollector.GetClasses(attributeToSpawn);

                foreach (var classToSpawn in classesToSpawn)
                {
                    _serviceLocator.GetInstance(classToSpawn);
                }
            }
        }

        public void Stop()
        {
            _serviceLocator.Dispose();
        }
    }
}