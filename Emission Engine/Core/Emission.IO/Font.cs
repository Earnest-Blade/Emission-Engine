using System;
using System.Collections.Generic;

using FreeTypeSharp;
using FreeTypeSharp.Native;
using static FreeTypeSharp.Native.FT;

using Emission.UI;
using Emission.Mathematics;
using static Emission.Natives.GL.Gl;
using Emission.Graphics;
using Assimp;
using System.Runtime.InteropServices;

namespace Emission.IO
{
    public unsafe class Font
    {
        public const uint CHAR_LENGTH = 128;

        public readonly FreeTypeLibrary Library;
        public FT_FaceRec Face;
        public FT_GlyphSlotRec Glyph;

        public readonly int FontSize;

        public Dictionary<char, Character> Characters;

        private IntPtr _librairyHandle;
        private IntPtr _faceHandle;

        private FT_Bitmap _bitmap;

        private uint _charLenght;
        private string _path;

        public Font(string path, int size) : this(path, size, CHAR_LENGTH){}
        public Font(string path, int size, uint charLenght)
        {
            _charLenght = charLenght;
            _path = path;
            FontSize = size;
            Characters = new Dictionary<char, Character>();

            Library = new FreeTypeLibrary();
            _librairyHandle = Library.Native;

            FT_New_Face(_librairyHandle, path, 0, out IntPtr face);
            _faceHandle = face;

            Face = Marshal.PtrToStructure<FT_FaceRec>(face);
            Glyph = *this.Face.glyph;

            _bitmap = (*Face.glyph).bitmap;

            FT_Set_Pixel_Sizes(_faceHandle, 0, (uint)FontSize);

            if (FT_Load_Char(_faceHandle, 'X', FT_LOAD_RENDER) != FT_Error.FT_Err_Ok)
                throw new EmissionException(EmissionErrors.EmissionIOException, $"Cannot load glyph from '{path}'!");

            InitializeGlyphs();

            FT_Done_Face(_faceHandle);
            FT_Done_FreeType(_librairyHandle);
        }

        private void InitializeGlyphs()
        {
            glPixelStorei(GL_UNPACK_ALIGNMENT, 1);
            
            for(uint c = 0; c < _charLenght; c++)
            {
                if(FT_Load_Char(_faceHandle, c, FT_LOAD_RENDER) != FT_Error.FT_Err_Ok)
                {
                    Debug.Warning($"[WARNING] Cannot load glyph '{c}' from '{_path}'!");
                    continue;
                }

                uint texID;
                glGenTextures(1, &texID);
                glBindTexture(GL_TEXTURE_2D, texID);

                //if (Glyph.bitmap.buffer == IntPtr.Zero)
                //    throw new EmissionException(Errors.EmissionIOException, $"Cannot load bitmap from font '{_path}'!");

                glTexImage2D(GL_TEXTURE_2D, 0, GL_R8, (int)Glyph.bitmap.width, (int)Glyph.bitmap.rows, 0, GL_RED, GL_UNSIGNED_BYTE, Glyph.bitmap.buffer.ToPointer());

                // Define attributes
                glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
                glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);

                glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
                glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

                glActiveTexture(GL_TEXTURE_2D);

                Characters.Add(Convert.ToChar(c), new Character()
                {
                    Char = Convert.ToChar(c),
                    TextureID = texID,
                    Size = (Glyph.bitmap.width, Glyph.bitmap.rows),
                    Bearing = (Glyph.bitmap_left, Glyph.bitmap_top),
                    Advance = (int)Glyph.advance.x
                });
            }
        }
        
    }
}
