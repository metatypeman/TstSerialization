namespace TestSandbox.SerializedObjects
{
    public class FirstComponent
    {
        public FirstComponent(EngineContext engineContext)
        {
            _engineContext = engineContext;
            _data = new FirstComponentData();
        }

        private EngineContext _engineContext;
        private FirstComponentData _data;
    }
}
