using System.Text;
using TestSandbox.Helpers;
using TestSandbox.Serialization;
using TestSandbox.SerializedObjects.PlainObjects;

namespace TestSandbox.SerializedObjects
{
    public class SecondComponentData : IObjectToString, ISerializable<SecondComponentDataPo>
    {
        public int SomeField { get; set; }

        void ISerializable<SecondComponentDataPo>.OnWritePlainObject(SecondComponentDataPo plainObject, ISerializer serializer)
        {
            plainObject.SomeField = SomeField;
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
