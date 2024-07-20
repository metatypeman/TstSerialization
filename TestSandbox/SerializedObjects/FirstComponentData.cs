using System.Text;
using TestSandbox.Helpers;
using TestSandbox.Serialization;
using TestSandbox.SerializedObjects.PlainObjects;

namespace TestSandbox.SerializedObjects
{
    public class FirstComponentData: IObjectToString, ISerializable<FirstComponentDataPo>
    {
        public int Field1 { get; set; }

        void ISerializable<FirstComponentDataPo>.OnWritePlainObject(FirstComponentDataPo plainObject, ISerializer serializer)
        {
            plainObject.Field1 = Field1;
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
            sb.AppendLine($"{spaces}{nameof(Field1)} = {Field1}");
            return sb.ToString();
        }
    }
}
