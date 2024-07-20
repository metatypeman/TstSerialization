namespace TestSandbox.Serialization
{
    public interface ISerializer
    {
        void Serialize<TPlainObject>(ISerializable<TPlainObject> serializable)
            where TPlainObject : class, new();
        ObjectPtr GetSerializedObjectPtr<TPlainObject>(ISerializable<TPlainObject> serializable)
            where TPlainObject : class, new();

        T Deserialize<T, TPlainObject>()
            where T : ISerializable<TPlainObject>, new()
            where TPlainObject: class, new();

        T GetDeserializedObject<T, TPlainObject>(ObjectPtr objectPtr)
            where T : ISerializable<TPlainObject>, new()
            where TPlainObject : class, new();
    }
}
