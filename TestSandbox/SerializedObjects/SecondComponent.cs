using System.Text;
using TestSandbox.Helpers;
using TestSandbox.Serialization;
using TestSandbox.SerializedObjects.PlainObjects;

namespace TestSandbox.SerializedObjects
{
    public class SecondComponent: IObjectToString, ISerializable<SecondComponentPo>
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

        void ISerializable<SecondComponentPo>.OnWritePlainObject(SecondComponentPo plainObject, ISerializer serializer)
        {
            plainObject.Data = serializer.GetSerializedObjectPtr(_data);
        }

        void ISerializable<SecondComponentPo>.OnReadPlainObject(SecondComponentPo plainObject, ISerializer serializer)
        {
            _data = serializer.GetDeserializedObject<SecondComponentData, SecondComponentDataPo>(plainObject.Data);
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
