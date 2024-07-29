using Newtonsoft.Json;
using NLog;

namespace TestSandbox.Serialization
{
    public class Serializer : ISerializer
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();

        public Serializer(ISerializationContext serializationContext)
        {
            _serializationContext = serializationContext;
        }

        private readonly ISerializationContext _serializationContext;

        /// <inheritdoc/>
        public void Serialize(ISerializable serializable)
        {
#if DEBUG
            _logger.Info($"serializable = {serializable}");
#endif

            if(_serializationContext.IsSerialized(serializable))
            {
                return;
            }

            var rootObject = new RootObject();
            rootObject.Data = NSerialize(serializable);

#if DEBUG
            _logger.Info($"rootObject = {rootObject}");
#endif

            var fileName = $"root.json";

            var fullFileName = Path.Combine(_serializationContext.DirName, fileName);

#if DEBUG
            _logger.Info($"fullFileName = {fullFileName}");
#endif

            File.WriteAllText(fullFileName, JsonConvert.SerializeObject(rootObject));
        }

        /// <inheritdoc/>
        public ObjectPtr GetSerializedObjectPtr(object obj)
        {
#if DEBUG
            _logger.Info($"obj = {obj}");
#endif

            if (_serializationContext.TryGetObjectPtr(serializable, out var objectPtr))
            {
                return objectPtr;
            }

            throw new NotImplementedException();

            //return NSerialize(serializable);
        }

        private ObjectPtr NSerialize(ISerializable serializable)
        {
            var instanceId = Guid.NewGuid().ToString("D");

#if DEBUG
            _logger.Info($"instanceId = {instanceId}");
#endif

            var objectPtr = new ObjectPtr(instanceId, serializable.GetType().FullName);

#if DEBUG
            _logger.Info($"objectPtr = {objectPtr}");
#endif

            _serializationContext.RegObjectPtr(serializable, objectPtr);

            var plainObject = Activator.CreateInstance(serializable.GetPlainObjectType());

            serializable.OnWritePlainObject(plainObject, this);

#if DEBUG
            _logger.Info($"plainObject = {plainObject}");
#endif

            var fileName = $"{instanceId}.json";

            var fullFileName = Path.Combine(_serializationContext.DirName, fileName);

#if DEBUG
            _logger.Info($"fullFileName = {fullFileName}");
#endif

            File.WriteAllText(fullFileName, JsonConvert.SerializeObject(plainObject));

            return objectPtr;
        }
    }
}
