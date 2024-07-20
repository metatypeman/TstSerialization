using System.Text;
using TestSandbox.Helpers;
using TestSandbox.Serialization;

namespace TestSandbox.SerializedObjects.PlainObjects
{
    public class EngineContextPo: IObjectToString
    {
        public ObjectPtr FirstComponent {  get; set; }
        public ObjectPtr SecondComponent { get; set; }

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
            sb.AppendLine($"{spaces}{nameof(FirstComponent)} = {FirstComponent}");
            sb.AppendLine($"{spaces}{nameof(SecondComponent)} = {SecondComponent}");
            return sb.ToString();
        }
    }
}
