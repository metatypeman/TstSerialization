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

        public void Serialize<TPlainObject>(ISerializable<TPlainObject> serializable)
            where TPlainObject : class, new()
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

        public ObjectPtr GetSerializedObjectPtr<TPlainObject>(ISerializable<TPlainObject> serializable)
            where TPlainObject : class, new()
        {
            _logger.Info($"serializable = {serializable}");

            if (_serializedObjects.ContainsKey(serializable))
            {
                return _serializedObjects[serializable];
            }

            return NSerialize(serializable);
        }

        private ObjectPtr NSerialize<TPlainObject>(ISerializable<TPlainObject> serializable)
            where TPlainObject : class, new ()
        {
            var instanceId = Guid.NewGuid().ToString("D");

            _logger.Info($"instanceId = {instanceId}");

            var objectPtr = new ObjectPtr(instanceId, serializable.GetType().FullName);

            _logger.Info($"objectPtr = {objectPtr}");

            _serializedObjects[serializable] = objectPtr;

            var plainObject = new TPlainObject();

            serializable.OnWritePlainObject(plainObject, this);

            _logger.Info($"plainObject = {plainObject}");

            var fileName = $"{instanceId}.json";

            var fullFileName = Path.Combine(_dirName, fileName);

            _logger.Info($"fullFileName = {fullFileName}");

            File.WriteAllText(fullFileName, JsonConvert.SerializeObject(plainObject));

            return objectPtr;
        }

        public T Deserialize<T, TPlainObject>()
            where T : ISerializable<TPlainObject>, new()
            where TPlainObject : class, new()
        {
            _dirName = @"d:\Repos\TstSerialization\TestSandbox\bin\Debug\net8.0\2c33fe67-1edf-48ab-ad78-17b3ed577be3\";

            _logger.Info($"_dirName = {_dirName}");

            var rootFileFullName = Path.Combine(_dirName, "root.json");

            _logger.Info($"rootFileFullName = {rootFileFullName}");

            var rootObject = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(rootFileFullName));

            _logger.Info($"rootObject = {rootObject}");

            var type = Type.GetType(rootObject.Data.TypeName);

            _logger.Info($"type.FullName = {type.FullName}");

            var obj = Activator.CreateInstance(type);

            _logger.Info($"obj = {obj}");

            throw new NotImplementedException();
        }
    }
}
