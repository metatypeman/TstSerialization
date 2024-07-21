using System.Text;
using TestSandbox.Helpers;
using TestSandbox.Serialization;
using TestSandbox.SerializedObjects.PlainObjects;

namespace TestSandbox.SerializedObjects
{
    public class SecondComponentData : IObjectToString, ISerializable
    {
        public int SomeField { get; set; }

        Type ISerializable.GetPlainObjectType() => typeof(SecondComponentDataPo);

        void ISerializable.OnWritePlainObject(object plainObject, ISerializer serializer)
        {
            OnWritePlainObject((SecondComponentDataPo)plainObject, serializer);
        }

        void OnWritePlainObject(SecondComponentDataPo plainObject, ISerializer serializer)
        {
            plainObject.SomeField = SomeField;
        }

        void ISerializable.OnReadPlainObject(object plainObject, ISerializer serializer)
        {
            OnReadPlainObject((SecondComponentDataPo)plainObject, serializer);
        }

        void OnReadPlainObject(SecondComponentDataPo plainObject, ISerializer serializer)
        {
            SomeField = plainObject.SomeField;
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
            sb.AppendLine($"{spaces}{nameof(SomeField)} = {SomeField}");
            return sb.ToString();
        }
    }
}
