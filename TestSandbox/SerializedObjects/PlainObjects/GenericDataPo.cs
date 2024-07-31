using System.Text;
using TestSandbox.Helpers;
using TestSandbox.Serialization;

namespace TestSandbox.SerializedObjects.PlainObjects
{
    public class GenericDataPo : IObjectToString
    {
        public ObjectPtr Value { get; set; }

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
