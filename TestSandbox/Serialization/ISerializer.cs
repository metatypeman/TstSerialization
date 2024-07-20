using System.Runtime.Serialization;

namespace TestSandbox.Serialization
{
    public interface ISerializer
    {
        void Serialize<TPlainObject>(ISerializable<TPlainObject> serializable)
            where TPlainObject : class, new();
        ObjectPtr GetSerializedObjectPtr<TPlainObject>(ISerializable<TPlainObject> serializable)
            where TPlainObject : class, new();
    }
}
