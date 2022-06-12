using System;

using OpenTK.Windowing.GraphicsLibraryFramework;

using Emission.Math;

namespace Emission
{
    class Input
    {
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

        private static readonly Input current = new Input();

        // Keyboard arrays
        private int[] _keyStates = new int[KEYBOARD_SIZE];
        private bool[] _activeKeys = new bool[KEYBOARD_SIZE];

        // Mouse arrays
        private int[] _mouseButtonStates = new int[MOUSE_SIZE];
        private bool[] _activeMouseButtons = new bool[MOUSE_SIZE];
        private long _lastPressedMouseTime = 0;
        private long _mouseDoubleClickPeriod = 1000000000 / 5;

        private Input()
        {
            resetKeys();
            resetMouse();
        }

        public void Update()
        {
            resetKeys();
            resetMouse();

            GLFW.PollEvents();
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

        private void resetKeys()
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

        private void resetMouse()
        {
            for(int i = 0; i < _mouseButtonStates.Length; i++)
            {
                _mouseButtonStates[i] = NO_STATE;
            }

            if(Time.NanoTime() - _lastPressedMouseTime > _mouseDoubleClickPeriod)
            {
                _lastPressedMouseTime = 0;
            }
        }

        public static bool IsKeyDown(Keys key) { return Current.KeyDown((int)key); }
        public static bool IsKeyPressed(Keys key) { return Current.KeyPressed((int)key); }
        public static bool IsKeyReleased(Keys key) { return Current.KeyReleased((int)key); }

        public static bool IsMouseButtonDown(MouseButton button) { return Current.MouseButtonDown((int)button); }
        public static bool IsMouseButtonPressed(MouseButton button) { return Current.MouseButtonPressed((int)button); }
        public static bool IsMouseButtonReleased(MouseButton button) { return Current.MouseButtonReleased((int)button); }
        public static bool IsMouseButtonDoubleClicked(MouseButton button) { return Current.MouseButtonDoubleClicked((int)button); }

        public static int Axis(Axis a) { return a.IsDown(); }

        public static unsafe void KeyCallback(OpenTK.Windowing.GraphicsLibraryFramework.Window* window, OpenTK.Windowing.GraphicsLibraryFramework.Keys key, int scanCode, InputAction action, KeyModifiers mod)
        {
            if (current != null)
            {
                Current._activeKeys[(int)key] = (action != InputAction.Release);
                Current._keyStates[(int)key] = (int)action;
            }
        }

        public static unsafe void MouseCallback(OpenTK.Windowing.GraphicsLibraryFramework.Window* window, OpenTK.Windowing.GraphicsLibraryFramework.MouseButton button, InputAction action, KeyModifiers mod)
        {
            if(current != null)
            {
                Current._activeMouseButtons[(int)button] = (action != InputAction.Release);
                Current._mouseButtonStates[(int)button] = (int)action;
            }
        }

        public static Input Current
        {
            get => current;
        }
    }

    class Axis
    {
        public static Axis Horizontal = new Axis("Horizontal", Keys.Left, Keys.Right);
        public static Axis Vertical = new Axis("Vertical", Keys.Down, Keys.Up);
        public static Axis UpDown = new Axis("UpDown", Keys.LeftShift, Keys.Space);

        public string Name { get; }

        private Keys _positiveKey;
        private Keys _negativeKey;

        public Axis(string name, Keys negativeKey, Keys positiveKey)
        {
            this._positiveKey = positiveKey;
            this._negativeKey = negativeKey;
        }

        public int IsDown()
        {
            if (Input.IsKeyDown(_negativeKey)) return -1;
            else if (Input.IsKeyDown(_positiveKey)) return 1;
            else return 0;
        }
    }
}
