namespace TestSandbox.Serialization
{
    public class SerializationContext: ISerializationContext
    {
        public SerializationContext()
        {
            _dirName = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid().ToString("D"));

            Directory.CreateDirectory(_dirName);
        }

        private string _dirName;
        private Dictionary<object, ObjectPtr> _serializedObjects = new Dictionary<object, ObjectPtr>();

        /// <inheritdoc/>
        public string DirName => _dirName;

        /// <inheritdoc/>
        public bool IsSerialized(ISerializable serializable)
        {
            return _serializedObjects.ContainsKey(serializable);
        }

        /// <inheritdoc/>
        public bool TryGetObjectPtr(ISerializable serializable, out ObjectPtr objectPtr)
        {
            return _serializedObjects.TryGetValue(serializable, out objectPtr);
        }

        /// <inheritdoc/>
        public void RegObjectPtr(ISerializable serializable, ObjectPtr objectPtr)
        {
            _serializedObjects[serializable] = objectPtr;
        }
    }
}
