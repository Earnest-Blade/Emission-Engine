#region License
/*
	Copyright (c) 2002-2006 Marcus Geelnard
	Copyright (c) 2006-2019 Camilla Löwy
	This software is provided 'as-is', without any express or implied
	warranty. In no event will the authors be held liable for any damages
	arising from the use of this software.
	Permission is granted to anyone to use this software for any purpose,
	including commercial applications, and to alter it and redistribute it
	freely, subject to the following restrictions:
	1. The origin of this software must not be misrepresented; you must not
	   claim that you wrote the original software. If you use this software
	   in a product, an acknowledgment in the product documentation would
	   be appreciated but is not required.
	2. Altered source versions must be plainly marked as such, and must not
	   be misrepresented as being the original software.
	3. This notice may not be removed or altered from any source
	   distribution. 
*/
#endregion

using System;
using System.Security;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Emission.IO;
using Emission.Window;

namespace Emission.Natives.GLFW
{
    /// <summary>
    /// The base class the vast majority of the GLFW functions.
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public static unsafe class Glfw
    {
        #region Fields / Properties
		private static IntPtr _handle;
		#endregion

		#region Constants
		public const int GLFW_VERSION_MAJOR = 3;
		public const int GLFW_VERSION_MINOR = 3;
		public const int GLFW_VERSION_REVISION = 2;
		public const int GLFW_TRUE = 1;
		public const int GLFW_FALSE = 0;
		public const int GLFW_RELEASE = 0;
		public const int GLFW_PRESS = 1;
		public const int GLFW_REPEAT = 2;
		public const int GLFW_HAT_CENTERED = 0;
		public const int GLFW_HAT_UP = 1;
		public const int GLFW_HAT_RIGHT = 2;
		public const int GLFW_HAT_DOWN = 4;
		public const int GLFW_HAT_LEFT = 8;
		public const int GLFW_KEY_UNKNOWN = -1;
		public const int GLFW_KEY_SPACE = 32;
		public const int GLFW_KEY_APOSTROPHE = 39;
		public const int GLFW_KEY_COMMA = 44;
		public const int GLFW_KEY_MINUS = 45;
		public const int GLFW_KEY_PERIOD = 46;
		public const int GLFW_KEY_SLASH = 47;
		public const int GLFW_KEY_0 = 48;
		public const int GLFW_KEY_1 = 49;
		public const int GLFW_KEY_2 = 50;
		public const int GLFW_KEY_3 = 51;
		public const int GLFW_KEY_4 = 52;
		public const int GLFW_KEY_5 = 53;
		public const int GLFW_KEY_6 = 54;
		public const int GLFW_KEY_7 = 55;
		public const int GLFW_KEY_8 = 56;
		public const int GLFW_KEY_9 = 57;
		public const int GLFW_KEY_SEMICOLON = 59;
		public const int GLFW_KEY_EQUAL = 61;
		public const int GLFW_KEY_A = 65;
		public const int GLFW_KEY_B = 66;
		public const int GLFW_KEY_C = 67;
		public const int GLFW_KEY_D = 68;
		public const int GLFW_KEY_E = 69;
		public const int GLFW_KEY_F = 70;
		public const int GLFW_KEY_G = 71;
		public const int GLFW_KEY_H = 72;
		public const int GLFW_KEY_I = 73;
		public const int GLFW_KEY_J = 74;
		public const int GLFW_KEY_K = 75;
		public const int GLFW_KEY_L = 76;
		public const int GLFW_KEY_M = 77;
		public const int GLFW_KEY_N = 78;
		public const int GLFW_KEY_O = 79;
		public const int GLFW_KEY_P = 80;
		public const int GLFW_KEY_Q = 81;
		public const int GLFW_KEY_R = 82;
		public const int GLFW_KEY_S = 83;
		public const int GLFW_KEY_T = 84;
		public const int GLFW_KEY_U = 85;
		public const int GLFW_KEY_V = 86;
		public const int GLFW_KEY_W = 87;
		public const int GLFW_KEY_X = 88;
		public const int GLFW_KEY_Y = 89;
		public const int GLFW_KEY_Z = 90;
		public const int GLFW_KEY_LEFT_BRACKET = 91;
		public const int GLFW_KEY_BACKSLASH = 92;
		public const int GLFW_KEY_RIGHT_BRACKET = 93;
		public const int GLFW_KEY_GRAVE_ACCENT = 96;
		public const int GLFW_KEY_WORLD_1 = 161;
		public const int GLFW_KEY_WORLD_2 = 162;
		public const int GLFW_KEY_ESCAPE = 256;
		public const int GLFW_KEY_ENTER = 257;
		public const int GLFW_KEY_TAB = 258;
		public const int GLFW_KEY_BACKSPACE = 259;
		public const int GLFW_KEY_INSERT = 260;
		public const int GLFW_KEY_DELETE = 261;
		public const int GLFW_KEY_RIGHT = 262;
		public const int GLFW_KEY_LEFT = 263;
		public const int GLFW_KEY_DOWN = 264;
		public const int GLFW_KEY_UP = 265;
		public const int GLFW_KEY_PAGE_UP = 266;
		public const int GLFW_KEY_PAGE_DOWN = 267;
		public const int GLFW_KEY_HOME = 268;
		public const int GLFW_KEY_END = 269;
		public const int GLFW_KEY_CAPS_LOCK = 280;
		public const int GLFW_KEY_SCROLL_LOCK = 281;
		public const int GLFW_KEY_NUM_LOCK = 282;
		public const int GLFW_KEY_PRINT_SCREEN = 283;
		public const int GLFW_KEY_PAUSE = 284;
		public const int GLFW_KEY_F1 = 290;
		public const int GLFW_KEY_F2 = 291;
		public const int GLFW_KEY_F3 = 292;
		public const int GLFW_KEY_F4 = 293;
		public const int GLFW_KEY_F5 = 294;
		public const int GLFW_KEY_F6 = 295;
		public const int GLFW_KEY_F7 = 296;
		public const int GLFW_KEY_F8 = 297;
		public const int GLFW_KEY_F9 = 298;
		public const int GLFW_KEY_F10 = 299;
		public const int GLFW_KEY_F11 = 300;
		public const int GLFW_KEY_F12 = 301;
		public const int GLFW_KEY_F13 = 302;
		public const int GLFW_KEY_F14 = 303;
		public const int GLFW_KEY_F15 = 304;
		public const int GLFW_KEY_F16 = 305;
		public const int GLFW_KEY_F17 = 306;
		public const int GLFW_KEY_F18 = 307;
		public const int GLFW_KEY_F19 = 308;
		public const int GLFW_KEY_F20 = 309;
		public const int GLFW_KEY_F21 = 310;
		public const int GLFW_KEY_F22 = 311;
		public const int GLFW_KEY_F23 = 312;
		public const int GLFW_KEY_F24 = 313;
		public const int GLFW_KEY_F25 = 314;
		public const int GLFW_KEY_KP_0 = 320;
		public const int GLFW_KEY_KP_1 = 321;
		public const int GLFW_KEY_KP_2 = 322;
		public const int GLFW_KEY_KP_3 = 323;
		public const int GLFW_KEY_KP_4 = 324;
		public const int GLFW_KEY_KP_5 = 325;
		public const int GLFW_KEY_KP_6 = 326;
		public const int GLFW_KEY_KP_7 = 327;
		public const int GLFW_KEY_KP_8 = 328;
		public const int GLFW_KEY_KP_9 = 329;
		public const int GLFW_KEY_KP_DECIMAL = 330;
		public const int GLFW_KEY_KP_DIVIDE = 331;
		public const int GLFW_KEY_KP_MULTIPLY = 332;
		public const int GLFW_KEY_KP_SUBTRACT = 333;
		public const int GLFW_KEY_KP_ADD = 334;
		public const int GLFW_KEY_KP_ENTER = 335;
		public const int GLFW_KEY_KP_EQUAL = 336;
		public const int GLFW_KEY_LEFT_SHIFT = 340;
		public const int GLFW_KEY_LEFT_CONTROL = 341;
		public const int GLFW_KEY_LEFT_ALT = 342;
		public const int GLFW_KEY_LEFT_SUPER = 343;
		public const int GLFW_KEY_RIGHT_SHIFT = 344;
		public const int GLFW_KEY_RIGHT_CONTROL = 345;
		public const int GLFW_KEY_RIGHT_ALT = 346;
		public const int GLFW_KEY_RIGHT_SUPER = 347;
		public const int GLFW_KEY_MENU = 348;
		public const int GLFW_MOD_SHIFT = 0x0001;
		public const int GLFW_MOD_CONTROL = 0x0002;
		public const int GLFW_MOD_ALT = 0x0004;
		public const int GLFW_MOD_SUPER = 0x0008;
		public const int GLFW_MOD_CAPS_LOCK = 0x0010;
		public const int GLFW_MOD_NUM_LOCK = 0x0020;
		public const int GLFW_MOUSE_BUTTON_1 = 0;
		public const int GLFW_MOUSE_BUTTON_2 = 1;
		public const int GLFW_MOUSE_BUTTON_3 = 2;
		public const int GLFW_MOUSE_BUTTON_4 = 3;
		public const int GLFW_MOUSE_BUTTON_5 = 4;
		public const int GLFW_MOUSE_BUTTON_6 = 5;
		public const int GLFW_MOUSE_BUTTON_7 = 6;
		public const int GLFW_MOUSE_BUTTON_8 = 7;
		public const int GLFW_JOYSTICK_1 = 0;
		public const int GLFW_JOYSTICK_2 = 1;
		public const int GLFW_JOYSTICK_3 = 2;
		public const int GLFW_JOYSTICK_4 = 3;
		public const int GLFW_JOYSTICK_5 = 4;
		public const int GLFW_JOYSTICK_6 = 5;
		public const int GLFW_JOYSTICK_7 = 6;
		public const int GLFW_JOYSTICK_8 = 7;
		public const int GLFW_JOYSTICK_9 = 8;
		public const int GLFW_JOYSTICK_10 = 9;
		public const int GLFW_JOYSTICK_11 = 10;
		public const int GLFW_JOYSTICK_12 = 11;
		public const int GLFW_JOYSTICK_13 = 12;
		public const int GLFW_JOYSTICK_14 = 13;
		public const int GLFW_JOYSTICK_15 = 14;
		public const int GLFW_JOYSTICK_16 = 15;
		public const int GLFW_GAMEPAD_BUTTON_A = 0;
		public const int GLFW_GAMEPAD_BUTTON_B = 1;
		public const int GLFW_GAMEPAD_BUTTON_X = 2;
		public const int GLFW_GAMEPAD_BUTTON_Y = 3;
		public const int GLFW_GAMEPAD_BUTTON_LEFT_BUMPER = 4;
		public const int GLFW_GAMEPAD_BUTTON_RIGHT_BUMPER = 5;
		public const int GLFW_GAMEPAD_BUTTON_BACK = 6;
		public const int GLFW_GAMEPAD_BUTTON_START = 7;
		public const int GLFW_GAMEPAD_BUTTON_GUIDE = 8;
		public const int GLFW_GAMEPAD_BUTTON_LEFT_THUMB = 9;
		public const int GLFW_GAMEPAD_BUTTON_RIGHT_THUMB = 10;
		public const int GLFW_GAMEPAD_BUTTON_DPAD_UP = 11;
		public const int GLFW_GAMEPAD_BUTTON_DPAD_RIGHT = 12;
		public const int GLFW_GAMEPAD_BUTTON_DPAD_DOWN = 13;
		public const int GLFW_GAMEPAD_BUTTON_DPAD_LEFT = 14;
		public const int GLFW_GAMEPAD_AXIS_LEFT_X = 0;
		public const int GLFW_GAMEPAD_AXIS_LEFT_Y = 1;
		public const int GLFW_GAMEPAD_AXIS_RIGHT_X = 2;
		public const int GLFW_GAMEPAD_AXIS_RIGHT_Y = 3;
		public const int GLFW_GAMEPAD_AXIS_LEFT_TRIGGER = 4;
		public const int GLFW_GAMEPAD_AXIS_RIGHT_TRIGGER = 5;
		public const int GLFW_NO_ERROR = 0;
		public const int GLFW_NOT_INITIALIZED = 0x00010001;
		public const int GLFW_NO_CURRENT_CONTEXT = 0x00010002;
		public const int GLFW_INVALID_ENUM = 0x00010003;
		public const int GLFW_INVALID_VALUE = 0x00010004;
		public const int GLFW_OUT_OF_MEMORY = 0x00010005;
		public const int GLFW_API_UNAVAILABLE = 0x00010006;
		public const int GLFW_VERSION_UNAVAILABLE = 0x00010007;
		public const int GLFW_PLATFORM_ERROR = 0x00010008;
		public const int GLFW_FORMAT_UNAVAILABLE = 0x00010009;
		public const int GLFW_NO_WINDOW_CONTEXT = 0x0001000A;
		public const int GLFW_FOCUSED = 0x00020001;
		public const int GLFW_ICONIFIED = 0x00020002;
		public const int GLFW_RESIZABLE = 0x00020003;
		public const int GLFW_VISIBLE = 0x00020004;
		public const int GLFW_DECORATED = 0x00020005;
		public const int GLFW_AUTO_ICONIFY = 0x00020006;
		public const int GLFW_FLOATING = 0x00020007;
		public const int GLFW_MAXIMIZED = 0x00020008;
		public const int GLFW_CENTER_CURSOR = 0x00020009;
		public const int GLFW_TRANSPARENT_FRAMEBUFFER = 0x0002000A;
		public const int GLFW_HOVERED = 0x0002000B;
		public const int GLFW_FOCUS_ON_SHOW = 0x0002000C;
		public const int GLFW_RED_BITS = 0x00021001;
		public const int GLFW_GREEN_BITS = 0x00021002;
		public const int GLFW_BLUE_BITS = 0x00021003;
		public const int GLFW_ALPHA_BITS = 0x00021004;
		public const int GLFW_DEPTH_BITS = 0x00021005;
		public const int GLFW_STENCIL_BITS = 0x00021006;
		public const int GLFW_ACCUM_RED_BITS = 0x00021007;
		public const int GLFW_ACCUM_GREEN_BITS = 0x00021008;
		public const int GLFW_ACCUM_BLUE_BITS = 0x00021009;
		public const int GLFW_ACCUM_ALPHA_BITS = 0x0002100A;
		public const int GLFW_AUX_BUFFERS = 0x0002100B;
		public const int GLFW_STEREO = 0x0002100C;
		public const int GLFW_SAMPLES = 0x0002100D;
		public const int GLFW_SRGB_CAPABLE = 0x0002100E;
		public const int GLFW_REFRESH_RATE = 0x0002100F;
		public const int GLFW_DOUBLEBUFFER = 0x00021010;
		public const int GLFW_CLIENT_API = 0x00022001;
		public const int GLFW_CONTEXT_VERSION_MAJOR = 0x00022002;
		public const int GLFW_CONTEXT_VERSION_MINOR = 0x00022003;
		public const int GLFW_CONTEXT_REVISION = 0x00022004;
		public const int GLFW_CONTEXT_ROBUSTNESS = 0x00022005;
		public const int GLFW_OPENGL_FORWARD_COMPAT = 0x00022006;
		public const int GLFW_OPENGL_DEBUG_CONTEXT = 0x00022007;
		public const int GLFW_OPENGL_PROFILE = 0x00022008;
		public const int GLFW_CONTEXT_RELEASE_BEHAVIOR = 0x00022009;
		public const int GLFW_CONTEXT_NO_ERROR = 0x0002200A;
		public const int GLFW_CONTEXT_CREATION_API = 0x0002200B;
		public const int GLFW_SCALE_TO_MONITOR = 0x0002200C;
		public const int GLFW_COCOA_RETINA_FRAMEBUFFER = 0x00023001;
		public const int GLFW_COCOA_FRAME_NAME = 0x00023002;
		public const int GLFW_COCOA_GRAPHICS_SWITCHING = 0x00023003;
		public const int GLFW_X11_CLASS_NAME = 0x00024001;
		public const int GLFW_X11_INSTANCE_NAME = 0x00024002;
		public const int GLFW_NO_API = 0;
		public const int GLFW_OPENGL_API = 0x00030001;
		public const int GLFW_OPENGL_ES_API = 0x00030002;
		public const int GLFW_NO_ROBUSTNESS = 0;
		public const int GLFW_NO_RESET_NOTIFICATION = 0x00031001;
		public const int GLFW_LOSE_CONTEXT_ON_RESET = 0x00031002;
		public const int GLFW_OPENGL_ANY_PROFILE = 0;
		public const int GLFW_OPENGL_CORE_PROFILE = 0x00032001;
		public const int GLFW_OPENGL_COMPAT_PROFILE = 0x00032002;
		public const int GLFW_CURSOR = 0x00033001;
		public const int GLFW_STICKY_KEYS = 0x00033002;
		public const int GLFW_STICKY_MOUSE_BUTTONS = 0x00033003;
		public const int GLFW_LOCK_KEY_MODS = 0x00033004;
		public const int GLFW_RAW_MOUSE_MOTION = 0x00033005;
		public const int GLFW_CURSOR_NORMAL = 0x00034001;
		public const int GLFW_CURSOR_HIDDEN = 0x00034002;
		public const int GLFW_CURSOR_DISABLED = 0x00034003;
		public const int GLFW_ANY_RELEASE_BEHAVIOR = 0;
		public const int GLFW_RELEASE_BEHAVIOR_FLUSH = 0x00035001;
		public const int GLFW_RELEASE_BEHAVIOR_NONE = 0x00035002;
		public const int GLFW_NATIVE_CONTEXT_API = 0x00036001;
		public const int GLFW_EGL_CONTEXT_API = 0x00036002;
		public const int GLFW_OSMESA_CONTEXT_API = 0x00036003;
		public const int GLFW_ARROW_CURSOR = 0x00036001;
		public const int GLFW_IBEAM_CURSOR = 0x00036002;
		public const int GLFW_CROSSHAIR_CURSOR = 0x00036003;
		public const int GLFW_HAND_CURSOR = 0x00036004;
		public const int GLFW_HRESIZE_CURSOR = 0x00036005;
		public const int GLFW_VRESIZE_CURSOR = 0x00036006;
		public const int GLFW_CONNECTED = 0x00040001;
		public const int GLFW_DISCONNECTED = 0x00040002;
		public const int GLFW_JOYSTICK_HAT_BUTTONS = 0x00050001;
		public const int GLFW_COCOA_CHDIR_RESOURCES = 0x00051001;
		public const int GLFW_COCOA_MENUBAR = 0x00051002;
		public const int GLFW_DONT_CARE = -1;
		public const int GLFW_HAT_RIGHT_UP = ( GLFW_HAT_RIGHT | GLFW_HAT_UP );
		public const int GLFW_HAT_RIGHT_DOWN = ( GLFW_HAT_RIGHT | GLFW_HAT_DOWN );
		public const int GLFW_HAT_LEFT_UP = ( GLFW_HAT_LEFT | GLFW_HAT_UP );
		public const int GLFW_HAT_LEFT_DOWN = ( GLFW_HAT_LEFT | GLFW_HAT_DOWN );
		public const int GLFW_KEY_LAST = GLFW_KEY_MENU;
		public const int GLFW_MOUSE_BUTTON_LAST = GLFW_MOUSE_BUTTON_8;
		public const int GLFW_MOUSE_BUTTON_LEFT = GLFW_MOUSE_BUTTON_1;
		public const int GLFW_MOUSE_BUTTON_RIGHT = GLFW_MOUSE_BUTTON_2;
		public const int GLFW_MOUSE_BUTTON_MIDDLE = GLFW_MOUSE_BUTTON_3;
		public const int GLFW_JOYSTICK_LAST = GLFW_JOYSTICK_16;
		public const int GLFW_GAMEPAD_BUTTON_LAST = GLFW_GAMEPAD_BUTTON_DPAD_LEFT;
		public const int GLFW_GAMEPAD_BUTTON_CROSS = GLFW_GAMEPAD_BUTTON_A;
		public const int GLFW_GAMEPAD_BUTTON_CIRCLE = GLFW_GAMEPAD_BUTTON_B;
		public const int GLFW_GAMEPAD_BUTTON_SQUARE = GLFW_GAMEPAD_BUTTON_X;
		public const int GLFW_GAMEPAD_BUTTON_TRIANGLE = GLFW_GAMEPAD_BUTTON_Y;
		public const int GLFW_GAMEPAD_AXIS_LAST = GLFW_GAMEPAD_AXIS_RIGHT_TRIGGER;
		#endregion

		public struct GlfwWindow { }

		#region Delegates
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWGLPROCPROC();
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWERRORFUNPROC( int error, byte* description );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWWINDOWPOSFUNPROC( GlfwWindow* window, int xpos, int ypos );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWWINDOWSIZEFUNPROC( GlfwWindow* window, int width, int height );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWWINDOWCLOSEFUNPROC( GlfwWindow* window );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWWINDOWREFRESHFUNPROC( GlfwWindow* window );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWWINDOWFOCUSFUNPROC( GlfwWindow* window, int focused );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWWINDOWICONIFYFUNPROC( GlfwWindow* window, int iconified );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWWINDOWMAXIMIZEFUNPROC( GlfwWindow* window, int maximized );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWFRAMEBUFFERSIZEFUNPROC( GlfwWindow* window, int width, int height );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWWINDOWCONTENTSCALEFUNPROC( GlfwWindow* window, float xscale, float yscale );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWMOUSEBUTTONFUNPROC( GlfwWindow* window, int button, int action, int mods );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWCURSORPOSFUNPROC( GlfwWindow* window, double xpos, double ypos );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWCURSORENTERFUNPROC( GlfwWindow* window, int entered );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSCROLLFUNPROC( GlfwWindow* window, double xoffset, double yoffset );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWKEYFUNPROC( GlfwWindow* window, int key, int scancode, int action, int mods );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWCHARFUNPROC( GlfwWindow* window, uint codepoint );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWCHARMODSFUNPROC( GlfwWindow* window, uint codepoint, int mods );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWDROPFUNPROC( GlfwWindow* window, int count, byte*[] paths );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNMonitorFUNPROC( Monitor* monitor, int @event );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWJOYSTICKFUNPROC( int jid, int @event );

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int PFNGLFWINITPROC();
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWTERMINATEPROC();
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWINITHINTPROC( int hint, int value );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWGETVERSIONPROC( int* major, int* minor, int* rev );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate byte* PFNGLFWGETVERSIONSTRINGPROC();
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int PFNGLFWGETERRORPROC( byte** description );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(ErrorCallback))]
		public delegate ErrorCallback PFNGLFWSETERRORCALLBACKPROC(ErrorCallback callback );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate Monitor** PFNGLFWGETMONITORSPROC( int* count );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate Monitor* PFNGLFWGETPRIMARYMONITORPROC();
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWGETMONITORPOSPROC( Monitor* monitor, int* xpos, int* ypos );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWGETMONITORWORKAREAPROC( Monitor* monitor, int* xpos, int* ypos, int* width, int* height );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWGETMONITORPHYSICALSIZEPROC( Monitor* monitor, int* widthMM, int* heightMM );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWGETMONITORCONTENTSCALEPROC( Monitor* monitor, float* xscale, float* yscale );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate byte* PFNGLFWGETMONITORNAMEPROC( Monitor* monitor );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSETMONITORUSERPOINTERPROC( Monitor* monitor, void* pointer );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void* PFNGLFWGETMONITORUSERPOINTERPROC( Monitor* monitor );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(MonitorCallback))]
		public delegate MonitorCallback PFNGLFWSETMONITORCALLBACKPROC( MonitorCallback callback );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate WindowVideoMode* PFNGLFWGETVIDEOMODESPROC( Monitor* monitor, int* count );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate WindowVideoMode* PFNGLFWGETVIDEOMODEPROC( Monitor* monitor );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSETGAMMAPROC( Monitor* monitor, float gamma );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate GammaRamp* PFNGLFWGETGAMMARAMPPROC( Monitor* monitor );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSETGAMMARAMPPROC( Monitor* monitor, GammaRamp* ramp );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWDEFAULTWINDOWHINTSPROC();
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWWINDOWHINTPROC( int hint, int value );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWWINDOWHINTSTRINGPROC( int hint, byte* value );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate GlfwWindow* PFNGLFWCREATEWINDOWPROC( int width, int height, byte* title, Monitor* monitor, GlfwWindow* share );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWDESTROYWINDOWPROC( GlfwWindow* window );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int PFNGLFWWINDOWSHOULDCLOSEPROC( GlfwWindow* window );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSETWINDOWSHOULDCLOSEPROC( GlfwWindow* window, int value );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSETWINDOWTITLEPROC( GlfwWindow* window, byte* title );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSETWINDOWICONPROC( GlfwWindow* window, int count, Icon images );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWGETWINDOWPOSPROC( GlfwWindow* window, int* xpos, int* ypos );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSETWINDOWPOSPROC( GlfwWindow* window, int xpos, int ypos );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWGETWINDOWSIZEPROC( GlfwWindow* window, int* width, int* height );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSETWINDOWSIZELIMITSPROC( GlfwWindow* window, int minwidth, int minheight, int maxwidth, int maxheight );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSETWINDOWASPECTRATIOPROC( GlfwWindow* window, int numer, int denom );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSETWINDOWSIZEPROC( GlfwWindow* window, int width, int height );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWGETFRAMEBUFFERSIZEPROC( GlfwWindow* window, int* width, int* height );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWGETWINDOWFRAMESIZEPROC( GlfwWindow* window, int* left, int* top, int* right, int* bottom );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWGETWINDOWCONTENTSCALEPROC( GlfwWindow* window, float* xscale, float* yscale );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float PFNGLFWGETWINDOWOPACITYPROC( GlfwWindow* window );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSETWINDOWOPACITYPROC( GlfwWindow* window, float opacity );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWICONIFYWINDOWPROC( GlfwWindow* window );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWRESTOREWINDOWPROC( GlfwWindow* window );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWMAXIMIZEWINDOWPROC( GlfwWindow* window );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSHOWWINDOWPROC( GlfwWindow* window );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWHIDEWINDOWPROC( GlfwWindow* window );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWFOCUSWINDOWPROC( GlfwWindow* window );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWREQUESTWINDOWATTENTIONPROC( GlfwWindow* window );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate Monitor* PFNGLFWGETWINDOWMONITORPROC( GlfwWindow* window );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSETWINDOWMONITORPROC( GlfwWindow* window, Monitor* monitor, int xpos, int ypos, int width, int height, int refreshRate );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int PFNGLFWGETWINDOWATTRIBPROC( GlfwWindow* window, int attrib );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSETWINDOWATTRIBPROC( GlfwWindow* window, int attrib, int value );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSETWINDOWUSERPOINTERPROC( GlfwWindow* window, void* pointer );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void* PFNGLFWGETWINDOWUSERPOINTERPROC( GlfwWindow* window );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(PositionCallback))]
		public delegate PositionCallback PFNGLFWSETWINDOWPOSCALLBACKPROC( GlfwWindow* window, PositionCallback callback );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(SizeCallback))]
		public delegate SizeCallback PFNGLFWSETWINDOWSIZECALLBACKPROC( GlfwWindow* window, SizeCallback callback );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(WindowCallback))]
		public delegate WindowCallback PFNGLFWSETWINDOWCLOSECALLBACKPROC( GlfwWindow* window, WindowCallback callback );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(WindowCallback))]
		public delegate WindowCallback PFNGLFWSETWINDOWREFRESHCALLBACKPROC( GlfwWindow* window, WindowCallback callback );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(FocusCallback))]
		public delegate FocusCallback PFNGLFWSETWINDOWFOCUSCALLBACKPROC( GlfwWindow* window, FocusCallback callback );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(WindowMaximizedCallback))]
		public delegate WindowMaximizedCallback PFNGLFWSETWINDOWICONIFYCALLBACKPROC( GlfwWindow* window, WindowMaximizedCallback callback );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(WindowMaximizedCallback))]
		public delegate WindowMaximizedCallback PFNGLFWSETWINDOWMAXIMIZECALLBACKPROC( GlfwWindow* window, WindowMaximizedCallback callback );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(WindowCallback))]
		public delegate WindowCallback PFNGLFWSETFRAMEBUFFERSIZECALLBACKPROC( GlfwWindow* window, WindowCallback callback );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(WindowContentsScaleCallback))]
		public delegate WindowContentsScaleCallback PFNGLFWSETWINDOWCONTENTSCALECALLBACKPROC( GlfwWindow* window, WindowContentsScaleCallback callback );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWPOLLEVENTSPROC();
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWWAITEVENTSPROC();
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWWAITEVENTSTIMEOUTPROC( double timeout );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWPOSTEMPTYEVENTPROC();
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int PFNGLFWGETINPUTMODEPROC( GlfwWindow* window, int mode );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSETINPUTMODEPROC( GlfwWindow* window, int mode, int value );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int PFNGLFWRAWMOUSEMOTIONSUPPORTEDPROC();
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate byte* PFNGLFWGETKEYNAMEPROC( int key, int scancode );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int PFNGLFWGETKEYSCANCODEPROC( int key );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int PFNGLFWGETKEYPROC( GlfwWindow* window, int key );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int PFNGLFWGETMOUSEBUTTONPROC( GlfwWindow* window, int button );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWGETCURSORPOSPROC( GlfwWindow* window, double* xpos, double* ypos );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSETCURSORPOSPROC( GlfwWindow* window, double xpos, double ypos );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate Cursor PFNGLFWCREATECURSORPROC( Icon image, int xhot, int yhot );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate Cursor PFNGLFWCREATESTANDARDCURSORPROC( int shape );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWDESTROYCURSORPROC( Cursor cursor );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSETCURSORPROC( GlfwWindow* window, Cursor cursor );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(KeyCallback))]
		public delegate KeyCallback PFNGLFWSETKEYCALLBACKPROC( GlfwWindow* window, KeyCallback callback );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(ErrorCallback))]
		public delegate CharCallback PFNGLFWSETCHARCALLBACKPROC( GlfwWindow* window, CharCallback callback );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(CharModsCallback))]
		public delegate CharModsCallback PFNGLFWSETCHARMODSCALLBACKPROC( GlfwWindow* window, CharModsCallback callback );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(MouseButtonCallback))]
		public delegate MouseButtonCallback PFNGLFWSETMOUSEBUTTONCALLBACKPROC( GlfwWindow* window, MouseButtonCallback callback );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(MouseCallback))]
		public delegate MouseCallback PFNGLFWSETCURSORPOSCALLBACKPROC( GlfwWindow* window, MouseCallback callback );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(MouseEnterCallback))]
		public delegate MouseEnterCallback PFNGLFWSETCURSORENTERCALLBACKPROC( GlfwWindow* window, MouseEnterCallback callback );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(MouseCallback))]
		public delegate MouseCallback PFNGLFWSETSCROLLCALLBACKPROC( GlfwWindow* window, MouseCallback callback );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(FileDropCallback))]
		public delegate FileDropCallback PFNGLFWSETDROPCALLBACKPROC( GlfwWindow* window, FileDropCallback callback );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int PFNGLFWJOYSTICKPRESENTPROC( int jid );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate float* PFNGLFWGETJOYSTICKAXESPROC( int jid, int* count );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate byte* PFNGLFWGETJOYSTICKBUTTONSPROC( int jid, int* count );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate byte* PFNGLFWGETJOYSTICKHATSPROC( int jid, int* count );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate byte* PFNGLFWGETJOYSTICKNAMEPROC( int jid );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate byte* PFNGLFWGETJOYSTICKGUIDPROC( int jid );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSETJOYSTICKUSERPOINTERPROC( int jid, void* pointer );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void* PFNGLFWGETJOYSTICKUSERPOINTERPROC( int jid );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int PFNGLFWJOYSTICKISGAMEPADPROC( int jid );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.FunctionPtr, MarshalTypeRef = typeof(JoystickCallback))]
		public delegate JoystickCallback PFNGLFWSETJOYSTICKCALLBACKPROC( JoystickCallback callback );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int PFNGLFWUPDATEGAMEPADMAPPINGSPROC( byte* @string );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate byte* PFNGLFWGETGAMEPADNAMEPROC( int jid );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int PFNGLFWGETGAMEPADSTATEPROC( int jid, GamePadState* state );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSETCLIPBOARDSTRINGPROC( GlfwWindow* window, byte* @string );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate byte* PFNGLFWGETCLIPBOARDSTRINGPROC( GlfwWindow* window );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate double PFNGLFWGETTIMEPROC();
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSETTIMEPROC( double time );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong PFNGLFWGETTIMERVALUEPROC();
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong PFNGLFWGETTIMERFREQUENCYPROC();
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWMAKECONTEXTCURRENTPROC( GlfwWindow* window );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate GlfwWindow* PFNGLFWGETCURRENTCONTEXTPROC();
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSWAPBUFFERSPROC( GlfwWindow* window );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void PFNGLFWSWAPINTERVALPROC( int interval );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int PFNGLFWEXTENSIONSUPPORTEDPROC( byte* extension );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr PFNGLFWGETPROCADDRESSPROC( byte* procname );
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int PFNGLFWVULKANSUPPORTEDPROC();
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate byte** PFNGLFWGETREQUIREDINSTANCEEXTENSIONSPROC( uint* count );
		#endregion

		#region Methods
		public static PFNGLFWINITPROC glfwInit;
		public static PFNGLFWTERMINATEPROC glfwTerminate;
		public static PFNGLFWINITHINTPROC glfwInitHint;
		public static PFNGLFWGETVERSIONPROC glfwGetVersion;
		public static PFNGLFWGETVERSIONSTRINGPROC glfwGetVersionString;
		public static PFNGLFWGETERRORPROC glfwGetError;
		public static PFNGLFWSETERRORCALLBACKPROC glfwSetErrorCallback;
		public static PFNGLFWGETMONITORSPROC glfwGetMonitors;
		public static PFNGLFWGETPRIMARYMONITORPROC glfwGetPrimaryMonitor;
		public static PFNGLFWGETMONITORPOSPROC glfwGetMonitorPos;
		public static PFNGLFWGETMONITORWORKAREAPROC glfwGetMonitorWorkarea;
		public static PFNGLFWGETMONITORPHYSICALSIZEPROC glfwGetMonitorPhysicalSize;
		public static PFNGLFWGETMONITORCONTENTSCALEPROC glfwGetMonitorContentScale;
		public static PFNGLFWGETMONITORNAMEPROC glfwGetMonitorName;
		public static PFNGLFWSETMONITORUSERPOINTERPROC glfwSetMonitorUserPointer;
		public static PFNGLFWGETMONITORUSERPOINTERPROC glfwGetMonitorUserPointer;
		public static PFNGLFWSETMONITORCALLBACKPROC glfwSetMonitorCallback;
		public static PFNGLFWGETVIDEOMODESPROC glfwGetVideoModes;
		public static PFNGLFWGETVIDEOMODEPROC glfwGetVideoMode;
		public static PFNGLFWSETGAMMAPROC glfwSetGamma;
		public static PFNGLFWGETGAMMARAMPPROC glfwGetGammaRamp;
		public static PFNGLFWSETGAMMARAMPPROC glfwSetGammaRamp;
		public static PFNGLFWDEFAULTWINDOWHINTSPROC glfwDefaultWindowHints;
		public static PFNGLFWWINDOWHINTPROC glfwWindowHint;
		public static PFNGLFWWINDOWHINTSTRINGPROC glfwWindowHintString;
		public static PFNGLFWCREATEWINDOWPROC glfwCreateWindow;
		public static PFNGLFWDESTROYWINDOWPROC glfwDestroyWindow;
		public static PFNGLFWWINDOWSHOULDCLOSEPROC glfwWindowShouldClose;
		public static PFNGLFWSETWINDOWSHOULDCLOSEPROC glfwSetWindowShouldClose;
		public static PFNGLFWSETWINDOWTITLEPROC glfwSetWindowTitle;
		public static PFNGLFWSETWINDOWICONPROC glfwSetWindowIcon;
		public static PFNGLFWGETWINDOWPOSPROC glfwGetWindowPos;
		public static PFNGLFWSETWINDOWPOSPROC glfwSetWindowPos;
		public static PFNGLFWGETWINDOWSIZEPROC glfwGetWindowSize;
		public static PFNGLFWSETWINDOWSIZELIMITSPROC glfwSetWindowSizeLimits;
		public static PFNGLFWSETWINDOWASPECTRATIOPROC glfwSetWindowAspectRatio;
		public static PFNGLFWSETWINDOWSIZEPROC glfwSetWindowSize;
		public static PFNGLFWGETFRAMEBUFFERSIZEPROC glfwGetFramebufferSize;
		public static PFNGLFWGETWINDOWFRAMESIZEPROC glfwGetWindowFrameSize;
		public static PFNGLFWGETWINDOWCONTENTSCALEPROC glfwGetWindowContentScale;
		public static PFNGLFWGETWINDOWOPACITYPROC glfwGetWindowOpacity;
		public static PFNGLFWSETWINDOWOPACITYPROC glfwSetWindowOpacity;
		public static PFNGLFWICONIFYWINDOWPROC glfwIconifyWindow;
		public static PFNGLFWRESTOREWINDOWPROC glfwRestoreWindow;
		public static PFNGLFWMAXIMIZEWINDOWPROC glfwMaximizeWindow;
		public static PFNGLFWSHOWWINDOWPROC glfwShowWindow;
		public static PFNGLFWHIDEWINDOWPROC glfwHideWindow;
		public static PFNGLFWFOCUSWINDOWPROC glfwFocusWindow;
		public static PFNGLFWREQUESTWINDOWATTENTIONPROC glfwRequestWindowAttention;
		public static PFNGLFWGETWINDOWMONITORPROC glfwGetWindowMonitor;
		public static PFNGLFWSETWINDOWMONITORPROC glfwSetWindowMonitor;
		public static PFNGLFWGETWINDOWATTRIBPROC glfwGetWindowAttrib;
		public static PFNGLFWSETWINDOWATTRIBPROC glfwSetWindowAttrib;
		public static PFNGLFWSETWINDOWUSERPOINTERPROC glfwSetWindowUserPointer;
		public static PFNGLFWGETWINDOWUSERPOINTERPROC glfwGetWindowUserPointer;
		public static PFNGLFWSETWINDOWPOSCALLBACKPROC glfwSetWindowPosCallback;
		public static PFNGLFWSETWINDOWSIZECALLBACKPROC glfwSetWindowSizeCallback;
		public static PFNGLFWSETWINDOWCLOSECALLBACKPROC glfwSetWindowCloseCallback;
		public static PFNGLFWSETWINDOWREFRESHCALLBACKPROC glfwSetWindowRefreshCallback;
		public static PFNGLFWSETWINDOWFOCUSCALLBACKPROC glfwSetWindowFocusCallback;
		public static PFNGLFWSETWINDOWICONIFYCALLBACKPROC glfwSetWindowIconifyCallback;
		public static PFNGLFWSETWINDOWMAXIMIZECALLBACKPROC glfwSetWindowMaximizeCallback;
		public static PFNGLFWSETFRAMEBUFFERSIZECALLBACKPROC glfwSetFramebufferSizeCallback;
		public static PFNGLFWSETWINDOWCONTENTSCALECALLBACKPROC glfwSetWindowContentScaleCallback;
		public static PFNGLFWPOLLEVENTSPROC glfwPollEvents;
		public static PFNGLFWWAITEVENTSPROC glfwWaitEvents;
		public static PFNGLFWWAITEVENTSTIMEOUTPROC glfwWaitEventsTimeout;
		public static PFNGLFWPOSTEMPTYEVENTPROC glfwPostEmptyEvent;
		public static PFNGLFWGETINPUTMODEPROC glfwGetInputMode;
		public static PFNGLFWSETINPUTMODEPROC glfwSetInputMode;
		public static PFNGLFWRAWMOUSEMOTIONSUPPORTEDPROC glfwRawMouseMotionSupported;
		public static PFNGLFWGETKEYNAMEPROC glfwGetKeyName;
		public static PFNGLFWGETKEYSCANCODEPROC glfwGetKeyScancode;
		public static PFNGLFWGETKEYPROC glfwGetKey;
		public static PFNGLFWGETMOUSEBUTTONPROC glfwGetMouseButton;
		public static PFNGLFWGETCURSORPOSPROC glfwGetCursorPos;
		public static PFNGLFWSETCURSORPOSPROC glfwSetCursorPos;
		public static PFNGLFWCREATECURSORPROC glfwCreateCursor;
		public static PFNGLFWCREATESTANDARDCURSORPROC glfwCreateStandardCursor;
		public static PFNGLFWDESTROYCURSORPROC glfwDestroyCursor;
		public static PFNGLFWSETCURSORPROC glfwSetCursor;
		public static PFNGLFWSETKEYCALLBACKPROC glfwSetKeyCallback;
		public static PFNGLFWSETCHARCALLBACKPROC glfwSetCharCallback;
		public static PFNGLFWSETCHARMODSCALLBACKPROC glfwSetCharModsCallback;
		public static PFNGLFWSETMOUSEBUTTONCALLBACKPROC glfwSetMouseButtonCallback;
		public static PFNGLFWSETCURSORPOSCALLBACKPROC glfwSetCursorPosCallback;
		public static PFNGLFWSETCURSORENTERCALLBACKPROC glfwSetCursorEnterCallback;
		public static PFNGLFWSETSCROLLCALLBACKPROC glfwSetScrollCallback;
		public static PFNGLFWSETDROPCALLBACKPROC glfwSetDropCallback;
		public static PFNGLFWJOYSTICKPRESENTPROC glfwJoystickPresent;
		public static PFNGLFWGETJOYSTICKAXESPROC glfwGetJoystickAxes;
		public static PFNGLFWGETJOYSTICKBUTTONSPROC glfwGetJoystickButtons;
		public static PFNGLFWGETJOYSTICKHATSPROC glfwGetJoystickHats;
		public static PFNGLFWGETJOYSTICKNAMEPROC glfwGetJoystickName;
		public static PFNGLFWGETJOYSTICKGUIDPROC glfwGetJoystickGUID;
		public static PFNGLFWSETJOYSTICKUSERPOINTERPROC glfwSetJoystickUserPointer;
		public static PFNGLFWGETJOYSTICKUSERPOINTERPROC glfwGetJoystickUserPointer;
		public static PFNGLFWJOYSTICKISGAMEPADPROC glfwJoystickIsGamepad;
		public static PFNGLFWSETJOYSTICKCALLBACKPROC glfwSetJoystickCallback;
		public static PFNGLFWUPDATEGAMEPADMAPPINGSPROC glfwUpdateGamepadMappings;
		public static PFNGLFWGETGAMEPADNAMEPROC glfwGetGamepadName;
		public static PFNGLFWGETGAMEPADSTATEPROC glfwGetGamepadState;
		public static PFNGLFWSETCLIPBOARDSTRINGPROC glfwSetClipboardString;
		public static PFNGLFWGETCLIPBOARDSTRINGPROC glfwGetClipboardString;
		public static PFNGLFWGETTIMEPROC glfwGetTime;
		public static PFNGLFWSETTIMEPROC glfwSetTime;
		public static PFNGLFWGETTIMERVALUEPROC glfwGetTimerValue;
		public static PFNGLFWGETTIMERFREQUENCYPROC glfwGetTimerFrequency;
		public static PFNGLFWMAKECONTEXTCURRENTPROC glfwMakeContextCurrent;
		public static PFNGLFWGETCURRENTCONTEXTPROC glfwGetCurrentContext;
		public static PFNGLFWSWAPBUFFERSPROC glfwSwapBuffers;
		public static PFNGLFWSWAPINTERVALPROC glfwSwapInterval;
		public static PFNGLFWEXTENSIONSUPPORTEDPROC glfwExtensionSupported;
		public static PFNGLFWGETPROCADDRESSPROC glfwGetProcAddress;
		
		public static PFNGLFWVULKANSUPPORTEDPROC glfwVulkanSupported;
		public static PFNGLFWGETREQUIREDINSTANCEEXTENSIONSPROC glfwGetRequiredInstanceExtensions;
		#endregion

		#region Constructors
		
		static Glfw()
		{
			_handle = GlfwLoader.Initialize();
		}
		
		#endregion
    }
}