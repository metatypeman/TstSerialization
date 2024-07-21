using Newtonsoft.Json.Linq;

namespace TestSandbox.Serialization
{
    public interface ISerializationContext
    {
        string DirName { get; }
        bool IsSerialized(ISerializable serializable);
        bool TryGetObjectPtr(ISerializable serializable, out ObjectPtr objectPtr);
        void RegObjectPtr(ISerializable serializable, ObjectPtr objectPtr);
    }
}
