using System;
using System.Collections.Generic;

using FreeTypeSharp;
using FreeTypeSharp.Native;

using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Emission.IO
{
    public struct Font
    {
        public readonly IntPtr LibraryPtr;
        public readonly IntPtr FacePtr;

        public readonly FreeTypeLibrary Library;
        public readonly FreeTypeFaceFacade Face;

        public Dictionary<char, FontCharacter> Characters { get; }

        private string _path;
        private uint _charLength;
        private uint _size;

        public Font(string path, int size, int charLength = 128)
        {
            _path = path;
            _charLength = (uint)charLength;
            _size = (uint)size;
            Characters = new Dictionary<char, FontCharacter>();

            Library = new FreeTypeLibrary();
            LibraryPtr = Library.Native;
            
            FT.FT_New_Face(LibraryPtr, _path, 0, out var face);
            Face = new FreeTypeFaceFacade(Library, face);
            FacePtr = face;

            FT.FT_Set_Pixel_Sizes(FacePtr, 0, _size);

            if (FT.FT_Load_Char(FacePtr, 'X', FT.FT_LOAD_RENDER) != FT_Error.FT_Err_Ok)
            {
                Debug.LogError($"[ERROR] Cannot load glyph from '{path}'!");
            }
            
            GenerateGlyphs();

            FT.FT_Done_Face(FacePtr);
            FT.FT_Done_FreeType(LibraryPtr);
        }

        private void GenerateGlyphs()
        {
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);

            for (uint c = 0; c < _charLength; c++)
            {
                if (FT.FT_Load_Char(FacePtr, c, FT.FT_LOAD_RENDER) != FT_Error.FT_Err_Ok)
                {
                    Debug.LogError($"[ERROR] Cannot load glyph '{c}' from '{_path}'!");
                    continue;
                }

                int textureID = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, textureID);
                GL.TexImage2D(
                    TextureTarget.Texture2D,
                    0,
                    PixelInternalFormat.R8,
                    (int)Face.GlyphBitmap.width,
                    (int)Face.GlyphBitmap.rows,
                    0,
                    PixelFormat.Red,
                    PixelType.UnsignedByte,
                    Face.GlyphBitmap.buffer);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS,
                    (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT,
                    (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                    (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                    (int)TextureMagFilter.Linear);
                
                Characters.Add(Convert.ToChar(c), new FontCharacter()
                {
                    TextureID = textureID,
                    Size = new Vector2(Face.GlyphBitmap.width, Face.GlyphBitmap.rows),
                    Bearing = new Vector2(Face.GlyphBitmapLeft, Face.GlyphBitmapTop),
                    Advance = Face.GlyphMetricHorizontalAdvance
                });
            }
        }
    }
    
    public struct FontCharacter
    {
        public int TextureID;
        public Vector2 Size;
        public Vector2 Bearing;
        public int Advance;
    }
}
