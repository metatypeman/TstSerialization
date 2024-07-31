using System.Text;
using TestSandbox.Helpers;
using TestSandbox.Serialization;
using TestSandbox.SerializedObjects.PlainObjects;

namespace TestSandbox.SerializedObjects
{
    public class GenericData<T> : IObjectToString, ISerializable
    {
        public T Value { get; set; }

        Type ISerializable.GetPlainObjectType() => typeof(GenericDataPo);

        void ISerializable.OnWritePlainObject(object plainObject, ISerializer serializer)
        {
            OnWritePlainObject((GenericDataPo)plainObject, serializer);
        }

        void OnWritePlainObject(GenericDataPo plainObject, ISerializer serializer)
        {
            plainObject.Value = serializer.GetSerializedObjectPtr(Value);
        }

        void ISerializable.OnReadPlainObject(object plainObject, IDeserializer deserializer)
        {
            OnReadPlainObject((GenericDataPo)plainObject, deserializer);
        }

        void OnReadPlainObject(GenericDataPo plainObject, IDeserializer deserializer)
        {
            Value = deserializer.GetDeserializedObject<T>(plainObject.Value);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return ToString(0u);
        }

        /// <inheritdoc/>
        public string ToString(uint n)
        {
            return this.GetDefaultToStringInformation(n);
        }

        /// <inheritdoc/>
        string IObjectToString.PropertiesToString(uint n)
        {
            var spaces = DisplayHelper.Spaces(n);
            var sb = new StringBuilder();
            sb.AppendLine($"{spaces}{nameof(Value)} = {Value}");
            return sb.ToString();
        }
    }
}
