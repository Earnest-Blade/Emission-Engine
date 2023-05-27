using Emission;
using Emission.Core;
using Emission.Core.Mathematics;
using Emission.Natives.GLFW.Input;

namespace Emission.Engine
{
    public class Input
    {
        /* Constants */
        public const int KEYBOARD_SIZE = 512;
        public const int MOUSE_SIZE = 16;
        public const int NO_STATE = -1;
        public const int MOUSE_DB_CLICK_COOLDOWN = 1000000000 / 5;

        /// <summary>
        /// Get if a key or a mouse button is pressed. Return a boolean.
        /// Check both variables, if any of these is true, it will return true.
        /// </summary>
        public static bool Any
        {
            get => AnyKey || AnyMouseButton;
        }

        /// <summary>
        /// Get if a keyboard key is pressed. Return a boolean.
        /// Loop throw all active keys, if one of them is true, it will return true.
        /// </summary>
        public static bool AnyKey => _instance._activeKeys.Any(t => t);

        /// <summary>
        /// Return current input Key. Return <see cref="Keys">Keys</cref> Enum.
        /// </summary>
        public static Keys CurrentKey
        {
            get
            {
                if (AnyKey)
                {
                    for (int i = 0; i < _instance._activeKeys.Length; i++)
                    {
                        if (_instance._activeKeys[i]) return (Keys)i;
                    }
                }
                
                return Keys.Unknown;
            }
        }

        /// <summary>
        /// Get if a mouse button is pressed. Return a boolean.
        /// Loop throw all active buttons, if one of them is true, it will return true.
        /// </summary>
        public static bool AnyMouseButton => _instance._activeButtons.Any(t => t);

        /// <summary>
        /// Return input mouse button. Return <see cref="MouseButton">Keys</cref> Enum.
        /// </summary>
        public static MouseButton CurrentButton
        {
            get
            {
                if (AnyMouseButton)
                {
                    for (int i = 0; i < _instance._activeButtons.Length; i++)
                    {
                        if (_instance._activeButtons[i]) return (MouseButton)i;
                    }
                }
                
                return MouseButton.Unknown;
            }
        }

        /// <summary>
        /// Return _instance mouse position on the screen as a <see cref="Vector2"/>.
        /// </summary>
        public static Vector2 MousePosition => _instance._mousePosition;

        /// <summary>
        /// Return value of <see cref="MousePosition"/> before update.
        /// </summary>
        public static Vector2 LastMousePosition => _instance._lastMousePosition;

        /// <summary>
        /// Return the result of the difference between <see cref="MousePosition"/> and <see cref="LastMousePosition"/>.
        /// Use to get mouse movement between two frames.
        /// </summary>
        public static Vector2 DeltaMousePosition => _instance._mousePosition - _instance._lastMousePosition;

        /// <summary>
        /// Return _instance mouse scroll value.
        /// </summary>
        public static float Scroll
        {
            get => _instance._mouseScroll;
        }

        /// <summary>
        /// Binding for mouse sensivity
        /// </summary>
        public static float Sensivity => _instance._mouseSensivity;

        private static Input _instance = null;
        private static readonly object _padlock = new object();

        private int[] _keyStates;
        private bool[] _activeKeys;

        private int[] _buttonStates;
        private bool[] _activeButtons;

        private float _mouseScroll;
        private float _mouseSensivity;
        private Vector2 _mousePosition;
        private Vector2 _lastMousePosition;
        private long _lastPressedMouse;

        private Input()
        {
            _keyStates = new int[KEYBOARD_SIZE];
            _activeKeys = new bool[KEYBOARD_SIZE];
            
            _buttonStates = new int[MOUSE_SIZE];
            _activeButtons = new bool[MOUSE_SIZE];
            
            _mouseScroll = 0;
            _mouseSensivity = 1f;
            _mousePosition = Vector2.Zero;
            _lastMousePosition = Vector2.Zero;
            _lastPressedMouse = 0;
        }

        public void Update()
        {
            ResetKeys();
            ResetMouse();
        }

        public virtual void KeyCallback((Keys keys, InputState action) args)
        {
            if (!HasInstance()) return;
            _activeKeys[(int)args.keys] = args.action != InputState.Release;
            _keyStates[(int)args.keys] = (int)args.action;
        }

        public virtual void MouseCallback((MouseButton mouse, InputState action) args)
        {
            if (!HasInstance()) return;
            _activeButtons[(int)args.mouse] = args.action != InputState.Release;
            _buttonStates[(int)args.mouse] = (int)args.action;
        }

        public virtual void CursorCallback(Vector2 position)
        {
            if (!HasInstance()) return;
            _mousePosition = position;
        }

        public virtual void ScrollCallback(double scroll)
        {
            if(!HasInstance()) return;
            _mouseScroll = (float)scroll;
        }
        
        private bool KeyDown(int key)
        {
            return _activeKeys[key];
        }

        private bool KeyPressed(int key)
        {
            return _keyStates[key] == (int)InputState.Press;
        }

        private bool KeyReleased(int key)
        {
            return _keyStates[key] == (int)InputState.Release;
        }

        private void ResetKeys()
        {
            for(int i = 0; i < _keyStates.Length; i++)
            {
                _keyStates[i] = NO_STATE;
            }
        }

        private bool MouseButtonDown(int button)
        {
            return _activeButtons[button];
        }

        private bool MouseButtonPressed(int button)
        {
            return _buttonStates[button] == (int)InputState.Press;
        }

        private bool MouseButtonReleased(int button)
        {
            bool flag = _buttonStates[button] == (int)InputState.Release;
            if (flag) _lastPressedMouse = Time.NanoTime();
            return flag;
        }

        private bool MouseButtonDoubleClicked(int button)
        {
            bool flag = MouseButtonReleased(button);

            long now = Time.NanoTime();

            if(flag && now - _lastPressedMouse < MOUSE_DB_CLICK_COOLDOWN)
            {
                _lastPressedMouse = 0;
                return true;
            }
            
            return false;
        }
        
        private void ResetMouse()
        {
            _mouseScroll = 0;
            
            for(int i = 0; i < _buttonStates.Length; i++)
            {
                _buttonStates[i] = NO_STATE;
            }

            if(Time.NanoTime() - _lastPressedMouse > MOUSE_DB_CLICK_COOLDOWN)
            {
                _lastPressedMouse = 0;
            }

            ResetDelta();
        }
        
        private void ResetDelta()
        {
            _lastMousePosition = _mousePosition;
        }
        
        public static bool IsKeyDown(Keys key) { return _instance.KeyDown((int)key); }
        public static bool IsKeyPressed(Keys key) { return _instance.KeyPressed((int)key); }
        public static bool IsKeyReleased(Keys key) { return _instance.KeyReleased((int)key); }

        public static bool IsMouseButtonDown(MouseButton button) { return _instance.MouseButtonDown((int)button); }
        public static bool IsMouseButtonPressed(MouseButton button) { return _instance.MouseButtonPressed((int)button); }
        public static bool IsMouseButtonReleased(MouseButton button) { return _instance.MouseButtonReleased((int)button); }
        public static bool IsMouseButtonDoubleClicked(MouseButton button) { return _instance.MouseButtonDoubleClicked((int)button); }

        public static int Axis(Axis a) { return a.IsDown(); }
        public static float Axis(Axis a, float mod) { return a.IsDown(mod); }
        
        public static int AxisPress(Axis a) { return a.IsPress(); }
        public static float AxisPress(Axis a, float mod) { return a.IsPress(mod); }

        public static bool HasInstance() => _instance != null;

        public static Input Instance
        {
            get { lock (_padlock) return _instance ??= new Input(); }
        }
    }
}
