using System;
using System.Reflection;
using XIV.Core.DataStructures;

namespace XIV.Core.Extensions
{
    public static class TypeExtensions
    {
        static readonly BindingFlags DefaultBindingFlags = 
            BindingFlags.Public | 
            BindingFlags.Static |
            BindingFlags.Instance | 
            BindingFlags.NonPublic | 
            BindingFlags.GetField |
            BindingFlags.SetField;
        
        public static void XIVSetFieldOrProperty(this Type type, string memberName, object instance, object memberValue, BindingFlags bindingFlags)
        {
            FieldInfo fieldInfo = type.GetField(memberName, bindingFlags);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(instance, memberValue);
                return;
            }
            PropertyInfo propertyInfo = type.GetProperty(memberName, bindingFlags);
            propertyInfo.SetValue(instance, memberValue, null);
        }

        public static void XIVSetFieldOrProperty(this Type type, string memberName, object instance, object memberValue)
        {
            XIVSetFieldOrProperty(type, memberName, instance, memberValue, DefaultBindingFlags);
        }
        
        public static object XIVGetFieldOrPropertyValue(this Type type, string memberName, object instance, BindingFlags bindingFlags)
        {
            FieldInfo fieldInfo = type.GetField(memberName, bindingFlags);
            if (fieldInfo != null)
            {
                return fieldInfo.GetValue(instance);
            }
            PropertyInfo propertyInfo = type.GetProperty(memberName, bindingFlags);
            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(instance);
            }
            return null;
        }

        public static T XIVGetFieldOrPropertyValue<T>(this Type type, string memberName, object instance)
        {
            return (T)XIVGetFieldOrPropertyValue(type, memberName, instance, DefaultBindingFlags);
        }

        public static object XIVGetFieldOrPropertyValue(this Type type, string memberName, object instance)
        {
            return XIVGetFieldOrPropertyValue(type, memberName, instance, DefaultBindingFlags);
        }
        
        public static void XIVSetField(this Type type, string fieldName, object instance, object fieldValue, BindingFlags bindingFlags)
        {
            FieldInfo fieldInfo = type.GetField(fieldName, bindingFlags);
            fieldInfo.SetValue(instance, fieldValue);
        }
        
        public static void XIVSetField(this Type type, string fieldName, object instance, object fieldValue)
        {
            XIVSetField(type, fieldName, instance, fieldValue, DefaultBindingFlags);
        }

        public static object XIVGetFieldValue(this Type type, string fieldName, object instance)
        {
            FieldInfo fieldInfo = type.GetField(fieldName, DefaultBindingFlags);
            return fieldInfo.GetValue(instance);
        }

        public static T XIVGetFieldValue<T>(this Type type, string fieldName, object instance)
        {
            return (T)XIVGetFieldValue(type, fieldName, instance);
        }
        
        public static void XIVSetProperty(this Type type, string propertyName, object instance, object fieldValue, BindingFlags bindingFlags)
        {
            PropertyInfo propertyInfo = type.GetProperty(propertyName, bindingFlags);
            propertyInfo.SetValue(instance, fieldValue, null);
        }
        
        public static void XIVSetProperty(this Type type, string propertyName, object instance, object fieldValue)
        {
            XIVSetProperty(type, propertyName, instance, fieldValue, DefaultBindingFlags);
        }

        public static object XIVGetPropertyValue(this Type type, string propertyName, object instance)
        {
            PropertyInfo propertyInfo = type.GetProperty(propertyName, DefaultBindingFlags);
            return propertyInfo.GetValue(instance);
        }

        public static T XIVGetPropertyValue<T>(this Type type, string propertyName, object instance)
        {
            return (T)XIVGetPropertyValue(type, propertyName, instance);
        }

        public static XIVMemory<MethodInfo> XIVGetMethodsHasAttribute<TAttribute>(this Type type, BindingFlags bindingFlags) where TAttribute : Attribute
        {
            var methods = type.GetMethods(bindingFlags);
            return methods.XIVFilterBy(methods.Length, p => p.GetCustomAttribute<TAttribute>() != null);
        }

        public static XIVMemory<MethodInfo> XIVGetMethodsHasAttribute<TAttribute>(this Type type) where TAttribute : Attribute
        {
            return XIVGetMethodsHasAttribute<TAttribute>(type, DefaultBindingFlags);
        }

        public static MethodInfo[] XIVGetMethods(this Type type)
        {
            return type.GetMethods(DefaultBindingFlags);
        }

        public static MethodInfo XIVGetMethodByName(this Type type, string methodName)
        {
            return type.GetMethods(DefaultBindingFlags).XIVFirstOrDefault(p => p.Name == methodName);
        }

        public static XIVMemory<MemberInfo> XIVGetMembers(this Type type, BindingFlags bindingFlags)
        {
            return type.GetMembers(bindingFlags);
        }

        public static XIVMemory<MemberInfo> XIVGetMembers(this Type type)
        {
            return XIVGetMembers(type, DefaultBindingFlags);
        }

        public static XIVMemory<MemberInfo> XIVGetMembersHasAttribute<TAttribute>(this Type type, BindingFlags bindingFlags, bool inherit) where TAttribute : Attribute
        {
            return type.GetMembers(bindingFlags).XIVFilterBy(p => p.GetCustomAttribute<TAttribute>(inherit) != null);
        }

        public static XIVMemory<MemberInfo> XIVGetMembersHasAttribute<TAttribute>(this Type type, bool inherit = true) where TAttribute : Attribute
        {
            return XIVGetMembersHasAttribute<TAttribute>(type, DefaultBindingFlags, inherit);
        }

        public static void XIVInvokeMethodsHasAttribute<TAttribute>(this Type type, object instance, object[] parameters, BindingFlags bindingFlags) where TAttribute : Attribute
        {
            var methods = XIVGetMethodsHasAttribute<TAttribute>(type, bindingFlags);
            int length = methods.Length;
            for (int i = 0; i < length; i++)
            {
                methods[i].Invoke(instance, parameters);
            }
        }

        public static void XIVInvokeMethodsHasAttribute<TAttribute>(this Type type, object instance, object[] parameters) where TAttribute : Attribute
        {
            XIVInvokeMethodsHasAttribute<TAttribute>(type, instance, parameters, DefaultBindingFlags);
        }

        public static void XIVInvokeMethodsHasAttribute<TAttribute>(this Type type, object instance) where TAttribute : Attribute
        {
            XIVInvokeMethodsHasAttribute<TAttribute>(type, instance, Array.Empty<object>(), DefaultBindingFlags);
        }
    }
}