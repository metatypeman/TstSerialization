using Newtonsoft.Json;
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
            where T : ISerializable, new()
        {
            var rootFileFullName = Path.Combine(_deserializationContext.DirName, "root.json");

#if DEBUG
            _logger.Info($"rootFileFullName = {rootFileFullName}");
#endif

            var rootObject = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(rootFileFullName));

#if DEBUG
            _logger.Info($"rootObject = {rootObject}");
#endif

            return NDeserialize<T>(rootObject.Data);
        }

        /// <inheritdoc/>
        public T GetDeserializedObject<T>(ObjectPtr objectPtr)
            where T : ISerializable, new()
        {
#if DEBUG
            _logger.Info($"objectPtr = {objectPtr}");
#endif

            var instanceId = objectPtr.Id;

            if (_deserializationContext.TryGetDeserializedObject(instanceId, out var instance))
            {
                return (T)instance;
            }

            return NDeserialize<T>(objectPtr);
        }

        private T NDeserialize<T>(ObjectPtr objectPtr)
            where T : ISerializable, new()
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

            var instance = Activator.CreateInstance(type);

#if DEBUG
            _logger.Info($"instance = {instance}");
#endif

            var serializable = (ISerializable)instance;

            var plainObject = JsonConvert.DeserializeObject(File.ReadAllText(fullFileName), serializable.GetPlainObjectType());

#if DEBUG
            _logger.Info($"plainObject = {plainObject}");
#endif

            serializable.OnReadPlainObject(plainObject, this);

#if DEBUG
            _logger.Info($"serializable = {serializable}");
#endif

            _deserializationContext.RegDeserializedObject(objectPtr.Id, instance);

            return (T)instance;
        }
    }
}
