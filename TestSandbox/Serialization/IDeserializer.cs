﻿namespace TestSandbox.Serialization
{
    public interface IDeserializer
    {
        T Deserialize<T>();

        T GetDeserializedObject<T>(ObjectPtr objectPtr);
    }
}
