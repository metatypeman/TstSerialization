using System.Text;
using TestSandbox.Helpers;
using TestSandbox.Serialization;
using TestSandbox.SerializedObjects.PlainObjects;

namespace TestSandbox.SerializedObjects
{
    public class Engine : IObjectToString, ISerializable<EnginePo>
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

        void ISerializable<EnginePo>.OnWritePlainObject(EnginePo plainObject, ISerializer serializer)
        {
            plainObject.EngineContext = serializer.GetSerializedObjectPtr(_engineContext);
        }

        void ISerializable<EnginePo>.OnReadPlainObject(EnginePo plainObject, ISerializer serializer)
        {
            _engineContext = serializer.GetDeserializedObject<EngineContext, EngineContextPo>(plainObject.EngineContext);
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
            sb.PrintObjProp(n, nameof(_engineContext), _engineContext);
            return sb.ToString();
        }
    }
}
