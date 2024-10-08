﻿using Newtonsoft.Json;
using NLog;

namespace TestSandbox.Serialization
{
    public class Deserializer: IDeserializer
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();

        public Deserializer(IDeserializationContext deserializationContext)
        {
            _deserializationContext = deserializationContext;
        }

        private readonly IDeserializationContext _deserializationContext;

        /// <inheritdoc/>
        public T Deserialize<T>()
        {
            var rootFileFullName = Path.Combine(_deserializationContext.DirName, "root.json");

#if DEBUG
            _logger.Info($"rootFileFullName = {rootFileFullName}");
#endif

            var rootObject = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(rootFileFullName), SerializationHelper.JsonSerializerSettings);

#if DEBUG
            _logger.Info($"rootObject = {rootObject}");
#endif

            return (T)NDeserialize(rootObject.Data);
        }

        /// <inheritdoc/>
        public T GetDeserializedObject<T>(ObjectPtr objectPtr)
        {
#if DEBUG
            _logger.Info($"objectPtr = {objectPtr}");
#endif

            return (T)GetDeserializedObject(objectPtr);
        }

        private object GetDeserializedObject(ObjectPtr objectPtr)
        {
#if DEBUG
            _logger.Info($"objectPtr = {objectPtr}");
#endif

            if(objectPtr.IsNull)
            {
                return null;
            }

            var instanceId = objectPtr.Id;

            if (_deserializationContext.TryGetDeserializedObject(instanceId, out var instance))
            {
                return instance;
            }

            return NDeserialize(objectPtr);
        }

        private object NDeserialize(ObjectPtr objectPtr)
        {
#if DEBUG
            _logger.Info($"objectPtr = {objectPtr}");
#endif

            var fileName = $"{objectPtr.Id}.json";

            var fullFileName = Path.Combine(_deserializationContext.DirName, fileName);

#if DEBUG
            _logger.Info($"fullFileName = {fullFileName}");
#endif

            var type = Type.GetType(objectPtr.TypeName);

#if DEBUG
            _logger.Info($"type.FullName = {type.FullName}");
#endif

            switch(type.Name)
            {
                case "List`1":
                    return NDeserializeGenericList(type, objectPtr, fullFileName);

                default:
                    {
                        if(IsSerializable(type))
                        {
                            return NDeserializeISerializable(type, objectPtr, fullFileName);
                        }
                    }
                    throw new NotImplementedException();
            }
        }

        private bool IsSerializable(Type type)
        {
#if DEBUG
            _logger.Info($"type.GetInterfaces() = {JsonConvert.SerializeObject(type.GetInterfaces().Select(p => p.FullName), Formatting.Indented)}");
#endif

            return type.GetInterfaces().Any(p => p == typeof(ISerializable));
        }

        private object NDeserializeGenericList(Type type, ObjectPtr objectPtr, string fullFileName)
        {
            var genericParameterType = type.GetGenericArguments()[0];

#if DEBUG
            _logger.Info($"genericParameterType.FullName = {genericParameterType.FullName}");
            _logger.Info($"genericParameterType.Name = {genericParameterType.Name}");
            _logger.Info($"genericParameterType.IsGenericType = {genericParameterType.IsGenericType}");
            _logger.Info($"genericParameterType.IsPrimitive = {genericParameterType.IsPrimitive}");
#endif

            if (SerializationHelper.IsObject(genericParameterType))
            {
                return NDeserializeListWithObjectParameter(type, objectPtr, fullFileName);
            }

            throw new NotImplementedException();
        }

        private object NDeserializeListWithObjectParameter(Type type, ObjectPtr objectPtr, string fullFileName)
        {
            var instance = Activator.CreateInstance(type);

#if DEBUG
            _logger.Info($"instance = {instance}");
#endif

            var list = (List<object>)instance;

            var listWithPlainObjects = JsonConvert.DeserializeObject<List<object>>(File.ReadAllText(fullFileName), SerializationHelper.JsonSerializerSettings);

            foreach (var plainObjectItem in listWithPlainObjects)
            {
#if DEBUG
                _logger.Info($"plainObjectItem = {JsonConvert.SerializeObject(plainObjectItem, Formatting.Indented)}");
#endif

                if (SerializationHelper.IsPrimitiveType(plainObjectItem))
                {
                    list.Add(plainObjectItem);

                    continue;
                }

                var itemType = plainObjectItem.GetType();

#if DEBUG
                _logger.Info($"itemType.FullName = {itemType.FullName}");
                _logger.Info($"itemType.Name = {itemType.Name}");
                _logger.Info($"itemType.IsGenericType = {itemType.IsGenericType}");
#endif

                if(SerializationHelper.IsObjectPtr(itemType))
                {
                    list.Add(GetDeserializedObject((ObjectPtr)plainObjectItem));

                    continue;
                }

                throw new NotImplementedException();

                //if (IsSerializable(type))
                //{
                //    return NDeserializeISerializable<T>(, objectPtr, fullFileName);
                //}

                //var serializable = plainObjectItem as ISerializable;

                //if (serializable == null)
                //{

                //    throw new NotImplementedException();
                //}
                //else
                //{
                //    list.Add(NDeserializeISerializable<T>(serializable));
                //}
            }

#if DEBUG
            _logger.Info($"list = {JsonConvert.SerializeObject(list, Formatting.Indented)}");
#endif

            return list;
        }

        private object NDeserializeISerializable(Type type, ObjectPtr objectPtr, string fullFileName)
        {
            var instance = Activator.CreateInstance(type);

#if DEBUG
            _logger.Info($"instance = {instance}");
#endif

            var serializable = (ISerializable)instance;

            var plainObject = JsonConvert.DeserializeObject(File.ReadAllText(fullFileName), serializable.GetPlainObjectType(), SerializationHelper.JsonSerializerSettings);

#if DEBUG
            _logger.Info($"plainObject = {plainObject}");
#endif

            serializable.OnReadPlainObject(plainObject, this);

#if DEBUG
            _logger.Info($"serializable = {serializable}");
#endif

            _deserializationContext.RegDeserializedObject(objectPtr.Id, serializable);

            return serializable;
        }
    }
}
