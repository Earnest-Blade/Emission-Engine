using System.Collections.Generic;
using System.IO;

namespace Emission.IO
{
    public class Data
    {
        private List<Bundle> _bundles;

        public Data()
        {
            _bundles = new List<Bundle>();
/*
            //TODO: Check for errors
            foreach (var file in GameDirectory.EnumerateFiles(GameFile.DATA_FILE))
            {
                _bundles.Add(Bundle.Load(file));
            }*/
        }

        public Bundle Find(string name)
        {
            return null;
            //return _bundles.Find(x => x.Path == name);
        }
    }
}
