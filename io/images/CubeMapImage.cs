namespace Emission.IO
{
    public class CubeMapImage
    {
        public Image[] Images => _images;
        
        private Image[] _images;
        private string[] _paths;

        public CubeMapImage()
        {
            _paths = new string[6];
        }
    }
}

