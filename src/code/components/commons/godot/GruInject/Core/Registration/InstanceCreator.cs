using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GruInject.Core.Injection;

namespace GruInject.Core.Registration
{
    public class InstanceCreator
    {
        private readonly InstanceContainer _instanceContainer;
        private readonly InstanceFiller _instanceFiller;

        public InstanceCreator(InstanceContainer instanceContainer, InstanceFiller instanceFiller)
        {
            _instanceFiller = instanceFiller;
            _instanceContainer = instanceContainer;
        }

        public object CreateInstance(Type type)
        {
            var createdInstance = CreateNotCtorInstance(type);
            _instanceFiller.FillInstance(createdInstance, CreateInstance);
            _instanceFiller.InitializeInstanceCtor(type, createdInstance, CreateInstance);
            _instanceFiller.InitializeMethods(createdInstance, CreateInstance);

            //ToDO: Should it be added to container when its not single or shared?
            if (_instanceContainer.InitializedInstances.TryGetValue(type, out List<object> litOfInstances))
            {
                litOfInstances.Add(createdInstance);
            }
            else
            {
                foreach (var assosiacetInterface in type.GetInterfaces())
                {
                    if (assosiacetInterface != typeof(IDisposable))
                    {
                        if (_instanceContainer.InitializedInstances.TryGetValue(assosiacetInterface, out List<object> listOfInterfaceInstances))
                            listOfInterfaceInstances.Add(assosiacetInterface);
                        else
                        {
                            _instanceContainer.InitializedInstances.Add(assosiacetInterface,new List<object>() {createdInstance});

                        }
                    }
                }
                _instanceContainer.InitializedInstances.Add(type, new List<object>() {createdInstance});
            }
            return createdInstance;
        }
        
        private object CreateNotCtorInstance(Type type)
        {
            return RuntimeHelpers.GetUninitializedObject(type);
        }
    }
}