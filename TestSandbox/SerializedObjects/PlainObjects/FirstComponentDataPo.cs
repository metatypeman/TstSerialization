using System.Text;
using TestSandbox.Helpers;

namespace TestSandbox.SerializedObjects.PlainObjects
{
    public class FirstComponentDataPo: IObjectToString
    {
        public int Field1 { get; set; }

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
