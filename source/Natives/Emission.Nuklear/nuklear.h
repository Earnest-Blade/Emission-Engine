#pragma once

#include "natives/cppsharp.h"

#include "nuklear_glfw.h"

#ifndef NUKLEAR
#define NUKLEAR

namespace nuklear
{
    enum class Nk;
    enum class NkAllocationType;
    enum class NkAntiAliasing;
    enum class NkBufferAllocationType;
    enum class NkButtonBehavior;
    enum class NkButtons;
    enum class NkChartEvent;
    enum class NkChartType;
    enum class NkCollapseStates;
    enum class NkColorFormat;
    enum class NkCommandClipping;
    enum class NkCommandType;
    enum class NkConvertResult;
    enum class NkEditEvents;
    enum class NkEditFlags;
    enum class NkEditTypes;
    enum class NkHeading;
    enum class NkKeys;
    enum class NkLayoutFormat;
    enum class NkModify;
    enum class NkOrientation;
    enum class NkPanelFlags;
    enum class NkPanelRowLayoutType;
    enum class NkPanelSet;
    enum class NkPanelType;
    enum class NkPopupType;
    enum class NkShowStates;
    enum class NkStyleColors;
    enum class NkStyleCursor;
    enum class NkStyleHeaderAlign;
    enum class NkStyleItemType;
    enum class NkSymbolType;
    enum class NkTextAlign;
    enum class NkTextAlignment;
    enum class NkTextEditMode;
    enum class NkTextEditType;
    enum class NkTreeType;
    enum class NkWidgetLayoutStates;
    enum class NkWidgetStates;
    enum class NkWindowFlags;
    ref class NkAlignof;
    ref class NkAllocator;
    ref class NkBuffer;
    ref class NkBufferMarker;
    ref class NkChart;
    ref class NkChartSlot;
    ref class NkClipboard;
    ref class NkColor;
    ref class NkColorf;
    ref class NkCommand;
    ref class NkCommandArc;
    ref class NkCommandArcFilled;
    ref class NkCommandBuffer;
    ref class NkCommandCircle;
    ref class NkCommandCircleFilled;
    ref class NkCommandCurve;
    ref class NkCommandCustom;
    ref class NkCommandImage;
    ref class NkCommandLine;
    ref class NkCommandPolygon;
    ref class NkCommandPolygonFilled;
    ref class NkCommandPolyline;
    ref class NkCommandRect;
    ref class NkCommandRectFilled;
    ref class NkCommandRectMultiColor;
    ref class NkCommandScissor;
    ref class NkCommandText;
    ref class NkCommandTriangle;
    ref class NkCommandTriangleFilled;
    ref class NkConfigStackButtonBehavior;
    ref class NkConfigStackButtonBehaviorElement;
    ref class NkConfigStackColor;
    ref class NkConfigStackColorElement;
    ref class NkConfigStackFlags;
    ref class NkConfigStackFlagsElement;
    ref class NkConfigStackFloat;
    ref class NkConfigStackFloatElement;
    ref class NkConfigStackStyleItem;
    ref class NkConfigStackStyleItemElement;
    ref class NkConfigStackUserFont;
    ref class NkConfigStackUserFontElement;
    ref class NkConfigStackVec2;
    ref class NkConfigStackVec2Element;
    ref class NkConfigurationStacks;
    ref class NkContext;
    ref class NkConvertConfig;
    ref class NkCursor;
    ref class NkDrawCommand;
    ref class NkDrawList;
    ref class NkDrawNullTexture;
    ref class NkDrawVertexLayoutElement;
    ref class NkEditState;
    ref class NkHelper;
    ref class NkImage;
    ref class NkInput;
    ref class NkKey;
    ref class NkKeyboard;
    ref class NkListView;
    ref class NkMemory;
    ref class NkMemoryStatus;
    ref class NkMenuState;
    ref class NkMouse;
    ref class NkMouseButton;
    ref class NkPage;
    ref class NkPageElement;
    ref class NkPanel;
    ref class NkPool;
    ref class NkPopupBuffer;
    ref class NkPopupState;
    ref class NkPropertyState;
    ref class NkRect;
    ref class NkRecti;
    ref class NkRowLayout;
    ref class NkScroll;
    ref class NkStr;
    ref class NkStyle;
    ref class NkStyleButton;
    ref class NkStyleChart;
    ref class NkStyleCombo;
    ref class NkStyleEdit;
    ref class NkStyleItem;
    ref class NkStyleProgress;
    ref class NkStyleProperty;
    ref class NkStyleScrollbar;
    ref class NkStyleSelectable;
    ref class NkStyleSlide;
    ref class NkStyleSlider;
    ref class NkStyleTab;
    ref class NkStyleText;
    ref class NkStyleToggle;
    ref class NkStyleWindow;
    ref class NkStyleWindowHeader;
    ref class NkTable;
    ref class NkTextEdit;
    ref class NkTextUndoRecord;
    ref class NkTextUndoState;
    ref class NkUserFont;
    ref class NkUserFontGlyph;
    ref class NkVec2;
    ref class NkVec2i;
    ref class NkWindow;
    value struct NkHandle;
    value struct NkPageData;
    value struct NkStyleItemData;
}

namespace nuklear
{
    public enum class NkAllocationType
    {
        NK_BUFFER_FIXED = 0,
        NK_BUFFER_DYNAMIC = 1
    };

    public enum class NkAntiAliasing
    {
        NK_ANTI_ALIASING_OFF = 0,
        NK_ANTI_ALIASING_ON = 1
    };

    public enum class NkStyleItemType
    {
        NK_STYLE_ITEM_COLOR = 0,
        NK_STYLE_ITEM_IMAGE = 1
    };

    [::System::Flags]
    public enum class NkPanelType
    {
        NK_PANEL_NONE = 0,
        NK_PANEL_WINDOW = 1,
        NK_PANEL_GROUP = 2,
        NK_PANEL_POPUP = 4,
        NK_PANEL_CONTEXTUAL = 16,
        NK_PANEL_COMBO = 32,
        NK_PANEL_MENU = 64,
        NK_PANEL_TOOLTIP = 128
    };

    public enum class NkPanelRowLayoutType
    {
        NK_LAYOUT_DYNAMIC_FIXED = 0,
        NK_LAYOUT_DYNAMIC_ROW = 1,
        NK_LAYOUT_DYNAMIC_FREE = 2,
        NK_LAYOUT_DYNAMIC = 3,
        NK_LAYOUT_STATIC_FIXED = 4,
        NK_LAYOUT_STATIC_ROW = 5,
        NK_LAYOUT_STATIC_FREE = 6,
        NK_LAYOUT_STATIC = 7,
        NK_LAYOUT_TEMPLATE = 8,
        NK_LAYOUT_COUNT = 9
    };

    public enum class NkChartType
    {
        NK_CHART_LINES = 0,
        NK_CHART_COLUMN = 1,
        NK_CHART_MAX = 2
    };

    public enum class NkSymbolType
    {
        NK_SYMBOL_NONE = 0,
        NK_SYMBOL_X = 1,
        NK_SYMBOL_UNDERSCORE = 2,
        NK_SYMBOL_CIRCLE_SOLID = 3,
        NK_SYMBOL_CIRCLE_OUTLINE = 4,
        NK_SYMBOL_RECT_SOLID = 5,
        NK_SYMBOL_RECT_OUTLINE = 6,
        NK_SYMBOL_TRIANGLE_UP = 7,
        NK_SYMBOL_TRIANGLE_DOWN = 8,
        NK_SYMBOL_TRIANGLE_LEFT = 9,
        NK_SYMBOL_TRIANGLE_RIGHT = 10,
        NK_SYMBOL_PLUS = 11,
        NK_SYMBOL_MINUS = 12,
        NK_SYMBOL_MAX = 13
    };

    public enum class NkStyleHeaderAlign
    {
        NK_HEADER_LEFT = 0,
        NK_HEADER_RIGHT = 1
    };

    public enum class NkButtonBehavior
    {
        NK_BUTTON_DEFAULT = 0,
        NK_BUTTON_REPEATER = 1
    };

    public enum class Nk
    {
        NkFalse = 0,
        NkTrue = 1
    };

    public enum class NkHeading
    {
        NK_UP = 0,
        NK_RIGHT = 1,
        NK_DOWN = 2,
        NK_LEFT = 3
    };

    public enum class NkModify
    {
        NK_FIXED = 0,
        NK_MODIFIABLE = 1
    };

    public enum class NkOrientation
    {
        NK_VERTICAL = 0,
        NK_HORIZONTAL = 1
    };

    public enum class NkCollapseStates
    {
        NK_MINIMIZED = 0,
        NK_MAXIMIZED = 1
    };

    public enum class NkShowStates
    {
        NK_HIDDEN = 0,
        NK_SHOWN = 1
    };

    public enum class NkChartEvent
    {
        NK_CHART_HOVERING = 1,
        NK_CHART_CLICKED = 2
    };

    public enum class NkColorFormat
    {
        NK_RGB = 0,
        NK_RGBA = 1
    };

    public enum class NkPopupType
    {
        NK_POPUP_STATIC = 0,
        NK_POPUP_DYNAMIC = 1
    };

    public enum class NkLayoutFormat
    {
        NK_DYNAMIC = 0,
        NK_STATIC = 1
    };

    public enum class NkTreeType
    {
        NK_TREE_NODE = 0,
        NK_TREE_TAB = 1
    };

    public enum class NkKeys
    {
        NK_KEY_NONE = 0,
        NK_KEY_SHIFT = 1,
        NK_KEY_CTRL = 2,
        NK_KEY_DEL = 3,
        NK_KEY_ENTER = 4,
        NK_KEY_TAB = 5,
        NK_KEY_BACKSPACE = 6,
        NK_KEY_COPY = 7,
        NK_KEY_CUT = 8,
        NK_KEY_PASTE = 9,
        NK_KEY_UP = 10,
        NK_KEY_DOWN = 11,
        NK_KEY_LEFT = 12,
        NK_KEY_RIGHT = 13,
        NK_KEY_TEXT_INSERT_MODE = 14,
        NK_KEY_TEXT_REPLACE_MODE = 15,
        NK_KEY_TEXT_RESET_MODE = 16,
        NK_KEY_TEXT_LINE_START = 17,
        NK_KEY_TEXT_LINE_END = 18,
        NK_KEY_TEXT_START = 19,
        NK_KEY_TEXT_END = 20,
        NK_KEY_TEXT_UNDO = 21,
        NK_KEY_TEXT_REDO = 22,
        NK_KEY_TEXT_SELECT_ALL = 23,
        NK_KEY_TEXT_WORD_LEFT = 24,
        NK_KEY_TEXT_WORD_RIGHT = 25,
        NK_KEY_SCROLL_START = 26,
        NK_KEY_SCROLL_END = 27,
        NK_KEY_SCROLL_DOWN = 28,
        NK_KEY_SCROLL_UP = 29,
        NK_KEY_MAX = 30
    };

    public enum class NkButtons
    {
        NK_BUTTON_LEFT = 0,
        NK_BUTTON_MIDDLE = 1,
        NK_BUTTON_RIGHT = 2,
        NK_BUTTON_DOUBLE = 3,
        NK_BUTTON_MAX = 4
    };

    [::System::Flags]
    public enum class NkConvertResult
    {
        NK_CONVERT_SUCCESS = 0,
        NK_CONVERT_INVALID_PARAM = 1,
        NK_CONVERT_COMMAND_BUFFER_FULL = 2,
        NK_CONVERT_VERTEX_BUFFER_FULL = 4,
        NK_CONVERT_ELEMENT_BUFFER_FULL = 8
    };

    public enum class NkCommandType
    {
        NK_COMMAND_NOP = 0,
        NK_COMMAND_SCISSOR = 1,
        NK_COMMAND_LINE = 2,
        NK_COMMAND_CURVE = 3,
        NK_COMMAND_RECT = 4,
        NK_COMMAND_RECT_FILLED = 5,
        NK_COMMAND_RECT_MULTI_COLOR = 6,
        NK_COMMAND_CIRCLE = 7,
        NK_COMMAND_CIRCLE_FILLED = 8,
        NK_COMMAND_ARC = 9,
        NK_COMMAND_ARC_FILLED = 10,
        NK_COMMAND_TRIANGLE = 11,
        NK_COMMAND_TRIANGLE_FILLED = 12,
        NK_COMMAND_POLYGON = 13,
        NK_COMMAND_POLYGON_FILLED = 14,
        NK_COMMAND_POLYLINE = 15,
        NK_COMMAND_TEXT = 16,
        NK_COMMAND_IMAGE = 17,
        NK_COMMAND_CUSTOM = 18
    };

    [::System::Flags]
    public enum class NkPanelFlags
    {
        NK_WINDOW_BORDER = 1,
        NK_WINDOW_MOVABLE = 2,
        NK_WINDOW_SCALABLE = 4,
        NK_WINDOW_CLOSABLE = 8,
        NK_WINDOW_MINIMIZABLE = 16,
        NK_WINDOW_NO_SCROLLBAR = 32,
        NK_WINDOW_TITLE = 64,
        NK_WINDOW_SCROLL_AUTO_HIDE = 128,
        NK_WINDOW_BACKGROUND = 256,
        NK_WINDOW_SCALE_LEFT = 512,
        NK_WINDOW_NO_INPUT = 1024
    };

    public enum class NkWidgetLayoutStates
    {
        NK_WIDGET_INVALID = 0,
        NK_WIDGET_VALID = 1,
        NK_WIDGET_ROM = 2
    };

    public enum class NkWidgetStates
    {
        NK_WIDGET_STATE_MODIFIED = 2,
        NK_WIDGET_STATE_INACTIVE = 4,
        NK_WIDGET_STATE_ENTERED = 8,
        NK_WIDGET_STATE_HOVER = 16,
        NK_WIDGET_STATE_ACTIVED = 32,
        NK_WIDGET_STATE_LEFT = 64,
        NK_WIDGET_STATE_HOVERED = 18,
        NK_WIDGET_STATE_ACTIVE = 34
    };

    [::System::Flags]
    public enum class NkTextAlign
    {
        NK_TEXT_ALIGN_LEFT = 1,
        NK_TEXT_ALIGN_CENTERED = 2,
        NK_TEXT_ALIGN_RIGHT = 4,
        NK_TEXT_ALIGN_TOP = 8,
        NK_TEXT_ALIGN_MIDDLE = 16,
        NK_TEXT_ALIGN_BOTTOM = 32
    };

    public enum class NkTextAlignment
    {
        NK_TEXT_LEFT = 17,
        NK_TEXT_CENTERED = 18,
        NK_TEXT_RIGHT = 20
    };

    [::System::Flags]
    public enum class NkEditFlags
    {
        NK_EDIT_DEFAULT = 0,
        NK_EDIT_READ_ONLY = 1,
        NK_EDIT_AUTO_SELECT = 2,
        NK_EDIT_SIG_ENTER = 4,
        NK_EDIT_ALLOW_TAB = 8,
        NK_EDIT_NO_CURSOR = 16,
        NK_EDIT_SELECTABLE = 32,
        NK_EDIT_CLIPBOARD = 64,
        NK_EDIT_CTRL_ENTER_NEWLINE = 128,
        NK_EDIT_NO_HORIZONTAL_SCROLL = 256,
        NK_EDIT_ALWAYS_INSERT_MODE = 512,
        NK_EDIT_MULTILINE = 1024,
        NK_EDIT_GOTO_END_ON_ACTIVATE = 2048
    };

    public enum class NkEditTypes
    {
        NK_EDIT_SIMPLE = 512,
        NK_EDIT_FIELD = 608,
        NK_EDIT_BOX = 1640,
        NK_EDIT_EDITOR = 1128
    };

    [::System::Flags]
    public enum class NkEditEvents
    {
        NK_EDIT_ACTIVE = 1,
        NK_EDIT_INACTIVE = 2,
        NK_EDIT_ACTIVATED = 4,
        NK_EDIT_DEACTIVATED = 8,
        NK_EDIT_COMMITED = 16
    };

    public enum class NkStyleColors
    {
        NK_COLOR_TEXT = 0,
        NK_COLOR_WINDOW = 1,
        NK_COLOR_HEADER = 2,
        NK_COLOR_BORDER = 3,
        NK_COLOR_BUTTON = 4,
        NK_COLOR_BUTTON_HOVER = 5,
        NK_COLOR_BUTTON_ACTIVE = 6,
        NK_COLOR_TOGGLE = 7,
        NK_COLOR_TOGGLE_HOVER = 8,
        NK_COLOR_TOGGLE_CURSOR = 9,
        NK_COLOR_SELECT = 10,
        NK_COLOR_SELECT_ACTIVE = 11,
        NK_COLOR_SLIDER = 12,
        NK_COLOR_SLIDER_CURSOR = 13,
        NK_COLOR_SLIDER_CURSOR_HOVER = 14,
        NK_COLOR_SLIDER_CURSOR_ACTIVE = 15,
        NK_COLOR_PROPERTY = 16,
        NK_COLOR_EDIT = 17,
        NK_COLOR_EDIT_CURSOR = 18,
        NK_COLOR_COMBO = 19,
        NK_COLOR_CHART = 20,
        NK_COLOR_CHART_COLOR = 21,
        NK_COLOR_CHART_COLOR_HIGHLIGHT = 22,
        NK_COLOR_SCROLLBAR = 23,
        NK_COLOR_SCROLLBAR_CURSOR = 24,
        NK_COLOR_SCROLLBAR_CURSOR_HOVER = 25,
        NK_COLOR_SCROLLBAR_CURSOR_ACTIVE = 26,
        NK_COLOR_TAB_HEADER = 27,
        NK_COLOR_COUNT = 28
    };

    public enum class NkStyleCursor
    {
        NK_CURSOR_ARROW = 0,
        NK_CURSOR_TEXT = 1,
        NK_CURSOR_MOVE = 2,
        NK_CURSOR_RESIZE_VERTICAL = 3,
        NK_CURSOR_RESIZE_HORIZONTAL = 4,
        NK_CURSOR_RESIZE_TOP_LEFT_DOWN_RIGHT = 5,
        NK_CURSOR_RESIZE_TOP_RIGHT_DOWN_LEFT = 6,
        NK_CURSOR_COUNT = 7
    };

    public enum class NkBufferAllocationType
    {
        NK_BUFFER_FRONT = 0,
        NK_BUFFER_BACK = 1,
        NK_BUFFER_MAX = 2
    };

    public enum class NkTextEditType
    {
        NK_TEXT_EDIT_SINGLE_LINE = 0,
        NK_TEXT_EDIT_MULTI_LINE = 1
    };

    public enum class NkTextEditMode
    {
        NK_TEXT_EDIT_MODE_VIEW = 0,
        NK_TEXT_EDIT_MODE_INSERT = 1,
        NK_TEXT_EDIT_MODE_REPLACE = 2
    };

    public enum class NkCommandClipping
    {
        NK_CLIPPING_OFF = 0,
        NK_CLIPPING_ON = 1
    };

    public enum class NkPanelSet
    {
        NK_PANEL_SET_NONBLOCK = 240,
        NK_PANEL_SET_POPUP = 244,
        NK_PANEL_SET_SUB = 246
    };

    public enum class NkWindowFlags
    {
        NK_WINDOW_PRIVATE = 2048,
        NK_WINDOW_DYNAMIC = 2048,
        NK_WINDOW_ROM = 4096,
        NK_WINDOW_NOT_INTERACTIVE = 5120,
        NK_WINDOW_HIDDEN = 8192,
        NK_WINDOW_CLOSED = 16384,
        NK_WINDOW_MINIMIZED = 32768,
        NK_WINDOW_REMOVE_ROM = 65536
    };

    [::System::Runtime::InteropServices::UnmanagedFunctionPointer(::System::Runtime::InteropServices::CallingConvention::Cdecl)] 
    public delegate ::System::IntPtr NkPluginAlloc(::nuklear::NkHandle __0, ::System::IntPtr old, unsigned long long __1);

    [::System::Runtime::InteropServices::UnmanagedFunctionPointer(::System::Runtime::InteropServices::CallingConvention::Cdecl)] 
    public delegate void NkPluginFree(::nuklear::NkHandle __0, ::System::IntPtr old);

    [::System::Runtime::InteropServices::UnmanagedFunctionPointer(::System::Runtime::InteropServices::CallingConvention::Cdecl)] 
    public delegate void NkPluginPaste(::nuklear::NkHandle __0, ::nuklear::NkTextEdit^ __1);

    [::System::Runtime::InteropServices::UnmanagedFunctionPointer(::System::Runtime::InteropServices::CallingConvention::Cdecl)] 
    public delegate void NkPluginCopy(::nuklear::NkHandle __0, ::System::String^ __1, int len);

    [::System::Runtime::InteropServices::UnmanagedFunctionPointer(::System::Runtime::InteropServices::CallingConvention::Cdecl)] 
    public delegate int NkPluginFilter(::nuklear::NkTextEdit^ __0, unsigned int unicode);

    [::System::Runtime::InteropServices::UnmanagedFunctionPointer(::System::Runtime::InteropServices::CallingConvention::Cdecl)] 
    public delegate float NkTextWidthF(::nuklear::NkHandle __0, float h, ::System::String^ __1, int len);

    [::System::Runtime::InteropServices::UnmanagedFunctionPointer(::System::Runtime::InteropServices::CallingConvention::Cdecl)] 
    public delegate void NkQueryFontGlyphF(::nuklear::NkHandle handle, float font_height, ::nuklear::NkUserFontGlyph^ glyph, unsigned int codepoint, unsigned int next_codepoint);

    [::System::Runtime::InteropServices::UnmanagedFunctionPointer(::System::Runtime::InteropServices::CallingConvention::Cdecl)] 
    public delegate void NkCommandCustomCallback(::System::IntPtr canvas, short x, short y, unsigned short w, unsigned short h, ::nuklear::NkHandle callback_data);

    [::System::Runtime::InteropServices::UnmanagedFunctionPointer(::System::Runtime::InteropServices::CallingConvention::Cdecl)] 
    public delegate float Func_float___IntPtr_int(::System::IntPtr user, int index);

    [::System::Runtime::InteropServices::UnmanagedFunctionPointer(::System::Runtime::InteropServices::CallingConvention::Cdecl)] 
    public delegate void Action___IntPtr_int_sbytePtrPtr(::System::IntPtr __0, int __1, ::System::String^* __2);

    public ref class NkColor : ICppInstance
    {
    public:

        property struct ::nk_color* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkColor(struct ::nk_color* native);
        NkColor(struct ::nk_color* native, bool ownNativeInstance);
        static NkColor^ __CreateInstance(::System::IntPtr native);
        static NkColor^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkColor();

        NkColor(::nuklear::NkColor^ _0);

        ~NkColor();

        !NkColor();

        property unsigned char R
        {
            unsigned char get();
            void set(unsigned char);
        }

        property unsigned char G
        {
            unsigned char get();
            void set(unsigned char);
        }

        property unsigned char B
        {
            unsigned char get();
            void set(unsigned char);
        }

        property unsigned char A
        {
            unsigned char get();
            void set(unsigned char);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkColorf : ICppInstance
    {
    public:

        property struct ::nk_colorf* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkColorf(struct ::nk_colorf* native);
        NkColorf(struct ::nk_colorf* native, bool ownNativeInstance);
        static NkColorf^ __CreateInstance(::System::IntPtr native);
        static NkColorf^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkColorf();

        NkColorf(::nuklear::NkColorf^ _0);

        ~NkColorf();

        !NkColorf();

        property float R
        {
            float get();
            void set(float);
        }

        property float G
        {
            float get();
            void set(float);
        }

        property float B
        {
            float get();
            void set(float);
        }

        property float A
        {
            float get();
            void set(float);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkVec2 : ICppInstance
    {
    public:

        property struct ::nk_vec2* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkVec2(struct ::nk_vec2* native);
        NkVec2(struct ::nk_vec2* native, bool ownNativeInstance);
        static NkVec2^ __CreateInstance(::System::IntPtr native);
        static NkVec2^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkVec2();

        NkVec2(::nuklear::NkVec2^ _0);

        ~NkVec2();

        !NkVec2();

        property float X
        {
            float get();
            void set(float);
        }

        property float Y
        {
            float get();
            void set(float);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkVec2i : ICppInstance
    {
    public:

        property struct ::nk_vec2i* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkVec2i(struct ::nk_vec2i* native);
        NkVec2i(struct ::nk_vec2i* native, bool ownNativeInstance);
        static NkVec2i^ __CreateInstance(::System::IntPtr native);
        static NkVec2i^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkVec2i();

        NkVec2i(::nuklear::NkVec2i^ _0);

        ~NkVec2i();

        !NkVec2i();

        property short X
        {
            short get();
            void set(short);
        }

        property short Y
        {
            short get();
            void set(short);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkRect : ICppInstance
    {
    public:

        property struct ::nk_rect* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkRect(struct ::nk_rect* native);
        NkRect(struct ::nk_rect* native, bool ownNativeInstance);
        NkRect(int x, int y, int w, int h);
        static NkRect^ __CreateInstance(::System::IntPtr native);
        static NkRect^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkRect();

        NkRect(::nuklear::NkRect^ _0);

        ~NkRect();

        !NkRect();

        property float X
        {
            float get();
            void set(float);
        }

        property float Y
        {
            float get();
            void set(float);
        }

        property float W
        {
            float get();
            void set(float);
        }

        property float H
        {
            float get();
            void set(float);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkRecti : ICppInstance
    {
    public:

        property struct ::nk_recti* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkRecti(struct ::nk_recti* native);
        NkRecti(struct ::nk_recti* native, bool ownNativeInstance);
        static NkRecti^ __CreateInstance(::System::IntPtr native);
        static NkRecti^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkRecti();

        NkRecti(::nuklear::NkRecti^ _0);

        ~NkRecti();

        !NkRecti();

        property short X
        {
            short get();
            void set(short);
        }

        property short Y
        {
            short get();
            void set(short);
        }

        property short W
        {
            short get();
            void set(short);
        }

        property short H
        {
            short get();
            void set(short);
        }

    protected:

        bool __ownsNativeInstance;
    };

    [::System::Runtime::InteropServices::StructLayout(::System::Runtime::InteropServices::LayoutKind::Explicit)]
    public value struct NkHandle
    {
    public:

        NkHandle(::nk_handle* native);
        NkHandle(::nk_handle* native, bool ownNativeInstance);
        static NkHandle^ __CreateInstance(::System::IntPtr native);
        static NkHandle^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        property ::System::IntPtr Ptr
        {
            ::System::IntPtr get();
            void set(::System::IntPtr);
        }

        property int Id
        {
            int get();
            void set(int);
        }

    private:

        [::System::Runtime::InteropServices::FieldOffset(0)]
        ::System::IntPtr __ptr;
        [::System::Runtime::InteropServices::FieldOffset(0)]
        int __id;
    };

    public ref class NkImage : ICppInstance
    {
    public:

        property struct ::nk_image* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkImage(struct ::nk_image* native);
        NkImage(struct ::nk_image* native, bool ownNativeInstance);
        static NkImage^ __CreateInstance(::System::IntPtr native);
        static NkImage^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkImage();

        NkImage(::nuklear::NkImage^ _0);

        ~NkImage();

        !NkImage();

        property ::nuklear::NkHandle Handle
        {
            ::nuklear::NkHandle get();
            void set(::nuklear::NkHandle);
        }

        property unsigned short W
        {
            unsigned short get();
            void set(unsigned short);
        }

        property unsigned short H
        {
            unsigned short get();
            void set(unsigned short);
        }

        property cli::array<unsigned short>^ Region
        {
            cli::array<unsigned short>^ get();
            void set(cli::array<unsigned short>^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCursor : ICppInstance
    {
    public:

        property struct ::nk_cursor* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCursor(struct ::nk_cursor* native);
        NkCursor(struct ::nk_cursor* native, bool ownNativeInstance);
        static NkCursor^ __CreateInstance(::System::IntPtr native);
        static NkCursor^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCursor();

        NkCursor(::nuklear::NkCursor^ _0);

        ~NkCursor();

        !NkCursor();

        property ::nuklear::NkImage^ Img
        {
            ::nuklear::NkImage^ get();
            void set(::nuklear::NkImage^);
        }

        property ::nuklear::NkVec2^ Size
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ Offset
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkScroll : ICppInstance
    {
    public:

        property struct ::nk_scroll* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkScroll(struct ::nk_scroll* native);
        NkScroll(struct ::nk_scroll* native, bool ownNativeInstance);
        static NkScroll^ __CreateInstance(::System::IntPtr native);
        static NkScroll^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkScroll();

        NkScroll(::nuklear::NkScroll^ _0);

        ~NkScroll();

        !NkScroll();

        property unsigned int X
        {
            unsigned int get();
            void set(unsigned int);
        }

        property unsigned int Y
        {
            unsigned int get();
            void set(unsigned int);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkAllocator : ICppInstance
    {
    public:

        property struct ::nk_allocator* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkAllocator(struct ::nk_allocator* native);
        NkAllocator(struct ::nk_allocator* native, bool ownNativeInstance);
        static NkAllocator^ __CreateInstance(::System::IntPtr native);
        static NkAllocator^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkAllocator();

        NkAllocator(::nuklear::NkAllocator^ _0);

        ~NkAllocator();

        !NkAllocator();

        property ::nuklear::NkHandle Userdata
        {
            ::nuklear::NkHandle get();
            void set(::nuklear::NkHandle);
        }

        property ::nuklear::NkPluginAlloc^ Alloc
        {
            ::nuklear::NkPluginAlloc^ get();
            void set(::nuklear::NkPluginAlloc^);
        }

        property ::nuklear::NkPluginFree^ Free
        {
            ::nuklear::NkPluginFree^ get();
            void set(::nuklear::NkPluginFree^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkDrawNullTexture : ICppInstance
    {
    public:

        property struct ::nk_draw_null_texture* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkDrawNullTexture(struct ::nk_draw_null_texture* native);
        NkDrawNullTexture(struct ::nk_draw_null_texture* native, bool ownNativeInstance);
        static NkDrawNullTexture^ __CreateInstance(::System::IntPtr native);
        static NkDrawNullTexture^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkDrawNullTexture();

        NkDrawNullTexture(::nuklear::NkDrawNullTexture^ _0);

        ~NkDrawNullTexture();

        !NkDrawNullTexture();

        property ::nuklear::NkHandle Texture
        {
            ::nuklear::NkHandle get();
            void set(::nuklear::NkHandle);
        }

        property ::nuklear::NkVec2^ Uv
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkConvertConfig : ICppInstance
    {
    public:

        property struct ::nk_convert_config* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkConvertConfig(struct ::nk_convert_config* native);
        NkConvertConfig(struct ::nk_convert_config* native, bool ownNativeInstance);
        static NkConvertConfig^ __CreateInstance(::System::IntPtr native);
        static NkConvertConfig^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkConvertConfig();

        NkConvertConfig(::nuklear::NkConvertConfig^ _0);

        ~NkConvertConfig();

        !NkConvertConfig();

        property float GlobalAlpha
        {
            float get();
            void set(float);
        }

        property ::nuklear::NkAntiAliasing LineAA
        {
            ::nuklear::NkAntiAliasing get();
            void set(::nuklear::NkAntiAliasing);
        }

        property ::nuklear::NkAntiAliasing ShapeAA
        {
            ::nuklear::NkAntiAliasing get();
            void set(::nuklear::NkAntiAliasing);
        }

        property unsigned int CircleSegmentCount
        {
            unsigned int get();
            void set(unsigned int);
        }

        property unsigned int ArcSegmentCount
        {
            unsigned int get();
            void set(unsigned int);
        }

        property unsigned int CurveSegmentCount
        {
            unsigned int get();
            void set(unsigned int);
        }

        property ::nuklear::NkDrawNullTexture^ Null
        {
            ::nuklear::NkDrawNullTexture^ get();
            void set(::nuklear::NkDrawNullTexture^);
        }

        property unsigned long long VertexSize
        {
            unsigned long long get();
            void set(unsigned long long);
        }

        property unsigned long long VertexAlignment
        {
            unsigned long long get();
            void set(unsigned long long);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkListView : ICppInstance
    {
    public:

        property struct ::nk_list_view* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkListView(struct ::nk_list_view* native);
        NkListView(struct ::nk_list_view* native, bool ownNativeInstance);
        static NkListView^ __CreateInstance(::System::IntPtr native);
        static NkListView^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkListView();

        NkListView(::nuklear::NkListView^ _0);

        ~NkListView();

        !NkListView();

        property int Begin
        {
            int get();
            void set(int);
        }

        property int End
        {
            int get();
            void set(int);
        }

        property int Count
        {
            int get();
            void set(int);
        }

        property int TotalHeight
        {
            int get();
            void set(int);
        }

        property ::nuklear::NkContext^ Ctx
        {
            ::nuklear::NkContext^ get();
            void set(::nuklear::NkContext^);
        }

        property unsigned int* ScrollPointer
        {
            unsigned int* get();
            void set(unsigned int*);
        }

        property unsigned int ScrollValue
        {
            unsigned int get();
            void set(unsigned int);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkUserFont : ICppInstance
    {
    public:

        property struct ::nk_user_font* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkUserFont(struct ::nk_user_font* native);
        NkUserFont(struct ::nk_user_font* native, bool ownNativeInstance);
        static NkUserFont^ __CreateInstance(::System::IntPtr native);
        static NkUserFont^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkUserFont();

        NkUserFont(::nuklear::NkUserFont^ _0);

        ~NkUserFont();

        !NkUserFont();

        property ::nuklear::NkHandle Userdata
        {
            ::nuklear::NkHandle get();
            void set(::nuklear::NkHandle);
        }

        property float Height
        {
            float get();
            void set(float);
        }

        property ::nuklear::NkTextWidthF^ Width
        {
            ::nuklear::NkTextWidthF^ get();
            void set(::nuklear::NkTextWidthF^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkMemoryStatus : ICppInstance
    {
    public:

        property struct ::nk_memory_status* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkMemoryStatus(struct ::nk_memory_status* native);
        NkMemoryStatus(struct ::nk_memory_status* native, bool ownNativeInstance);
        static NkMemoryStatus^ __CreateInstance(::System::IntPtr native);
        static NkMemoryStatus^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkMemoryStatus();

        NkMemoryStatus(::nuklear::NkMemoryStatus^ _0);

        ~NkMemoryStatus();

        !NkMemoryStatus();

        property ::System::IntPtr Memory
        {
            ::System::IntPtr get();
            void set(::System::IntPtr);
        }

        property unsigned int Type
        {
            unsigned int get();
            void set(unsigned int);
        }

        property unsigned long long Size
        {
            unsigned long long get();
            void set(unsigned long long);
        }

        property unsigned long long Allocated
        {
            unsigned long long get();
            void set(unsigned long long);
        }

        property unsigned long long Needed
        {
            unsigned long long get();
            void set(unsigned long long);
        }

        property unsigned long long Calls
        {
            unsigned long long get();
            void set(unsigned long long);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkBufferMarker : ICppInstance
    {
    public:

        property struct ::nk_buffer_marker* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkBufferMarker(struct ::nk_buffer_marker* native);
        NkBufferMarker(struct ::nk_buffer_marker* native, bool ownNativeInstance);
        static NkBufferMarker^ __CreateInstance(::System::IntPtr native);
        static NkBufferMarker^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkBufferMarker();

        NkBufferMarker(::nuklear::NkBufferMarker^ _0);

        ~NkBufferMarker();

        !NkBufferMarker();

        property int Active
        {
            int get();
            void set(int);
        }

        property unsigned long long Offset
        {
            unsigned long long get();
            void set(unsigned long long);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkMemory : ICppInstance
    {
    public:

        property struct ::nk_memory* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkMemory(struct ::nk_memory* native);
        NkMemory(struct ::nk_memory* native, bool ownNativeInstance);
        static NkMemory^ __CreateInstance(::System::IntPtr native);
        static NkMemory^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkMemory();

        NkMemory(::nuklear::NkMemory^ _0);

        ~NkMemory();

        !NkMemory();

        property ::System::IntPtr Ptr
        {
            ::System::IntPtr get();
            void set(::System::IntPtr);
        }

        property unsigned long long Size
        {
            unsigned long long get();
            void set(unsigned long long);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkBuffer : ICppInstance
    {
    public:

        property struct ::nk_buffer* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkBuffer(struct ::nk_buffer* native);
        NkBuffer(struct ::nk_buffer* native, bool ownNativeInstance);
        static NkBuffer^ __CreateInstance(::System::IntPtr native);
        static NkBuffer^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkBuffer();

        NkBuffer(::nuklear::NkBuffer^ _0);

        ~NkBuffer();

        !NkBuffer();

        property cli::array<::nuklear::NkBufferMarker^>^ Marker
        {
            cli::array<::nuklear::NkBufferMarker^>^ get();
            void set(cli::array<::nuklear::NkBufferMarker^>^);
        }

        property ::nuklear::NkAllocator^ Pool
        {
            ::nuklear::NkAllocator^ get();
            void set(::nuklear::NkAllocator^);
        }

        property ::nuklear::NkAllocationType Type
        {
            ::nuklear::NkAllocationType get();
            void set(::nuklear::NkAllocationType);
        }

        property ::nuklear::NkMemory^ Memory
        {
            ::nuklear::NkMemory^ get();
            void set(::nuklear::NkMemory^);
        }

        property float GrowFactor
        {
            float get();
            void set(float);
        }

        property unsigned long long Allocated
        {
            unsigned long long get();
            void set(unsigned long long);
        }

        property unsigned long long Needed
        {
            unsigned long long get();
            void set(unsigned long long);
        }

        property unsigned long long Calls
        {
            unsigned long long get();
            void set(unsigned long long);
        }

        property unsigned long long Size
        {
            unsigned long long get();
            void set(unsigned long long);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkStr : ICppInstance
    {
    public:

        property struct ::nk_str* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkStr(struct ::nk_str* native);
        NkStr(struct ::nk_str* native, bool ownNativeInstance);
        static NkStr^ __CreateInstance(::System::IntPtr native);
        static NkStr^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkStr();

        NkStr(::nuklear::NkStr^ _0);

        ~NkStr();

        !NkStr();

        property ::nuklear::NkBuffer^ Buffer
        {
            ::nuklear::NkBuffer^ get();
            void set(::nuklear::NkBuffer^);
        }

        property int Len
        {
            int get();
            void set(int);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkClipboard : ICppInstance
    {
    public:

        property struct ::nk_clipboard* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkClipboard(struct ::nk_clipboard* native);
        NkClipboard(struct ::nk_clipboard* native, bool ownNativeInstance);
        static NkClipboard^ __CreateInstance(::System::IntPtr native);
        static NkClipboard^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkClipboard();

        NkClipboard(::nuklear::NkClipboard^ _0);

        ~NkClipboard();

        !NkClipboard();

        property ::nuklear::NkHandle Userdata
        {
            ::nuklear::NkHandle get();
            void set(::nuklear::NkHandle);
        }

        property ::nuklear::NkPluginPaste^ Paste
        {
            ::nuklear::NkPluginPaste^ get();
            void set(::nuklear::NkPluginPaste^);
        }

        property ::nuklear::NkPluginCopy^ Copy
        {
            ::nuklear::NkPluginCopy^ get();
            void set(::nuklear::NkPluginCopy^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkTextUndoRecord : ICppInstance
    {
    public:

        property struct ::nk_text_undo_record* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkTextUndoRecord(struct ::nk_text_undo_record* native);
        NkTextUndoRecord(struct ::nk_text_undo_record* native, bool ownNativeInstance);
        static NkTextUndoRecord^ __CreateInstance(::System::IntPtr native);
        static NkTextUndoRecord^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkTextUndoRecord();

        NkTextUndoRecord(::nuklear::NkTextUndoRecord^ _0);

        ~NkTextUndoRecord();

        !NkTextUndoRecord();

        property int Where
        {
            int get();
            void set(int);
        }

        property short InsertLength
        {
            short get();
            void set(short);
        }

        property short DeleteLength
        {
            short get();
            void set(short);
        }

        property short CharStorage
        {
            short get();
            void set(short);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkTextUndoState : ICppInstance
    {
    public:

        property struct ::nk_text_undo_state* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkTextUndoState(struct ::nk_text_undo_state* native);
        NkTextUndoState(struct ::nk_text_undo_state* native, bool ownNativeInstance);
        static NkTextUndoState^ __CreateInstance(::System::IntPtr native);
        static NkTextUndoState^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkTextUndoState();

        NkTextUndoState(::nuklear::NkTextUndoState^ _0);

        ~NkTextUndoState();

        !NkTextUndoState();

        property cli::array<::nuklear::NkTextUndoRecord^>^ UndoRec
        {
            cli::array<::nuklear::NkTextUndoRecord^>^ get();
            void set(cli::array<::nuklear::NkTextUndoRecord^>^);
        }

        property cli::array<unsigned int>^ UndoChar
        {
            cli::array<unsigned int>^ get();
            void set(cli::array<unsigned int>^);
        }

        property short UndoPoint
        {
            short get();
            void set(short);
        }

        property short RedoPoint
        {
            short get();
            void set(short);
        }

        property short UndoCharPoint
        {
            short get();
            void set(short);
        }

        property short RedoCharPoint
        {
            short get();
            void set(short);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkTextEdit : ICppInstance
    {
    public:

        property struct ::nk_text_edit* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkTextEdit(struct ::nk_text_edit* native);
        NkTextEdit(struct ::nk_text_edit* native, bool ownNativeInstance);
        static NkTextEdit^ __CreateInstance(::System::IntPtr native);
        static NkTextEdit^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkTextEdit();

        NkTextEdit(::nuklear::NkTextEdit^ _0);

        ~NkTextEdit();

        !NkTextEdit();

        property ::nuklear::NkClipboard^ Clip
        {
            ::nuklear::NkClipboard^ get();
            void set(::nuklear::NkClipboard^);
        }

        property ::nuklear::NkStr^ String
        {
            ::nuklear::NkStr^ get();
            void set(::nuklear::NkStr^);
        }

        property ::nuklear::NkPluginFilter^ Filter
        {
            ::nuklear::NkPluginFilter^ get();
            void set(::nuklear::NkPluginFilter^);
        }

        property ::nuklear::NkVec2^ Scrollbar
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property int Cursor
        {
            int get();
            void set(int);
        }

        property int SelectStart
        {
            int get();
            void set(int);
        }

        property int SelectEnd
        {
            int get();
            void set(int);
        }

        property unsigned char Mode
        {
            unsigned char get();
            void set(unsigned char);
        }

        property unsigned char CursorAtEndOfLine
        {
            unsigned char get();
            void set(unsigned char);
        }

        property unsigned char Initialized
        {
            unsigned char get();
            void set(unsigned char);
        }

        property unsigned char HasPreferredX
        {
            unsigned char get();
            void set(unsigned char);
        }

        property unsigned char SingleLine
        {
            unsigned char get();
            void set(unsigned char);
        }

        property unsigned char Active
        {
            unsigned char get();
            void set(unsigned char);
        }

        property unsigned char Padding1
        {
            unsigned char get();
            void set(unsigned char);
        }

        property float PreferredX
        {
            float get();
            void set(float);
        }

        property ::nuklear::NkTextUndoState^ Undo
        {
            ::nuklear::NkTextUndoState^ get();
            void set(::nuklear::NkTextUndoState^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCommand : ICppInstance
    {
    public:

        property struct ::nk_command* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCommand(struct ::nk_command* native);
        NkCommand(struct ::nk_command* native, bool ownNativeInstance);
        static NkCommand^ __CreateInstance(::System::IntPtr native);
        static NkCommand^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCommand();

        NkCommand(::nuklear::NkCommand^ _0);

        ~NkCommand();

        !NkCommand();

        property ::nuklear::NkCommandType Type
        {
            ::nuklear::NkCommandType get();
            void set(::nuklear::NkCommandType);
        }

        property unsigned long long Next
        {
            unsigned long long get();
            void set(unsigned long long);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCommandScissor : ICppInstance
    {
    public:

        property struct ::nk_command_scissor* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCommandScissor(struct ::nk_command_scissor* native);
        NkCommandScissor(struct ::nk_command_scissor* native, bool ownNativeInstance);
        static NkCommandScissor^ __CreateInstance(::System::IntPtr native);
        static NkCommandScissor^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCommandScissor();

        NkCommandScissor(::nuklear::NkCommandScissor^ _0);

        ~NkCommandScissor();

        !NkCommandScissor();

        property ::nuklear::NkCommand^ Header
        {
            ::nuklear::NkCommand^ get();
            void set(::nuklear::NkCommand^);
        }

        property short X
        {
            short get();
            void set(short);
        }

        property short Y
        {
            short get();
            void set(short);
        }

        property unsigned short W
        {
            unsigned short get();
            void set(unsigned short);
        }

        property unsigned short H
        {
            unsigned short get();
            void set(unsigned short);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCommandLine : ICppInstance
    {
    public:

        property struct ::nk_command_line* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCommandLine(struct ::nk_command_line* native);
        NkCommandLine(struct ::nk_command_line* native, bool ownNativeInstance);
        static NkCommandLine^ __CreateInstance(::System::IntPtr native);
        static NkCommandLine^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCommandLine();

        NkCommandLine(::nuklear::NkCommandLine^ _0);

        ~NkCommandLine();

        !NkCommandLine();

        property ::nuklear::NkCommand^ Header
        {
            ::nuklear::NkCommand^ get();
            void set(::nuklear::NkCommand^);
        }

        property unsigned short LineThickness
        {
            unsigned short get();
            void set(unsigned short);
        }

        property ::nuklear::NkVec2i^ Begin
        {
            ::nuklear::NkVec2i^ get();
            void set(::nuklear::NkVec2i^);
        }

        property ::nuklear::NkVec2i^ End
        {
            ::nuklear::NkVec2i^ get();
            void set(::nuklear::NkVec2i^);
        }

        property ::nuklear::NkColor^ Color
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCommandCurve : ICppInstance
    {
    public:

        property struct ::nk_command_curve* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCommandCurve(struct ::nk_command_curve* native);
        NkCommandCurve(struct ::nk_command_curve* native, bool ownNativeInstance);
        static NkCommandCurve^ __CreateInstance(::System::IntPtr native);
        static NkCommandCurve^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCommandCurve();

        NkCommandCurve(::nuklear::NkCommandCurve^ _0);

        ~NkCommandCurve();

        !NkCommandCurve();

        property ::nuklear::NkCommand^ Header
        {
            ::nuklear::NkCommand^ get();
            void set(::nuklear::NkCommand^);
        }

        property unsigned short LineThickness
        {
            unsigned short get();
            void set(unsigned short);
        }

        property ::nuklear::NkVec2i^ Begin
        {
            ::nuklear::NkVec2i^ get();
            void set(::nuklear::NkVec2i^);
        }

        property ::nuklear::NkVec2i^ End
        {
            ::nuklear::NkVec2i^ get();
            void set(::nuklear::NkVec2i^);
        }

        property cli::array<::nuklear::NkVec2i^>^ Ctrl
        {
            cli::array<::nuklear::NkVec2i^>^ get();
            void set(cli::array<::nuklear::NkVec2i^>^);
        }

        property ::nuklear::NkColor^ Color
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCommandRect : ICppInstance
    {
    public:

        property struct ::nk_command_rect* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCommandRect(struct ::nk_command_rect* native);
        NkCommandRect(struct ::nk_command_rect* native, bool ownNativeInstance);
        static NkCommandRect^ __CreateInstance(::System::IntPtr native);
        static NkCommandRect^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCommandRect();

        NkCommandRect(::nuklear::NkCommandRect^ _0);

        ~NkCommandRect();

        !NkCommandRect();

        property ::nuklear::NkCommand^ Header
        {
            ::nuklear::NkCommand^ get();
            void set(::nuklear::NkCommand^);
        }

        property unsigned short Rounding
        {
            unsigned short get();
            void set(unsigned short);
        }

        property unsigned short LineThickness
        {
            unsigned short get();
            void set(unsigned short);
        }

        property short X
        {
            short get();
            void set(short);
        }

        property short Y
        {
            short get();
            void set(short);
        }

        property unsigned short W
        {
            unsigned short get();
            void set(unsigned short);
        }

        property unsigned short H
        {
            unsigned short get();
            void set(unsigned short);
        }

        property ::nuklear::NkColor^ Color
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCommandRectFilled : ICppInstance
    {
    public:

        property struct ::nk_command_rect_filled* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCommandRectFilled(struct ::nk_command_rect_filled* native);
        NkCommandRectFilled(struct ::nk_command_rect_filled* native, bool ownNativeInstance);
        static NkCommandRectFilled^ __CreateInstance(::System::IntPtr native);
        static NkCommandRectFilled^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCommandRectFilled();

        NkCommandRectFilled(::nuklear::NkCommandRectFilled^ _0);

        ~NkCommandRectFilled();

        !NkCommandRectFilled();

        property ::nuklear::NkCommand^ Header
        {
            ::nuklear::NkCommand^ get();
            void set(::nuklear::NkCommand^);
        }

        property unsigned short Rounding
        {
            unsigned short get();
            void set(unsigned short);
        }

        property short X
        {
            short get();
            void set(short);
        }

        property short Y
        {
            short get();
            void set(short);
        }

        property unsigned short W
        {
            unsigned short get();
            void set(unsigned short);
        }

        property unsigned short H
        {
            unsigned short get();
            void set(unsigned short);
        }

        property ::nuklear::NkColor^ Color
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCommandRectMultiColor : ICppInstance
    {
    public:

        property struct ::nk_command_rect_multi_color* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCommandRectMultiColor(struct ::nk_command_rect_multi_color* native);
        NkCommandRectMultiColor(struct ::nk_command_rect_multi_color* native, bool ownNativeInstance);
        static NkCommandRectMultiColor^ __CreateInstance(::System::IntPtr native);
        static NkCommandRectMultiColor^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCommandRectMultiColor();

        NkCommandRectMultiColor(::nuklear::NkCommandRectMultiColor^ _0);

        ~NkCommandRectMultiColor();

        !NkCommandRectMultiColor();

        property ::nuklear::NkCommand^ Header
        {
            ::nuklear::NkCommand^ get();
            void set(::nuklear::NkCommand^);
        }

        property short X
        {
            short get();
            void set(short);
        }

        property short Y
        {
            short get();
            void set(short);
        }

        property unsigned short W
        {
            unsigned short get();
            void set(unsigned short);
        }

        property unsigned short H
        {
            unsigned short get();
            void set(unsigned short);
        }

        property ::nuklear::NkColor^ Left
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ Top
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ Bottom
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ Right
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCommandTriangle : ICppInstance
    {
    public:

        property struct ::nk_command_triangle* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCommandTriangle(struct ::nk_command_triangle* native);
        NkCommandTriangle(struct ::nk_command_triangle* native, bool ownNativeInstance);
        static NkCommandTriangle^ __CreateInstance(::System::IntPtr native);
        static NkCommandTriangle^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCommandTriangle();

        NkCommandTriangle(::nuklear::NkCommandTriangle^ _0);

        ~NkCommandTriangle();

        !NkCommandTriangle();

        property ::nuklear::NkCommand^ Header
        {
            ::nuklear::NkCommand^ get();
            void set(::nuklear::NkCommand^);
        }

        property unsigned short LineThickness
        {
            unsigned short get();
            void set(unsigned short);
        }

        property ::nuklear::NkVec2i^ A
        {
            ::nuklear::NkVec2i^ get();
            void set(::nuklear::NkVec2i^);
        }

        property ::nuklear::NkVec2i^ B
        {
            ::nuklear::NkVec2i^ get();
            void set(::nuklear::NkVec2i^);
        }

        property ::nuklear::NkVec2i^ C
        {
            ::nuklear::NkVec2i^ get();
            void set(::nuklear::NkVec2i^);
        }

        property ::nuklear::NkColor^ Color
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCommandTriangleFilled : ICppInstance
    {
    public:

        property struct ::nk_command_triangle_filled* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCommandTriangleFilled(struct ::nk_command_triangle_filled* native);
        NkCommandTriangleFilled(struct ::nk_command_triangle_filled* native, bool ownNativeInstance);
        static NkCommandTriangleFilled^ __CreateInstance(::System::IntPtr native);
        static NkCommandTriangleFilled^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCommandTriangleFilled();

        NkCommandTriangleFilled(::nuklear::NkCommandTriangleFilled^ _0);

        ~NkCommandTriangleFilled();

        !NkCommandTriangleFilled();

        property ::nuklear::NkCommand^ Header
        {
            ::nuklear::NkCommand^ get();
            void set(::nuklear::NkCommand^);
        }

        property ::nuklear::NkVec2i^ A
        {
            ::nuklear::NkVec2i^ get();
            void set(::nuklear::NkVec2i^);
        }

        property ::nuklear::NkVec2i^ B
        {
            ::nuklear::NkVec2i^ get();
            void set(::nuklear::NkVec2i^);
        }

        property ::nuklear::NkVec2i^ C
        {
            ::nuklear::NkVec2i^ get();
            void set(::nuklear::NkVec2i^);
        }

        property ::nuklear::NkColor^ Color
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCommandCircle : ICppInstance
    {
    public:

        property struct ::nk_command_circle* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCommandCircle(struct ::nk_command_circle* native);
        NkCommandCircle(struct ::nk_command_circle* native, bool ownNativeInstance);
        static NkCommandCircle^ __CreateInstance(::System::IntPtr native);
        static NkCommandCircle^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCommandCircle();

        NkCommandCircle(::nuklear::NkCommandCircle^ _0);

        ~NkCommandCircle();

        !NkCommandCircle();

        property ::nuklear::NkCommand^ Header
        {
            ::nuklear::NkCommand^ get();
            void set(::nuklear::NkCommand^);
        }

        property short X
        {
            short get();
            void set(short);
        }

        property short Y
        {
            short get();
            void set(short);
        }

        property unsigned short LineThickness
        {
            unsigned short get();
            void set(unsigned short);
        }

        property unsigned short W
        {
            unsigned short get();
            void set(unsigned short);
        }

        property unsigned short H
        {
            unsigned short get();
            void set(unsigned short);
        }

        property ::nuklear::NkColor^ Color
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCommandCircleFilled : ICppInstance
    {
    public:

        property struct ::nk_command_circle_filled* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCommandCircleFilled(struct ::nk_command_circle_filled* native);
        NkCommandCircleFilled(struct ::nk_command_circle_filled* native, bool ownNativeInstance);
        static NkCommandCircleFilled^ __CreateInstance(::System::IntPtr native);
        static NkCommandCircleFilled^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCommandCircleFilled();

        NkCommandCircleFilled(::nuklear::NkCommandCircleFilled^ _0);

        ~NkCommandCircleFilled();

        !NkCommandCircleFilled();

        property ::nuklear::NkCommand^ Header
        {
            ::nuklear::NkCommand^ get();
            void set(::nuklear::NkCommand^);
        }

        property short X
        {
            short get();
            void set(short);
        }

        property short Y
        {
            short get();
            void set(short);
        }

        property unsigned short W
        {
            unsigned short get();
            void set(unsigned short);
        }

        property unsigned short H
        {
            unsigned short get();
            void set(unsigned short);
        }

        property ::nuklear::NkColor^ Color
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCommandArc : ICppInstance
    {
    public:

        property struct ::nk_command_arc* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCommandArc(struct ::nk_command_arc* native);
        NkCommandArc(struct ::nk_command_arc* native, bool ownNativeInstance);
        static NkCommandArc^ __CreateInstance(::System::IntPtr native);
        static NkCommandArc^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCommandArc();

        NkCommandArc(::nuklear::NkCommandArc^ _0);

        ~NkCommandArc();

        !NkCommandArc();

        property ::nuklear::NkCommand^ Header
        {
            ::nuklear::NkCommand^ get();
            void set(::nuklear::NkCommand^);
        }

        property short Cx
        {
            short get();
            void set(short);
        }

        property short Cy
        {
            short get();
            void set(short);
        }

        property unsigned short R
        {
            unsigned short get();
            void set(unsigned short);
        }

        property unsigned short LineThickness
        {
            unsigned short get();
            void set(unsigned short);
        }

        property cli::array<float>^ A
        {
            cli::array<float>^ get();
            void set(cli::array<float>^);
        }

        property ::nuklear::NkColor^ Color
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCommandArcFilled : ICppInstance
    {
    public:

        property struct ::nk_command_arc_filled* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCommandArcFilled(struct ::nk_command_arc_filled* native);
        NkCommandArcFilled(struct ::nk_command_arc_filled* native, bool ownNativeInstance);
        static NkCommandArcFilled^ __CreateInstance(::System::IntPtr native);
        static NkCommandArcFilled^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCommandArcFilled();

        NkCommandArcFilled(::nuklear::NkCommandArcFilled^ _0);

        ~NkCommandArcFilled();

        !NkCommandArcFilled();

        property ::nuklear::NkCommand^ Header
        {
            ::nuklear::NkCommand^ get();
            void set(::nuklear::NkCommand^);
        }

        property short Cx
        {
            short get();
            void set(short);
        }

        property short Cy
        {
            short get();
            void set(short);
        }

        property unsigned short R
        {
            unsigned short get();
            void set(unsigned short);
        }

        property cli::array<float>^ A
        {
            cli::array<float>^ get();
            void set(cli::array<float>^);
        }

        property ::nuklear::NkColor^ Color
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCommandPolygon : ICppInstance
    {
    public:

        property struct ::nk_command_polygon* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCommandPolygon(struct ::nk_command_polygon* native);
        NkCommandPolygon(struct ::nk_command_polygon* native, bool ownNativeInstance);
        static NkCommandPolygon^ __CreateInstance(::System::IntPtr native);
        static NkCommandPolygon^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCommandPolygon();

        NkCommandPolygon(::nuklear::NkCommandPolygon^ _0);

        ~NkCommandPolygon();

        !NkCommandPolygon();

        property ::nuklear::NkCommand^ Header
        {
            ::nuklear::NkCommand^ get();
            void set(::nuklear::NkCommand^);
        }

        property ::nuklear::NkColor^ Color
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property unsigned short LineThickness
        {
            unsigned short get();
            void set(unsigned short);
        }

        property unsigned short PointCount
        {
            unsigned short get();
            void set(unsigned short);
        }

        property cli::array<::nuklear::NkVec2i^>^ Points
        {
            cli::array<::nuklear::NkVec2i^>^ get();
            void set(cli::array<::nuklear::NkVec2i^>^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCommandPolygonFilled : ICppInstance
    {
    public:

        property struct ::nk_command_polygon_filled* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCommandPolygonFilled(struct ::nk_command_polygon_filled* native);
        NkCommandPolygonFilled(struct ::nk_command_polygon_filled* native, bool ownNativeInstance);
        static NkCommandPolygonFilled^ __CreateInstance(::System::IntPtr native);
        static NkCommandPolygonFilled^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCommandPolygonFilled();

        NkCommandPolygonFilled(::nuklear::NkCommandPolygonFilled^ _0);

        ~NkCommandPolygonFilled();

        !NkCommandPolygonFilled();

        property ::nuklear::NkCommand^ Header
        {
            ::nuklear::NkCommand^ get();
            void set(::nuklear::NkCommand^);
        }

        property ::nuklear::NkColor^ Color
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property unsigned short PointCount
        {
            unsigned short get();
            void set(unsigned short);
        }

        property cli::array<::nuklear::NkVec2i^>^ Points
        {
            cli::array<::nuklear::NkVec2i^>^ get();
            void set(cli::array<::nuklear::NkVec2i^>^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCommandPolyline : ICppInstance
    {
    public:

        property struct ::nk_command_polyline* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCommandPolyline(struct ::nk_command_polyline* native);
        NkCommandPolyline(struct ::nk_command_polyline* native, bool ownNativeInstance);
        static NkCommandPolyline^ __CreateInstance(::System::IntPtr native);
        static NkCommandPolyline^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCommandPolyline();

        NkCommandPolyline(::nuklear::NkCommandPolyline^ _0);

        ~NkCommandPolyline();

        !NkCommandPolyline();

        property ::nuklear::NkCommand^ Header
        {
            ::nuklear::NkCommand^ get();
            void set(::nuklear::NkCommand^);
        }

        property ::nuklear::NkColor^ Color
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property unsigned short LineThickness
        {
            unsigned short get();
            void set(unsigned short);
        }

        property unsigned short PointCount
        {
            unsigned short get();
            void set(unsigned short);
        }

        property cli::array<::nuklear::NkVec2i^>^ Points
        {
            cli::array<::nuklear::NkVec2i^>^ get();
            void set(cli::array<::nuklear::NkVec2i^>^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCommandImage : ICppInstance
    {
    public:

        property struct ::nk_command_image* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCommandImage(struct ::nk_command_image* native);
        NkCommandImage(struct ::nk_command_image* native, bool ownNativeInstance);
        static NkCommandImage^ __CreateInstance(::System::IntPtr native);
        static NkCommandImage^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCommandImage();

        NkCommandImage(::nuklear::NkCommandImage^ _0);

        ~NkCommandImage();

        !NkCommandImage();

        property ::nuklear::NkCommand^ Header
        {
            ::nuklear::NkCommand^ get();
            void set(::nuklear::NkCommand^);
        }

        property short X
        {
            short get();
            void set(short);
        }

        property short Y
        {
            short get();
            void set(short);
        }

        property unsigned short W
        {
            unsigned short get();
            void set(unsigned short);
        }

        property unsigned short H
        {
            unsigned short get();
            void set(unsigned short);
        }

        property ::nuklear::NkImage^ Img
        {
            ::nuklear::NkImage^ get();
            void set(::nuklear::NkImage^);
        }

        property ::nuklear::NkColor^ Col
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCommandCustom : ICppInstance
    {
    public:

        property struct ::nk_command_custom* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCommandCustom(struct ::nk_command_custom* native);
        NkCommandCustom(struct ::nk_command_custom* native, bool ownNativeInstance);
        static NkCommandCustom^ __CreateInstance(::System::IntPtr native);
        static NkCommandCustom^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCommandCustom();

        NkCommandCustom(::nuklear::NkCommandCustom^ _0);

        ~NkCommandCustom();

        !NkCommandCustom();

        property ::nuklear::NkCommand^ Header
        {
            ::nuklear::NkCommand^ get();
            void set(::nuklear::NkCommand^);
        }

        property short X
        {
            short get();
            void set(short);
        }

        property short Y
        {
            short get();
            void set(short);
        }

        property unsigned short W
        {
            unsigned short get();
            void set(unsigned short);
        }

        property unsigned short H
        {
            unsigned short get();
            void set(unsigned short);
        }

        property ::nuklear::NkHandle CallbackData
        {
            ::nuklear::NkHandle get();
            void set(::nuklear::NkHandle);
        }

        property ::nuklear::NkCommandCustomCallback^ Callback
        {
            ::nuklear::NkCommandCustomCallback^ get();
            void set(::nuklear::NkCommandCustomCallback^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCommandText : ICppInstance
    {
    public:

        property struct ::nk_command_text* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCommandText(struct ::nk_command_text* native);
        NkCommandText(struct ::nk_command_text* native, bool ownNativeInstance);
        static NkCommandText^ __CreateInstance(::System::IntPtr native);
        static NkCommandText^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCommandText();

        NkCommandText(::nuklear::NkCommandText^ _0);

        ~NkCommandText();

        !NkCommandText();

        property ::nuklear::NkCommand^ Header
        {
            ::nuklear::NkCommand^ get();
            void set(::nuklear::NkCommand^);
        }

        property ::nuklear::NkUserFont^ Font
        {
            ::nuklear::NkUserFont^ get();
        }

        property ::nuklear::NkColor^ Background
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ Foreground
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property short X
        {
            short get();
            void set(short);
        }

        property short Y
        {
            short get();
            void set(short);
        }

        property unsigned short W
        {
            unsigned short get();
            void set(unsigned short);
        }

        property unsigned short H
        {
            unsigned short get();
            void set(unsigned short);
        }

        property float Height
        {
            float get();
            void set(float);
        }

        property int Length
        {
            int get();
            void set(int);
        }

        property cli::array<char>^ String
        {
            cli::array<char>^ get();
            void set(cli::array<char>^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkCommandBuffer : ICppInstance
    {
    public:

        property struct ::nk_command_buffer* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkCommandBuffer(struct ::nk_command_buffer* native);
        NkCommandBuffer(struct ::nk_command_buffer* native, bool ownNativeInstance);
        static NkCommandBuffer^ __CreateInstance(::System::IntPtr native);
        static NkCommandBuffer^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkCommandBuffer();

        NkCommandBuffer(::nuklear::NkCommandBuffer^ _0);

        ~NkCommandBuffer();

        !NkCommandBuffer();

        property ::nuklear::NkBuffer^ Base
        {
            ::nuklear::NkBuffer^ get();
            void set(::nuklear::NkBuffer^);
        }

        property ::nuklear::NkRect^ Clip
        {
            ::nuklear::NkRect^ get();
            void set(::nuklear::NkRect^);
        }

        property int UseClipping
        {
            int get();
            void set(int);
        }

        property ::nuklear::NkHandle Userdata
        {
            ::nuklear::NkHandle get();
            void set(::nuklear::NkHandle);
        }

        property unsigned long long Begin
        {
            unsigned long long get();
            void set(unsigned long long);
        }

        property unsigned long long End
        {
            unsigned long long get();
            void set(unsigned long long);
        }

        property unsigned long long Last
        {
            unsigned long long get();
            void set(unsigned long long);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkMouseButton : ICppInstance
    {
    public:

        property struct ::nk_mouse_button* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkMouseButton(struct ::nk_mouse_button* native);
        NkMouseButton(struct ::nk_mouse_button* native, bool ownNativeInstance);
        static NkMouseButton^ __CreateInstance(::System::IntPtr native);
        static NkMouseButton^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkMouseButton();

        NkMouseButton(::nuklear::NkMouseButton^ _0);

        ~NkMouseButton();

        !NkMouseButton();

        property int Down
        {
            int get();
            void set(int);
        }

        property unsigned int Clicked
        {
            unsigned int get();
            void set(unsigned int);
        }

        property ::nuklear::NkVec2^ ClickedPos
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkMouse : ICppInstance
    {
    public:

        property struct ::nk_mouse* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkMouse(struct ::nk_mouse* native);
        NkMouse(struct ::nk_mouse* native, bool ownNativeInstance);
        static NkMouse^ __CreateInstance(::System::IntPtr native);
        static NkMouse^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkMouse();

        NkMouse(::nuklear::NkMouse^ _0);

        ~NkMouse();

        !NkMouse();

        property cli::array<::nuklear::NkMouseButton^>^ Buttons
        {
            cli::array<::nuklear::NkMouseButton^>^ get();
            void set(cli::array<::nuklear::NkMouseButton^>^);
        }

        property ::nuklear::NkVec2^ Pos
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ Prev
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ Delta
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ ScrollDelta
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property unsigned char Grab
        {
            unsigned char get();
            void set(unsigned char);
        }

        property unsigned char Grabbed
        {
            unsigned char get();
            void set(unsigned char);
        }

        property unsigned char Ungrab
        {
            unsigned char get();
            void set(unsigned char);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkKey : ICppInstance
    {
    public:

        property struct ::nk_key* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkKey(struct ::nk_key* native);
        NkKey(struct ::nk_key* native, bool ownNativeInstance);
        static NkKey^ __CreateInstance(::System::IntPtr native);
        static NkKey^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkKey();

        NkKey(::nuklear::NkKey^ _0);

        ~NkKey();

        !NkKey();

        property int Down
        {
            int get();
            void set(int);
        }

        property unsigned int Clicked
        {
            unsigned int get();
            void set(unsigned int);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkKeyboard : ICppInstance
    {
    public:

        property struct ::nk_keyboard* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkKeyboard(struct ::nk_keyboard* native);
        NkKeyboard(struct ::nk_keyboard* native, bool ownNativeInstance);
        static NkKeyboard^ __CreateInstance(::System::IntPtr native);
        static NkKeyboard^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkKeyboard();

        NkKeyboard(::nuklear::NkKeyboard^ _0);

        ~NkKeyboard();

        !NkKeyboard();

        property cli::array<::nuklear::NkKey^>^ Keys
        {
            cli::array<::nuklear::NkKey^>^ get();
            void set(cli::array<::nuklear::NkKey^>^);
        }

        property cli::array<char>^ Text
        {
            cli::array<char>^ get();
            void set(cli::array<char>^);
        }

        property int TextLen
        {
            int get();
            void set(int);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkInput : ICppInstance
    {
    public:

        property struct ::nk_input* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkInput(struct ::nk_input* native);
        NkInput(struct ::nk_input* native, bool ownNativeInstance);
        static NkInput^ __CreateInstance(::System::IntPtr native);
        static NkInput^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkInput();

        NkInput(::nuklear::NkInput^ _0);

        ~NkInput();

        !NkInput();

        property ::nuklear::NkKeyboard^ Keyboard
        {
            ::nuklear::NkKeyboard^ get();
            void set(::nuklear::NkKeyboard^);
        }

        property ::nuklear::NkMouse^ Mouse
        {
            ::nuklear::NkMouse^ get();
            void set(::nuklear::NkMouse^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    [::System::Runtime::InteropServices::StructLayout(::System::Runtime::InteropServices::LayoutKind::Explicit)]
    public value struct NkStyleItemData
    {
    public:

        NkStyleItemData(::nk_style_item_data* native);
        NkStyleItemData(::nk_style_item_data* native, bool ownNativeInstance);
        static NkStyleItemData^ __CreateInstance(::System::IntPtr native);
        static NkStyleItemData^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        property ::nuklear::NkImage^ Image
        {
            ::nuklear::NkImage^ get();
            void set(::nuklear::NkImage^);
        }

        property ::nuklear::NkColor^ Color
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

    private:

        [::System::Runtime::InteropServices::FieldOffset(0)]
        ::nuklear::NkImage^ __image;
        [::System::Runtime::InteropServices::FieldOffset(0)]
        ::nuklear::NkColor^ __color;
    };

    public ref class NkStyleItem : ICppInstance
    {
    public:

        property struct ::nk_style_item* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkStyleItem(struct ::nk_style_item* native);
        NkStyleItem(struct ::nk_style_item* native, bool ownNativeInstance);
        static NkStyleItem^ __CreateInstance(::System::IntPtr native);
        static NkStyleItem^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkStyleItem();

        NkStyleItem(::nuklear::NkStyleItem^ _0);

        ~NkStyleItem();

        !NkStyleItem();

        property ::nuklear::NkStyleItemType Type
        {
            ::nuklear::NkStyleItemType get();
            void set(::nuklear::NkStyleItemType);
        }

        property ::nuklear::NkStyleItemData Data
        {
            ::nuklear::NkStyleItemData get();
            void set(::nuklear::NkStyleItemData);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkStyleText : ICppInstance
    {
    public:

        property struct ::nk_style_text* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkStyleText(struct ::nk_style_text* native);
        NkStyleText(struct ::nk_style_text* native, bool ownNativeInstance);
        static NkStyleText^ __CreateInstance(::System::IntPtr native);
        static NkStyleText^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkStyleText();

        NkStyleText(::nuklear::NkStyleText^ _0);

        ~NkStyleText();

        !NkStyleText();

        property ::nuklear::NkColor^ Color
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkVec2^ Padding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkStyleButton : ICppInstance
    {
    public:

        [::System::Runtime::InteropServices::UnmanagedFunctionPointer(::System::Runtime::InteropServices::CallingConvention::Cdecl)] 
        delegate void Action___IntPtr_nuklear_nk_handle___Internal(::nuklear::NkCommandBuffer^ __0, ::nuklear::NkHandle userdata);

        property struct ::nk_style_button* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkStyleButton(struct ::nk_style_button* native);
        NkStyleButton(struct ::nk_style_button* native, bool ownNativeInstance);
        static NkStyleButton^ __CreateInstance(::System::IntPtr native);
        static NkStyleButton^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkStyleButton();

        NkStyleButton(::nuklear::NkStyleButton^ _0);

        ~NkStyleButton();

        !NkStyleButton();

        property ::nuklear::NkStyleItem^ Normal
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ Hover
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ Active
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkColor^ BorderColor
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ TextBackground
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ TextNormal
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ TextHover
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ TextActive
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property unsigned int TextAlignment
        {
            unsigned int get();
            void set(unsigned int);
        }

        property float Border
        {
            float get();
            void set(float);
        }

        property float Rounding
        {
            float get();
            void set(float);
        }

        property ::nuklear::NkVec2^ Padding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ ImagePadding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ TouchPadding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkHandle Userdata
        {
            ::nuklear::NkHandle get();
            void set(::nuklear::NkHandle);
        }

        property ::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ DrawBegin
        {
            ::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ get();
            void set(::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^);
        }

        property ::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ DrawEnd
        {
            ::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ get();
            void set(::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkStyleToggle : ICppInstance
    {
    public:

        property struct ::nk_style_toggle* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkStyleToggle(struct ::nk_style_toggle* native);
        NkStyleToggle(struct ::nk_style_toggle* native, bool ownNativeInstance);
        static NkStyleToggle^ __CreateInstance(::System::IntPtr native);
        static NkStyleToggle^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkStyleToggle();

        NkStyleToggle(::nuklear::NkStyleToggle^ _0);

        ~NkStyleToggle();

        !NkStyleToggle();

        property ::nuklear::NkStyleItem^ Normal
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ Hover
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ Active
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkColor^ BorderColor
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkStyleItem^ CursorNormal
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ CursorHover
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkColor^ TextNormal
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ TextHover
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ TextActive
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ TextBackground
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property unsigned int TextAlignment
        {
            unsigned int get();
            void set(unsigned int);
        }

        property ::nuklear::NkVec2^ Padding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ TouchPadding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property float Spacing
        {
            float get();
            void set(float);
        }

        property float Border
        {
            float get();
            void set(float);
        }

        property ::nuklear::NkHandle Userdata
        {
            ::nuklear::NkHandle get();
            void set(::nuklear::NkHandle);
        }

        property ::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ DrawBegin
        {
            ::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ get();
            void set(::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^);
        }

        property::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ DrawEnd
        {
           ::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ get();
            void set(::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkStyleSelectable : ICppInstance
    {
    public:

        property struct ::nk_style_selectable* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkStyleSelectable(struct ::nk_style_selectable* native);
        NkStyleSelectable(struct ::nk_style_selectable* native, bool ownNativeInstance);
        static NkStyleSelectable^ __CreateInstance(::System::IntPtr native);
        static NkStyleSelectable^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkStyleSelectable();

        NkStyleSelectable(::nuklear::NkStyleSelectable^ _0);

        ~NkStyleSelectable();

        !NkStyleSelectable();

        property ::nuklear::NkStyleItem^ Normal
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ Hover
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ Pressed
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ NormalActive
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ HoverActive
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ PressedActive
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkColor^ TextNormal
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ TextHover
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ TextPressed
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ TextNormalActive
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ TextHoverActive
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ TextPressedActive
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ TextBackground
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property unsigned int TextAlignment
        {
            unsigned int get();
            void set(unsigned int);
        }

        property float Rounding
        {
            float get();
            void set(float);
        }

        property ::nuklear::NkVec2^ Padding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ TouchPadding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ ImagePadding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkHandle Userdata
        {
            ::nuklear::NkHandle get();
            void set(::nuklear::NkHandle);
        }

        property::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ DrawBegin
        {
           ::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ get();
            void set(::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^);
        }

        property::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ DrawEnd
        {
           ::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ get();
            void set(::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkStyleSlider : ICppInstance
    {
    public:

        property struct ::nk_style_slider* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkStyleSlider(struct ::nk_style_slider* native);
        NkStyleSlider(struct ::nk_style_slider* native, bool ownNativeInstance);
        static NkStyleSlider^ __CreateInstance(::System::IntPtr native);
        static NkStyleSlider^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkStyleSlider();

        NkStyleSlider(::nuklear::NkStyleSlider^ _0);

        ~NkStyleSlider();

        !NkStyleSlider();

        property ::nuklear::NkStyleItem^ Normal
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ Hover
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ Active
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkColor^ BorderColor
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ BarNormal
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ BarHover
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ BarActive
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ BarFilled
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkStyleItem^ CursorNormal
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ CursorHover
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ CursorActive
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property float Border
        {
            float get();
            void set(float);
        }

        property float Rounding
        {
            float get();
            void set(float);
        }

        property float BarHeight
        {
            float get();
            void set(float);
        }

        property ::nuklear::NkVec2^ Padding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ Spacing
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ CursorSize
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property int ShowButtons
        {
            int get();
            void set(int);
        }

        property ::nuklear::NkStyleButton^ IncButton
        {
            ::nuklear::NkStyleButton^ get();
            void set(::nuklear::NkStyleButton^);
        }

        property ::nuklear::NkStyleButton^ DecButton
        {
            ::nuklear::NkStyleButton^ get();
            void set(::nuklear::NkStyleButton^);
        }

        property ::nuklear::NkSymbolType IncSymbol
        {
            ::nuklear::NkSymbolType get();
            void set(::nuklear::NkSymbolType);
        }

        property ::nuklear::NkSymbolType DecSymbol
        {
            ::nuklear::NkSymbolType get();
            void set(::nuklear::NkSymbolType);
        }

        property ::nuklear::NkHandle Userdata
        {
            ::nuklear::NkHandle get();
            void set(::nuklear::NkHandle);
        }

        property::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ DrawBegin
        {
           ::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ get();
            void set(::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^);
        }

        property::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ DrawEnd
        {
           ::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ get();
            void set(::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkStyleProgress : ICppInstance
    {
    public:

        property struct ::nk_style_progress* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkStyleProgress(struct ::nk_style_progress* native);
        NkStyleProgress(struct ::nk_style_progress* native, bool ownNativeInstance);
        static NkStyleProgress^ __CreateInstance(::System::IntPtr native);
        static NkStyleProgress^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkStyleProgress();

        NkStyleProgress(::nuklear::NkStyleProgress^ _0);

        ~NkStyleProgress();

        !NkStyleProgress();

        property ::nuklear::NkStyleItem^ Normal
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ Hover
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ Active
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkColor^ BorderColor
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkStyleItem^ CursorNormal
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ CursorHover
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ CursorActive
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkColor^ CursorBorderColor
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property float Rounding
        {
            float get();
            void set(float);
        }

        property float Border
        {
            float get();
            void set(float);
        }

        property float CursorBorder
        {
            float get();
            void set(float);
        }

        property float CursorRounding
        {
            float get();
            void set(float);
        }

        property ::nuklear::NkVec2^ Padding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkHandle Userdata
        {
            ::nuklear::NkHandle get();
            void set(::nuklear::NkHandle);
        }

        property::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ DrawBegin
        {
           ::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ get();
            void set(::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^);
        }

        property::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ DrawEnd
        {
           ::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ get();
            void set(::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkStyleScrollbar : ICppInstance
    {
    public:

        property struct ::nk_style_scrollbar* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkStyleScrollbar(struct ::nk_style_scrollbar* native);
        NkStyleScrollbar(struct ::nk_style_scrollbar* native, bool ownNativeInstance);
        static NkStyleScrollbar^ __CreateInstance(::System::IntPtr native);
        static NkStyleScrollbar^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkStyleScrollbar();

        NkStyleScrollbar(::nuklear::NkStyleScrollbar^ _0);

        ~NkStyleScrollbar();

        !NkStyleScrollbar();

        property ::nuklear::NkStyleItem^ Normal
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ Hover
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ Active
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkColor^ BorderColor
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkStyleItem^ CursorNormal
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ CursorHover
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ CursorActive
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkColor^ CursorBorderColor
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property float Border
        {
            float get();
            void set(float);
        }

        property float Rounding
        {
            float get();
            void set(float);
        }

        property float BorderCursor
        {
            float get();
            void set(float);
        }

        property float RoundingCursor
        {
            float get();
            void set(float);
        }

        property ::nuklear::NkVec2^ Padding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property int ShowButtons
        {
            int get();
            void set(int);
        }

        property ::nuklear::NkStyleButton^ IncButton
        {
            ::nuklear::NkStyleButton^ get();
            void set(::nuklear::NkStyleButton^);
        }

        property ::nuklear::NkStyleButton^ DecButton
        {
            ::nuklear::NkStyleButton^ get();
            void set(::nuklear::NkStyleButton^);
        }

        property ::nuklear::NkSymbolType IncSymbol
        {
            ::nuklear::NkSymbolType get();
            void set(::nuklear::NkSymbolType);
        }

        property ::nuklear::NkSymbolType DecSymbol
        {
            ::nuklear::NkSymbolType get();
            void set(::nuklear::NkSymbolType);
        }

        property ::nuklear::NkHandle Userdata
        {
            ::nuklear::NkHandle get();
            void set(::nuklear::NkHandle);
        }

        property::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ DrawBegin
        {
           ::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ get();
            void set(::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^);
        }

        property::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ DrawEnd
        {
           ::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ get();
            void set(::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkStyleEdit : ICppInstance
    {
    public:

        property struct ::nk_style_edit* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkStyleEdit(struct ::nk_style_edit* native);
        NkStyleEdit(struct ::nk_style_edit* native, bool ownNativeInstance);
        static NkStyleEdit^ __CreateInstance(::System::IntPtr native);
        static NkStyleEdit^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkStyleEdit();

        NkStyleEdit(::nuklear::NkStyleEdit^ _0);

        ~NkStyleEdit();

        !NkStyleEdit();

        property ::nuklear::NkStyleItem^ Normal
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ Hover
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ Active
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkColor^ BorderColor
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkStyleScrollbar^ Scrollbar
        {
            ::nuklear::NkStyleScrollbar^ get();
            void set(::nuklear::NkStyleScrollbar^);
        }

        property ::nuklear::NkColor^ CursorNormal
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ CursorHover
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ CursorTextNormal
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ CursorTextHover
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ TextNormal
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ TextHover
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ TextActive
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ SelectedNormal
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ SelectedHover
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ SelectedTextNormal
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ SelectedTextHover
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property float Border
        {
            float get();
            void set(float);
        }

        property float Rounding
        {
            float get();
            void set(float);
        }

        property float CursorSize
        {
            float get();
            void set(float);
        }

        property ::nuklear::NkVec2^ ScrollbarSize
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ Padding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property float RowPadding
        {
            float get();
            void set(float);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkStyleProperty : ICppInstance
    {
    public:

        property struct ::nk_style_property* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkStyleProperty(struct ::nk_style_property* native);
        NkStyleProperty(struct ::nk_style_property* native, bool ownNativeInstance);
        static NkStyleProperty^ __CreateInstance(::System::IntPtr native);
        static NkStyleProperty^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkStyleProperty();

        NkStyleProperty(::nuklear::NkStyleProperty^ _0);

        ~NkStyleProperty();

        !NkStyleProperty();

        property ::nuklear::NkStyleItem^ Normal
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ Hover
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ Active
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkColor^ BorderColor
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ LabelNormal
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ LabelHover
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ LabelActive
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkSymbolType SymLeft
        {
            ::nuklear::NkSymbolType get();
            void set(::nuklear::NkSymbolType);
        }

        property ::nuklear::NkSymbolType SymRight
        {
            ::nuklear::NkSymbolType get();
            void set(::nuklear::NkSymbolType);
        }

        property float Border
        {
            float get();
            void set(float);
        }

        property float Rounding
        {
            float get();
            void set(float);
        }

        property ::nuklear::NkVec2^ Padding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkStyleEdit^ Edit
        {
            ::nuklear::NkStyleEdit^ get();
            void set(::nuklear::NkStyleEdit^);
        }

        property ::nuklear::NkStyleButton^ IncButton
        {
            ::nuklear::NkStyleButton^ get();
            void set(::nuklear::NkStyleButton^);
        }

        property ::nuklear::NkStyleButton^ DecButton
        {
            ::nuklear::NkStyleButton^ get();
            void set(::nuklear::NkStyleButton^);
        }

        property ::nuklear::NkHandle Userdata
        {
            ::nuklear::NkHandle get();
            void set(::nuklear::NkHandle);
        }

        property::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ DrawBegin
        {
           ::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ get();
            void set(::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^);
        }

        property::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ DrawEnd
        {
           ::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^ get();
            void set(::nuklear::NkStyleButton::Action___IntPtr_nuklear_nk_handle___Internal^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkStyleChart : ICppInstance
    {
    public:

        property struct ::nk_style_chart* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkStyleChart(struct ::nk_style_chart* native);
        NkStyleChart(struct ::nk_style_chart* native, bool ownNativeInstance);
        static NkStyleChart^ __CreateInstance(::System::IntPtr native);
        static NkStyleChart^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkStyleChart();

        NkStyleChart(::nuklear::NkStyleChart^ _0);

        ~NkStyleChart();

        !NkStyleChart();

        property ::nuklear::NkStyleItem^ Background
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkColor^ BorderColor
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ SelectedColor
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ Color
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property float Border
        {
            float get();
            void set(float);
        }

        property float Rounding
        {
            float get();
            void set(float);
        }

        property ::nuklear::NkVec2^ Padding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkStyleCombo : ICppInstance
    {
    public:

        property struct ::nk_style_combo* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkStyleCombo(struct ::nk_style_combo* native);
        NkStyleCombo(struct ::nk_style_combo* native, bool ownNativeInstance);
        static NkStyleCombo^ __CreateInstance(::System::IntPtr native);
        static NkStyleCombo^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkStyleCombo();

        NkStyleCombo(::nuklear::NkStyleCombo^ _0);

        ~NkStyleCombo();

        !NkStyleCombo();

        property ::nuklear::NkStyleItem^ Normal
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ Hover
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ Active
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkColor^ BorderColor
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ LabelNormal
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ LabelHover
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ LabelActive
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ SymbolNormal
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ SymbolHover
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ SymbolActive
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkStyleButton^ Button
        {
            ::nuklear::NkStyleButton^ get();
            void set(::nuklear::NkStyleButton^);
        }

        property ::nuklear::NkSymbolType SymNormal
        {
            ::nuklear::NkSymbolType get();
            void set(::nuklear::NkSymbolType);
        }

        property ::nuklear::NkSymbolType SymHover
        {
            ::nuklear::NkSymbolType get();
            void set(::nuklear::NkSymbolType);
        }

        property ::nuklear::NkSymbolType SymActive
        {
            ::nuklear::NkSymbolType get();
            void set(::nuklear::NkSymbolType);
        }

        property float Border
        {
            float get();
            void set(float);
        }

        property float Rounding
        {
            float get();
            void set(float);
        }

        property ::nuklear::NkVec2^ ContentPadding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ ButtonPadding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ Spacing
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkStyleTab : ICppInstance
    {
    public:

        property struct ::nk_style_tab* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkStyleTab(struct ::nk_style_tab* native);
        NkStyleTab(struct ::nk_style_tab* native, bool ownNativeInstance);
        static NkStyleTab^ __CreateInstance(::System::IntPtr native);
        static NkStyleTab^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkStyleTab();

        NkStyleTab(::nuklear::NkStyleTab^ _0);

        ~NkStyleTab();

        !NkStyleTab();

        property ::nuklear::NkStyleItem^ Background
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkColor^ BorderColor
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ Text
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkStyleButton^ TabMaximizeButton
        {
            ::nuklear::NkStyleButton^ get();
            void set(::nuklear::NkStyleButton^);
        }

        property ::nuklear::NkStyleButton^ TabMinimizeButton
        {
            ::nuklear::NkStyleButton^ get();
            void set(::nuklear::NkStyleButton^);
        }

        property ::nuklear::NkStyleButton^ NodeMaximizeButton
        {
            ::nuklear::NkStyleButton^ get();
            void set(::nuklear::NkStyleButton^);
        }

        property ::nuklear::NkStyleButton^ NodeMinimizeButton
        {
            ::nuklear::NkStyleButton^ get();
            void set(::nuklear::NkStyleButton^);
        }

        property ::nuklear::NkSymbolType SymMinimize
        {
            ::nuklear::NkSymbolType get();
            void set(::nuklear::NkSymbolType);
        }

        property ::nuklear::NkSymbolType SymMaximize
        {
            ::nuklear::NkSymbolType get();
            void set(::nuklear::NkSymbolType);
        }

        property float Border
        {
            float get();
            void set(float);
        }

        property float Rounding
        {
            float get();
            void set(float);
        }

        property float Indent
        {
            float get();
            void set(float);
        }

        property ::nuklear::NkVec2^ Padding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ Spacing
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkStyleWindowHeader : ICppInstance
    {
    public:

        property struct ::nk_style_window_header* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkStyleWindowHeader(struct ::nk_style_window_header* native);
        NkStyleWindowHeader(struct ::nk_style_window_header* native, bool ownNativeInstance);
        static NkStyleWindowHeader^ __CreateInstance(::System::IntPtr native);
        static NkStyleWindowHeader^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkStyleWindowHeader();

        NkStyleWindowHeader(::nuklear::NkStyleWindowHeader^ _0);

        ~NkStyleWindowHeader();

        !NkStyleWindowHeader();

        property ::nuklear::NkStyleItem^ Normal
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ Hover
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ Active
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleButton^ CloseButton
        {
            ::nuklear::NkStyleButton^ get();
            void set(::nuklear::NkStyleButton^);
        }

        property ::nuklear::NkStyleButton^ MinimizeButton
        {
            ::nuklear::NkStyleButton^ get();
            void set(::nuklear::NkStyleButton^);
        }

        property ::nuklear::NkSymbolType CloseSymbol
        {
            ::nuklear::NkSymbolType get();
            void set(::nuklear::NkSymbolType);
        }

        property ::nuklear::NkSymbolType MinimizeSymbol
        {
            ::nuklear::NkSymbolType get();
            void set(::nuklear::NkSymbolType);
        }

        property ::nuklear::NkSymbolType MaximizeSymbol
        {
            ::nuklear::NkSymbolType get();
            void set(::nuklear::NkSymbolType);
        }

        property ::nuklear::NkColor^ LabelNormal
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ LabelHover
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ LabelActive
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkStyleHeaderAlign Align
        {
            ::nuklear::NkStyleHeaderAlign get();
            void set(::nuklear::NkStyleHeaderAlign);
        }

        property ::nuklear::NkVec2^ Padding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ LabelPadding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ Spacing
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkStyleWindow : ICppInstance
    {
    public:

        property struct ::nk_style_window* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkStyleWindow(struct ::nk_style_window* native);
        NkStyleWindow(struct ::nk_style_window* native, bool ownNativeInstance);
        static NkStyleWindow^ __CreateInstance(::System::IntPtr native);
        static NkStyleWindow^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkStyleWindow();

        NkStyleWindow(::nuklear::NkStyleWindow^ _0);

        ~NkStyleWindow();

        !NkStyleWindow();

        property ::nuklear::NkStyleWindowHeader^ Header
        {
            ::nuklear::NkStyleWindowHeader^ get();
            void set(::nuklear::NkStyleWindowHeader^);
        }

        property ::nuklear::NkStyleItem^ FixedBackground
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkColor^ Background
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ BorderColor
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ PopupBorderColor
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ ComboBorderColor
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ ContextualBorderColor
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ MenuBorderColor
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ GroupBorderColor
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ TooltipBorderColor
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkStyleItem^ Scaler
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property float Border
        {
            float get();
            void set(float);
        }

        property float ComboBorder
        {
            float get();
            void set(float);
        }

        property float ContextualBorder
        {
            float get();
            void set(float);
        }

        property float MenuBorder
        {
            float get();
            void set(float);
        }

        property float GroupBorder
        {
            float get();
            void set(float);
        }

        property float TooltipBorder
        {
            float get();
            void set(float);
        }

        property float PopupBorder
        {
            float get();
            void set(float);
        }

        property float MinRowHeightPadding
        {
            float get();
            void set(float);
        }

        property float Rounding
        {
            float get();
            void set(float);
        }

        property ::nuklear::NkVec2^ Spacing
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ ScrollbarSize
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ MinSize
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ Padding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ GroupPadding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ PopupPadding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ ComboPadding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ ContextualPadding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ MenuPadding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ TooltipPadding
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkStyle : ICppInstance
    {
    public:

        property struct ::nk_style* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkStyle(struct ::nk_style* native);
        NkStyle(struct ::nk_style* native, bool ownNativeInstance);
        static NkStyle^ __CreateInstance(::System::IntPtr native);
        static NkStyle^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkStyle();

        NkStyle(::nuklear::NkStyle^ _0);

        ~NkStyle();

        !NkStyle();

        property ::nuklear::NkUserFont^ Font
        {
            ::nuklear::NkUserFont^ get();
        }

        property ::nuklear::NkCursor^ CursorActive
        {
            ::nuklear::NkCursor^ get();
        }

        property ::nuklear::NkCursor^ CursorLast
        {
            ::nuklear::NkCursor^ get();
            void set(::nuklear::NkCursor^);
        }

        property int CursorVisible
        {
            int get();
            void set(int);
        }

        property ::nuklear::NkStyleText^ Text
        {
            ::nuklear::NkStyleText^ get();
            void set(::nuklear::NkStyleText^);
        }

        property ::nuklear::NkStyleButton^ Button
        {
            ::nuklear::NkStyleButton^ get();
            void set(::nuklear::NkStyleButton^);
        }

        property ::nuklear::NkStyleButton^ ContextualButton
        {
            ::nuklear::NkStyleButton^ get();
            void set(::nuklear::NkStyleButton^);
        }

        property ::nuklear::NkStyleButton^ MenuButton
        {
            ::nuklear::NkStyleButton^ get();
            void set(::nuklear::NkStyleButton^);
        }

        property ::nuklear::NkStyleToggle^ Option
        {
            ::nuklear::NkStyleToggle^ get();
            void set(::nuklear::NkStyleToggle^);
        }

        property ::nuklear::NkStyleToggle^ Checkbox
        {
            ::nuklear::NkStyleToggle^ get();
            void set(::nuklear::NkStyleToggle^);
        }

        property ::nuklear::NkStyleSelectable^ Selectable
        {
            ::nuklear::NkStyleSelectable^ get();
            void set(::nuklear::NkStyleSelectable^);
        }

        property ::nuklear::NkStyleSlider^ Slider
        {
            ::nuklear::NkStyleSlider^ get();
            void set(::nuklear::NkStyleSlider^);
        }

        property ::nuklear::NkStyleProgress^ Progress
        {
            ::nuklear::NkStyleProgress^ get();
            void set(::nuklear::NkStyleProgress^);
        }

        property ::nuklear::NkStyleProperty^ Property
        {
            ::nuklear::NkStyleProperty^ get();
            void set(::nuklear::NkStyleProperty^);
        }

        property ::nuklear::NkStyleEdit^ Edit
        {
            ::nuklear::NkStyleEdit^ get();
            void set(::nuklear::NkStyleEdit^);
        }

        property ::nuklear::NkStyleChart^ Chart
        {
            ::nuklear::NkStyleChart^ get();
            void set(::nuklear::NkStyleChart^);
        }

        property ::nuklear::NkStyleScrollbar^ Scrollh
        {
            ::nuklear::NkStyleScrollbar^ get();
            void set(::nuklear::NkStyleScrollbar^);
        }

        property ::nuklear::NkStyleScrollbar^ Scrollv
        {
            ::nuklear::NkStyleScrollbar^ get();
            void set(::nuklear::NkStyleScrollbar^);
        }

        property ::nuklear::NkStyleTab^ Tab
        {
            ::nuklear::NkStyleTab^ get();
            void set(::nuklear::NkStyleTab^);
        }

        property ::nuklear::NkStyleCombo^ Combo
        {
            ::nuklear::NkStyleCombo^ get();
            void set(::nuklear::NkStyleCombo^);
        }

        property ::nuklear::NkStyleWindow^ Window
        {
            ::nuklear::NkStyleWindow^ get();
            void set(::nuklear::NkStyleWindow^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkChartSlot : ICppInstance
    {
    public:

        property struct ::nk_chart_slot* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkChartSlot(struct ::nk_chart_slot* native);
        NkChartSlot(struct ::nk_chart_slot* native, bool ownNativeInstance);
        static NkChartSlot^ __CreateInstance(::System::IntPtr native);
        static NkChartSlot^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkChartSlot();

        NkChartSlot(::nuklear::NkChartSlot^ _0);

        ~NkChartSlot();

        !NkChartSlot();

        property ::nuklear::NkChartType Type
        {
            ::nuklear::NkChartType get();
            void set(::nuklear::NkChartType);
        }

        property ::nuklear::NkColor^ Color
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ Highlight
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property float Min
        {
            float get();
            void set(float);
        }

        property float Max
        {
            float get();
            void set(float);
        }

        property float Range
        {
            float get();
            void set(float);
        }

        property int Count
        {
            int get();
            void set(int);
        }

        property ::nuklear::NkVec2^ Last
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property int Index
        {
            int get();
            void set(int);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkChart : ICppInstance
    {
    public:

        property struct ::nk_chart* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkChart(struct ::nk_chart* native);
        NkChart(struct ::nk_chart* native, bool ownNativeInstance);
        static NkChart^ __CreateInstance(::System::IntPtr native);
        static NkChart^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkChart();

        NkChart(::nuklear::NkChart^ _0);

        ~NkChart();

        !NkChart();

        property int Slot
        {
            int get();
            void set(int);
        }

        property float X
        {
            float get();
            void set(float);
        }

        property float Y
        {
            float get();
            void set(float);
        }

        property float W
        {
            float get();
            void set(float);
        }

        property float H
        {
            float get();
            void set(float);
        }

        property cli::array<::nuklear::NkChartSlot^>^ Slots
        {
            cli::array<::nuklear::NkChartSlot^>^ get();
            void set(cli::array<::nuklear::NkChartSlot^>^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkRowLayout : ICppInstance
    {
    public:

        property struct ::nk_row_layout* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkRowLayout(struct ::nk_row_layout* native);
        NkRowLayout(struct ::nk_row_layout* native, bool ownNativeInstance);
        static NkRowLayout^ __CreateInstance(::System::IntPtr native);
        static NkRowLayout^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkRowLayout();

        NkRowLayout(::nuklear::NkRowLayout^ _0);

        ~NkRowLayout();

        !NkRowLayout();

        property ::nuklear::NkPanelRowLayoutType Type
        {
            ::nuklear::NkPanelRowLayoutType get();
            void set(::nuklear::NkPanelRowLayoutType);
        }

        property int Index
        {
            int get();
            void set(int);
        }

        property float Height
        {
            float get();
            void set(float);
        }

        property float MinHeight
        {
            float get();
            void set(float);
        }

        property int Columns
        {
            int get();
            void set(int);
        }

        property float* Ratio
        {
            float* get();
        }

        property float ItemWidth
        {
            float get();
            void set(float);
        }

        property float ItemHeight
        {
            float get();
            void set(float);
        }

        property float ItemOffset
        {
            float get();
            void set(float);
        }

        property float Filled
        {
            float get();
            void set(float);
        }

        property ::nuklear::NkRect^ Item
        {
            ::nuklear::NkRect^ get();
            void set(::nuklear::NkRect^);
        }

        property int TreeDepth
        {
            int get();
            void set(int);
        }

        property cli::array<float>^ Templates
        {
            cli::array<float>^ get();
            void set(cli::array<float>^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkPopupBuffer : ICppInstance
    {
    public:

        property struct ::nk_popup_buffer* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkPopupBuffer(struct ::nk_popup_buffer* native);
        NkPopupBuffer(struct ::nk_popup_buffer* native, bool ownNativeInstance);
        static NkPopupBuffer^ __CreateInstance(::System::IntPtr native);
        static NkPopupBuffer^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkPopupBuffer();

        NkPopupBuffer(::nuklear::NkPopupBuffer^ _0);

        ~NkPopupBuffer();

        !NkPopupBuffer();

        property unsigned long long Begin
        {
            unsigned long long get();
            void set(unsigned long long);
        }

        property unsigned long long Parent
        {
            unsigned long long get();
            void set(unsigned long long);
        }

        property unsigned long long Last
        {
            unsigned long long get();
            void set(unsigned long long);
        }

        property unsigned long long End
        {
            unsigned long long get();
            void set(unsigned long long);
        }

        property int Active
        {
            int get();
            void set(int);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkMenuState : ICppInstance
    {
    public:

        property struct ::nk_menu_state* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkMenuState(struct ::nk_menu_state* native);
        NkMenuState(struct ::nk_menu_state* native, bool ownNativeInstance);
        static NkMenuState^ __CreateInstance(::System::IntPtr native);
        static NkMenuState^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkMenuState();

        NkMenuState(::nuklear::NkMenuState^ _0);

        ~NkMenuState();

        !NkMenuState();

        property float X
        {
            float get();
            void set(float);
        }

        property float Y
        {
            float get();
            void set(float);
        }

        property float W
        {
            float get();
            void set(float);
        }

        property float H
        {
            float get();
            void set(float);
        }

        property ::nuklear::NkScroll^ Offset
        {
            ::nuklear::NkScroll^ get();
            void set(::nuklear::NkScroll^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkPanel : ICppInstance
    {
    public:

        property struct ::nk_panel* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkPanel(struct ::nk_panel* native);
        NkPanel(struct ::nk_panel* native, bool ownNativeInstance);
        static NkPanel^ __CreateInstance(::System::IntPtr native);
        static NkPanel^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkPanel();

        NkPanel(::nuklear::NkPanel^ _0);

        ~NkPanel();

        !NkPanel();

        property ::nuklear::NkPanelType Type
        {
            ::nuklear::NkPanelType get();
            void set(::nuklear::NkPanelType);
        }

        property unsigned int Flags
        {
            unsigned int get();
            void set(unsigned int);
        }

        property ::nuklear::NkRect^ Bounds
        {
            ::nuklear::NkRect^ get();
            void set(::nuklear::NkRect^);
        }

        property unsigned int* OffsetX
        {
            unsigned int* get();
            void set(unsigned int*);
        }

        property unsigned int* OffsetY
        {
            unsigned int* get();
            void set(unsigned int*);
        }

        property float AtX
        {
            float get();
            void set(float);
        }

        property float AtY
        {
            float get();
            void set(float);
        }

        property float MaxX
        {
            float get();
            void set(float);
        }

        property float FooterHeight
        {
            float get();
            void set(float);
        }

        property float HeaderHeight
        {
            float get();
            void set(float);
        }

        property float Border
        {
            float get();
            void set(float);
        }

        property unsigned int HasScrolling
        {
            unsigned int get();
            void set(unsigned int);
        }

        property ::nuklear::NkRect^ Clip
        {
            ::nuklear::NkRect^ get();
            void set(::nuklear::NkRect^);
        }

        property ::nuklear::NkMenuState^ Menu
        {
            ::nuklear::NkMenuState^ get();
            void set(::nuklear::NkMenuState^);
        }

        property ::nuklear::NkRowLayout^ Row
        {
            ::nuklear::NkRowLayout^ get();
            void set(::nuklear::NkRowLayout^);
        }

        property ::nuklear::NkChart^ Chart
        {
            ::nuklear::NkChart^ get();
            void set(::nuklear::NkChart^);
        }

        property ::nuklear::NkCommandBuffer^ Buffer
        {
            ::nuklear::NkCommandBuffer^ get();
            void set(::nuklear::NkCommandBuffer^);
        }

        property ::nuklear::NkPanel^ Parent
        {
            ::nuklear::NkPanel^ get();
            void set(::nuklear::NkPanel^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkPopupState : ICppInstance
    {
    public:

        property struct ::nk_popup_state* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkPopupState(struct ::nk_popup_state* native);
        NkPopupState(struct ::nk_popup_state* native, bool ownNativeInstance);
        static NkPopupState^ __CreateInstance(::System::IntPtr native);
        static NkPopupState^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkPopupState();

        NkPopupState(::nuklear::NkPopupState^ _0);

        ~NkPopupState();

        !NkPopupState();

        property ::nuklear::NkWindow^ Win
        {
            ::nuklear::NkWindow^ get();
            void set(::nuklear::NkWindow^);
        }

        property ::nuklear::NkPanelType Type
        {
            ::nuklear::NkPanelType get();
            void set(::nuklear::NkPanelType);
        }

        property ::nuklear::NkPopupBuffer^ Buf
        {
            ::nuklear::NkPopupBuffer^ get();
            void set(::nuklear::NkPopupBuffer^);
        }

        property unsigned int Name
        {
            unsigned int get();
            void set(unsigned int);
        }

        property int Active
        {
            int get();
            void set(int);
        }

        property unsigned int ComboCount
        {
            unsigned int get();
            void set(unsigned int);
        }

        property unsigned int ConCount
        {
            unsigned int get();
            void set(unsigned int);
        }

        property unsigned int ConOld
        {
            unsigned int get();
            void set(unsigned int);
        }

        property unsigned int ActiveCon
        {
            unsigned int get();
            void set(unsigned int);
        }

        property ::nuklear::NkRect^ Header
        {
            ::nuklear::NkRect^ get();
            void set(::nuklear::NkRect^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkEditState : ICppInstance
    {
    public:

        property struct ::nk_edit_state* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkEditState(struct ::nk_edit_state* native);
        NkEditState(struct ::nk_edit_state* native, bool ownNativeInstance);
        static NkEditState^ __CreateInstance(::System::IntPtr native);
        static NkEditState^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkEditState();

        NkEditState(::nuklear::NkEditState^ _0);

        ~NkEditState();

        !NkEditState();

        property unsigned int Name
        {
            unsigned int get();
            void set(unsigned int);
        }

        property unsigned int Seq
        {
            unsigned int get();
            void set(unsigned int);
        }

        property unsigned int Old
        {
            unsigned int get();
            void set(unsigned int);
        }

        property int Active
        {
            int get();
            void set(int);
        }

        property int Prev
        {
            int get();
            void set(int);
        }

        property int Cursor
        {
            int get();
            void set(int);
        }

        property int SelStart
        {
            int get();
            void set(int);
        }

        property int SelEnd
        {
            int get();
            void set(int);
        }

        property ::nuklear::NkScroll^ Scrollbar
        {
            ::nuklear::NkScroll^ get();
            void set(::nuklear::NkScroll^);
        }

        property unsigned char Mode
        {
            unsigned char get();
            void set(unsigned char);
        }

        property unsigned char SingleLine
        {
            unsigned char get();
            void set(unsigned char);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkPropertyState : ICppInstance
    {
    public:

        property struct ::nk_property_state* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkPropertyState(struct ::nk_property_state* native);
        NkPropertyState(struct ::nk_property_state* native, bool ownNativeInstance);
        static NkPropertyState^ __CreateInstance(::System::IntPtr native);
        static NkPropertyState^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkPropertyState();

        NkPropertyState(::nuklear::NkPropertyState^ _0);

        ~NkPropertyState();

        !NkPropertyState();

        property int Active
        {
            int get();
            void set(int);
        }

        property int Prev
        {
            int get();
            void set(int);
        }

        property cli::array<char>^ Buffer
        {
            cli::array<char>^ get();
            void set(cli::array<char>^);
        }

        property int Length
        {
            int get();
            void set(int);
        }

        property int Cursor
        {
            int get();
            void set(int);
        }

        property int SelectStart
        {
            int get();
            void set(int);
        }

        property int SelectEnd
        {
            int get();
            void set(int);
        }

        property unsigned int Name
        {
            unsigned int get();
            void set(unsigned int);
        }

        property unsigned int Seq
        {
            unsigned int get();
            void set(unsigned int);
        }

        property unsigned int Old
        {
            unsigned int get();
            void set(unsigned int);
        }

        property int State
        {
            int get();
            void set(int);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkWindow : ICppInstance
    {
    public:

        property struct ::nk_window* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkWindow(struct ::nk_window* native);
        NkWindow(struct ::nk_window* native, bool ownNativeInstance);
        static NkWindow^ __CreateInstance(::System::IntPtr native);
        static NkWindow^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkWindow();

        NkWindow(::nuklear::NkWindow^ _0);

        ~NkWindow();

        !NkWindow();

        property unsigned int Seq
        {
            unsigned int get();
            void set(unsigned int);
        }

        property unsigned int Name
        {
            unsigned int get();
            void set(unsigned int);
        }

        property cli::array<char>^ NameString
        {
            cli::array<char>^ get();
            void set(cli::array<char>^);
        }

        property unsigned int Flags
        {
            unsigned int get();
            void set(unsigned int);
        }

        property ::nuklear::NkRect^ Bounds
        {
            ::nuklear::NkRect^ get();
            void set(::nuklear::NkRect^);
        }

        property ::nuklear::NkScroll^ Scrollbar
        {
            ::nuklear::NkScroll^ get();
            void set(::nuklear::NkScroll^);
        }

        property ::nuklear::NkCommandBuffer^ Buffer
        {
            ::nuklear::NkCommandBuffer^ get();
            void set(::nuklear::NkCommandBuffer^);
        }

        property ::nuklear::NkPanel^ Layout
        {
            ::nuklear::NkPanel^ get();
            void set(::nuklear::NkPanel^);
        }

        property float ScrollbarHidingTimer
        {
            float get();
            void set(float);
        }

        property ::nuklear::NkPropertyState^ Property
        {
            ::nuklear::NkPropertyState^ get();
            void set(::nuklear::NkPropertyState^);
        }

        property ::nuklear::NkPopupState^ Popup
        {
            ::nuklear::NkPopupState^ get();
            void set(::nuklear::NkPopupState^);
        }

        property ::nuklear::NkEditState^ Edit
        {
            ::nuklear::NkEditState^ get();
            void set(::nuklear::NkEditState^);
        }

        property unsigned int Scrolled
        {
            unsigned int get();
            void set(unsigned int);
        }

        property ::nuklear::NkTable^ Tables
        {
            ::nuklear::NkTable^ get();
            void set(::nuklear::NkTable^);
        }

        property unsigned int TableCount
        {
            unsigned int get();
            void set(unsigned int);
        }

        property ::nuklear::NkWindow^ Next
        {
            ::nuklear::NkWindow^ get();
            void set(::nuklear::NkWindow^);
        }

        property ::nuklear::NkWindow^ Prev
        {
            ::nuklear::NkWindow^ get();
            void set(::nuklear::NkWindow^);
        }

        property ::nuklear::NkWindow^ Parent
        {
            ::nuklear::NkWindow^ get();
            void set(::nuklear::NkWindow^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkConfigStackStyleItemElement : ICppInstance
    {
    public:

        property struct ::nk_config_stack_style_item_element* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkConfigStackStyleItemElement(struct ::nk_config_stack_style_item_element* native);
        NkConfigStackStyleItemElement(struct ::nk_config_stack_style_item_element* native, bool ownNativeInstance);
        static NkConfigStackStyleItemElement^ __CreateInstance(::System::IntPtr native);
        static NkConfigStackStyleItemElement^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkConfigStackStyleItemElement();

        NkConfigStackStyleItemElement(::nuklear::NkConfigStackStyleItemElement^ _0);

        ~NkConfigStackStyleItemElement();

        !NkConfigStackStyleItemElement();

        property ::nuklear::NkStyleItem^ Address
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

        property ::nuklear::NkStyleItem^ OldValue
        {
            ::nuklear::NkStyleItem^ get();
            void set(::nuklear::NkStyleItem^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkConfigStackFloatElement : ICppInstance
    {
    public:

        property struct ::nk_config_stack_float_element* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkConfigStackFloatElement(struct ::nk_config_stack_float_element* native);
        NkConfigStackFloatElement(struct ::nk_config_stack_float_element* native, bool ownNativeInstance);
        static NkConfigStackFloatElement^ __CreateInstance(::System::IntPtr native);
        static NkConfigStackFloatElement^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkConfigStackFloatElement();

        NkConfigStackFloatElement(::nuklear::NkConfigStackFloatElement^ _0);

        ~NkConfigStackFloatElement();

        !NkConfigStackFloatElement();

        property float* Address
        {
            float* get();
            void set(float*);
        }

        property float OldValue
        {
            float get();
            void set(float);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkConfigStackVec2Element : ICppInstance
    {
    public:

        property struct ::nk_config_stack_vec2_element* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkConfigStackVec2Element(struct ::nk_config_stack_vec2_element* native);
        NkConfigStackVec2Element(struct ::nk_config_stack_vec2_element* native, bool ownNativeInstance);
        static NkConfigStackVec2Element^ __CreateInstance(::System::IntPtr native);
        static NkConfigStackVec2Element^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkConfigStackVec2Element();

        NkConfigStackVec2Element(::nuklear::NkConfigStackVec2Element^ _0);

        ~NkConfigStackVec2Element();

        !NkConfigStackVec2Element();

        property ::nuklear::NkVec2^ Address
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

        property ::nuklear::NkVec2^ OldValue
        {
            ::nuklear::NkVec2^ get();
            void set(::nuklear::NkVec2^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkConfigStackFlagsElement : ICppInstance
    {
    public:

        property struct ::nk_config_stack_flags_element* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkConfigStackFlagsElement(struct ::nk_config_stack_flags_element* native);
        NkConfigStackFlagsElement(struct ::nk_config_stack_flags_element* native, bool ownNativeInstance);
        static NkConfigStackFlagsElement^ __CreateInstance(::System::IntPtr native);
        static NkConfigStackFlagsElement^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkConfigStackFlagsElement();

        NkConfigStackFlagsElement(::nuklear::NkConfigStackFlagsElement^ _0);

        ~NkConfigStackFlagsElement();

        !NkConfigStackFlagsElement();

        property unsigned int* Address
        {
            unsigned int* get();
            void set(unsigned int*);
        }

        property unsigned int OldValue
        {
            unsigned int get();
            void set(unsigned int);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkConfigStackColorElement : ICppInstance
    {
    public:

        property struct ::nk_config_stack_color_element* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkConfigStackColorElement(struct ::nk_config_stack_color_element* native);
        NkConfigStackColorElement(struct ::nk_config_stack_color_element* native, bool ownNativeInstance);
        static NkConfigStackColorElement^ __CreateInstance(::System::IntPtr native);
        static NkConfigStackColorElement^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkConfigStackColorElement();

        NkConfigStackColorElement(::nuklear::NkConfigStackColorElement^ _0);

        ~NkConfigStackColorElement();

        !NkConfigStackColorElement();

        property ::nuklear::NkColor^ Address
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

        property ::nuklear::NkColor^ OldValue
        {
            ::nuklear::NkColor^ get();
            void set(::nuklear::NkColor^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkConfigStackUserFontElement : ICppInstance
    {
    public:

        property struct ::nk_config_stack_user_font_element* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkConfigStackUserFontElement(struct ::nk_config_stack_user_font_element* native);
        NkConfigStackUserFontElement(struct ::nk_config_stack_user_font_element* native, bool ownNativeInstance);
        static NkConfigStackUserFontElement^ __CreateInstance(::System::IntPtr native);
        static NkConfigStackUserFontElement^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkConfigStackUserFontElement();

        NkConfigStackUserFontElement(::nuklear::NkConfigStackUserFontElement^ _0);

        ~NkConfigStackUserFontElement();

        !NkConfigStackUserFontElement();

        property ::nuklear::NkUserFont^ Address
        {
            ::nuklear::NkUserFont^ get();
        }

        property ::nuklear::NkUserFont^ OldValue
        {
            ::nuklear::NkUserFont^ get();
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkConfigStackButtonBehaviorElement : ICppInstance
    {
    public:

        property struct ::nk_config_stack_button_behavior_element* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkConfigStackButtonBehaviorElement(struct ::nk_config_stack_button_behavior_element* native);
        NkConfigStackButtonBehaviorElement(struct ::nk_config_stack_button_behavior_element* native, bool ownNativeInstance);
        static NkConfigStackButtonBehaviorElement^ __CreateInstance(::System::IntPtr native);
        static NkConfigStackButtonBehaviorElement^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkConfigStackButtonBehaviorElement();

        NkConfigStackButtonBehaviorElement(::nuklear::NkConfigStackButtonBehaviorElement^ _0);

        ~NkConfigStackButtonBehaviorElement();

        !NkConfigStackButtonBehaviorElement();

        property ::nuklear::NkButtonBehavior* Address
        {
            ::nuklear::NkButtonBehavior* get();
            void set(::nuklear::NkButtonBehavior*);
        }

        property ::nuklear::NkButtonBehavior OldValue
        {
            ::nuklear::NkButtonBehavior get();
            void set(::nuklear::NkButtonBehavior);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkConfigStackStyleItem : ICppInstance
    {
    public:

        property struct ::nk_config_stack_style_item* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkConfigStackStyleItem(struct ::nk_config_stack_style_item* native);
        NkConfigStackStyleItem(struct ::nk_config_stack_style_item* native, bool ownNativeInstance);
        static NkConfigStackStyleItem^ __CreateInstance(::System::IntPtr native);
        static NkConfigStackStyleItem^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkConfigStackStyleItem();

        NkConfigStackStyleItem(::nuklear::NkConfigStackStyleItem^ _0);

        ~NkConfigStackStyleItem();

        !NkConfigStackStyleItem();

        property int Head
        {
            int get();
            void set(int);
        }

        property cli::array<::nuklear::NkConfigStackStyleItemElement^>^ Elements
        {
            cli::array<::nuklear::NkConfigStackStyleItemElement^>^ get();
            void set(cli::array<::nuklear::NkConfigStackStyleItemElement^>^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkConfigStackFloat : ICppInstance
    {
    public:

        property struct ::nk_config_stack_float* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkConfigStackFloat(struct ::nk_config_stack_float* native);
        NkConfigStackFloat(struct ::nk_config_stack_float* native, bool ownNativeInstance);
        static NkConfigStackFloat^ __CreateInstance(::System::IntPtr native);
        static NkConfigStackFloat^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkConfigStackFloat();

        NkConfigStackFloat(::nuklear::NkConfigStackFloat^ _0);

        ~NkConfigStackFloat();

        !NkConfigStackFloat();

        property int Head
        {
            int get();
            void set(int);
        }

        property cli::array<::nuklear::NkConfigStackFloatElement^>^ Elements
        {
            cli::array<::nuklear::NkConfigStackFloatElement^>^ get();
            void set(cli::array<::nuklear::NkConfigStackFloatElement^>^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkConfigStackVec2 : ICppInstance
    {
    public:

        property struct ::nk_config_stack_vec2* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkConfigStackVec2(struct ::nk_config_stack_vec2* native);
        NkConfigStackVec2(struct ::nk_config_stack_vec2* native, bool ownNativeInstance);
        static NkConfigStackVec2^ __CreateInstance(::System::IntPtr native);
        static NkConfigStackVec2^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkConfigStackVec2();

        NkConfigStackVec2(::nuklear::NkConfigStackVec2^ _0);

        ~NkConfigStackVec2();

        !NkConfigStackVec2();

        property int Head
        {
            int get();
            void set(int);
        }

        property cli::array<::nuklear::NkConfigStackVec2Element^>^ Elements
        {
            cli::array<::nuklear::NkConfigStackVec2Element^>^ get();
            void set(cli::array<::nuklear::NkConfigStackVec2Element^>^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkConfigStackFlags : ICppInstance
    {
    public:

        property struct ::nk_config_stack_flags* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkConfigStackFlags(struct ::nk_config_stack_flags* native);
        NkConfigStackFlags(struct ::nk_config_stack_flags* native, bool ownNativeInstance);
        static NkConfigStackFlags^ __CreateInstance(::System::IntPtr native);
        static NkConfigStackFlags^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkConfigStackFlags();

        NkConfigStackFlags(::nuklear::NkConfigStackFlags^ _0);

        ~NkConfigStackFlags();

        !NkConfigStackFlags();

        property int Head
        {
            int get();
            void set(int);
        }

        property cli::array<::nuklear::NkConfigStackFlagsElement^>^ Elements
        {
            cli::array<::nuklear::NkConfigStackFlagsElement^>^ get();
            void set(cli::array<::nuklear::NkConfigStackFlagsElement^>^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkConfigStackColor : ICppInstance
    {
    public:

        property struct ::nk_config_stack_color* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkConfigStackColor(struct ::nk_config_stack_color* native);
        NkConfigStackColor(struct ::nk_config_stack_color* native, bool ownNativeInstance);
        static NkConfigStackColor^ __CreateInstance(::System::IntPtr native);
        static NkConfigStackColor^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkConfigStackColor();

        NkConfigStackColor(::nuklear::NkConfigStackColor^ _0);

        ~NkConfigStackColor();

        !NkConfigStackColor();

        property int Head
        {
            int get();
            void set(int);
        }

        property cli::array<::nuklear::NkConfigStackColorElement^>^ Elements
        {
            cli::array<::nuklear::NkConfigStackColorElement^>^ get();
            void set(cli::array<::nuklear::NkConfigStackColorElement^>^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkConfigStackUserFont : ICppInstance
    {
    public:

        property struct ::nk_config_stack_user_font* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkConfigStackUserFont(struct ::nk_config_stack_user_font* native);
        NkConfigStackUserFont(struct ::nk_config_stack_user_font* native, bool ownNativeInstance);
        static NkConfigStackUserFont^ __CreateInstance(::System::IntPtr native);
        static NkConfigStackUserFont^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkConfigStackUserFont();

        NkConfigStackUserFont(::nuklear::NkConfigStackUserFont^ _0);

        ~NkConfigStackUserFont();

        !NkConfigStackUserFont();

        property int Head
        {
            int get();
            void set(int);
        }

        property cli::array<::nuklear::NkConfigStackUserFontElement^>^ Elements
        {
            cli::array<::nuklear::NkConfigStackUserFontElement^>^ get();
            void set(cli::array<::nuklear::NkConfigStackUserFontElement^>^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkConfigStackButtonBehavior : ICppInstance
    {
    public:

        property struct ::nk_config_stack_button_behavior* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkConfigStackButtonBehavior(struct ::nk_config_stack_button_behavior* native);
        NkConfigStackButtonBehavior(struct ::nk_config_stack_button_behavior* native, bool ownNativeInstance);
        static NkConfigStackButtonBehavior^ __CreateInstance(::System::IntPtr native);
        static NkConfigStackButtonBehavior^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkConfigStackButtonBehavior();

        NkConfigStackButtonBehavior(::nuklear::NkConfigStackButtonBehavior^ _0);

        ~NkConfigStackButtonBehavior();

        !NkConfigStackButtonBehavior();

        property int Head
        {
            int get();
            void set(int);
        }

        property cli::array<::nuklear::NkConfigStackButtonBehaviorElement^>^ Elements
        {
            cli::array<::nuklear::NkConfigStackButtonBehaviorElement^>^ get();
            void set(cli::array<::nuklear::NkConfigStackButtonBehaviorElement^>^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkConfigurationStacks : ICppInstance
    {
    public:

        property struct ::nk_configuration_stacks* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkConfigurationStacks(struct ::nk_configuration_stacks* native);
        NkConfigurationStacks(struct ::nk_configuration_stacks* native, bool ownNativeInstance);
        static NkConfigurationStacks^ __CreateInstance(::System::IntPtr native);
        static NkConfigurationStacks^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkConfigurationStacks();

        NkConfigurationStacks(::nuklear::NkConfigurationStacks^ _0);

        ~NkConfigurationStacks();

        !NkConfigurationStacks();

        property ::nuklear::NkConfigStackStyleItem^ StyleItems
        {
            ::nuklear::NkConfigStackStyleItem^ get();
            void set(::nuklear::NkConfigStackStyleItem^);
        }

        property ::nuklear::NkConfigStackFloat^ Floats
        {
            ::nuklear::NkConfigStackFloat^ get();
            void set(::nuklear::NkConfigStackFloat^);
        }

        property ::nuklear::NkConfigStackVec2^ Vectors
        {
            ::nuklear::NkConfigStackVec2^ get();
            void set(::nuklear::NkConfigStackVec2^);
        }

        property ::nuklear::NkConfigStackFlags^ Flags
        {
            ::nuklear::NkConfigStackFlags^ get();
            void set(::nuklear::NkConfigStackFlags^);
        }

        property ::nuklear::NkConfigStackColor^ Colors
        {
            ::nuklear::NkConfigStackColor^ get();
            void set(::nuklear::NkConfigStackColor^);
        }

        property ::nuklear::NkConfigStackUserFont^ Fonts
        {
            ::nuklear::NkConfigStackUserFont^ get();
            void set(::nuklear::NkConfigStackUserFont^);
        }

        property ::nuklear::NkConfigStackButtonBehavior^ ButtonBehaviors
        {
            ::nuklear::NkConfigStackButtonBehavior^ get();
            void set(::nuklear::NkConfigStackButtonBehavior^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkTable : ICppInstance
    {
    public:

        property struct ::nk_table* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkTable(struct ::nk_table* native);
        NkTable(struct ::nk_table* native, bool ownNativeInstance);
        static NkTable^ __CreateInstance(::System::IntPtr native);
        static NkTable^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkTable();

        NkTable(::nuklear::NkTable^ _0);

        ~NkTable();

        !NkTable();

        property unsigned int Seq
        {
            unsigned int get();
            void set(unsigned int);
        }

        property unsigned int Size
        {
            unsigned int get();
            void set(unsigned int);
        }

        property cli::array<unsigned int>^ Keys
        {
            cli::array<unsigned int>^ get();
            void set(cli::array<unsigned int>^);
        }

        property cli::array<unsigned int>^ Values
        {
            cli::array<unsigned int>^ get();
            void set(cli::array<unsigned int>^);
        }

        property ::nuklear::NkTable^ Next
        {
            ::nuklear::NkTable^ get();
            void set(::nuklear::NkTable^);
        }

        property ::nuklear::NkTable^ Prev
        {
            ::nuklear::NkTable^ get();
            void set(::nuklear::NkTable^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    [::System::Runtime::InteropServices::StructLayout(::System::Runtime::InteropServices::LayoutKind::Explicit)]
    public value struct NkPageData
    {
    public:

        NkPageData(::nk_page_data* native);
        NkPageData(::nk_page_data* native, bool ownNativeInstance);
        static NkPageData^ __CreateInstance(::System::IntPtr native);
        static NkPageData^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        property ::nuklear::NkTable^ Tbl
        {
            ::nuklear::NkTable^ get();
            void set(::nuklear::NkTable^);
        }

        property ::nuklear::NkPanel^ Pan
        {
            ::nuklear::NkPanel^ get();
            void set(::nuklear::NkPanel^);
        }

        property ::nuklear::NkWindow^ Win
        {
            ::nuklear::NkWindow^ get();
            void set(::nuklear::NkWindow^);
        }

    private:

        [::System::Runtime::InteropServices::FieldOffset(0)]
        ::nuklear::NkTable^ __tbl;
        [::System::Runtime::InteropServices::FieldOffset(0)]
        ::nuklear::NkPanel^ __pan;
        [::System::Runtime::InteropServices::FieldOffset(0)]
        ::nuklear::NkWindow^ __win;
    };

    public ref class NkPageElement : ICppInstance
    {
    public:

        property struct ::nk_page_element* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkPageElement(struct ::nk_page_element* native);
        NkPageElement(struct ::nk_page_element* native, bool ownNativeInstance);
        static NkPageElement^ __CreateInstance(::System::IntPtr native);
        static NkPageElement^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkPageElement();

        NkPageElement(::nuklear::NkPageElement^ _0);

        ~NkPageElement();

        !NkPageElement();

        property ::nuklear::NkPageData Data
        {
            ::nuklear::NkPageData get();
            void set(::nuklear::NkPageData);
        }

        property ::nuklear::NkPageElement^ Next
        {
            ::nuklear::NkPageElement^ get();
            void set(::nuklear::NkPageElement^);
        }

        property ::nuklear::NkPageElement^ Prev
        {
            ::nuklear::NkPageElement^ get();
            void set(::nuklear::NkPageElement^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkPage : ICppInstance
    {
    public:

        property struct ::nk_page* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkPage(struct ::nk_page* native);
        NkPage(struct ::nk_page* native, bool ownNativeInstance);
        static NkPage^ __CreateInstance(::System::IntPtr native);
        static NkPage^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkPage();

        NkPage(::nuklear::NkPage^ _0);

        ~NkPage();

        !NkPage();

        property unsigned int Size
        {
            unsigned int get();
            void set(unsigned int);
        }

        property ::nuklear::NkPage^ Next
        {
            ::nuklear::NkPage^ get();
            void set(::nuklear::NkPage^);
        }

        property cli::array<::nuklear::NkPageElement^>^ Win
        {
            cli::array<::nuklear::NkPageElement^>^ get();
            void set(cli::array<::nuklear::NkPageElement^>^);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkPool : ICppInstance
    {
    public:

        property struct ::nk_pool* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkPool(struct ::nk_pool* native);
        NkPool(struct ::nk_pool* native, bool ownNativeInstance);
        static NkPool^ __CreateInstance(::System::IntPtr native);
        static NkPool^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkPool();

        NkPool(::nuklear::NkPool^ _0);

        ~NkPool();

        !NkPool();

        property ::nuklear::NkAllocator^ Alloc
        {
            ::nuklear::NkAllocator^ get();
            void set(::nuklear::NkAllocator^);
        }

        property ::nuklear::NkAllocationType Type
        {
            ::nuklear::NkAllocationType get();
            void set(::nuklear::NkAllocationType);
        }

        property unsigned int PageCount
        {
            unsigned int get();
            void set(unsigned int);
        }

        property ::nuklear::NkPage^ Pages
        {
            ::nuklear::NkPage^ get();
            void set(::nuklear::NkPage^);
        }

        property ::nuklear::NkPageElement^ Freelist
        {
            ::nuklear::NkPageElement^ get();
            void set(::nuklear::NkPageElement^);
        }

        property unsigned int Capacity
        {
            unsigned int get();
            void set(unsigned int);
        }

        property unsigned long long Size
        {
            unsigned long long get();
            void set(unsigned long long);
        }

        property unsigned long long Cap
        {
            unsigned long long get();
            void set(unsigned long long);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class NkContext : ICppInstance
    {
    public:

        property struct ::nk_context* NativePtr;
        property ::System::IntPtr __Instance
        {
            virtual ::System::IntPtr get();
            virtual void set(::System::IntPtr instance);
        }

        NkContext(struct ::nk_context* native);
        NkContext(struct ::nk_context* native, bool ownNativeInstance);
        static NkContext^ __CreateInstance(::System::IntPtr native);
        static NkContext^ __CreateInstance(::System::IntPtr native, bool __ownsNativeInstance);
        NkContext();

        NkContext(::nuklear::NkContext^ _0);

        ~NkContext();

        !NkContext();

        property ::nuklear::NkInput^ Input
        {
            ::nuklear::NkInput^ get();
            void set(::nuklear::NkInput^);
        }

        property ::nuklear::NkStyle^ Style
        {
            ::nuklear::NkStyle^ get();
            void set(::nuklear::NkStyle^);
        }

        property ::nuklear::NkBuffer^ Memory
        {
            ::nuklear::NkBuffer^ get();
            void set(::nuklear::NkBuffer^);
        }

        property ::nuklear::NkClipboard^ Clip
        {
            ::nuklear::NkClipboard^ get();
            void set(::nuklear::NkClipboard^);
        }

        property unsigned int LastWidgetState
        {
            unsigned int get();
            void set(unsigned int);
        }

        property ::nuklear::NkButtonBehavior ButtonBehavior
        {
            ::nuklear::NkButtonBehavior get();
            void set(::nuklear::NkButtonBehavior);
        }

        property ::nuklear::NkConfigurationStacks^ Stacks
        {
            ::nuklear::NkConfigurationStacks^ get();
            void set(::nuklear::NkConfigurationStacks^);
        }

        property float DeltaTimeSeconds
        {
            float get();
            void set(float);
        }

        property ::nuklear::NkTextEdit^ TextEdit
        {
            ::nuklear::NkTextEdit^ get();
            void set(::nuklear::NkTextEdit^);
        }

        property ::nuklear::NkCommandBuffer^ Overlay
        {
            ::nuklear::NkCommandBuffer^ get();
            void set(::nuklear::NkCommandBuffer^);
        }

        property int Build
        {
            int get();
            void set(int);
        }

        property int UsePool
        {
            int get();
            void set(int);
        }

        property ::nuklear::NkPool^ Pool
        {
            ::nuklear::NkPool^ get();
            void set(::nuklear::NkPool^);
        }

        property ::nuklear::NkWindow^ Begin
        {
            ::nuklear::NkWindow^ get();
            void set(::nuklear::NkWindow^);
        }

        property ::nuklear::NkWindow^ End
        {
            ::nuklear::NkWindow^ get();
            void set(::nuklear::NkWindow^);
        }

        property ::nuklear::NkWindow^ Active
        {
            ::nuklear::NkWindow^ get();
            void set(::nuklear::NkWindow^);
        }

        property ::nuklear::NkWindow^ Current
        {
            ::nuklear::NkWindow^ get();
            void set(::nuklear::NkWindow^);
        }

        property ::nuklear::NkPageElement^ Freelist
        {
            ::nuklear::NkPageElement^ get();
            void set(::nuklear::NkPageElement^);
        }

        property unsigned int Count
        {
            unsigned int get();
            void set(unsigned int);
        }

        property unsigned int Seq
        {
            unsigned int get();
            void set(unsigned int);
        }

    protected:

        bool __ownsNativeInstance;
    };

    public ref class nuklear
    {
    public:
        static int NkInitFixed(::nuklear::NkContext^ _0, ::System::IntPtr memory, unsigned long long size, ::nuklear::NkUserFont^ _1);
        static int NkInit(::nuklear::NkContext^ _0, ::nuklear::NkAllocator^ _1, ::nuklear::NkUserFont^ _2);
        static int NkInitCustom(::nuklear::NkContext^ _0, ::nuklear::NkBuffer^ cmds, ::nuklear::NkBuffer^ pool, ::nuklear::NkUserFont^ _1);
        static void NkClear(::nuklear::NkContext^ _0);
        static void NkFree(::nuklear::NkContext^ _0);
        static void NkInputBegin(::nuklear::NkContext^ _0);
        static void NkInputMotion(::nuklear::NkContext^ _0, int x, int y);
        static void NkInputKey(::nuklear::NkContext^ _0, ::nuklear::NkKeys _1, int down);
        static void NkInputButton(::nuklear::NkContext^ _0, ::nuklear::NkButtons _1, int x, int y, int down);
        static void NkInputScroll(::nuklear::NkContext^ _0, ::nuklear::NkVec2^ val);
        static void NkInputChar(::nuklear::NkContext^ _0, char _1);
        static void NkInputGlyph(::nuklear::NkContext^ _0, cli::array<char>^ _1);
        static void NkInputUnicode(::nuklear::NkContext^ _0, unsigned int _1);
        static void NkInputEnd(::nuklear::NkContext^ _0);
        static ::nuklear::NkCommand^ NkBegin(::nuklear::NkContext^ _0);
        static ::nuklear::NkCommand^ NkNext(::nuklear::NkContext^ _0, ::nuklear::NkCommand^ _1);
        static bool NkBegin(::nuklear::NkContext^ ctx, ::System::String^ title, ::nuklear::NkRect^ bounds, unsigned int flags);
        static bool NkBeginTitled(::nuklear::NkContext^ ctx, ::System::String^ name, ::System::String^ title, ::nuklear::NkRect^ bounds, unsigned int flags);
        static void NkEnd(::nuklear::NkContext^ ctx);
        static ::nuklear::NkWindow^ NkWindowFind(::nuklear::NkContext^ ctx, ::System::String^ name);
        static ::nuklear::NkRect^ NkWindowGetBounds(::nuklear::NkContext^ ctx);
        static ::nuklear::NkVec2^ NkWindowGetPosition(::nuklear::NkContext^ ctx);
        static ::nuklear::NkVec2^ NkWindowGetSize(::nuklear::NkContext^ _0);
        static float NkWindowGetWidth(::nuklear::NkContext^ _0);
        static float NkWindowGetHeight(::nuklear::NkContext^ _0);
        static ::nuklear::NkPanel^ NkWindowGetPanel(::nuklear::NkContext^ _0);
        static ::nuklear::NkRect^ NkWindowGetContentRegion(::nuklear::NkContext^ _0);
        static ::nuklear::NkVec2^ NkWindowGetContentRegionMin(::nuklear::NkContext^ _0);
        static ::nuklear::NkVec2^ NkWindowGetContentRegionMax(::nuklear::NkContext^ _0);
        static ::nuklear::NkVec2^ NkWindowGetContentRegionSize(::nuklear::NkContext^ _0);
        static ::nuklear::NkCommandBuffer^ NkWindowGetCanvas(::nuklear::NkContext^ _0);
        static void NkWindowGetScroll(::nuklear::NkContext^ _0, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] unsigned int% offset_x, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] unsigned int% offset_y);
        static int NkWindowHasFocus(::nuklear::NkContext^ _0);
        static int NkWindowIsHovered(::nuklear::NkContext^ _0);
        static int NkWindowIsCollapsed(::nuklear::NkContext^ ctx, ::System::String^ name);
        static int NkWindowIsClosed(::nuklear::NkContext^ _0, ::System::String^ _1);
        static int NkWindowIsHidden(::nuklear::NkContext^ _0, ::System::String^ _1);
        static int NkWindowIsActive(::nuklear::NkContext^ _0, ::System::String^ _1);
        static int NkWindowIsAnyHovered(::nuklear::NkContext^ _0);
        static int NkItemIsAnyActive(::nuklear::NkContext^ _0);
        static void NkWindowSetBounds(::nuklear::NkContext^ _0, ::System::String^ name, ::nuklear::NkRect^ bounds);
        static void NkWindowSetPosition(::nuklear::NkContext^ _0, ::System::String^ name, ::nuklear::NkVec2^ pos);
        static void NkWindowSetSize(::nuklear::NkContext^ _0, ::System::String^ name, ::nuklear::NkVec2^ _1);
        static void NkWindowSetFocus(::nuklear::NkContext^ _0, ::System::String^ name);
        static void NkWindowSetScroll(::nuklear::NkContext^ _0, unsigned int offset_x, unsigned int offset_y);
        static void NkWindowClose(::nuklear::NkContext^ ctx, ::System::String^ name);
        static void NkWindowCollapse(::nuklear::NkContext^ _0, ::System::String^ name, ::nuklear::NkCollapseStates state);
        static void NkWindowCollapseIf(::nuklear::NkContext^ _0, ::System::String^ name, ::nuklear::NkCollapseStates _1, int cond);
        static void NkWindowShow(::nuklear::NkContext^ _0, ::System::String^ name, ::nuklear::NkShowStates _1);
        static void NkWindowShowIf(::nuklear::NkContext^ _0, ::System::String^ name, ::nuklear::NkShowStates _1, int cond);
        static void NkLayoutSetMinRowHeight(::nuklear::NkContext^ _0, float height);
        static void NkLayoutResetMinRowHeight(::nuklear::NkContext^ _0);
        static ::nuklear::NkRect^ NkLayoutWidgetBounds(::nuklear::NkContext^ _0);
        static float NkLayoutRatioFromPixel(::nuklear::NkContext^ _0, float pixel_width);
        static void NkLayoutRowDynamic(::nuklear::NkContext^ ctx, float height, int cols);
        static void NkLayoutRowStatic(::nuklear::NkContext^ ctx, float height, int item_width, int cols);
        static void NkLayoutRowBegin(::nuklear::NkContext^ ctx, ::nuklear::NkLayoutFormat fmt, float row_height, int cols);
        static void NkLayoutRowPush(::nuklear::NkContext^ _0, float value);
        static void NkLayoutRowEnd(::nuklear::NkContext^ _0);
        static void NkLayoutRow(::nuklear::NkContext^ _0, ::nuklear::NkLayoutFormat _1, float height, int cols, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% ratio);
        static void NkLayoutRowTemplateBegin(::nuklear::NkContext^ _0, float row_height);
        static void NkLayoutRowTemplatePushDynamic(::nuklear::NkContext^ _0);
        static void NkLayoutRowTemplatePushVariable(::nuklear::NkContext^ _0, float min_width);
        static void NkLayoutRowTemplatePushStatic(::nuklear::NkContext^ _0, float width);
        static void NkLayoutRowTemplateEnd(::nuklear::NkContext^ _0);
        static void NkLayoutSpaceBegin(::nuklear::NkContext^ _0, ::nuklear::NkLayoutFormat _1, float height, int widget_count);
        static void NkLayoutSpacePush(::nuklear::NkContext^ _0, ::nuklear::NkRect^ bounds);
        static void NkLayoutSpaceEnd(::nuklear::NkContext^ _0);
        static ::nuklear::NkRect^ NkLayoutSpaceBounds(::nuklear::NkContext^ _0);
        static ::nuklear::NkVec2^ NkLayoutSpaceToScreen(::nuklear::NkContext^ _0, ::nuklear::NkVec2^ _1);
        static ::nuklear::NkVec2^ NkLayoutSpaceToLocal(::nuklear::NkContext^ _0, ::nuklear::NkVec2^ _1);
        static ::nuklear::NkRect^ NkLayoutSpaceRectToScreen(::nuklear::NkContext^ _0, ::nuklear::NkRect^ _1);
        static ::nuklear::NkRect^ NkLayoutSpaceRectToLocal(::nuklear::NkContext^ _0, ::nuklear::NkRect^ _1);
        static int NkGroupBegin(::nuklear::NkContext^ _0, ::System::String^ title, unsigned int _1);
        static int NkGroupBeginTitled(::nuklear::NkContext^ _0, ::System::String^ name, ::System::String^ title, unsigned int _1);
        static void NkGroupEnd(::nuklear::NkContext^ _0);
        static int NkGroupScrolledOffsetBegin(::nuklear::NkContext^ _0, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] unsigned int% x_offset, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] unsigned int% y_offset, ::System::String^ title, unsigned int flags);
        static int NkGroupScrolledBegin(::nuklear::NkContext^ _0, ::nuklear::NkScroll^ off, ::System::String^ title, unsigned int _1);
        static void NkGroupScrolledEnd(::nuklear::NkContext^ _0);
        static void NkGroupGetScroll(::nuklear::NkContext^ _0, ::System::String^ id, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] unsigned int% x_offset, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] unsigned int% y_offset);
        static void NkGroupSetScroll(::nuklear::NkContext^ _0, ::System::String^ id, unsigned int x_offset, unsigned int y_offset);
        static int NkTreePushHashed(::nuklear::NkContext^ _0, ::nuklear::NkTreeType _1, ::System::String^ title, ::nuklear::NkCollapseStates initial_state, ::System::String^ hash, int len, int seed);
        static int NkTreeImagePushHashed(::nuklear::NkContext^ _0, ::nuklear::NkTreeType _1, ::nuklear::NkImage^ _2, ::System::String^ title, ::nuklear::NkCollapseStates initial_state, ::System::String^ hash, int len, int seed);
        static void NkTreePop(::nuklear::NkContext^ _0);
        static int NkTreeStatePush(::nuklear::NkContext^ _0, ::nuklear::NkTreeType _1, ::System::String^ title, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] ::nuklear::NkCollapseStates% state);
        static int NkTreeStateImagePush(::nuklear::NkContext^ _0, ::nuklear::NkTreeType _1, ::nuklear::NkImage^ _2, ::System::String^ title, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] ::nuklear::NkCollapseStates% state);
        static void NkTreeStatePop(::nuklear::NkContext^ _0);
        static int NkTreeElementPushHashed(::nuklear::NkContext^ _0, ::nuklear::NkTreeType _1, ::System::String^ title, ::nuklear::NkCollapseStates initial_state, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% selected, ::System::String^ hash, int len, int seed);
        static int NkTreeElementImagePushHashed(::nuklear::NkContext^ _0, ::nuklear::NkTreeType _1, ::nuklear::NkImage^ _2, ::System::String^ title, ::nuklear::NkCollapseStates initial_state, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% selected, ::System::String^ hash, int len, int seed);
        static void NkTreeElementPop(::nuklear::NkContext^ _0);
        static int NkListViewBegin(::nuklear::NkContext^ _0, ::nuklear::NkListView^ out, ::System::String^ id, unsigned int _1, int row_height, int row_count);
        static void NkListViewEnd(::nuklear::NkListView^ _0);
        static ::nuklear::NkWidgetLayoutStates NkWidget(::nuklear::NkRect^ _0, ::nuklear::NkContext^ _1);
        static ::nuklear::NkWidgetLayoutStates NkWidgetFitting(::nuklear::NkRect^ _0, ::nuklear::NkContext^ _1, ::nuklear::NkVec2^ _2);
        static ::nuklear::NkRect^ NkWidgetBounds(::nuklear::NkContext^ _0);
        static ::nuklear::NkVec2^ NkWidgetPosition(::nuklear::NkContext^ _0);
        static ::nuklear::NkVec2^ NkWidgetSize(::nuklear::NkContext^ _0);
        static float NkWidgetWidth(::nuklear::NkContext^ _0);
        static float NkWidgetHeight(::nuklear::NkContext^ _0);
        static int NkWidgetIsHovered(::nuklear::NkContext^ _0);
        static int NkWidgetIsMouseClicked(::nuklear::NkContext^ _0, ::nuklear::NkButtons _1);
        static int NkWidgetHasMouseClickDown(::nuklear::NkContext^ _0, ::nuklear::NkButtons _1, int down);
        static void NkSpacing(::nuklear::NkContext^ _0, int cols);
        static void NkText(::nuklear::NkContext^ _0, ::System::String^ _1, int _2, unsigned int _3);
        static void NkTextColored(::nuklear::NkContext^ _0, ::System::String^ _1, int _2, unsigned int _3, ::nuklear::NkColor^ _4);
        static void NkTextWrap(::nuklear::NkContext^ _0, ::System::String^ _1, int _2);
        static void NkTextWrapColored(::nuklear::NkContext^ _0, ::System::String^ _1, int _2, ::nuklear::NkColor^ _3);
        static void NkLabel(::nuklear::NkContext^ _0, ::System::String^ _1, unsigned int align);
        static void NkLabelColored(::nuklear::NkContext^ _0, ::System::String^ _1, unsigned int align, ::nuklear::NkColor^ _2);
        static void NkLabelWrap(::nuklear::NkContext^ _0, ::System::String^ _1);
        static void NkLabelColoredWrap(::nuklear::NkContext^ _0, ::System::String^ _1, ::nuklear::NkColor^ _2);
        static void nk_image(::nuklear::NkContext^ _0, ::nuklear::NkImage^ _1);
        static void NkImageColor(::nuklear::NkContext^ _0, ::nuklear::NkImage^ _1, ::nuklear::NkColor^ _2);
        static int NkButtonText(::nuklear::NkContext^ _0, ::System::String^ title, int len);
        static int NkButtonLabel(::nuklear::NkContext^ _0, ::System::String^ title);
        static int NkButtonColor(::nuklear::NkContext^ _0, ::nuklear::NkColor^ _1);
        static int NkButtonSymbol(::nuklear::NkContext^ _0, ::nuklear::NkSymbolType _1);
        static int NkButtonImage(::nuklear::NkContext^ _0, ::nuklear::NkImage^ img);
        static int NkButtonSymbolLabel(::nuklear::NkContext^ _0, ::nuklear::NkSymbolType _1, ::System::String^ _2, unsigned int text_alignment);
        static int NkButtonSymbolText(::nuklear::NkContext^ _0, ::nuklear::NkSymbolType _1, ::System::String^ _2, int _3, unsigned int alignment);
        static int NkButtonImageLabel(::nuklear::NkContext^ _0, ::nuklear::NkImage^ img, ::System::String^ _1, unsigned int text_alignment);
        static int NkButtonImageText(::nuklear::NkContext^ _0, ::nuklear::NkImage^ img, ::System::String^ _1, int _2, unsigned int alignment);
        static int NkButtonTextStyled(::nuklear::NkContext^ _0, ::nuklear::NkStyleButton^ _1, ::System::String^ title, int len);
        static int NkButtonLabelStyled(::nuklear::NkContext^ _0, ::nuklear::NkStyleButton^ _1, ::System::String^ title);
        static int NkButtonSymbolStyled(::nuklear::NkContext^ _0, ::nuklear::NkStyleButton^ _1, ::nuklear::NkSymbolType _2);
        static int NkButtonImageStyled(::nuklear::NkContext^ _0, ::nuklear::NkStyleButton^ _1, ::nuklear::NkImage^ img);
        static int NkButtonSymbolTextStyled(::nuklear::NkContext^ _0, ::nuklear::NkStyleButton^ _1, ::nuklear::NkSymbolType _2, ::System::String^ _3, int _4, unsigned int alignment);
        static int NkButtonSymbolLabelStyled(::nuklear::NkContext^ ctx, ::nuklear::NkStyleButton^ style, ::nuklear::NkSymbolType symbol, ::System::String^ title, unsigned int align);
        static int NkButtonImageLabelStyled(::nuklear::NkContext^ _0, ::nuklear::NkStyleButton^ _1, ::nuklear::NkImage^ img, ::System::String^ _2, unsigned int text_alignment);
        static int NkButtonImageTextStyled(::nuklear::NkContext^ _0, ::nuklear::NkStyleButton^ _1, ::nuklear::NkImage^ img, ::System::String^ _2, int _3, unsigned int alignment);
        static void NkButtonSetBehavior(::nuklear::NkContext^ _0, ::nuklear::NkButtonBehavior _1);
        static int NkButtonPushBehavior(::nuklear::NkContext^ _0, ::nuklear::NkButtonBehavior _1);
        static int NkButtonPopBehavior(::nuklear::NkContext^ _0);
        static int NkCheckLabel(::nuklear::NkContext^ _0, ::System::String^ _1, int active);
        static int NkCheckText(::nuklear::NkContext^ _0, ::System::String^ _1, int _2, int active);
        static unsigned int NkCheckFlagsLabel(::nuklear::NkContext^ _0, ::System::String^ _1, unsigned int flags, unsigned int value);
        static unsigned int NkCheckFlagsText(::nuklear::NkContext^ _0, ::System::String^ _1, int _2, unsigned int flags, unsigned int value);
        static int NkCheckboxLabel(::nuklear::NkContext^ _0, ::System::String^ _1, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% active);
        static int NkCheckboxText(::nuklear::NkContext^ _0, ::System::String^ _1, int _2, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% active);
        static int NkCheckboxFlagsLabel(::nuklear::NkContext^ _0, ::System::String^ _1, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] unsigned int% flags, unsigned int value);
        static int NkCheckboxFlagsText(::nuklear::NkContext^ _0, ::System::String^ _1, int _2, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] unsigned int% flags, unsigned int value);
        static int NkRadioLabel(::nuklear::NkContext^ _0, ::System::String^ _1, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% active);
        static int NkRadioText(::nuklear::NkContext^ _0, ::System::String^ _1, int _2, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% active);
        static int NkOptionLabel(::nuklear::NkContext^ _0, ::System::String^ _1, int active);
        static int NkOptionText(::nuklear::NkContext^ _0, ::System::String^ _1, int _2, int active);
        static int NkSelectableLabel(::nuklear::NkContext^ _0, ::System::String^ _1, unsigned int align, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% value);
        static int NkSelectableText(::nuklear::NkContext^ _0, ::System::String^ _1, int _2, unsigned int align, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% value);
        static int NkSelectableImageLabel(::nuklear::NkContext^ _0, ::nuklear::NkImage^ _1, ::System::String^ _2, unsigned int align, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% value);
        static int NkSelectableImageText(::nuklear::NkContext^ _0, ::nuklear::NkImage^ _1, ::System::String^ _2, int _3, unsigned int align, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% value);
        static int NkSelectableSymbolLabel(::nuklear::NkContext^ _0, ::nuklear::NkSymbolType _1, ::System::String^ _2, unsigned int align, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% value);
        static int NkSelectableSymbolText(::nuklear::NkContext^ _0, ::nuklear::NkSymbolType _1, ::System::String^ _2, int _3, unsigned int align, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% value);
        static int NkSelectLabel(::nuklear::NkContext^ _0, ::System::String^ _1, unsigned int align, int value);
        static int NkSelectText(::nuklear::NkContext^ _0, ::System::String^ _1, int _2, unsigned int align, int value);
        static int NkSelectImageLabel(::nuklear::NkContext^ _0, ::nuklear::NkImage^ _1, ::System::String^ _2, unsigned int align, int value);
        static int NkSelectImageText(::nuklear::NkContext^ _0, ::nuklear::NkImage^ _1, ::System::String^ _2, int _3, unsigned int align, int value);
        static int NkSelectSymbolLabel(::nuklear::NkContext^ _0, ::nuklear::NkSymbolType _1, ::System::String^ _2, unsigned int align, int value);
        static int NkSelectSymbolText(::nuklear::NkContext^ _0, ::nuklear::NkSymbolType _1, ::System::String^ _2, int _3, unsigned int align, int value);
        static float NkSlideFloat(::nuklear::NkContext^ _0, float min, float val, float max, float step);
        static int NkSlideInt(::nuklear::NkContext^ _0, int min, int val, int max, int step);
        static int NkSliderFloat(::nuklear::NkContext^ _0, float min, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% val, float max, float step);
        static int NkSliderInt(::nuklear::NkContext^ _0, int min, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% val, int max, int step);
        static int NkProgress(::nuklear::NkContext^ _0, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] unsigned long long% cur, unsigned long long max, int modifyable);
        static unsigned long long NkProg(::nuklear::NkContext^ _0, unsigned long long cur, unsigned long long max, int modifyable);
        static ::nuklear::NkColorf^ NkColorPicker(::nuklear::NkContext^ _0, ::nuklear::NkColorf^ _1, ::nuklear::NkColorFormat _2);
        static int NkColorPick(::nuklear::NkContext^ _0, ::nuklear::NkColorf^ _1, ::nuklear::NkColorFormat _2);
        static void NkPropertyInt(::nuklear::NkContext^ _0, ::System::String^ name, int min, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% val, int max, int step, float inc_per_pixel);
        static void NkPropertyFloat(::nuklear::NkContext^ _0, ::System::String^ name, float min, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% val, float max, float step, float inc_per_pixel);
        static void NkPropertyDouble(::nuklear::NkContext^ _0, ::System::String^ name, double min, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] double% val, double max, double step, float inc_per_pixel);
        static int NkPropertyi(::nuklear::NkContext^ _0, ::System::String^ name, int min, int val, int max, int step, float inc_per_pixel);
        static float NkPropertyf(::nuklear::NkContext^ _0, ::System::String^ name, float min, float val, float max, float step, float inc_per_pixel);
        static double NkPropertyd(::nuklear::NkContext^ _0, ::System::String^ name, double min, double val, double max, double step, float inc_per_pixel);
        static unsigned int NkEditString(::nuklear::NkContext^ _0, unsigned int _1, char* buffer, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% len, int max, ::nuklear::NkPluginFilter^ _2);
        static unsigned int NkEditStringZeroTerminated(::nuklear::NkContext^ _0, unsigned int _1, char* buffer, int max, ::nuklear::NkPluginFilter^ _2);
        static unsigned int NkEditBuffer(::nuklear::NkContext^ _0, unsigned int _1, ::nuklear::NkTextEdit^ _2, ::nuklear::NkPluginFilter^ _3);
        static void NkEditFocus(::nuklear::NkContext^ _0, unsigned int flags);
        static void NkEditUnfocus(::nuklear::NkContext^ _0);
        static int NkChartBegin(::nuklear::NkContext^ _0, ::nuklear::NkChartType _1, int num, float min, float max);
        static int NkChartBeginColored(::nuklear::NkContext^ _0, ::nuklear::NkChartType _1, ::nuklear::NkColor^ _2, ::nuklear::NkColor^ active, int num, float min, float max);
        static void NkChartAddSlot(::nuklear::NkContext^ ctx, ::nuklear::NkChartType _0, int count, float min_value, float max_value);
        static void NkChartAddSlotColored(::nuklear::NkContext^ ctx, ::nuklear::NkChartType _0, ::nuklear::NkColor^ _1, ::nuklear::NkColor^ active, int count, float min_value, float max_value);
        static unsigned int NkChartPush(::nuklear::NkContext^ _0, float _1);
        static unsigned int NkChartPushSlot(::nuklear::NkContext^ _0, float _1, int _2);
        static void NkChartEnd(::nuklear::NkContext^ _0);
        static void NkPlot(::nuklear::NkContext^ _0, ::nuklear::NkChartType _1, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% values, int count, int offset);
        static void NkPlotFunction(::nuklear::NkContext^ _0, ::nuklear::NkChartType _1, ::System::IntPtr userdata, ::nuklear::Func_float___IntPtr_int^ value_getter, int count, int offset);
        static int NkPopupBegin(::nuklear::NkContext^ _0, ::nuklear::NkPopupType _1, ::System::String^ _2, unsigned int _3, ::nuklear::NkRect^ bounds);
        static void NkPopupClose(::nuklear::NkContext^ _0);
        static void NkPopupEnd(::nuklear::NkContext^ _0);
        static void NkPopupGetScroll(::nuklear::NkContext^ _0, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] unsigned int% offset_x, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] unsigned int% offset_y);
        static void NkPopupSetScroll(::nuklear::NkContext^ _0, unsigned int offset_x, unsigned int offset_y);
        static int NkCombo(::nuklear::NkContext^ _0, ::System::String^* items, int count, int selected, int item_height, ::nuklear::NkVec2^ size);
        static int NkComboSeparator(::nuklear::NkContext^ _0, ::System::String^ items_separated_by_separator, int separator, int selected, int count, int item_height, ::nuklear::NkVec2^ size);
        static int NkComboString(::nuklear::NkContext^ _0, ::System::String^ items_separated_by_zeros, int selected, int count, int item_height, ::nuklear::NkVec2^ size);
        static int NkComboCallback(::nuklear::NkContext^ _0, ::nuklear::Action___IntPtr_int_sbytePtrPtr^ item_getter, ::System::IntPtr userdata, int selected, int count, int item_height, ::nuklear::NkVec2^ size);
        static void NkCombobox(::nuklear::NkContext^ _0, ::System::String^* items, int count, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% selected, int item_height, ::nuklear::NkVec2^ size);
        static void NkComboboxString(::nuklear::NkContext^ _0, ::System::String^ items_separated_by_zeros, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% selected, int count, int item_height, ::nuklear::NkVec2^ size);
        static void NkComboboxSeparator(::nuklear::NkContext^ _0, ::System::String^ items_separated_by_separator, int separator, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% selected, int count, int item_height, ::nuklear::NkVec2^ size);
        static void NkComboboxCallback(::nuklear::NkContext^ _0, ::nuklear::Action___IntPtr_int_sbytePtrPtr^ item_getter, ::System::IntPtr _1, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% selected, int count, int item_height, ::nuklear::NkVec2^ size);
        static int NkComboBeginText(::nuklear::NkContext^ _0, ::System::String^ selected, int _1, ::nuklear::NkVec2^ size);
        static int NkComboBeginLabel(::nuklear::NkContext^ _0, ::System::String^ selected, ::nuklear::NkVec2^ size);
        static int NkComboBeginColor(::nuklear::NkContext^ _0, ::nuklear::NkColor^ color, ::nuklear::NkVec2^ size);
        static int NkComboBeginSymbol(::nuklear::NkContext^ _0, ::nuklear::NkSymbolType _1, ::nuklear::NkVec2^ size);
        static int NkComboBeginSymbolLabel(::nuklear::NkContext^ _0, ::System::String^ selected, ::nuklear::NkSymbolType _1, ::nuklear::NkVec2^ size);
        static int NkComboBeginSymbolText(::nuklear::NkContext^ _0, ::System::String^ selected, int _1, ::nuklear::NkSymbolType _2, ::nuklear::NkVec2^ size);
        static int NkComboBeginImage(::nuklear::NkContext^ _0, ::nuklear::NkImage^ img, ::nuklear::NkVec2^ size);
        static int NkComboBeginImageLabel(::nuklear::NkContext^ _0, ::System::String^ selected, ::nuklear::NkImage^ _1, ::nuklear::NkVec2^ size);
        static int NkComboBeginImageText(::nuklear::NkContext^ _0, ::System::String^ selected, int _1, ::nuklear::NkImage^ _2, ::nuklear::NkVec2^ size);
        static int NkComboItemLabel(::nuklear::NkContext^ _0, ::System::String^ _1, unsigned int alignment);
        static int NkComboItemText(::nuklear::NkContext^ _0, ::System::String^ _1, int _2, unsigned int alignment);
        static int NkComboItemImageLabel(::nuklear::NkContext^ _0, ::nuklear::NkImage^ _1, ::System::String^ _2, unsigned int alignment);
        static int NkComboItemImageText(::nuklear::NkContext^ _0, ::nuklear::NkImage^ _1, ::System::String^ _2, int _3, unsigned int alignment);
        static int NkComboItemSymbolLabel(::nuklear::NkContext^ _0, ::nuklear::NkSymbolType _1, ::System::String^ _2, unsigned int alignment);
        static int NkComboItemSymbolText(::nuklear::NkContext^ _0, ::nuklear::NkSymbolType _1, ::System::String^ _2, int _3, unsigned int alignment);
        static void NkComboClose(::nuklear::NkContext^ _0);
        static void NkComboEnd(::nuklear::NkContext^ _0);
        static int NkContextualBegin(::nuklear::NkContext^ _0, unsigned int _1, ::nuklear::NkVec2^ _2, ::nuklear::NkRect^ trigger_bounds);
        static int NkContextualItemText(::nuklear::NkContext^ _0, ::System::String^ _1, int _2, unsigned int align);
        static int NkContextualItemLabel(::nuklear::NkContext^ _0, ::System::String^ _1, unsigned int align);
        static int NkContextualItemImageLabel(::nuklear::NkContext^ _0, ::nuklear::NkImage^ _1, ::System::String^ _2, unsigned int alignment);
        static int NkContextualItemImageText(::nuklear::NkContext^ _0, ::nuklear::NkImage^ _1, ::System::String^ _2, int len, unsigned int alignment);
        static int NkContextualItemSymbolLabel(::nuklear::NkContext^ _0, ::nuklear::NkSymbolType _1, ::System::String^ _2, unsigned int alignment);
        static int NkContextualItemSymbolText(::nuklear::NkContext^ _0, ::nuklear::NkSymbolType _1, ::System::String^ _2, int _3, unsigned int alignment);
        static void NkContextualClose(::nuklear::NkContext^ _0);
        static void NkContextualEnd(::nuklear::NkContext^ _0);
        static void NkTooltip(::nuklear::NkContext^ _0, ::System::String^ _1);
        static int NkTooltipBegin(::nuklear::NkContext^ _0, float width);
        static void NkTooltipEnd(::nuklear::NkContext^ _0);
        static void NkMenubarBegin(::nuklear::NkContext^ _0);
        static void NkMenubarEnd(::nuklear::NkContext^ _0);
        static int NkMenuBeginText(::nuklear::NkContext^ _0, ::System::String^ title, int title_len, unsigned int align, ::nuklear::NkVec2^ size);
        static int NkMenuBeginLabel(::nuklear::NkContext^ _0, ::System::String^ _1, unsigned int align, ::nuklear::NkVec2^ size);
        static int NkMenuBeginImage(::nuklear::NkContext^ _0, ::System::String^ _1, ::nuklear::NkImage^ _2, ::nuklear::NkVec2^ size);
        static int NkMenuBeginImageText(::nuklear::NkContext^ _0, ::System::String^ _1, int _2, unsigned int align, ::nuklear::NkImage^ _3, ::nuklear::NkVec2^ size);
        static int NkMenuBeginImageLabel(::nuklear::NkContext^ _0, ::System::String^ _1, unsigned int align, ::nuklear::NkImage^ _2, ::nuklear::NkVec2^ size);
        static int NkMenuBeginSymbol(::nuklear::NkContext^ _0, ::System::String^ _1, ::nuklear::NkSymbolType _2, ::nuklear::NkVec2^ size);
        static int NkMenuBeginSymbolText(::nuklear::NkContext^ _0, ::System::String^ _1, int _2, unsigned int align, ::nuklear::NkSymbolType _3, ::nuklear::NkVec2^ size);
        static int NkMenuBeginSymbolLabel(::nuklear::NkContext^ _0, ::System::String^ _1, unsigned int align, ::nuklear::NkSymbolType _2, ::nuklear::NkVec2^ size);
        static int NkMenuItemText(::nuklear::NkContext^ _0, ::System::String^ _1, int _2, unsigned int align);
        static int NkMenuItemLabel(::nuklear::NkContext^ _0, ::System::String^ _1, unsigned int alignment);
        static int NkMenuItemImageLabel(::nuklear::NkContext^ _0, ::nuklear::NkImage^ _1, ::System::String^ _2, unsigned int alignment);
        static int NkMenuItemImageText(::nuklear::NkContext^ _0, ::nuklear::NkImage^ _1, ::System::String^ _2, int len, unsigned int alignment);
        static int NkMenuItemSymbolText(::nuklear::NkContext^ _0, ::nuklear::NkSymbolType _1, ::System::String^ _2, int _3, unsigned int alignment);
        static int NkMenuItemSymbolLabel(::nuklear::NkContext^ _0, ::nuklear::NkSymbolType _1, ::System::String^ _2, unsigned int alignment);
        static void NkMenuClose(::nuklear::NkContext^ _0);
        static void NkMenuEnd(::nuklear::NkContext^ _0);
        static void NkStyleDefault(::nuklear::NkContext^ _0);
        static void NkStyleFromTable(::nuklear::NkContext^ _0, ::nuklear::NkColor^ _1);
        static void NkStyleLoadCursor(::nuklear::NkContext^ _0, ::nuklear::NkStyleCursor _1, ::nuklear::NkCursor^ _2);
        static void NkStyleLoadAllCursors(::nuklear::NkContext^ _0, ::nuklear::NkCursor^ _1);
        static ::System::String^ NkStyleGetColorByName(::nuklear::NkStyleColors _0);
        static void NkStyleSetFont(::nuklear::NkContext^ _0, ::nuklear::NkUserFont^ _1);
        static int NkStyleSetCursor(::nuklear::NkContext^ _0, ::nuklear::NkStyleCursor _1);
        static void NkStyleShowCursor(::nuklear::NkContext^ _0);
        static void NkStyleHideCursor(::nuklear::NkContext^ _0);
        static int NkStylePushFont(::nuklear::NkContext^ _0, ::nuklear::NkUserFont^ _1);
        static int NkStylePushFloat(::nuklear::NkContext^ _0, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% _1, float _2);
        static int NkStylePushVec2(::nuklear::NkContext^ _0, ::nuklear::NkVec2^ _1, ::nuklear::NkVec2^ _2);
        static int NkStylePushStyleItem(::nuklear::NkContext^ _0, ::nuklear::NkStyleItem^ _1, ::nuklear::NkStyleItem^ _2);
        static int NkStylePushFlags(::nuklear::NkContext^ _0, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] unsigned int% _1, unsigned int _2);
        static int NkStylePushColor(::nuklear::NkContext^ _0, ::nuklear::NkColor^ _1, ::nuklear::NkColor^ _2);
        static int NkStylePopFont(::nuklear::NkContext^ _0);
        static int NkStylePopFloat(::nuklear::NkContext^ _0);
        static int NkStylePopVec2(::nuklear::NkContext^ _0);
        static int NkStylePopStyleItem(::nuklear::NkContext^ _0);
        static int NkStylePopFlags(::nuklear::NkContext^ _0);
        static int NkStylePopColor(::nuklear::NkContext^ _0);
        static ::nuklear::NkColor^ NkRgb(int r, int g, int b);
        static ::nuklear::NkColor^ NkRgbIv([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% rgb);
        static ::nuklear::NkColor^ NkRgbBv(unsigned char* rgb);
        static ::nuklear::NkColor^ NkRgbF(float r, float g, float b);
        static ::nuklear::NkColor^ NkRgbFv([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% rgb);
        static ::nuklear::NkColor^ NkRgbCf(::nuklear::NkColorf^ c);
        static ::nuklear::NkColor^ NkRgbHex(::System::String^ rgb);
        static ::nuklear::NkColor^ NkRgba(int r, int g, int b, int a);
        static ::nuklear::NkColor^ NkRgbaU32(unsigned int _0);
        static ::nuklear::NkColor^ NkRgbaIv([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% rgba);
        static ::nuklear::NkColor^ NkRgbaBv(unsigned char* rgba);
        static ::nuklear::NkColor^ NkRgbaF(float r, float g, float b, float a);
        static ::nuklear::NkColor^ NkRgbaFv([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% rgba);
        static ::nuklear::NkColor^ NkRgbaCf(::nuklear::NkColorf^ c);
        static ::nuklear::NkColor^ NkRgbaHex(::System::String^ rgb);
        static ::nuklear::NkColorf^ NkHsvaColorf(float h, float s, float v, float a);
        static ::nuklear::NkColorf^ NkHsvaColorfv([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% c);
        static void NkColorfHsvaF([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% out_h, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% out_s, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% out_v, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% out_a, ::nuklear::NkColorf^ in);
        static void NkColorfHsvaFv([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% hsva, ::nuklear::NkColorf^ in);
        static ::nuklear::NkColor^ NkHsv(int h, int s, int v);
        static ::nuklear::NkColor^ NkHsvIv([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% hsv);
        static ::nuklear::NkColor^ NkHsvBv(unsigned char* hsv);
        static ::nuklear::NkColor^ NkHsvF(float h, float s, float v);
        static ::nuklear::NkColor^ NkHsvFv([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% hsv);
        static ::nuklear::NkColor^ NkHsva(int h, int s, int v, int a);
        static ::nuklear::NkColor^ NkHsvaIv([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% hsva);
        static ::nuklear::NkColor^ NkHsvaBv(unsigned char* hsva);
        static ::nuklear::NkColor^ NkHsvaF(float h, float s, float v, float a);
        static ::nuklear::NkColor^ NkHsvaFv([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% hsva);
        static void NkColorF([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% r, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% g, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% b, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% a, ::nuklear::NkColor^ _0);
        static void NkColorFv([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% rgba_out, ::nuklear::NkColor^ _0);
        static ::nuklear::NkColorf^ NkColorCf(::nuklear::NkColor^ _0);
        static void NkColorD([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] double% r, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] double% g, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] double% b, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] double% a, ::nuklear::NkColor^ _0);
        static void NkColorDv([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] double% rgba_out, ::nuklear::NkColor^ _0);
        static unsigned int NkColorU32(::nuklear::NkColor^ _0);
        static void NkColorHexRgba(char* output, ::nuklear::NkColor^ _0);
        static void NkColorHexRgb(char* output, ::nuklear::NkColor^ _0);
        static void NkColorHsvI([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% out_h, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% out_s, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% out_v, ::nuklear::NkColor^ _0);
        static void NkColorHsvB(unsigned char* out_h, unsigned char* out_s, unsigned char* out_v, ::nuklear::NkColor^ _0);
        static void NkColorHsvIv([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% hsv_out, ::nuklear::NkColor^ _0);
        static void NkColorHsvBv(unsigned char* hsv_out, ::nuklear::NkColor^ _0);
        static void NkColorHsvF([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% out_h, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% out_s, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% out_v, ::nuklear::NkColor^ _0);
        static void NkColorHsvFv([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% hsv_out, ::nuklear::NkColor^ _0);
        static void NkColorHsvaI([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% h, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% s, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% v, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% a, ::nuklear::NkColor^ _0);
        static void NkColorHsvaB(unsigned char* h, unsigned char* s, unsigned char* v, unsigned char* a, ::nuklear::NkColor^ _0);
        static void NkColorHsvaIv([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% hsva_out, ::nuklear::NkColor^ _0);
        static void NkColorHsvaBv(unsigned char* hsva_out, ::nuklear::NkColor^ _0);
        static void NkColorHsvaF([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% out_h, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% out_s, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% out_v, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% out_a, ::nuklear::NkColor^ _0);
        static void NkColorHsvaFv([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% hsva_out, ::nuklear::NkColor^ _0);
        static ::nuklear::NkHandle NkHandlePtr(::System::IntPtr _0);
        static ::nuklear::NkHandle NkHandleId(int _0);
        static ::nuklear::NkImage^ NkImageHandle(::nuklear::NkHandle _0);
        static ::nuklear::NkImage^ NkImagePtr(::System::IntPtr _0);
        static ::nuklear::NkImage^ NkImageId(int _0);
        static int NkImageIsSubimage(::nuklear::NkImage^ img);
        static ::nuklear::NkImage^ NkSubimagePtr(::System::IntPtr _0, unsigned short w, unsigned short h, ::nuklear::NkRect^ sub_region);
        static ::nuklear::NkImage^ NkSubimageId(int _0, unsigned short w, unsigned short h, ::nuklear::NkRect^ sub_region);
        static ::nuklear::NkImage^ NkSubimageHandle(::nuklear::NkHandle _0, unsigned short w, unsigned short h, ::nuklear::NkRect^ sub_region);
        static unsigned int NkMurmurHash(::System::IntPtr key, int len, unsigned int seed);
        static void NkTriangleFromDirection(::nuklear::NkVec2^ result, ::nuklear::NkRect^ r, float pad_x, float pad_y, ::nuklear::NkHeading _0);
        static ::nuklear::NkVec2^ nk_vec2(float x, float y);
        static ::nuklear::NkVec2^ nk_vec2i(int x, int y);
        static ::nuklear::NkVec2^ NkVec2v([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% xy);
        static ::nuklear::NkVec2^ NkVec2iv([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% xy);
        static ::nuklear::NkRect^ NkGetNullRect();
        static ::nuklear::NkRect^ nk_rect(float x, float y, float w, float h);
        static ::nuklear::NkRect^ nk_recti(int x, int y, int w, int h);
        static ::nuklear::NkRect^ NkRecta(::nuklear::NkVec2^ pos, ::nuklear::NkVec2^ size);
        static ::nuklear::NkRect^ NkRectv([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% xywh);
        static ::nuklear::NkRect^ NkRectiv([::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% xywh);
        static ::nuklear::NkVec2^ NkRectPos(::nuklear::NkRect^ _0);
        static ::nuklear::NkVec2^ NkRectSize(::nuklear::NkRect^ _0);
        static int NkStrlen(::System::String^ str);
        static int NkStricmp(::System::String^ s1, ::System::String^ s2);
        static int NkStricmpn(::System::String^ s1, ::System::String^ s2, int n);
        static int NkStrtoi(::System::String^ str, ::System::String^* endptr);
        static float NkStrtof(::System::String^ str, ::System::String^* endptr);
        static double NkStrtod(::System::String^ str, ::System::String^* endptr);
        static int NkStrfilter(::System::String^ text, ::System::String^ regexp);
        static int NkStrmatchFuzzyString(::System::String^ str, ::System::String^ pattern, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% out_score);
        static int NkStrmatchFuzzyText(::System::String^ txt, int txt_len, ::System::String^ pattern, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% out_score);
        static int NkUtfDecode(::System::String^ _0, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] unsigned int% _1, int _2);
        static int NkUtfEncode(unsigned int _0, char* _1, int _2);
        static int NkUtfLen(::System::String^ _0, int byte_len);
        static ::System::String^ NkUtfAt(::System::String^ buffer, int length, int index, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] unsigned int% unicode, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% len);
        static void NkBufferInit(::nuklear::NkBuffer^ _0, ::nuklear::NkAllocator^ _1, unsigned long long size);
        static void NkBufferInitFixed(::nuklear::NkBuffer^ _0, ::System::IntPtr memory, unsigned long long size);
        static void NkBufferInfo(::nuklear::NkMemoryStatus^ _0, ::nuklear::NkBuffer^ _1);
        static void NkBufferPush(::nuklear::NkBuffer^ _0, ::nuklear::NkBufferAllocationType type, ::System::IntPtr memory, unsigned long long size, unsigned long long align);
        static void NkBufferMark(::nuklear::NkBuffer^ _0, ::nuklear::NkBufferAllocationType type);
        static void NkBufferReset(::nuklear::NkBuffer^ _0, ::nuklear::NkBufferAllocationType type);
        static void NkBufferClear(::nuklear::NkBuffer^ _0);
        static void NkBufferFree(::nuklear::NkBuffer^ _0);
        static ::System::IntPtr NkBufferMemory(::nuklear::NkBuffer^ _0);
        static ::System::IntPtr NkBufferMemoryConst(::nuklear::NkBuffer^ _0);
        static unsigned long long NkBufferTotal(::nuklear::NkBuffer^ _0);
        static void NkStrInit(::nuklear::NkStr^ _0, ::nuklear::NkAllocator^ _1, unsigned long long size);
        static void NkStrInitFixed(::nuklear::NkStr^ _0, ::System::IntPtr memory, unsigned long long size);
        static void NkStrClear(::nuklear::NkStr^ _0);
        static void NkStrFree(::nuklear::NkStr^ _0);
        static int NkStrAppendTextChar(::nuklear::NkStr^ _0, ::System::String^ _1, int _2);
        static int NkStrAppendStrChar(::nuklear::NkStr^ _0, ::System::String^ _1);
        static int NkStrAppendTextUtf8(::nuklear::NkStr^ _0, ::System::String^ _1, int _2);
        static int NkStrAppendStrUtf8(::nuklear::NkStr^ _0, ::System::String^ _1);
        static int NkStrAppendTextRunes(::nuklear::NkStr^ _0, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] unsigned int% _1, int _2);
        static int NkStrAppendStrRunes(::nuklear::NkStr^ _0, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] unsigned int% _1);
        static int NkStrInsertAtChar(::nuklear::NkStr^ _0, int pos, ::System::String^ _1, int _2);
        static int NkStrInsertAtRune(::nuklear::NkStr^ _0, int pos, ::System::String^ _1, int _2);
        static int NkStrInsertTextChar(::nuklear::NkStr^ _0, int pos, ::System::String^ _1, int _2);
        static int NkStrInsertStrChar(::nuklear::NkStr^ _0, int pos, ::System::String^ _1);
        static int NkStrInsertTextUtf8(::nuklear::NkStr^ _0, int pos, ::System::String^ _1, int _2);
        static int NkStrInsertStrUtf8(::nuklear::NkStr^ _0, int pos, ::System::String^ _1);
        static int NkStrInsertTextRunes(::nuklear::NkStr^ _0, int pos, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] unsigned int% _1, int _2);
        static int NkStrInsertStrRunes(::nuklear::NkStr^ _0, int pos, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] unsigned int% _1);
        static void NkStrRemoveChars(::nuklear::NkStr^ _0, int len);
        static void NkStrRemoveRunes(::nuklear::NkStr^ str, int len);
        static void NkStrDeleteChars(::nuklear::NkStr^ _0, int pos, int len);
        static void NkStrDeleteRunes(::nuklear::NkStr^ _0, int pos, int len);
        static char* NkStrAtChar(::nuklear::NkStr^ _0, int pos);
        static char* NkStrAtRune(::nuklear::NkStr^ _0, int pos, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] unsigned int% unicode, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% len);
        static unsigned int NkStrRuneAt(::nuklear::NkStr^ _0, int pos);
        static ::System::String^ NkStrAtCharConst(::nuklear::NkStr^ _0, int pos);
        static ::System::String^ NkStrAtConst(::nuklear::NkStr^ _0, int pos, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] unsigned int% unicode, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] int% len);
        static char* NkStrGet(::nuklear::NkStr^ _0);
        static ::System::String^ NkStrGetConst(::nuklear::NkStr^ _0);
        static int NkStrLen(::nuklear::NkStr^ _0);
        static int NkStrLenChar(::nuklear::NkStr^ _0);
        static int NkFilterDefault(::nuklear::NkTextEdit^ _0, unsigned int unicode);
        static int NkFilterAscii(::nuklear::NkTextEdit^ _0, unsigned int unicode);
        static int NkFilterFloat(::nuklear::NkTextEdit^ _0, unsigned int unicode);
        static int NkFilterDecimal(::nuklear::NkTextEdit^ _0, unsigned int unicode);
        static int NkFilterHex(::nuklear::NkTextEdit^ _0, unsigned int unicode);
        static int NkFilterOct(::nuklear::NkTextEdit^ _0, unsigned int unicode);
        static int NkFilterBinary(::nuklear::NkTextEdit^ _0, unsigned int unicode);
        static void NkTexteditInit(::nuklear::NkTextEdit^ _0, ::nuklear::NkAllocator^ _1, unsigned long long size);
        static void NkTexteditInitFixed(::nuklear::NkTextEdit^ _0, ::System::IntPtr memory, unsigned long long size);
        static void NkTexteditFree(::nuklear::NkTextEdit^ _0);
        static void NkTexteditText(::nuklear::NkTextEdit^ _0, ::System::String^ _1, int total_len);
        static void NkTexteditDelete(::nuklear::NkTextEdit^ _0, int where, int len);
        static void NkTexteditDeleteSelection(::nuklear::NkTextEdit^ _0);
        static void NkTexteditSelectAll(::nuklear::NkTextEdit^ _0);
        static int NkTexteditCut(::nuklear::NkTextEdit^ _0);
        static int NkTexteditPaste(::nuklear::NkTextEdit^ _0, ::System::String^ _1, int len);
        static void NkTexteditUndo(::nuklear::NkTextEdit^ _0);
        static void NkTexteditRedo(::nuklear::NkTextEdit^ _0);
        static void NkStrokeLine(::nuklear::NkCommandBuffer^ b, float x0, float y0, float x1, float y1, float line_thickness, ::nuklear::NkColor^ _0);
        static void NkStrokeCurve(::nuklear::NkCommandBuffer^ _0, float _1, float _2, float _3, float _4, float _5, float _6, float _7, float _8, float line_thickness, ::nuklear::NkColor^ _9);
        static void NkStrokeRect(::nuklear::NkCommandBuffer^ _0, ::nuklear::NkRect^ _1, float rounding, float line_thickness, ::nuklear::NkColor^ _2);
        static void NkStrokeCircle(::nuklear::NkCommandBuffer^ _0, ::nuklear::NkRect^ _1, float line_thickness, ::nuklear::NkColor^ _2);
        static void NkStrokeArc(::nuklear::NkCommandBuffer^ _0, float cx, float cy, float radius, float a_min, float a_max, float line_thickness, ::nuklear::NkColor^ _1);
        static void NkStrokeTriangle(::nuklear::NkCommandBuffer^ _0, float _1, float _2, float _3, float _4, float _5, float _6, float line_thichness, ::nuklear::NkColor^ _7);
        static void NkStrokePolyline(::nuklear::NkCommandBuffer^ _0, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% points, int point_count, float line_thickness, ::nuklear::NkColor^ col);
        static void NkStrokePolygon(::nuklear::NkCommandBuffer^ _0, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% _1, int point_count, float line_thickness, ::nuklear::NkColor^ _2);
        static void NkFillRect(::nuklear::NkCommandBuffer^ _0, ::nuklear::NkRect^ _1, float rounding, ::nuklear::NkColor^ _2);
        static void NkFillRectMultiColor(::nuklear::NkCommandBuffer^ _0, ::nuklear::NkRect^ _1, ::nuklear::NkColor^ left, ::nuklear::NkColor^ top, ::nuklear::NkColor^ right, ::nuklear::NkColor^ bottom);
        static void NkFillCircle(::nuklear::NkCommandBuffer^ _0, ::nuklear::NkRect^ _1, ::nuklear::NkColor^ _2);
        static void NkFillArc(::nuklear::NkCommandBuffer^ _0, float cx, float cy, float radius, float a_min, float a_max, ::nuklear::NkColor^ _1);
        static void NkFillTriangle(::nuklear::NkCommandBuffer^ _0, float x0, float y0, float x1, float y1, float x2, float y2, ::nuklear::NkColor^ _1);
        static void NkFillPolygon(::nuklear::NkCommandBuffer^ _0, [::System::Runtime::InteropServices::In, ::System::Runtime::InteropServices::Out] float% _1, int point_count, ::nuklear::NkColor^ _2);
        static void NkDrawImage(::nuklear::NkCommandBuffer^ _0, ::nuklear::NkRect^ _1, ::nuklear::NkImage^ _2, ::nuklear::NkColor^ _3);
        static void NkDrawText(::nuklear::NkCommandBuffer^ _0, ::nuklear::NkRect^ _1, ::System::String^ text, int len, ::nuklear::NkUserFont^ _2, ::nuklear::NkColor^ _3, ::nuklear::NkColor^ _4);
        static void NkPushScissor(::nuklear::NkCommandBuffer^ _0, ::nuklear::NkRect^ _1);
        static void NkPushCustom(::nuklear::NkCommandBuffer^ _0, ::nuklear::NkRect^ _1, ::nuklear::NkCommandCustomCallback^ _2, ::nuklear::NkHandle usr);
        static int NkInputHasMouseClick(::nuklear::NkInput^ _0, ::nuklear::NkButtons _1);
        static int NkInputHasMouseClickInRect(::nuklear::NkInput^ _0, ::nuklear::NkButtons _1, ::nuklear::NkRect^ _2);
        static int NkInputHasMouseClickDownInRect(::nuklear::NkInput^ _0, ::nuklear::NkButtons _1, ::nuklear::NkRect^ _2, int down);
        static int NkInputIsMouseClickInRect(::nuklear::NkInput^ _0, ::nuklear::NkButtons _1, ::nuklear::NkRect^ _2);
        static int NkInputIsMouseClickDownInRect(::nuklear::NkInput^ i, ::nuklear::NkButtons id, ::nuklear::NkRect^ b, int down);
        static int NkInputAnyMouseClickInRect(::nuklear::NkInput^ _0, ::nuklear::NkRect^ _1);
        static int NkInputIsMousePrevHoveringRect(::nuklear::NkInput^ _0, ::nuklear::NkRect^ _1);
        static int NkInputIsMouseHoveringRect(::nuklear::NkInput^ _0, ::nuklear::NkRect^ _1);
        static int NkInputMouseClicked(::nuklear::NkInput^ _0, ::nuklear::NkButtons _1, ::nuklear::NkRect^ _2);
        static int NkInputIsMouseDown(::nuklear::NkInput^ _0, ::nuklear::NkButtons _1);
        static int NkInputIsMousePressed(::nuklear::NkInput^ _0, ::nuklear::NkButtons _1);
        static int NkInputIsMouseReleased(::nuklear::NkInput^ _0, ::nuklear::NkButtons _1);
        static int NkInputIsKeyPressed(::nuklear::NkInput^ _0, ::nuklear::NkKeys _1);
        static int NkInputIsKeyReleased(::nuklear::NkInput^ _0, ::nuklear::NkKeys _1);
        static int NkInputIsKeyDown(::nuklear::NkInput^ _0, ::nuklear::NkKeys _1);
        static ::nuklear::NkStyleItem^ NkStyleItemImage(::nuklear::NkImage^ img);
        static ::nuklear::NkStyleItem^ NkStyleItemColor(::nuklear::NkColor^ _0);
        static ::nuklear::NkStyleItem^ NkStyleItemHide();

        static ::nuklear::NkContext^ NkCreateGlfwContext(long long* window, int max_vertex_buffer, int max_element_buffer);
        static void NkBeginFontAtlas();
        static void NkFontAtlasAddFromFile(System::String^ path, int height);
        static void NkEndFontAtlas();
        static NkImage^ NkCreateBlindlessTexture(int width, int height);
        static void NkNewFrame();
        static void NkRender(bool useAntiAliasing);
        static void NkShutdown();

        static void DrawBindlessTexture(NkContext^ ctx, NkImage^ image, float x, float y, float w, float h);
    };
}
#endif