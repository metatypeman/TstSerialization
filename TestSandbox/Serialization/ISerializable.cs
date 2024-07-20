using TestSandbox.Helpers;

namespace TestSandbox.Serialization
{
    public interface ISerializable<TPlainObject>: IObjectToString
        where TPlainObject : class, new()
    {
        void OnWritePlainObject(TPlainObject plainObject, ISerializer serializer);
        void OnReadPlainObject(TPlainObject plainObject, ISerializer serializer);
    }
}
