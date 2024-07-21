﻿namespace TestSandbox.Serialization
{
    public interface IDeserializer
    {
        T Deserialize<T>()
            where T : ISerializable, new();

        T GetDeserializedObject<T>(ObjectPtr objectPtr)
            where T : ISerializable, new();
    }
}
