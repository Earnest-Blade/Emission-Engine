namespace Emission
{
    public unsafe struct GamePadState
    {
        public fixed byte buttons[15];
        public fixed float axes[6];
    }
}
