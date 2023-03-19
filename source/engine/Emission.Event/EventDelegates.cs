namespace Emission
{
    public delegate void EmissionHandler();
    public delegate void EmissionHandler<TEvent>(TEvent args);
}
