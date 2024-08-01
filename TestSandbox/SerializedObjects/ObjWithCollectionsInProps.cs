using System.Text;
using TestSandbox.Helpers;
using TestSandbox.Serialization;
using TestSandbox.SerializedObjects.PlainObjects;

namespace TestSandbox.SerializedObjects
{
    public class ObjWithCollectionsInProps : IObjectToString, ISerializable
    {
        public List<object> ObjListProp { get; set; }

        Type ISerializable.GetPlainObjectType() => typeof(ObjWithCollectionsInPropsPo);

        void ISerializable.OnWritePlainObject(object plainObject, ISerializer serializer)
        {
            OnWritePlainObject((ObjWithCollectionsInPropsPo)plainObject, serializer);
        }

        void OnWritePlainObject(ObjWithCollectionsInPropsPo plainObject, ISerializer serializer)
        {
            plainObject.ObjListProp = serializer.GetSerializedObjectPtr(ObjListProp);

            //plainObject.SomeField = SomeField;
        }

        void ISerializable.OnReadPlainObject(object plainObject, IDeserializer deserializer)
        {
            OnReadPlainObject((ObjWithCollectionsInPropsPo)plainObject, deserializer);
        }

        void OnReadPlainObject(ObjWithCollectionsInPropsPo plainObject, IDeserializer deserializer)
        {
            ObjListProp = deserializer.GetDeserializedObject<List<object>>(plainObject.ObjListProp);

            //SomeField = plainObject.SomeField;
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
            sb.PrintPODList(n, nameof(ObjListProp), ObjListProp);
            //sb.AppendLine($"{spaces}{nameof(SomeField)} = {SomeField}");
            return sb.ToString();
        }
    }
}
