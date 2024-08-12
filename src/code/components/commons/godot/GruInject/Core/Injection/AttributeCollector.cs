using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GruInject.Core.Injection
{
    public class AttributeCollector
    {
        public IEnumerable<Type> GetClasses(Type attribute)
        {
            return from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                where type.IsDefined(attribute, false)
                select type;
        }

        public List<MethodInfo> GetMethods(Type attribute, Type type)
        {
            List<MethodInfo> infos = new();

            foreach (var methodInfo in type.GetMethods( BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (methodInfo.IsDefined(attribute,false))
                {
                    infos.Add(methodInfo);
                }
            }

            return infos;
        }

        public List<MethodInfo> GetMethodsWithAttributeOnParam(Type attribute, Type type)
        {
            List<MethodInfo> infos = new();

            foreach (var methodInfo in type.GetMethods( BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                ParameterInfo[] parameters = methodInfo.GetParameters();

                bool shouldAdd = false;
                foreach (var parameter in parameters)
                {
                    if (parameter.IsDefined(attribute, false))
                    {
                        shouldAdd = true;
                    }
                }

                if (shouldAdd)
                {
                    infos.Add(methodInfo);
                }
            }

            return infos;
        }

        public List<FieldInfo> GetFields(Type attribute)
        {
            List<FieldInfo> fieldInfos = new();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    foreach (var field in type.GetFields(BindingFlags.Public| BindingFlags.NonPublic))
                    {
                        if (field.IsDefined(attribute,false))
                        {
                            fieldInfos.Add(field);
                        }
                    }
                }
            }

            return fieldInfos;
        }

        public List<FieldInfo> GetFields(Type attribute,Type type) 
        {
            List<FieldInfo> fieldInfos = new();

            foreach (var field in type.GetFields( BindingFlags.NonPublic |BindingFlags.Public | BindingFlags.Instance))
            {
                if (field.IsDefined(attribute,false))
                {
                    fieldInfos.Add(field);
                }
            }

            return fieldInfos;
        }

        public List<PropertyInfo> GetProperties(Type attribute,Type type)
        {
            List<PropertyInfo> propertyInfos = new();

            foreach (var propertyInfo in type.GetProperties(BindingFlags.NonPublic |BindingFlags.Public | BindingFlags.Instance))
            {
                if (propertyInfo.IsDefined(attribute,false))
                {
                    propertyInfos.Add(propertyInfo);
                }
            }

            return propertyInfos;
        }
    }
}