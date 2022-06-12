struct WindowSettings
{
    public int Width;
    public int Height;
    public int Scale;
    public string Title;

    public string Version;
    public int APIversion;

    public bool IsOpenES;
    public bool ShowOpenGLVersion;

    public WindowProjection Projection;

    public float NearPlane;
    public float FarPlane;
    public float FieldOfView;

    public static WindowSettings GetDefault()
    {
        return new WindowSettings()
        {
            // Window Settings
            Width = 640,
            Height = 480,
            Scale = 2,
            Title = "Window",

            // Version Settings
            Version = "0.0.1",
            APIversion = 4,

            // OpenGL Settings
            IsOpenES = false,
            ShowOpenGLVersion = true,

            // Projection Settings
            Projection = WindowProjection.Perspective,

            // Projection Parameters
            NearPlane =  0.1f,
            FarPlane = 100.0f,
            FieldOfView = 45.0f
        };
    }

    public enum WindowProjection
    {
        Orthographic, Perspective
    }
}