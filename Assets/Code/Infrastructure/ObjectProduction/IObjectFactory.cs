namespace Code.Infrastructure.ObjectProduction
{
    public interface IObjectFactory
    {
        T Construct<T>();
    }
}