using System.Text;
using TestSandbox.Helpers;

namespace TestSandbox.SerializedObjects
{
    public class FirstComponent : IObjectToString
    {
        public FirstComponent(EngineContext engineContext)
        {
            _engineContext = engineContext;
            _data = new FirstComponentData();
        }

        private EngineContext _engineContext;
        private FirstComponentData _data;

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
