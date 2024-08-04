using System.Text;
using TestSandbox.Helpers;
using TestSandbox.Serialization;

namespace TestSandbox.SerializedObjects
{
    [SocSerialization]
    public partial class Engine : IObjectToString
    {
        public Engine()
        {
            InitContex();
        }

        private void InitContex()
        {
            _engineContext = new EngineContext();
            _engineContext.FirstComponent = new FirstComponent(_engineContext);
            _engineContext.SecondComponent = new SecondComponent(_engineContext);
        }

        public EngineContext _engineContext;

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
            sb.PrintObjProp(n, nameof(_engineContext), _engineContext);
            return sb.ToString();
        }
    }
}
