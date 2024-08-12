using System;
using System.Collections.Generic;
using Godot;
using GruInject.Core.Helpers;

namespace GruInject.Core.Registration
{
    public class InstanceContainer : IDisposable
    {
        public Dictionary<Type, List<object>> InitializedInstances = new();

        public void Dispose()
        {
            foreach (var instance in InitializedInstances)
            {
                if (typeof(IDisposable).IsAssignableFrom(instance.Key))
                {
                    foreach (IDisposable element in instance.Value)
                    {
                        if (!DisposeChecker.IsDisposed(element))
                            element.Dispose();
                        else
                        {
                            GD.PushWarning($"Found not disposed instance while closing GruInj of type: {instance.Key}");
                        }
                    }
                }
                else
                {
                    GD.PushWarning($"Type {instance.Key} don't implement IDisposable - It lends to memory leak. Found {instance.Value.Count} instances not Disposed.");
                }
            }

            InitializedInstances = null;
        }
    }
}