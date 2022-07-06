using System;

using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

using Emission.Math;

namespace Emission
{
    class Input
    {
        // Constants
        public const int KEYBOARD_SIZE = 512;
        public const int MOUSE_SIZE = 16;
        public const int NO_STATE = -1;
        
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
        public static bool AnyKey
        {
            get
            {
                for(int i = 0; i < Current._activeKeys.Length; i++)
                {
                    if (Current._activeKeys[i] == true) return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Return current input Key. Return <see cref="Keys">Keys</cref> Enum.
        /// </summary>
        public static Keys CurrentKey
        {
            get
            {
                if (AnyKey)
                {
                    for (int i = 0; i < Current._activeKeys.Length; i++)
                    {
                        if (Current._activeKeys[i] == true) return (Keys)i;
                    }
                }
                
                return Keys.Unknown;
            }
        }

        /// <summary>
        /// Get if a mouse button is pressed. Return a boolean.
        /// Loop throw all active buttons, if one of them is true, it will return true.
        /// </summary>
        public static bool AnyMouseButton
        {
            get
            {
                for (int i = 0; i < Current._activeMouseButtons.Length; i++)
                {
                    if (Current._activeMouseButtons[i] == true) return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Return current input mouse button. Return <see cref="MouseButton">Keys</cref> Enum.
        /// </summary>
        public static MouseButton CurrentMouseButton
        {
            get
            {
                if (AnyMouseButton)
                {
                    for (int i = 0; i < Current._activeMouseButtons.Length; i++)
                    {
                        if (Current._activeMouseButtons[i] == true) return (MouseButton)i;
                    }
                }
                
                return MouseButton.Unknown;
            }
        }

        /// <summary>
        /// Return current mouse position on the screen as a <see cref="Vector2"/>.
        /// </summary>
        public static Vector2 MousePosition
        {
            get => Current._mousePosition;
        }

        public static Vector2 MousePositionCenter
        {
            get => Current._mousePosition - Window.Current.WindowSize / 2;
        }
        
        /// <summary>
        /// Return value of <see cref="MousePosition"/> before update.
        /// </summary>
        public static Vector2 LastMousePosition
        {
            get => Current._lastMousePosition;
        }
        
        public static Vector2 LastMousePositionCenter
        {
            get => Current._lastMousePosition - Window.Current.WindowSize / 2;
        }
        
        /// <summary>
        /// Return the result of the difference between <see cref="MousePosition"/> and <see cref="LastMousePosition"/>.
        /// Use to get mouse movement between two frames.
        /// </summary>
        public static Vector2 DeltaMousePosition
        {
            get => Current._mousePosition - Current._lastMousePosition;
        }
        
        public static Vector2 DeltaMousePositionCenter
        {
            get
            {
                if (DeltaMousePosition == Vector2.Zero)  return Vector2.Zero;
                return DeltaMousePosition - (Window.Current.WindowSize / 2);
            }
        }

        /// <summary>
        /// Return current mouse scroll value.
        /// </summary>
        public static float Scroll
        {
            get => Current._mouseScroll;
        }

        /// <summary>
        /// Binding for mouse sensivity
        /// </summary>
        public static float Sensivity => Current._mouseSensivity;
        
        private static readonly Input current = new Input();

        // Keyboard arrays
        private int[] _keyStates = new int[KEYBOARD_SIZE];
        private bool[] _activeKeys = new bool[KEYBOARD_SIZE];

        // Mouse arrays
        private int[] _mouseButtonStates = new int[MOUSE_SIZE];
        private bool[] _activeMouseButtons = new bool[MOUSE_SIZE];
        
        // Mouse state
        private float _mouseScroll = 0;
        private float _mouseSensivity = 2f;
        private Vector2 _mousePosition = Vector2.Zero;
        private Vector2 _lastMousePosition = Vector2.Zero;
        private long _lastPressedMouseTime = 0;
        private long _mouseDoubleClickPeriod = 1000000000 / 5;
        
        private Input() { }

        public void Update()
        {
            ResetKeys();
            ResetMouse();
        }

        private bool KeyDown(int key)
        {
            return _activeKeys[key];
        }

        private bool KeyPressed(int key)
        {
            return _keyStates[key] == (int)InputAction.Press;
        }

        private bool KeyReleased(int key)
        {
            return _keyStates[key] == (int)InputAction.Release;
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
            return _activeMouseButtons[button];
        }

        private bool MouseButtonPressed(int button)
        {
            return _mouseButtonStates[button] == (int)InputAction.Press;
        }

        private bool MouseButtonReleased(int button)
        {
            bool flag = _mouseButtonStates[button] == (int)InputAction.Release;
            if (flag) _lastPressedMouseTime = Time.NanoTime();
            return flag;
        }

        private bool MouseButtonDoubleClicked(int button)
        {
            bool flag = MouseButtonReleased(button);

            long now = Time.NanoTime();

            if(flag && now - _lastPressedMouseTime < _mouseDoubleClickPeriod)
            {
                _lastPressedMouseTime = 0;
                return true;
            }
            return false;
        }

        private void ResetMouse()
        {
            _mouseScroll = 0;
            
            for(int i = 0; i < _mouseButtonStates.Length; i++)
            {
                _mouseButtonStates[i] = NO_STATE;
            }

            if(Time.NanoTime() - _lastPressedMouseTime > _mouseDoubleClickPeriod)
            {
                _lastPressedMouseTime = 0;
            }

            _lastMousePosition = _mousePosition;
        }

        public static bool IsKeyDown(Keys key) { return Current.KeyDown((int)key); }
        public static bool IsKeyPressed(Keys key) { return Current.KeyPressed((int)key); }
        public static bool IsKeyReleased(Keys key) { return Current.KeyReleased((int)key); }

        public static bool IsMouseButtonDown(MouseButton button) { return Current.MouseButtonDown((int)button); }
        public static bool IsMouseButtonPressed(MouseButton button) { return Current.MouseButtonPressed((int)button); }
        public static bool IsMouseButtonReleased(MouseButton button) { return Current.MouseButtonReleased((int)button); }
        public static bool IsMouseButtonDoubleClicked(MouseButton button) { return Current.MouseButtonDoubleClicked((int)button); }

        public static int Axis(Axis a) { return a.IsDown(); }
        public static float Axis(Axis a, float mod) { return a.IsDown(mod); }
        
        public static int AxisPress(Axis a) { return a.IsPress(); }
        public static float AxisPress(Axis a, float mod) { return a.IsPress(mod); }

        public static OpenTK.Windowing.GraphicsLibraryFramework.Keys ToOpenGLKey(Keys k)
        {
            return (OpenTK.Windowing.GraphicsLibraryFramework.Keys)((int)k);
        }

        public virtual void KeyCallback(OpenTK.Windowing.GraphicsLibraryFramework.Keys key, int scanCode, 
            InputAction action, KeyModifiers mod)
        {
            if (current == null) return;
            Current._activeKeys[(int)key] = (action != InputAction.Release);
            Current._keyStates[(int)key] = (int)action;
        }

        public virtual void MouseCallback( OpenTK.Windowing.GraphicsLibraryFramework.MouseButton button, 
            InputAction action, KeyModifiers mod)
        {
            if (current == null) return;
            Current._activeMouseButtons[(int)button] = (action != InputAction.Release);
            Current._mouseButtonStates[(int)button] = (int)action;
        }

        public virtual void CursorPosition(double x, double y)
        {
            if (current == null) return;
            Current._mousePosition = new Vector2((float)x, (float)y);
        }
        
        public virtual void ScrollCallback(double y)
        {
            if (current == null) return;
            Current._mouseScroll = (float)y;
        }

        public static Input Current
        {
            get => current;
        }
    }

    class Axis
    {
        public static Axis Horizontal = new Axis( Keys.Left, Keys.Right);
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
        
        public float IsDown(float mod)
        {
            if (Input.IsKeyDown(_negativeKey)) return -mod;
            if (Input.IsKeyDown(_positiveKey)) return mod;
            return 0;
        }

        public int IsPress()
        {
            if (Input.IsKeyPressed(_negativeKey)) return -1;
            if (Input.IsKeyPressed(_positiveKey)) return 1;
            return 0;
        }
        
        public float IsPress(float mod)
        {
            if (Input.IsKeyPressed(_negativeKey)) return -mod;
            if (Input.IsKeyPressed(_positiveKey)) return mod;
            return 0;
        }

        public static Axis operator +(Axis a) => a;
        public static Axis operator -(Axis a) => new Axis(a._positiveKey, a._negativeKey);
    }
}
