using System.Runtime.InteropServices;
using Emission;
using Emission.Core;
using Emission.Core.Mathematics;
using Emission.Core.Memory;
using Emission.Natives.GLFW;
using Emission.Natives.GLFW.Input;

namespace Emission.Engine
{
    public unsafe class Input
    {
        /* Constants */
        public const int KEYBOARD_SIZE = 512;
        public const int MOUSE_SIZE = 16;
        public const int NO_STATE = -1;
        public const int MOUSE_DB_CLICK_COOLDOWN = 1000000000 / 5;

        public const int MAX_CONNECTED_CONTROLLERS = 16;
        public const int CONTROLLER_SIZE = 14;
        public const int CONTROLLER_AXIS = 6;

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

        /// <summary>
        /// Return the number of connected controllers.
        /// </summary>
        public static int ControllerCount => _instance._controllerCount;

        private static Input _instance;
        private static readonly object _padlock = new ();

        private int[] _keyStates;
        private bool[] _activeKeys;

        private int[] _buttonStates;
        private bool[] _activeButtons;

        private float _mouseScroll;
        private float _mouseSensivity;
        private Vector2 _mousePosition;
        private Vector2 _lastMousePosition;
        private long _lastPressedMouse;

        /// <summary>
        /// If element = 0 -> this joystick isn't a controller
        /// If element = 1 -> this joystick is a a controller
        /// </summary>
        private int[] _joysticks;
        private int[] _controllers;
        private int _controllerCount;

        private int _activeController;
        private bool[,] _activateControllersButtons;
        private int[,] _controllersButtonsStates;
        private float[,] _controllersAxes;

        private Input()
        {
            _keyStates = new int[KEYBOARD_SIZE];
            _activeKeys = new bool[KEYBOARD_SIZE];
            
            _buttonStates = new int[MOUSE_SIZE];
            _activeButtons = new bool[MOUSE_SIZE];

            _joysticks = new int[MAX_CONNECTED_CONTROLLERS];
            _controllers = new int[MAX_CONNECTED_CONTROLLERS];

            _activeController = 0;
            _activateControllersButtons = new bool[MAX_CONNECTED_CONTROLLERS, CONTROLLER_SIZE];
            _controllersButtonsStates = new int[MAX_CONNECTED_CONTROLLERS, CONTROLLER_SIZE];
            _controllersAxes = new float[MAX_CONNECTED_CONTROLLERS, CONTROLLER_AXIS];
            UpdateControllers();

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
            
            UpdateControllerState();
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

        public virtual void ControllerCallback((Controllers jid, int @event) args)
        {
            UpdateControllers();
        }
        
        private bool KeyDown(int key) => _activeKeys[key];
        private bool KeyPressed(int key) => _keyStates[key] == (int)InputState.Press;
        private bool KeyReleased(int key) => _keyStates[key] == (int)InputState.Release;

        private void ResetKeys()
        {
            for(int i = 0; i < _keyStates.Length; i++)
            {
                _keyStates[i] = NO_STATE;
            }
        }

        private bool ControllerButtonDown(int controller, int button) => _activateControllersButtons[controller, button];
        private bool ControllerButtonPress(int controller, int button) => _controllersButtonsStates[controller, button] == (int)InputState.Press;
        private bool ControllerButtonRelease(int controller, int button) => _controllersButtonsStates[controller, button] == (int)InputState.Release;

        private float ControllerAxis(int controller, int axis) => _controllersAxes[controller, axis];
        
        private void UpdateControllers()
        {
            int y = 0;
            for (int i = 0; i < MAX_CONNECTED_CONTROLLERS; i++)
            {
                if (Glfw.glfwJoystickPresent(i) == 1)
                {
                    _joysticks[i] = Glfw.glfwJoystickIsGamepad(i);
                    if (_joysticks[i] == Glfw.GLFW_TRUE)
                    {
                        _controllers[y++] = i + 1;
                    }
                }
                else _joysticks[i] = -1;
            }

            _controllerCount = y;
        }

        private void UpdateControllerState()
        {
            for (int i = 0; i < _controllerCount; i++)
            {
                GamePadState s;
                Glfw.glfwGetGamepadState(_controllers[i] - 1, &s);

                for (int j = 0; j < CONTROLLER_SIZE; j++)
                {
                    _controllersButtonsStates[_controllers[i] - 1, j] = s.buttons[j];
                    _activateControllersButtons[_controllers[i] - 1, j] = s.buttons[j] != (int)InputState.Release;
                }

                for (int j = 0; j < CONTROLLER_AXIS; j++)
                {
                    _controllersAxes[_controllers[i] - 1, j] = s.axes[j];
                }
            }
            
        }

        private bool MouseButtonDown(int button) => _activeButtons[button];
        private bool MouseButtonPressed(int button) => _buttonStates[button] == (int)InputState.Press;

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
        
        public static bool IsKeyDown(Keys key) => _instance.KeyDown((int)key);
        public static bool IsKeyPressed(Keys key) => _instance.KeyPressed((int)key); 
        public static bool IsKeyReleased(Keys key) => _instance.KeyReleased((int)key); 

        public static bool IsMouseButtonDown(MouseButton button) => _instance.MouseButtonDown((int)button); 
        public static bool IsMouseButtonPressed(MouseButton button) => _instance.MouseButtonPressed((int)button); 
        public static bool IsMouseButtonReleased(MouseButton button) => _instance.MouseButtonReleased((int)button); 
        public static bool IsMouseButtonDoubleClicked(MouseButton button) => _instance.MouseButtonDoubleClicked((int)button);

        public static bool IsControllerButtonDown(Controllers controller, ControllerButton button) => _instance.ControllerButtonDown((int)controller, (int)button);
        public static bool IsControllerButtonDown(ControllerButton button) => _instance.ControllerButtonDown(_instance._activeController, (int)button);
        public static bool IsControllerButtonRelease(Controllers controllers, ControllerButton button) => _instance.ControllerButtonRelease((int)controllers, (int)button);
        public static bool IsControllerButtonRelease(ControllerButton button) => _instance.ControllerButtonRelease(_instance._activeController, (int)button);
        public static bool IsControllerButtonPress(Controllers controllers, ControllerButton button) => _instance.ControllerButtonPress((int)controllers, (int)button);
        public static bool IsControllerButtonPress(ControllerButton button) => _instance.ControllerButtonPress(_instance._activeController, (int)button);

        public static float GetControllerAxis(Controllers controller, ControllerAxis axis) => _instance.ControllerAxis((int)controller, (int)axis);
        public static float GetControllerAxis(ControllerAxis axis) => _instance.ControllerAxis(_instance._activeController, (int)axis);
        
        public static Controllers[] GetControllers()
        {
            if (_instance._controllers[0] == 0)
                return Array.Empty<Controllers>();

            Controllers[] controllers = new Controllers[_instance._controllerCount];
            for (int i = 0; i < controllers.Length; i++)
            {
                if(_instance._controllers[i] == 0) continue;
                
                controllers[i] = (Controllers)_instance._controllers[i] - 1;
            }

            return controllers;
        }
        
        public static IEnumerable<Controllers> GetControllerEnumerator()
        {
            for (int i = 0; i < _instance._joysticks.Length; i++)
            {
                if (_instance._joysticks[i] == 1) yield return (Controllers)i;
            }
        }

        public static bool IsJoystickController(Controllers controller)
        {
            return _instance._controllers.Contains((int)controller);
        }

        public static string GetControllerName(Controllers controller)
        {
            if(!IsJoystickController(controller))
                Debug.LogWarning($"[WARNING] Cannot get '{controller.ToString()}''s name because it's not a controller!");
            
            string? s = Memory.PtrToStringUtf8(Glfw.glfwGetGamepadName((int)controller));
            return string.IsNullOrEmpty(s) ? "null" : s;
        }

        public static string GetJoystickName(Controllers controller)
        {
            string? s = Memory.PtrToStringUtf8(Glfw.glfwGetJoystickName((int)controller));
            return string.IsNullOrEmpty(s) ? "null" : s;
        }
        
        public static bool HasInstance() => _instance != null;

        public static void CreateInstance()
        {
            lock (_padlock)
            {
                _instance ??= new Input();
            }
        }
        
        public static Input Instance => _instance;
    }
}
