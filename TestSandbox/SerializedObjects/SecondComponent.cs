using System.Text;
using TestSandbox.Helpers;
using TestSandbox.Serialization;

namespace TestSandbox.SerializedObjects
{
    [SocSerialization]
    public partial class SecondComponent: IObjectToString
    {
        public SecondComponent()
        {
        }

        public SecondComponent(EngineContext engineContext)
        {
            _engineContext = engineContext;
            _data = new SecondComponentData();
        }

        private EngineContext _engineContext;

        public SecondComponentData _data;

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
            sb.PrintObjProp(n, nameof(_data), _data);
            return sb.ToString();
        }
    }
}
