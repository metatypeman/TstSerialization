using System.Text;
using TestSandbox.Helpers;
using TestSandbox.Serialization;

namespace TestSandbox.SerializedObjects
{
    [SocSerialization]
    public partial class ObjWithCollectionsInProps : IObjectToString
    {
        public List<object> ObjListProp { get; set; }

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
            sb.PrintPODList(n, nameof(ObjListProp), ObjListProp);
            //sb.AppendLine($"{spaces}{nameof(SomeField)} = {SomeField}");
            return sb.ToString();
        }
    }
}
