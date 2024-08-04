using System.Text;
using TestSandbox.Helpers;
using TestSandbox.Serialization;

namespace TestSandbox.SerializedObjects
{
    [SocSerialization]
    public partial class SecondComponentData : IObjectToString
    {
        public int SomeField { get; set; }

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
