using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lunha.DevKit.Utilities;

namespace Lunha.DevKit.Extensions
{
    public static class DictionaryExtension
    {
        /// <summary>
        /// Returns the composition of the structure's public fields
        /// </summary>
        /// <param name="data">Structure's instance</param>
        /// <param name="nameConvention">Dictionary keys name convention</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Dictionary of key(string) / value(object)</returns>
        public static Dictionary<string, object> ToDictionary<T>(this T data, NameConventionType nameConvention)
        {
            var dictionary = new Dictionary<string, object>();
            var fields = data.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (var fieldInfo in fields)
            {
                var key = NameConventionBuilder.FormatTextByConvention(fieldInfo.Name, nameConvention);
                dictionary.Add(key, fieldInfo.GetValue(data));
            }

            return dictionary;
        }

        /// <summary>
        /// Returns an instance of T with filled public fields by dictionary
        /// </summary>
        /// <param name="dictionary">Dictionary of fields</param>
        /// <param name="nameConvention">T fields name convention</param>
        /// <typeparam name="T">Return type</typeparam>
        /// <returns>Instance of T or null</returns>
        public static T ToObject<T>(this Dictionary<string, object> dictionary, NameConventionType nameConvention)
            where T : class
        {
            if (!(Activator.CreateInstance(typeof(T)) is T objectInstance))
            {
                return null;
            }

            var fields = objectInstance.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (var keyValuePair in dictionary)
            {
                var fieldName = NameConventionBuilder.FormatTextByConvention(keyValuePair.Key, nameConvention);
                var fieldInfo = fields.FirstOrDefault(i => i.Name.Equals(fieldName));

                if (fieldInfo == null)
                {
#if UNITY_EDITOR || ENVIRONMENT_DEV
                    UnityEngine.Debug.LogWarning(
                        $"Casting Dictionary to {nameof(T)}: can't find field with field name '{fieldName}'");
#endif
                    continue;
                }

                if (fieldInfo.FieldType.IsArray)
                {
                    var toObjectMethod = typeof(DictionaryExtension).GetMethod(nameof(ToObject));
                    if (toObjectMethod == null)
                    {
                        throw new ReflectionTypeLoadException(
                            new[] { typeof(DictionaryExtension) },
                            new Exception[] { new MissingMethodException() },
                            $"Method '{nameof(ToObject)}' not found in '{typeof(DictionaryExtension).Assembly.FullName}'");
                    }

                    var fieldArrayElementType = fieldInfo.FieldType.GetElementType();
                    if (fieldArrayElementType == null)
                    {
                        throw new ReflectionTypeLoadException(
                            new[] { typeof(T) },
                            new Exception[] { new MissingFieldException() },
                            $"Field of array '{fieldInfo.FieldType.Name}' is null in '{fieldInfo.GetType().FullName}'");
                    }

                    var toObjectMethodInfo = toObjectMethod.MakeGenericMethod(fieldArrayElementType);
                    var inputValues = (List<object>)keyValuePair.Value;
                    var valueInstancesArray = Array.CreateInstance(fieldArrayElementType, inputValues.Count);

                    for (var index = 0; index < inputValues.Count; index++)
                    {
                        var valueElementInstance =
                            toObjectMethodInfo.Invoke(null, new[] { inputValues[index], nameConvention });
                        valueInstancesArray.SetValue(valueElementInstance, index);
                    }

                    fieldInfo.SetValue(objectInstance, valueInstancesArray);
                }
                else
                {
                    fieldInfo.SetValue(objectInstance, Convert.ChangeType(keyValuePair.Value, fieldInfo.FieldType));
                }
            }

            return objectInstance;
        }
    }
}