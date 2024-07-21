using Newtonsoft.Json;
using NLog;

namespace TestSandbox.Serialization
{
    public class Serializer : ISerializer
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();

        public Serializer()
        {
        }

        private string _dirName;

        private Dictionary<object, ObjectPtr> _serializedObjects = new Dictionary<object, ObjectPtr>();
        private Dictionary<string, object> _deserializedObject = new Dictionary<string, object>();

        public void Serialize(ISerializable serializable)
        {
            _dirName = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid().ToString("D"));

            Directory.CreateDirectory(_dirName);

            _logger.Info($"serializable = {serializable}");

            if(_serializedObjects.ContainsKey(serializable))
            {
                return;
            }

            var rootObject = new RootObject();
            rootObject.Data = NSerialize(serializable);

            _logger.Info($"rootObject = {rootObject}");

            var fileName = $"root.json";

            var fullFileName = Path.Combine(_dirName, fileName);

            _logger.Info($"fullFileName = {fullFileName}");

            File.WriteAllText(fullFileName, JsonConvert.SerializeObject(rootObject));
        }

        public ObjectPtr GetSerializedObjectPtr(ISerializable serializable)
        {
            _logger.Info($"serializable = {serializable}");

            if (_serializedObjects.ContainsKey(serializable))
            {
                return _serializedObjects[serializable];
            }

            return NSerialize(serializable);
        }

        private ObjectPtr NSerialize(ISerializable serializable)
        {
            var instanceId = Guid.NewGuid().ToString("D");

            _logger.Info($"instanceId = {instanceId}");

            var objectPtr = new ObjectPtr(instanceId, serializable.GetType().FullName);

            _logger.Info($"objectPtr = {objectPtr}");

            _serializedObjects[serializable] = objectPtr;

            var plainObject = Activator.CreateInstance(serializable.GetPlainObjectType());

            serializable.OnWritePlainObject(plainObject, this);

            _logger.Info($"plainObject = {plainObject}");

            var fileName = $"{instanceId}.json";

            var fullFileName = Path.Combine(_dirName, fileName);

            _logger.Info($"fullFileName = {fullFileName}");

            File.WriteAllText(fullFileName, JsonConvert.SerializeObject(plainObject));

            _serializedObjects[serializable] = objectPtr;

            return objectPtr;
        }

        public T Deserialize<T>()
            where T : ISerializable, new()
        {
            _dirName = @"d:\Repos\TstSerialization\TestSandbox\bin\Debug\net8.0\2c33fe67-1edf-48ab-ad78-17b3ed577be3\";

            _logger.Info($"_dirName = {_dirName}");

            var rootFileFullName = Path.Combine(_dirName, "root.json");

            _logger.Info($"rootFileFullName = {rootFileFullName}");

            var rootObject = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(rootFileFullName));

            _logger.Info($"rootObject = {rootObject}");

            return NDeserialize<T>(rootObject.Data);
        }

        public T GetDeserializedObject<T>(ObjectPtr objectPtr)
            where T : ISerializable, new()
        {
            _logger.Info($"objectPtr = {objectPtr}");

            var instanceId = objectPtr.Id;

            if(_deserializedObject.ContainsKey(instanceId))
            {
                return (T)_deserializedObject[instanceId];
            }

            return NDeserialize<T>(objectPtr);
        }

        private T NDeserialize<T>(ObjectPtr objectPtr)
            where T : ISerializable, new()
        {
            _logger.Info($"objectPtr = {objectPtr}");

            var fileName = $"{objectPtr.Id}.json";

            var fullFileName = Path.Combine(_dirName, fileName);

            _logger.Info($"fullFileName = {fullFileName}");

            var type = Type.GetType(objectPtr.TypeName);

            _logger.Info($"type.FullName = {type.FullName}");

            var obj = Activator.CreateInstance(type);

            _logger.Info($"obj = {obj}");

            var serializable = (ISerializable)obj;

            var plainObject = JsonConvert.DeserializeObject(File.ReadAllText(fullFileName), serializable.GetPlainObjectType());

            _logger.Info($"plainObject = {plainObject}");

            serializable.OnReadPlainObject(plainObject, this);

            _logger.Info($"serializable = {serializable}");

            _deserializedObject[objectPtr.Id] = obj;

            return (T)obj;
        }
    }
}
