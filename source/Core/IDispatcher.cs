namespace Emission.Core;

public interface IDispatcher<T>
{
    public Stack<T> Stack { get; }

    public void Attach(T item);
    
    public void Clear();
    public bool Contains(T item);
}