namespace Emission.Engine
{
    public delegate void EmissionHandler();
    public delegate void EmissionHandler<TEvent>(TEvent args);
}
