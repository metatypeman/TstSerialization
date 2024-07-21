using Newtonsoft.Json;
using NLog;

namespace TestSandbox.Serialization
{
    public class Deserializer: IDeserializer
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();

        public Deserializer()
        {
        }

        private string _dirName;
        private Dictionary<string, object> _deserializedObject = new Dictionary<string, object>();

        /// <inheritdoc/>
        public T Deserialize<T>()
            where T : ISerializable, new()
        {
            _dirName = @"d:\Repos\TstSerialization\TestSandbox\bin\Debug\net8.0\2c33fe67-1edf-48ab-ad78-17b3ed577be3\";

#if DEBUG
            _logger.Info($"_dirName = {_dirName}");
#endif

            var rootFileFullName = Path.Combine(_dirName, "root.json");

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

            if (_deserializedObject.ContainsKey(instanceId))
            {
                return (T)_deserializedObject[instanceId];
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

            var fullFileName = Path.Combine(_dirName, fileName);

#if DEBUG
            _logger.Info($"fullFileName = {fullFileName}");
#endif

            var type = Type.GetType(objectPtr.TypeName);

#if DEBUG
            _logger.Info($"type.FullName = {type.FullName}");
#endif

            var obj = Activator.CreateInstance(type);

#if DEBUG
            _logger.Info($"obj = {obj}");
#endif

            var serializable = (ISerializable)obj;

            var plainObject = JsonConvert.DeserializeObject(File.ReadAllText(fullFileName), serializable.GetPlainObjectType());

#if DEBUG
            _logger.Info($"plainObject = {plainObject}");
#endif

            serializable.OnReadPlainObject(plainObject, this);

#if DEBUG
            _logger.Info($"serializable = {serializable}");
#endif

            _deserializedObject[objectPtr.Id] = obj;

            return (T)obj;
        }
    }
}
