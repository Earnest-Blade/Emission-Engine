using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Emission.Mathematics.Numerics
{
    [Serializable]
    [DataContract]
    [StructLayout(LayoutKind.Sequential, Size = 4)]
    public struct ColorArgb : IEquatable<ColorArgb>
    {
        #region Statics Colors
            public static ColorArgb Transparent = new(0x00FFFFFF); // Transparent
            public static ColorArgb AliceBlue = new(0xFFF0F8FF); // AliceBlue
            public static ColorArgb AntiqueWhite = new(0xFFFAEBD7); // AntiqueWhite
            public static ColorArgb Aqua = new(0xFF00FFFF); // Aqua
            public static ColorArgb Aquamarine = new(0xFF7FFFD4); // Aquamarine
            public static ColorArgb Azure = new(0xFFF0FFFF); // Azure
            public static ColorArgb Beige = new(0xFFF5F5DC); // Beige
            public static ColorArgb Bisque = new(0xFFFFE4C4); // Bisque
            public static ColorArgb Black = new(0xFF000000); // Black
            public static ColorArgb BlanchedAlmond = new(0xFFFFEBCD); // BlanchedAlmond
            public static ColorArgb Blue = new(0xFF0000FF); // Blue
            public static ColorArgb BlueViolet = new(0xFF8A2BE2); // BlueViolet
            public static ColorArgb Brown = new(0xFFA52A2A); // Brown
            public static ColorArgb BurlyWood = new(0xFFDEB887); // BurlyWood
            public static ColorArgb CadetBlue = new(0xFF5F9EA0); // CadetBlue
            public static ColorArgb Chartreuse = new(0xFF7FFF00); // Chartreuse
            public static ColorArgb Chocolate = new(0xFFD2691E); // Chocolate
            public static ColorArgb Coral = new(0xFFFF7F50); // Coral
            public static ColorArgb CornflowerBlue = new(0xFF6495ED); // CornflowerBlue
            public static ColorArgb Cornsilk = new(0xFFFFF8DC); // Cornsilk
            public static ColorArgb Crimson = new(0xFFDC143C); // Crimson
            public static ColorArgb Cyan = new(0xFF00FFFF); // Cyan
            public static ColorArgb DarkBlue = new(0xFF00008B); // DarkBlue
            public static ColorArgb DarkCyan = new(0xFF008B8B); // DarkCyan
            public static ColorArgb DarkGoldenrod = new(0xFFB8860B); // DarkGoldenrod
            public static ColorArgb DarkGray = new(0xFFA9A9A9); // DarkGray
            public static ColorArgb DarkGreen = new(0xFF006400); // DarkGreen
            public static ColorArgb DarkKhaki = new(0xFFBDB76B); // DarkKhaki
            public static ColorArgb DarkMagenta = new(0xFF8B008B); // DarkMagenta
            public static ColorArgb DarkOliveGreen = new(0xFF556B2F); // DarkOliveGreen
            public static ColorArgb DarkOrange = new(0xFFFF8C00); // DarkOrange
            public static ColorArgb DarkOrchid = new(0xFF9932CC); // DarkOrchid
            public static ColorArgb DarkRed = new(0xFF8B0000); // DarkRed
            public static ColorArgb DarkSalmon = new(0xFFE9967A); // DarkSalmon
            public static ColorArgb DarkSeaGreen = new(0xFF8FBC8B); // DarkSeaGreen
            public static ColorArgb DarkSlateBlue = new(0xFF483D8B); // DarkSlateBlue
            public static ColorArgb DarkSlateGray = new(0xFF2F4F4F); // DarkSlateGray
            public static ColorArgb DarkTurquoise = new(0xFF00CED1); // DarkTurquoise
            public static ColorArgb DarkViolet = new(0xFF9400D3); // DarkViolet
            public static ColorArgb DeepPink = new(0xFFFF1493); // DeepPink
            public static ColorArgb DeepSkyBlue = new(0xFF00BFFF); // DeepSkyBlue
            public static ColorArgb DimGray = new(0xFF696969); // DimGray
            public static ColorArgb DodgerBlue = new(0xFF1E90FF); // DodgerBlue
            public static ColorArgb Firebrick = new(0xFFB22222); // Firebrick
            public static ColorArgb FloralWhite = new(0xFFFFFAF0); // FloralWhite
            public static ColorArgb ForestGreen = new(0xFF228B22); // ForestGreen
            public static ColorArgb Fuchsia = new(0xFFFF00FF); // Fuchsia
            public static ColorArgb Gainsboro = new(0xFFDCDCDC); // Gainsboro
            public static ColorArgb GhostWhite = new(0xFFF8F8FF); // GhostWhite
            public static ColorArgb Gold = new(0xFFFFD700); // Gold
            public static ColorArgb Goldenrod = new(0xFFDAA520); // Goldenrod
            public static ColorArgb Gray = new(0xFF808080); // Gray
            public static ColorArgb Green = new(0xFF008000); // Green
            public static ColorArgb GreenYellow = new(0xFFADFF2F); // GreenYellow
            public static ColorArgb Honeydew = new(0xFFF0FFF0); // Honeydew
            public static ColorArgb HotPink = new(0xFFFF69B4); // HotPink
            public static ColorArgb IndianRed = new(0xFFCD5C5C); // IndianRed
            public static ColorArgb Indigo = new(0xFF4B0082); // Indigo
            public static ColorArgb Ivory = new(0xFFFFFFF0); // Ivory
            public static ColorArgb Khaki = new(0xFFF0E68C); // Khaki
            public static ColorArgb Lavender = new(0xFFE6E6FA); // Lavender
            public static ColorArgb LavenderBlush = new(0xFFFFF0F5); // LavenderBlush
            public static ColorArgb LawnGreen = new(0xFF7CFC00); // LawnGreen
            public static ColorArgb LemonChiffon = new(0xFFFFFACD); // LemonChiffon
            public static ColorArgb LightBlue = new(0xFFADD8E6); // LightBlue
            public static ColorArgb LightCoral = new(0xFFF08080); // LightCoral
            public static ColorArgb LightCyan = new(0xFFE0FFFF); // LightCyan
            public static ColorArgb LightGoldenrodYellow = new(0xFFFAFAD2); // LightGoldenrodYellow
            public static ColorArgb LightGray = new(0xFFD3D3D3); // LightGray
            public static ColorArgb LightGreen = new(0xFF90EE90); // LightGreen
            public static ColorArgb LightPink = new(0xFFFFB6C1); // LightPink
            public static ColorArgb LightSalmon = new(0xFFFFA07A); // LightSalmon
            public static ColorArgb LightSeaGreen = new(0xFF20B2AA); // LightSeaGreen
            public static ColorArgb LightSkyBlue = new(0xFF87CEFA); // LightSkyBlue
            public static ColorArgb LightSlateGray = new(0xFF778899); // LightSlateGray
            public static ColorArgb LightSteelBlue = new(0xFFB0C4DE); // LightSteelBlue
            public static ColorArgb LightYellow = new(0xFFFFFFE0); // LightYellow
            public static ColorArgb Lime = new(0xFF00FF00); // Lime
            public static ColorArgb LimeGreen = new(0xFF32CD32); // LimeGreen
            public static ColorArgb Linen = new(0xFFFAF0E6); // Linen
            public static ColorArgb Magenta = new(0xFFFF00FF); // Magenta
            public static ColorArgb Maroon = new(0xFF800000); // Maroon
            public static ColorArgb MediumAquamarine = new(0xFF66CDAA); // MediumAquamarine
            public static ColorArgb MediumBlue = new(0xFF0000CD); // MediumBlue
            public static ColorArgb MediumOrchid = new(0xFFBA55D3); // MediumOrchid
            public static ColorArgb MediumPurple = new(0xFF9370DB); // MediumPurple
            public static ColorArgb MediumSeaGreen = new(0xFF3CB371); // MediumSeaGreen
            public static ColorArgb MediumSlateBlue = new(0xFF7B68EE); // MediumSlateBlue
            public static ColorArgb MediumSpringGreen = new(0xFF00FA9A); // MediumSpringGreen
            public static ColorArgb MediumTurquoise = new(0xFF48D1CC); // MediumTurquoise
            public static ColorArgb MediumVioletRed = new(0xFFC71585); // MediumVioletRed
            public static ColorArgb MidnightBlue = new(0xFF191970); // MidnightBlue
            public static ColorArgb MintCream = new(0xFFF5FFFA); // MintCream
            public static ColorArgb MistyRose = new(0xFFFFE4E1); // MistyRose
            public static ColorArgb Moccasin = new(0xFFFFE4B5); // Moccasin
            public static ColorArgb NavajoWhite = new(0xFFFFDEAD); // NavajoWhite
            public static ColorArgb Navy = new(0xFF000080); // Navy
            public static ColorArgb OldLace = new(0xFFFDF5E6); // OldLace
            public static ColorArgb Olive = new(0xFF808000); // Olive
            public static ColorArgb OliveDrab = new(0xFF6B8E23); // OliveDrab
            public static ColorArgb Orange = new(0xFFFFA500); // Orange
            public static ColorArgb OrangeRed = new(0xFFFF4500); // OrangeRed
            public static ColorArgb Orchid = new(0xFFDA70D6); // Orchid
            public static ColorArgb PaleGoldenrod = new(0xFFEEE8AA); // PaleGoldenrod
            public static ColorArgb PaleGreen = new(0xFF98FB98); // PaleGreen
            public static ColorArgb PaleTurquoise = new(0xFFAFEEEE); // PaleTurquoise
            public static ColorArgb PaleVioletRed = new(0xFFDB7093); // PaleVioletRed
            public static ColorArgb PapayaWhip = new(0xFFFFEFD5); // PapayaWhip
            public static ColorArgb PeachPuff = new(0xFFFFDAB9); // PeachPuff
            public static ColorArgb Peru = new(0xFFCD853F); // Peru
            public static ColorArgb Pink = new(0xFFFFC0CB); // Pink
            public static ColorArgb Plum = new(0xFFDDA0DD); // Plum
            public static ColorArgb PowderBlue = new(0xFFB0E0E6); // PowderBlue
            public static ColorArgb Purple = new(0xFF800080); // Purple
            public static ColorArgb Red = new(0xFFFF0000); // Red
            public static ColorArgb RosyBrown = new(0xFFBC8F8F); // RosyBrown
            public static ColorArgb RoyalBlue = new(0xFF4169E1); // RoyalBlue
            public static ColorArgb SaddleBrown = new(0xFF8B4513); // SaddleBrown
            public static ColorArgb Salmon = new(0xFFFA8072); // Salmon
            public static ColorArgb SandyBrown = new(0xFFF4A460); // SandyBrown
            public static ColorArgb SeaGreen = new(0xFF2E8B57); // SeaGreen
            public static ColorArgb SeaShell = new(0xFFFFF5EE); // SeaShell
            public static ColorArgb Sienna = new(0xFFA0522D); // Sienna
            public static ColorArgb Silver = new(0xFFC0C0C0); // Silver
            public static ColorArgb SkyBlue = new(0xFF87CEEB); // SkyBlue
            public static ColorArgb SlateBlue = new(0xFF6A5ACD); // SlateBlue
            public static ColorArgb SlateGray = new(0xFF708090); // SlateGray
            public static ColorArgb Snow = new(0xFFFFFAFA); // Snow
            public static ColorArgb SpringGreen = new(0xFF00FF7F); // SpringGreen
            public static ColorArgb SteelBlue = new(0xFF4682B4); // SteelBlue
            public static ColorArgb Tan = new(0xFFD2B48C); // Tan
            public static ColorArgb Teal = new(0xFF008080); // Teal
            public static ColorArgb Thistle = new(0xFFD8BFD8); // Thistle
            public static ColorArgb Tomato = new(0xFFFF6347); // Tomato
            public static ColorArgb Turquoise = new(0xFF40E0D0); // Turquoise
            public static ColorArgb Violet = new(0xFFEE82EE); // Violet
            public static ColorArgb Wheat = new(0xFFF5DEB3); // Wheat
            public static ColorArgb White = new(0xFFFFFFFF); // White
            public static ColorArgb WhiteSmoke = new(0xFFF5F5F5); // WhiteSmoke
            public static ColorArgb Yellow = new(0xFFFFFF00); // Yellow
            public static ColorArgb YellowGreen = new(0xFF9ACD32); // YellowGreen
        #endregion
        
        internal const int ARGBAlphaShift = 24;
        internal const int ARGBRedShift = 16;
        internal const int ARGBGreenShift = 8;
        internal const int ARGBBlueShift = 0;

        public uint Value;

        [DataMember] public byte A;
        [DataMember] public byte R;
        [DataMember] public byte G;
        [DataMember] public byte B;

        public ColorArgb(uint code)
        {
            Value = code;
            A = unchecked((byte)(code >> ARGBAlphaShift));
            R = unchecked((byte)(code >> ARGBRedShift));
            G = unchecked((byte)(code >> ARGBGreenShift));
            B = unchecked((byte)(code >> ARGBBlueShift));
        }

        public ColorArgb(float a, float r, float g, float b) : this((byte)a, (byte)r, (byte)g, (byte)b) {}
        public ColorArgb(int a, int r, int g, int b) : this((byte)a, (byte)r, (byte)g, (byte)b) {}
        public ColorArgb(double a, double r, double g, double b) : this((byte)a, (byte)r, (byte)g, (byte)b) {}

        public ColorArgb(byte a, byte r, byte g, byte b) : this((uint)a << ARGBAlphaShift |
                                                            (uint)r << ARGBRedShift |
                                                            (uint)g << ARGBGreenShift |
                                                            (uint)b << ARGBBlueShift) {}

        public static ColorArgb From(System.Drawing.Color color)
        {
            return new ColorArgb(color.A, color.R, color.G, color.B);
        }
        
        public float GetBrightness()
        {
            int min = Math.Min(Math.Min(R, G), B);
            int max = Math.Max(Math.Max(R, G), B);

            return (max + min) / (byte.MaxValue * 2f);
        }

        public float GetHue()
        {
            if (R == G && G == B)
                return 0f;

            int min = Math.Min(Math.Min(R, G), B);
            int max = Math.Max(Math.Max(R, G), B);

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

            int min = Math.Min(Math.Min(R, G), B);
            int max = Math.Max(Math.Max(R, G), B);

            int div = max + min;
            if (div > byte.MaxValue)
                div = byte.MaxValue * 2 - max - min;

            return (max - min) / (float)div;
        }
        
        public static implicit operator ColorArgb(ColorRgb color) => new ((byte)255, color.R, color.G, color.B);

        public bool Equals(ColorArgb other)
        {
            return A == other.A && R == other.R && G == other.G && B == other.B;
        }

        public override bool Equals(object obj)
        {
            return obj is ColorArgb other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(A, R, G, B);
        }
    }
}
