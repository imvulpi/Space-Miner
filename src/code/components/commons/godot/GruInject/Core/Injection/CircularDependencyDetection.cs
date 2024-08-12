using System;
using System.Collections.Generic;
using System.Linq;

namespace GruInject.Core.Injection
{
    public class CircularDependencyDetection
    {
        private AttributeCollector _attributeCollector = new();
        private List<Type> _alreadyCheckedTypes = new();

        public List<Type> FindCircularDependency(Type attribute ,Type type)
        {
            if (_alreadyCheckedTypes.Contains(type))
            {
                _alreadyCheckedTypes.Add(type);
                return _alreadyCheckedTypes;
            }
            
            _alreadyCheckedTypes.Add(type);
            
            var fields= _attributeCollector.GetFields(attribute, type);
            foreach (var fieldInfo in fields)
            {
                var result = FindCircularDependency(attribute ,fieldInfo.FieldType);
                if (result.Count > 0)
                {
                    return _alreadyCheckedTypes;
                }
            }

            var ctorParameters = type.GetConstructors()
                .Where(c => c.IsPublic)
                .OrderByDescending(c => c.GetParameters().Length)
                .FirstOrDefault()?.GetParameters();

            if (ctorParameters != null)
            {
                foreach (var parameters in ctorParameters)
                {
                    var result = FindCircularDependency(attribute, parameters.ParameterType);
                    if (result.Count > 0)
                    {
                        return _alreadyCheckedTypes;
                    }
                }
            }

            _alreadyCheckedTypes.Remove(type);
            return new List<Type>();
        }
    }
}