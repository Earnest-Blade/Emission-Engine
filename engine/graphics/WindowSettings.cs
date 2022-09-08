public struct WindowSettings
{
    public int Width;
    public int Height;
    public int XPos;
    public int YPos;
    public float Scale;
    public string Title;

    public string Version;
    public int APIversion;

    public bool IsOpenES;
    public bool ShowOpenGLVersion;

    public GraphicProjectionMode ProjectionMode;

    public float NearDepth;
    public float FarDepth;
    public float FieldOfView;

    public static WindowSettings GetDefault()
    {
        return new WindowSettings()
        {
            // Window Settings
            Width = 1920,
            Height = 1080,
            XPos = 50,
            YPos = 50,
            Scale = 1f,
            Title = "Window",

            // Version Settings
            Version = "0.0.1",
            APIversion = 4,

            // OpenGL Settings
            IsOpenES = false,
            ShowOpenGLVersion = true,

            // Projection Settings
            ProjectionMode = GraphicProjectionMode.Perspective,

            // Projection Parameters
            NearDepth =  0.1f,
            FarDepth = 100.0f,
            FieldOfView = 45.0f
        };
    }

    public enum GraphicProjectionMode
    {
        Orthographic, Perspective
    }
}