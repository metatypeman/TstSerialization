using NLog;

namespace TestSandbox.Serialization
{
    public class DeserializationContext: IDeserializationContext
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();

        public DeserializationContext() 
        {
            _dirName = @"d:\Repos\TstSerialization\TestSandbox\bin\Debug\net8.0\2c33fe67-1edf-48ab-ad78-17b3ed577be3\";

#if DEBUG
            _logger.Info($"_dirName = {_dirName}");
#endif
        }

        private string _dirName;
        private Dictionary<string, object> _deserializedObject = new Dictionary<string, object>();

        /// <inheritdoc/>
        public string DirName => _dirName;

        /// <inheritdoc/>
        public bool TryGetDeserializedObject(string instanceId, out object instance)
        {
            return _deserializedObject.TryGetValue(instanceId, out instance);
        }

        /// <inheritdoc/>
        public void RegDeserializedObject(string instanceId, object instance)
        {
            _deserializedObject[instanceId] = instance;
        }
    }
}
