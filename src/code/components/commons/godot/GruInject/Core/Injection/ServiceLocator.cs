using System;
using System.Collections.Generic;
using Godot;
using GruInject.API.Initializators;
using GruInject.API.Nodes;
using GruInject.Core.Registration;

namespace GruInject.Core.Injection
{
    public class ServiceLocator : IDisposable, IInstanceInitializator
    {
        private readonly InstanceProvider _instanceProvider;
        private readonly InstanceFiller _instanceFiller;
        private bool _enableCircularDependencyDetection;
        private CircularDependencyDetection _circularDependencyDetection;
        private List<Type> _injectAttributes;
        private ServiceLocator _parentServiceLocator;
        private ServiceLocator _childServiceLocator;

        public ServiceLocator(List<Type> injectAttributes, bool enableCircularDependencyDetection, bool allowOnlyRegisteredInstances, ServiceLocator parentServiceLocator = null)
        {
            _parentServiceLocator = parentServiceLocator;
            _parentServiceLocator?.LinkAsChild(this);
            _injectAttributes = injectAttributes;
            _enableCircularDependencyDetection = enableCircularDependencyDetection;
            _instanceFiller = new InstanceFiller(injectAttributes);
            _instanceProvider =  new InstanceProvider(allowOnlyRegisteredInstances, _instanceFiller);
            _circularDependencyDetection = new CircularDependencyDetection();
        }
        
        public T GetInstance<T>()
        {
            return (T) GetInstance(typeof(T));
        }
        
        public object GetInstance(Type type)
        {
            if (_parentServiceLocator != null)
            {
                  var instance = _parentServiceLocator._instanceProvider.CheckInstanceAvailability(type); //ToDo test for multiple Service locators
                  if (instance != null) return instance;
            }
            
            if (_enableCircularDependencyDetection)
            {
                foreach (var attribute in _injectAttributes)
                {
                   var result = _circularDependencyDetection.FindCircularDependency(attribute, type);
                   if (result.Count > 0)
                   {
                       throw new Exception($"Circular dependency detected: {result}");
                   }
                }
            }

            return _instanceProvider.Get(type);
        }
        private void LinkAsChild(ServiceLocator serviceLocator)
        {
            _childServiceLocator = serviceLocator;
        }

        public void Dispose()
        {
            _childServiceLocator?.Dispose();
            _instanceProvider.Dispose();
            _instanceFiller.Dispose();
        }

        public void InitializeGruInstance(GruNode gruNode)
        {
            _instanceFiller.FillInstance(gruNode, _instanceProvider.Get);
            _instanceFiller.InitializeMethods(gruNode, _instanceProvider.Get);
        }

        public void InitializeGruInstance(GruNode2D gruNode)
        {
            _instanceFiller.FillInstance(gruNode, _instanceProvider.Get);
            _instanceFiller.InitializeMethods(gruNode, _instanceProvider.Get);
        }

        public void InitializeGruInstance(GruNode3D gruNode)
        {
            _instanceFiller.FillInstance(gruNode, _instanceProvider.Get);
            _instanceFiller.InitializeMethods(gruNode, _instanceProvider.Get);
        }

        public void InitializeGruInstance(GruControl gruNode)
        {
            _instanceFiller.FillInstance(gruNode, _instanceProvider.Get);
            _instanceFiller.InitializeMethods(gruNode, _instanceProvider.Get);
        }

        public void InitializeNodeInstance<T>(T node) where T : Node
        {
            _instanceFiller.FillInstance(node, _instanceProvider.Get);
            _instanceFiller.InitializeMethods(node, _instanceProvider.Get);
        }
    }
}