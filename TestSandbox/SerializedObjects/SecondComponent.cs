namespace TestSandbox.SerializedObjects
{
    public class SecondComponent
    {
        public SecondComponent(EngineContext engineContext)
        {
            _engineContext = engineContext;
            _data = new SecondComponentData();
        }

        private EngineContext _engineContext;

        private SecondComponentData _data;
    }
}
