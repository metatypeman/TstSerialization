using System.Text;
using TestSandbox.Helpers;
using TestSandbox.Serialization;
using TestSandbox.SerializedObjects.PlainObjects;

namespace TestSandbox.SerializedObjects
{
    public class EngineContext : IObjectToString, ISerializable
    {
        public FirstComponent FirstComponent { get; set; }
        public SecondComponent SecondComponent { get; set; }

        Type ISerializable.GetPlainObjectType() => typeof(EngineContextPo);

        void ISerializable.OnWritePlainObject(object plainObject, ISerializer serializer)
        {
            OnWritePlainObject((EngineContextPo)plainObject, serializer);
        }

        void OnWritePlainObject(EngineContextPo plainObject, ISerializer serializer)
        {
            plainObject.FirstComponent = serializer.GetSerializedObjectPtr(FirstComponent);
            plainObject.SecondComponent = serializer.GetSerializedObjectPtr(SecondComponent);
        }

        void ISerializable.OnReadPlainObject(object plainObject, ISerializer serializer)
        {
            OnReadPlainObject((EngineContextPo)plainObject, serializer);
        }

        void OnReadPlainObject(EngineContextPo plainObject, ISerializer serializer)
        {
            FirstComponent = serializer.GetDeserializedObject<FirstComponent>(plainObject.FirstComponent);
            SecondComponent = serializer.GetDeserializedObject<SecondComponent>(plainObject.SecondComponent);
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
            sb.PrintObjProp(n, nameof(FirstComponent), FirstComponent);
            sb.PrintObjProp(n, nameof(SecondComponent), SecondComponent);
            return sb.ToString();
        }
    }
}
