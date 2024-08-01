namespace TestSandbox.Serialization
{
    public readonly struct ObjectPtr
    {
        public ObjectPtr(bool isNull)
            : this(null, null, isNull)
        {
        }

        public ObjectPtr(string id, string typeName)
            : this(id, typeName, false)
        {
        }

        public ObjectPtr(string id, string typeName, bool isNull)
        {
            Id = id;
            TypeName = typeName;
            IsNull = isNull;
        }

        public string Id { get; init; }
        public string TypeName { get; init; }
        public bool IsNull { get; init; }

        public override string ToString() => $"({nameof(Id)}: '{Id}', {nameof(TypeName)}: '{TypeName}', {nameof(IsNull)}: {IsNull})";
    }
}
