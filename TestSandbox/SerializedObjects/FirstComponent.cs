using System.Text;
using TestSandbox.Helpers;
using TestSandbox.Serialization;
using TestSandbox.SerializedObjects.PlainObjects;

namespace TestSandbox.SerializedObjects
{
    public class FirstComponent : IObjectToString, ISerializable<FirstComponentPo>
    {
        public FirstComponent(EngineContext engineContext)
        {
            _engineContext = engineContext;
            _data = new FirstComponentData();
        }

        private EngineContext _engineContext;
        public FirstComponentData _data;

        void ISerializable<FirstComponentPo>.OnWritePlainObject(FirstComponentPo plainObject, ISerializer serializer)
        {
            plainObject.Data = serializer.GetSerializedObjectPtr(_data);
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
            sb.PrintObjProp(n, nameof(_data), _data);
            return sb.ToString();
        }
    }
}
