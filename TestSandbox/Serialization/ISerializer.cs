namespace TestSandbox.Serialization
{
    public interface ISerializer
    {
        void Serialize(ISerializable serializable);
        ObjectPtr GetSerializedObjectPtr(ISerializable serializable);

        T Deserialize<T>()
            where T : ISerializable, new();

        T GetDeserializedObject<T>(ObjectPtr objectPtr)
            where T : ISerializable, new();
    }
}
