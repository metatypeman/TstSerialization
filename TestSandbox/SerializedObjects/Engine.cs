namespace TestSandbox.SerializedObjects
{
    public class Engine
    {
        public Engine()
        {
            InitContex();
        }

        private void InitContex()
        {
            _engineContext = new EngineContext();
            _engineContext.FirstComponent = new FirstComponent(_engineContext);
            _engineContext.SecondComponent = new SecondComponent(_engineContext);
        }

        private EngineContext _engineContext;
    }
}
