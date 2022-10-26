using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Emission.Mathematics
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct ColorRgb : IEquatable<ColorRgb>
    {
        #region Statics Colors
            public static ColorRgb Transparent = new(0x00FFFFFF); // Transparent
            public static ColorRgb AliceBlue = new(0xFFF0F8FF); // AliceBlue
            public static ColorRgb AntiqueWhite = new(0xFFFAEBD7); // AntiqueWhite
            public static ColorRgb Aqua = new(0xFF00FFFF); // Aqua
            public static ColorRgb Aquamarine = new(0xFF7FFFD4); // Aquamarine
            public static ColorRgb Azure = new(0xFFF0FFFF); // Azure
            public static ColorRgb Beige = new(0xFFF5F5DC); // Beige
            public static ColorRgb Bisque = new(0xFFFFE4C4); // Bisque
            public static ColorRgb Black = new(0xFF000000); // Black
            public static ColorRgb BlanchedAlmond = new(0xFFFFEBCD); // BlanchedAlmond
            public static ColorRgb Blue = new(0xFF0000FF); // Blue
            public static ColorRgb BlueViolet = new(0xFF8A2BE2); // BlueViolet
            public static ColorRgb Brown = new(0xFFA52A2A); // Brown
            public static ColorRgb BurlyWood = new(0xFFDEB887); // BurlyWood
            public static ColorRgb CadetBlue = new(0xFF5F9EA0); // CadetBlue
            public static ColorRgb Chartreuse = new(0xFF7FFF00); // Chartreuse
            public static ColorRgb Chocolate = new(0xFFD2691E); // Chocolate
            public static ColorRgb Coral = new(0xFFFF7F50); // Coral
            public static ColorRgb CornflowerBlue = new(0xFF6495ED); // CornflowerBlue
            public static ColorRgb Cornsilk = new(0xFFFFF8DC); // Cornsilk
            public static ColorRgb Crimson = new(0xFFDC143C); // Crimson
            public static ColorRgb Cyan = new(0xFF00FFFF); // Cyan
            public static ColorRgb DarkBlue = new(0xFF00008B); // DarkBlue
            public static ColorRgb DarkCyan = new(0xFF008B8B); // DarkCyan
            public static ColorRgb DarkGoldenrod = new(0xFFB8860B); // DarkGoldenrod
            public static ColorRgb DarkGray = new(0xFFA9A9A9); // DarkGray
            public static ColorRgb DarkGreen = new(0xFF006400); // DarkGreen
            public static ColorRgb DarkKhaki = new(0xFFBDB76B); // DarkKhaki
            public static ColorRgb DarkMagenta = new(0xFF8B008B); // DarkMagenta
            public static ColorRgb DarkOliveGreen = new(0xFF556B2F); // DarkOliveGreen
            public static ColorRgb DarkOrange = new(0xFFFF8C00); // DarkOrange
            public static ColorRgb DarkOrchid = new(0xFF9932CC); // DarkOrchid
            public static ColorRgb DarkRed = new(0xFF8B0000); // DarkRed
            public static ColorRgb DarkSalmon = new(0xFFE9967A); // DarkSalmon
            public static ColorRgb DarkSeaGreen = new(0xFF8FBC8B); // DarkSeaGreen
            public static ColorRgb DarkSlateBlue = new(0xFF483D8B); // DarkSlateBlue
            public static ColorRgb DarkSlateGray = new(0xFF2F4F4F); // DarkSlateGray
            public static ColorRgb DarkTurquoise = new(0xFF00CED1); // DarkTurquoise
            public static ColorRgb DarkViolet = new(0xFF9400D3); // DarkViolet
            public static ColorRgb DeepPink = new(0xFFFF1493); // DeepPink
            public static ColorRgb DeepSkyBlue = new(0xFF00BFFF); // DeepSkyBlue
            public static ColorRgb DimGray = new(0xFF696969); // DimGray
            public static ColorRgb DodgerBlue = new(0xFF1E90FF); // DodgerBlue
            public static ColorRgb Firebrick = new(0xFFB22222); // Firebrick
            public static ColorRgb FloralWhite = new(0xFFFFFAF0); // FloralWhite
            public static ColorRgb ForestGreen = new(0xFF228B22); // ForestGreen
            public static ColorRgb Fuchsia = new(0xFFFF00FF); // Fuchsia
            public static ColorRgb Gainsboro = new(0xFFDCDCDC); // Gainsboro
            public static ColorRgb GhostWhite = new(0xFFF8F8FF); // GhostWhite
            public static ColorRgb Gold = new(0xFFFFD700); // Gold
            public static ColorRgb Goldenrod = new(0xFFDAA520); // Goldenrod
            public static ColorRgb Gray = new(0xFF808080); // Gray
            public static ColorRgb Green = new(0xFF008000); // Green
            public static ColorRgb GreenYellow = new(0xFFADFF2F); // GreenYellow
            public static ColorRgb Honeydew = new(0xFFF0FFF0); // Honeydew
            public static ColorRgb HotPink = new(0xFFFF69B4); // HotPink
            public static ColorRgb IndianRed = new(0xFFCD5C5C); // IndianRed
            public static ColorRgb Indigo = new(0xFF4B0082); // Indigo
            public static ColorRgb Ivory = new(0xFFFFFFF0); // Ivory
            public static ColorRgb Khaki = new(0xFFF0E68C); // Khaki
            public static ColorRgb Lavender = new(0xFFE6E6FA); // Lavender
            public static ColorRgb LavenderBlush = new(0xFFFFF0F5); // LavenderBlush
            public static ColorRgb LawnGreen = new(0xFF7CFC00); // LawnGreen
            public static ColorRgb LemonChiffon = new(0xFFFFFACD); // LemonChiffon
            public static ColorRgb LightBlue = new(0xFFADD8E6); // LightBlue
            public static ColorRgb LightCoral = new(0xFFF08080); // LightCoral
            public static ColorRgb LightCyan = new(0xFFE0FFFF); // LightCyan
            public static ColorRgb LightGoldenrodYellow = new(0xFFFAFAD2); // LightGoldenrodYellow
            public static ColorRgb LightGray = new(0xFFD3D3D3); // LightGray
            public static ColorRgb LightGreen = new(0xFF90EE90); // LightGreen
            public static ColorRgb LightPink = new(0xFFFFB6C1); // LightPink
            public static ColorRgb LightSalmon = new(0xFFFFA07A); // LightSalmon
            public static ColorRgb LightSeaGreen = new(0xFF20B2AA); // LightSeaGreen
            public static ColorRgb LightSkyBlue = new(0xFF87CEFA); // LightSkyBlue
            public static ColorRgb LightSlateGray = new(0xFF778899); // LightSlateGray
            public static ColorRgb LightSteelBlue = new(0xFFB0C4DE); // LightSteelBlue
            public static ColorRgb LightYellow = new(0xFFFFFFE0); // LightYellow
            public static ColorRgb Lime = new(0xFF00FF00); // Lime
            public static ColorRgb LimeGreen = new(0xFF32CD32); // LimeGreen
            public static ColorRgb Linen = new(0xFFFAF0E6); // Linen
            public static ColorRgb Magenta = new(0xFFFF00FF); // Magenta
            public static ColorRgb Maroon = new(0xFF800000); // Maroon
            public static ColorRgb MediumAquamarine = new(0xFF66CDAA); // MediumAquamarine
            public static ColorRgb MediumBlue = new(0xFF0000CD); // MediumBlue
            public static ColorRgb MediumOrchid = new(0xFFBA55D3); // MediumOrchid
            public static ColorRgb MediumPurple = new(0xFF9370DB); // MediumPurple
            public static ColorRgb MediumSeaGreen = new(0xFF3CB371); // MediumSeaGreen
            public static ColorRgb MediumSlateBlue = new(0xFF7B68EE); // MediumSlateBlue
            public static ColorRgb MediumSpringGreen = new(0xFF00FA9A); // MediumSpringGreen
            public static ColorRgb MediumTurquoise = new(0xFF48D1CC); // MediumTurquoise
            public static ColorRgb MediumVioletRed = new(0xFFC71585); // MediumVioletRed
            public static ColorRgb MidnightBlue = new(0xFF191970); // MidnightBlue
            public static ColorRgb MintCream = new(0xFFF5FFFA); // MintCream
            public static ColorRgb MistyRose = new(0xFFFFE4E1); // MistyRose
            public static ColorRgb Moccasin = new(0xFFFFE4B5); // Moccasin
            public static ColorRgb NavajoWhite = new(0xFFFFDEAD); // NavajoWhite
            public static ColorRgb Navy = new(0xFF000080); // Navy
            public static ColorRgb OldLace = new(0xFFFDF5E6); // OldLace
            public static ColorRgb Olive = new(0xFF808000); // Olive
            public static ColorRgb OliveDrab = new(0xFF6B8E23); // OliveDrab
            public static ColorRgb Orange = new(0xFFFFA500); // Orange
            public static ColorRgb OrangeRed = new(0xFFFF4500); // OrangeRed
            public static ColorRgb Orchid = new(0xFFDA70D6); // Orchid
            public static ColorRgb PaleGoldenrod = new(0xFFEEE8AA); // PaleGoldenrod
            public static ColorRgb PaleGreen = new(0xFF98FB98); // PaleGreen
            public static ColorRgb PaleTurquoise = new(0xFFAFEEEE); // PaleTurquoise
            public static ColorRgb PaleVioletRed = new(0xFFDB7093); // PaleVioletRed
            public static ColorRgb PapayaWhip = new(0xFFFFEFD5); // PapayaWhip
            public static ColorRgb PeachPuff = new(0xFFFFDAB9); // PeachPuff
            public static ColorRgb Peru = new(0xFFCD853F); // Peru
            public static ColorRgb Pink = new(0xFFFFC0CB); // Pink
            public static ColorRgb Plum = new(0xFFDDA0DD); // Plum
            public static ColorRgb PowderBlue = new(0xFFB0E0E6); // PowderBlue
            public static ColorRgb Purple = new(0xFF800080); // Purple
            public static ColorRgb Red = new(0xFFFF0000); // Red
            public static ColorRgb RosyBrown = new(0xFFBC8F8F); // RosyBrown
            public static ColorRgb RoyalBlue = new(0xFF4169E1); // RoyalBlue
            public static ColorRgb SaddleBrown = new(0xFF8B4513); // SaddleBrown
            public static ColorRgb Salmon = new(0xFFFA8072); // Salmon
            public static ColorRgb SandyBrown = new(0xFFF4A460); // SandyBrown
            public static ColorRgb SeaGreen = new(0xFF2E8B57); // SeaGreen
            public static ColorRgb SeaShell = new(0xFFFFF5EE); // SeaShell
            public static ColorRgb Sienna = new(0xFFA0522D); // Sienna
            public static ColorRgb Silver = new(0xFFC0C0C0); // Silver
            public static ColorRgb SkyBlue = new(0xFF87CEEB); // SkyBlue
            public static ColorRgb SlateBlue = new(0xFF6A5ACD); // SlateBlue
            public static ColorRgb SlateGray = new(0xFF708090); // SlateGray
            public static ColorRgb Snow = new(0xFFFFFAFA); // Snow
            public static ColorRgb SpringGreen = new(0xFF00FF7F); // SpringGreen
            public static ColorRgb SteelBlue = new(0xFF4682B4); // SteelBlue
            public static ColorRgb Tan = new(0xFFD2B48C); // Tan
            public static ColorRgb Teal = new(0xFF008080); // Teal
            public static ColorRgb Thistle = new(0xFFD8BFD8); // Thistle
            public static ColorRgb Tomato = new(0xFFFF6347); // Tomato
            public static ColorRgb Turquoise = new(0xFF40E0D0); // Turquoise
            public static ColorRgb Violet = new(0xFFEE82EE); // Violet
            public static ColorRgb Wheat = new(0xFFF5DEB3); // Wheat
            public static ColorRgb White = new(0xFFFFFFFF); // White
            public static ColorRgb WhiteSmoke = new(0xFFF5F5F5); // WhiteSmoke
            public static ColorRgb Yellow = new(0xFFFFFF00); // Yellow
            public static ColorRgb YellowGreen = new(0xFF9ACD32); // YellowGreen
        #endregion
        
        internal const int ARGBRedShift = 16;
        internal const int ARGBGreenShift = 8;
        internal const int ARGBBlueShift = 0;

        public uint Value;

        [DataMember] public byte R;
        [DataMember] public byte G;
        [DataMember] public byte B;

        public ColorRgb(uint code)
        {
            Value = code;
            R = unchecked((byte)(code >> ARGBRedShift));
            G = unchecked((byte)(code >> ARGBGreenShift));
            B = unchecked((byte)(code >> ARGBBlueShift));
        }

        public ColorRgb(float r, float g, float b) : this((byte)r, (byte)g, (byte)b) {}
        public ColorRgb(int r, int g, int b) : this((byte)r, (byte)g, (byte)b) {}
        public ColorRgb(double r, double g, double b) : this((byte)r, (byte)g, (byte)b) {}

        public ColorRgb(byte r, byte g, byte b) : this((uint)r << ARGBRedShift |
                                                       (uint)g << ARGBGreenShift |
                                                       (uint)b << ARGBBlueShift) {}

        public static ColorRgb From(System.Drawing.Color color)
        {
            return new ColorRgb(color.R, color.G, color.B);
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

        public static implicit operator ColorRgb(ColorArgb color) => new (color.R, color.G, color.B);

        public bool Equals(ColorRgb other)
        {
            return R == other.R && G == other.G && B == other.B;
        }

        public override bool Equals(object obj)
        {
            return obj is ColorRgb other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(R, G, B);
        }
    }
}
