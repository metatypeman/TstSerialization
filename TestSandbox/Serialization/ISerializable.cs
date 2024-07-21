using TestSandbox.Helpers;

namespace TestSandbox.Serialization
{
    public interface ISerializable: IObjectToString
    {
        Type GetPlainObjectType();
        void OnWritePlainObject(object plainObject, ISerializer serializer);
        void OnReadPlainObject(object plainObject, ISerializer serializer);
    }
}
