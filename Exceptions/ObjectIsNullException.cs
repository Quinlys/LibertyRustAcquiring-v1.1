namespace LibertyRustAcquiring.Exceptions
{
    public class ObjectIsNullException<T> : Exception
    {
        private readonly string typeName;
        public ObjectIsNullException() : base()
        {
            typeName = typeof(T).Name;
        }

        public override string Message => $"Object of {typeName} type is not found.";
    }
}
