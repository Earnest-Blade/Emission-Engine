namespace Emission
{
    public class Axis
    {
        public static Axis Horizontal = new Axis(Keys.Left, Keys.Right);
        public static Axis Vertical = new Axis(Keys.Down, Keys.Up);
        public static Axis UpDown = new Axis(Keys.LeftShift, Keys.Space);

        private Keys _positiveKey;
        private Keys _negativeKey;

        public Axis(Keys negativeKey, Keys positiveKey)
        {
            this._positiveKey = positiveKey;
            this._negativeKey = negativeKey;
        }

        public int IsDown()
        {
            if (Input.IsKeyDown(_negativeKey)) return -1;
            if (Input.IsKeyDown(_positiveKey)) return 1;
            return 0;
        }
        
        public float IsDown(float strength)
        {
            if (Input.IsKeyDown(_negativeKey)) return -strength;
            if (Input.IsKeyDown(_positiveKey)) return strength;
            return 0;
        }

        public int IsPress()
        {
            if (Input.IsKeyPressed(_negativeKey)) return -1;
            if (Input.IsKeyPressed(_positiveKey)) return 1;
            return 0;
        }
        
        public float IsPress(float strength)
        {
            if (Input.IsKeyPressed(_negativeKey)) return -strength;
            if (Input.IsKeyPressed(_positiveKey)) return strength;
            return 0;
        }

        public static Axis operator +(Axis a) => a;
        public static Axis operator -(Axis a) => new Axis(a._positiveKey, a._negativeKey);
    }
}
