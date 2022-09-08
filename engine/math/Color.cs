using System;
using OpenTK.Mathematics;

namespace Emission.Math
{
    [Serializable]
    public struct Color
    {
        #region Statics Colors
            public static Color Transparent = new(0x00FFFFFF); // Transparent
            public static Color AliceBlue = new(0xFFF0F8FF); // AliceBlue
            public static Color AntiqueWhite = new(0xFFFAEBD7); // AntiqueWhite
            public static Color Aqua = new(0xFF00FFFF); // Aqua
            public static Color Aquamarine = new(0xFF7FFFD4); // Aquamarine
            public static Color Azure = new(0xFFF0FFFF); // Azure
            public static Color Beige = new(0xFFF5F5DC); // Beige
            public static Color Bisque = new(0xFFFFE4C4); // Bisque
            public static Color Black = new(0xFF000000); // Black
            public static Color BlanchedAlmond = new(0xFFFFEBCD); // BlanchedAlmond
            public static Color Blue = new(0xFF0000FF); // Blue
            public static Color BlueViolet = new(0xFF8A2BE2); // BlueViolet
            public static Color Brown = new(0xFFA52A2A); // Brown
            public static Color BurlyWood = new(0xFFDEB887); // BurlyWood
            public static Color CadetBlue = new(0xFF5F9EA0); // CadetBlue
            public static Color Chartreuse = new(0xFF7FFF00); // Chartreuse
            public static Color Chocolate = new(0xFFD2691E); // Chocolate
            public static Color Coral = new(0xFFFF7F50); // Coral
            public static Color CornflowerBlue = new(0xFF6495ED); // CornflowerBlue
            public static Color Cornsilk = new(0xFFFFF8DC); // Cornsilk
            public static Color Crimson = new(0xFFDC143C); // Crimson
            public static Color Cyan = new(0xFF00FFFF); // Cyan
            public static Color DarkBlue = new(0xFF00008B); // DarkBlue
            public static Color DarkCyan = new(0xFF008B8B); // DarkCyan
            public static Color DarkGoldenrod = new(0xFFB8860B); // DarkGoldenrod
            public static Color DarkGray = new(0xFFA9A9A9); // DarkGray
            public static Color DarkGreen = new(0xFF006400); // DarkGreen
            public static Color DarkKhaki = new(0xFFBDB76B); // DarkKhaki
            public static Color DarkMagenta = new(0xFF8B008B); // DarkMagenta
            public static Color DarkOliveGreen = new(0xFF556B2F); // DarkOliveGreen
            public static Color DarkOrange = new(0xFFFF8C00); // DarkOrange
            public static Color DarkOrchid = new(0xFF9932CC); // DarkOrchid
            public static Color DarkRed = new(0xFF8B0000); // DarkRed
            public static Color DarkSalmon = new(0xFFE9967A); // DarkSalmon
            public static Color DarkSeaGreen = new(0xFF8FBC8B); // DarkSeaGreen
            public static Color DarkSlateBlue = new(0xFF483D8B); // DarkSlateBlue
            public static Color DarkSlateGray = new(0xFF2F4F4F); // DarkSlateGray
            public static Color DarkTurquoise = new(0xFF00CED1); // DarkTurquoise
            public static Color DarkViolet = new(0xFF9400D3); // DarkViolet
            public static Color DeepPink = new(0xFFFF1493); // DeepPink
            public static Color DeepSkyBlue = new(0xFF00BFFF); // DeepSkyBlue
            public static Color DimGray = new(0xFF696969); // DimGray
            public static Color DodgerBlue = new(0xFF1E90FF); // DodgerBlue
            public static Color Firebrick = new(0xFFB22222); // Firebrick
            public static Color FloralWhite = new(0xFFFFFAF0); // FloralWhite
            public static Color ForestGreen = new(0xFF228B22); // ForestGreen
            public static Color Fuchsia = new(0xFFFF00FF); // Fuchsia
            public static Color Gainsboro = new(0xFFDCDCDC); // Gainsboro
            public static Color GhostWhite = new(0xFFF8F8FF); // GhostWhite
            public static Color Gold = new(0xFFFFD700); // Gold
            public static Color Goldenrod = new(0xFFDAA520); // Goldenrod
            public static Color Gray = new(0xFF808080); // Gray
            public static Color Green = new(0xFF008000); // Green
            public static Color GreenYellow = new(0xFFADFF2F); // GreenYellow
            public static Color Honeydew = new(0xFFF0FFF0); // Honeydew
            public static Color HotPink = new(0xFFFF69B4); // HotPink
            public static Color IndianRed = new(0xFFCD5C5C); // IndianRed
            public static Color Indigo = new(0xFF4B0082); // Indigo
            public static Color Ivory = new(0xFFFFFFF0); // Ivory
            public static Color Khaki = new(0xFFF0E68C); // Khaki
            public static Color Lavender = new(0xFFE6E6FA); // Lavender
            public static Color LavenderBlush = new(0xFFFFF0F5); // LavenderBlush
            public static Color LawnGreen = new(0xFF7CFC00); // LawnGreen
            public static Color LemonChiffon = new(0xFFFFFACD); // LemonChiffon
            public static Color LightBlue = new(0xFFADD8E6); // LightBlue
            public static Color LightCoral = new(0xFFF08080); // LightCoral
            public static Color LightCyan = new(0xFFE0FFFF); // LightCyan
            public static Color LightGoldenrodYellow = new(0xFFFAFAD2); // LightGoldenrodYellow
            public static Color LightGray = new(0xFFD3D3D3); // LightGray
            public static Color LightGreen = new(0xFF90EE90); // LightGreen
            public static Color LightPink = new(0xFFFFB6C1); // LightPink
            public static Color LightSalmon = new(0xFFFFA07A); // LightSalmon
            public static Color LightSeaGreen = new(0xFF20B2AA); // LightSeaGreen
            public static Color LightSkyBlue = new(0xFF87CEFA); // LightSkyBlue
            public static Color LightSlateGray = new(0xFF778899); // LightSlateGray
            public static Color LightSteelBlue = new(0xFFB0C4DE); // LightSteelBlue
            public static Color LightYellow = new(0xFFFFFFE0); // LightYellow
            public static Color Lime = new(0xFF00FF00); // Lime
            public static Color LimeGreen = new(0xFF32CD32); // LimeGreen
            public static Color Linen = new(0xFFFAF0E6); // Linen
            public static Color Magenta = new(0xFFFF00FF); // Magenta
            public static Color Maroon = new(0xFF800000); // Maroon
            public static Color MediumAquamarine = new(0xFF66CDAA); // MediumAquamarine
            public static Color MediumBlue = new(0xFF0000CD); // MediumBlue
            public static Color MediumOrchid = new(0xFFBA55D3); // MediumOrchid
            public static Color MediumPurple = new(0xFF9370DB); // MediumPurple
            public static Color MediumSeaGreen = new(0xFF3CB371); // MediumSeaGreen
            public static Color MediumSlateBlue = new(0xFF7B68EE); // MediumSlateBlue
            public static Color MediumSpringGreen = new(0xFF00FA9A); // MediumSpringGreen
            public static Color MediumTurquoise = new(0xFF48D1CC); // MediumTurquoise
            public static Color MediumVioletRed = new(0xFFC71585); // MediumVioletRed
            public static Color MidnightBlue = new(0xFF191970); // MidnightBlue
            public static Color MintCream = new(0xFFF5FFFA); // MintCream
            public static Color MistyRose = new(0xFFFFE4E1); // MistyRose
            public static Color Moccasin = new(0xFFFFE4B5); // Moccasin
            public static Color NavajoWhite = new(0xFFFFDEAD); // NavajoWhite
            public static Color Navy = new(0xFF000080); // Navy
            public static Color OldLace = new(0xFFFDF5E6); // OldLace
            public static Color Olive = new(0xFF808000); // Olive
            public static Color OliveDrab = new(0xFF6B8E23); // OliveDrab
            public static Color Orange = new(0xFFFFA500); // Orange
            public static Color OrangeRed = new(0xFFFF4500); // OrangeRed
            public static Color Orchid = new(0xFFDA70D6); // Orchid
            public static Color PaleGoldenrod = new(0xFFEEE8AA); // PaleGoldenrod
            public static Color PaleGreen = new(0xFF98FB98); // PaleGreen
            public static Color PaleTurquoise = new(0xFFAFEEEE); // PaleTurquoise
            public static Color PaleVioletRed = new(0xFFDB7093); // PaleVioletRed
            public static Color PapayaWhip = new(0xFFFFEFD5); // PapayaWhip
            public static Color PeachPuff = new(0xFFFFDAB9); // PeachPuff
            public static Color Peru = new(0xFFCD853F); // Peru
            public static Color Pink = new(0xFFFFC0CB); // Pink
            public static Color Plum = new(0xFFDDA0DD); // Plum
            public static Color PowderBlue = new(0xFFB0E0E6); // PowderBlue
            public static Color Purple = new(0xFF800080); // Purple
            public static Color Red = new(0xFFFF0000); // Red
            public static Color RosyBrown = new(0xFFBC8F8F); // RosyBrown
            public static Color RoyalBlue = new(0xFF4169E1); // RoyalBlue
            public static Color SaddleBrown = new(0xFF8B4513); // SaddleBrown
            public static Color Salmon = new(0xFFFA8072); // Salmon
            public static Color SandyBrown = new(0xFFF4A460); // SandyBrown
            public static Color SeaGreen = new(0xFF2E8B57); // SeaGreen
            public static Color SeaShell = new(0xFFFFF5EE); // SeaShell
            public static Color Sienna = new(0xFFA0522D); // Sienna
            public static Color Silver = new(0xFFC0C0C0); // Silver
            public static Color SkyBlue = new(0xFF87CEEB); // SkyBlue
            public static Color SlateBlue = new(0xFF6A5ACD); // SlateBlue
            public static Color SlateGray = new(0xFF708090); // SlateGray
            public static Color Snow = new(0xFFFFFAFA); // Snow
            public static Color SpringGreen = new(0xFF00FF7F); // SpringGreen
            public static Color SteelBlue = new(0xFF4682B4); // SteelBlue
            public static Color Tan = new(0xFFD2B48C); // Tan
            public static Color Teal = new(0xFF008080); // Teal
            public static Color Thistle = new(0xFFD8BFD8); // Thistle
            public static Color Tomato = new(0xFFFF6347); // Tomato
            public static Color Turquoise = new(0xFF40E0D0); // Turquoise
            public static Color Violet = new(0xFFEE82EE); // Violet
            public static Color Wheat = new(0xFFF5DEB3); // Wheat
            public static Color White = new(0xFFFFFFFF); // White
            public static Color WhiteSmoke = new(0xFFF5F5F5); // WhiteSmoke
            public static Color Yellow = new(0xFFFFFF00); // Yellow
            public static Color YellowGreen = new(0xFF9ACD32); // YellowGreen
        #endregion
        
        internal const int ARGBAlphaShift = 24;
        internal const int ARGBRedShift = 16;
        internal const int ARGBGreenShift = 8;
        internal const int ARGBBlueShift = 0;

        public uint Value;

        public byte A;
        public byte R;
        public byte G;
        public byte B;

        public Color(uint code)
        {
            Value = code;
            A = unchecked((byte)(code >> ARGBAlphaShift));
            R = unchecked((byte)(code >> ARGBRedShift));
            G = unchecked((byte)(code >> ARGBGreenShift));
            B = unchecked((byte)(code >> ARGBBlueShift));
        }

        public Color(float a, float r, float g, float b) : this((byte)a, (byte)r, (byte)g, (byte)b) {}
        public Color(int a, int r, int g, int b) : this((byte)a, (byte)r, (byte)g, (byte)b) {}
        public Color(double a, double r, double g, double b) : this((byte)a, (byte)r, (byte)g, (byte)b) {}

        public Color(byte a, byte r, byte g, byte b) : this((uint)a << ARGBAlphaShift |
                                                            (uint)r << ARGBRedShift |
                                                            (uint)g << ARGBGreenShift |
                                                            (uint)b << ARGBBlueShift) {}

        public static Color From(System.Drawing.Color color)
        {
            return new Color(color.A, color.R, color.G, color.B);
        }
        
        public float GetBrightness()
        {
            int min = Mathf.Min(Mathf.Min(R, G), B);
            int max = Mathf.Max(Mathf.Max(R, G), B);

            return (max + min) / (byte.MaxValue * 2f);
        }

        public float GetHue()
        {
            if (R == G && G == B)
                return 0f;

            int min = Mathf.Min(Mathf.Min(R, G), B);
            int max = Mathf.Max(Mathf.Max(R, G), B);

            float delta = max - min;
            float hue;

            if (R == max)
                hue = (G - B) / delta;
            else if (G == max)
                hue = (B - R) / delta + 2f;
            else
                hue = (R - G) / delta + 4f;

            hue *= 60f;
            if (hue < 0f)
                hue += 360f;

            return hue;
        }
        
        public float GetSaturation()
        {
            if (R == G && G == B)
                return 0f;

            int min = Mathf.Min(Mathf.Min(R, G), B);
            int max = Mathf.Max(Mathf.Max(R, G), B);

            int div = max + min;
            if (div > byte.MaxValue)
                div = byte.MaxValue * 2 - max - min;

            return (max - min) / (float)div;
        }

        public static implicit operator Vector3(Color color)
        {
            return new Vector3(color.R/255, color.G/255, color.B/255);
        }
        
        public static implicit operator Vector4(Color color)
        {
            return new Vector4(color.R/255, color.G/255, color.B/255, color.A/255);
        }

        public static implicit operator System.Numerics.Vector4(Color color)
        {
            return new System.Numerics.Vector4(color.R, color.G, color.B, color.A);
        }
    }
}
