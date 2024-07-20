using System.Text;
using TestSandbox.Helpers;
using TestSandbox.Serialization;
using TestSandbox.SerializedObjects.PlainObjects;

namespace TestSandbox.SerializedObjects
{
    public class EngineContext : IObjectToString, ISerializable<EngineContextPo>
    {
        public FirstComponent FirstComponent { get; set; }
        public SecondComponent SecondComponent { get; set; }

        void ISerializable<EngineContextPo>.OnWritePlainObject(EngineContextPo plainObject, ISerializer serializer)
        {
            plainObject.FirstComponent = serializer.GetSerializedObjectPtr(FirstComponent);
            plainObject.SecondComponent = serializer.GetSerializedObjectPtr(SecondComponent);
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
