using System;
using Assimp;
using Emission.Assets;
using Emission.Core;
using Emission.Core.IO;
using Emission.Core.Mathematics;
using Emission.Core.Memory;
using static Emission.Natives.GL.Gl;

namespace Emission.Graphics
{
    public unsafe class Texture : IEquatable<Texture>, IDisposable
    {
        public string? Name;
        
        public Vector2 Dimension => new Vector2(_width, _height);
        public byte[] Bytes => _bytes;

        public TextureUnit TextureUnit
        {
            get => (TextureUnit)_texUnit;
            internal set => _texUnit = (uint)value;
        }
        
        public uint TextureId => _texId;
        
        public readonly ColorFormat ColorFormat;
        public TextureType TextureType;

        public TextureFilterMode FilterMode
        {
            get => (TextureFilterMode)_filter;
            set
            {
                TextureFilter((int)value);
                _filter = (int)value;
            }
        }

        public TextureWrapMode WrapMode
        {
            set
            {
                _wrapModeS = (int)value;
                _wrapModeT = (int)value;
                _wrapModeR = (int)value;
                TextureWrap((int)value, (int)value, (int)value);
            }
        }

        public TextureWrapMode WrapModeS
        {
            get => (TextureWrapMode)_wrapModeS;
            set
            {
                _wrapModeS = (int)value;
                TextureWrap((int)value, _wrapModeT, _wrapModeR);
            }
        }

        public TextureWrapMode WrapModeT
        {
            get => (TextureWrapMode)_wrapModeT;
            set
            {
                _wrapModeS = (int)value;
                TextureWrap(_wrapModeS, (int)value, _wrapModeR);
            }
        }
        
        public TextureWrapMode WrapModeR
        {
            get => (TextureWrapMode)_wrapModeR;
            set
            {
                _wrapModeS = (int)value;
                TextureWrap(_wrapModeS, _wrapModeT, (int)value);
            }
        }
        
        private readonly byte[] _bytes;
        private readonly int _width;
        private readonly int _height;

        private uint _texId;
        private uint _texUnit;
        private int _filter;
        private int _wrapModeS;
        private int _wrapModeT;
        private int _wrapModeR;

        public Texture(string? name, byte[] bytes, int width, int height, ColorFormat colorFormat, TextureUnit textureUnit) : this(name, bytes, width, height, colorFormat, textureUnit, TextureFilterMode.Nearest, TextureWrapMode.Repeat) {}
        public Texture(string? name, byte[] bytes, int width, int height, ColorFormat colorFormat, TextureUnit textureUnit, TextureFilterMode filter, TextureWrapMode wrapMode) : this(name, bytes, width, height, colorFormat, textureUnit, filter, wrapMode, TextureType.Unknown) {}
        
        public Texture(string? name, byte[] bytes, int width, int height, ColorFormat colorFormat, TextureUnit textureUnit, TextureFilterMode filter, TextureWrapMode wrapMode, TextureType type)
        {
            Name = name;
            ColorFormat = colorFormat;
            TextureType = type;
            
            _bytes = bytes;
            _width = width;
            _height = height;
            _texUnit = (uint)textureUnit;

            _texId = Renderer.BindTexture2D(_bytes, width, height);

            FilterMode = filter;
            WrapMode = wrapMode;
        }

        public void Use()
        {
            glActiveTexture(_texUnit);
            glBindTexture(GL_TEXTURE_2D, _texId);
        }

        public void Close()
        {
            glActiveTexture(_texUnit);
            glBindTexture(GL_TEXTURE_2D, 0);
        }
        
        private void TextureWrap(int wrapS, int wrapT, int wrapR)
        {
            Use();
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, wrapS);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, wrapT);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_R, wrapR);
            Close();
        }

        private void TextureFilter(int filter)
        {
            Use();
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, filter);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, filter);
            Close();
        }
        
        public void Dispose()
        {
            uint texId = _texId;
            glDeleteTextures(1, &texId);

            Renderer.RemoveTextureId(texId);
            _texId = 0;
        }

        public bool Equals(Texture? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _bytes.Equals(other._bytes) && _width == other._width && _height == other._height && _texId == other._texId;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Texture)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_bytes, _width, _height, _texId);
        }

        public override string ToString()
        {
            return TextureUnit.ToString();
        }

        public static Texture CreateTextureRGBFromPath(string? path, string? name) => CreateTextureFromPath(path, name, TextureUnit.Texture0, ColorFormat.RedGreenBlue, TextureFilterMode.Linear, TextureWrapMode.Repeat);
        public static Texture CreateTextureARGBFromPath(string? path, string? name) => CreateTextureFromPath(path, name, TextureUnit.Texture0, ColorFormat.RedGreenBlueAlpha, TextureFilterMode.Linear, TextureWrapMode.Repeat);
        public static Texture CreateTextureRGBFromPath(string? path, string? name, TextureUnit unit) => CreateTextureFromPath(path, name, unit, ColorFormat.RedGreenBlue, TextureFilterMode.Linear, TextureWrapMode.Repeat);
        public static Texture CreateTextureARGBFromPath(string? path, string? name, TextureUnit unit) => CreateTextureFromPath(path, name, unit, ColorFormat.RedGreenBlueAlpha, TextureFilterMode.Linear, TextureWrapMode.Repeat);

        public static Texture CreateTextureFromPath(string? path, string? name, TextureUnit unit, ColorFormat colorFormat, TextureFilterMode filterMode, TextureWrapMode wrapMode)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            
            if (!EFile.Exists(path))
                throw new FileNotFoundException(path);

            int width, height;
            byte[] buffer = Image.LoadImageFromPath(path, &width, &height, (int*)0, colorFormat);
            if (buffer == null)
            {
                throw new FatalEmissionException(EmissionException.ERR_IO, $"An error occured while loading '{path}'.");
            }

            return new Texture(name, buffer, width, height, colorFormat, unit, filterMode, wrapMode);
        }
        
        public static Texture CreateTextureRGBFromMemory(MemoryStream? stream, string? name) => CreateTextureFromStream(stream, name, TextureUnit.Texture0, ColorFormat.RedGreenBlue, TextureFilterMode.Linear, TextureWrapMode.Repeat);
        public static Texture CreateTextureARGBFromMemory(MemoryStream? stream, string? name) => CreateTextureFromStream(stream, name, TextureUnit.Texture0, ColorFormat.RedGreenBlueAlpha, TextureFilterMode.Linear, TextureWrapMode.Repeat);
        public static Texture CreateTextureRGBFromMemory(MemoryStream? stream, string? name, TextureUnit unit) => CreateTextureFromStream(stream, name, unit, ColorFormat.RedGreenBlue, TextureFilterMode.Linear, TextureWrapMode.Repeat);
        public static Texture CreateTextureARGBFromMemory(MemoryStream? stream, string? name,  TextureUnit unit) => CreateTextureFromStream(stream, name, unit, ColorFormat.RedGreenBlueAlpha, TextureFilterMode.Linear, TextureWrapMode.Repeat);
        public static Texture CreateTextureFromMemory(MemoryStream? stream, string? name, TextureUnit unit, ColorFormat colorFormat, TextureFilterMode filterMode, TextureWrapMode wrapMode) => CreateTextureFromStream(stream, name, unit, colorFormat, filterMode, wrapMode);
        
        public static Texture CreateTextureRGBFromStream(Stream? stream, string? name) => CreateTextureFromStream(stream, name, TextureUnit.Texture0, ColorFormat.RedGreenBlue, TextureFilterMode.Linear, TextureWrapMode.Repeat);
        public static Texture CreateTextureARGBFromStream(Stream? stream, string? name) => CreateTextureFromStream(stream, name, TextureUnit.Texture0, ColorFormat.RedGreenBlueAlpha, TextureFilterMode.Linear, TextureWrapMode.Repeat);
        public static Texture CreateTextureRGBFromStream(Stream? stream, string? name, TextureUnit unit) => CreateTextureFromStream(stream, name, unit, ColorFormat.RedGreenBlue, TextureFilterMode.Linear, TextureWrapMode.Repeat);
        public static Texture CreateTextureARGBFromStream(Stream? stream, string? name, TextureUnit unit) => CreateTextureFromStream(stream, name, unit, ColorFormat.RedGreenBlueAlpha, TextureFilterMode.Linear, TextureWrapMode.Repeat);
        public static Texture CreateTextureFromStream(Stream? stream, string? name, TextureUnit unit, ColorFormat colorFormat, TextureFilterMode filterMode, TextureWrapMode wrapMode)
        {
            int width, height;
            byte[] buffer = Image.LoadImageFromMemory(Memory.ReadStream(stream, (int)stream.Length), &width, &height, (int*)0, colorFormat);
            if (buffer == null)
            {
                throw new FatalEmissionException(EmissionException.ERR_IO, $"An error occured while loading '{name}'.");
            }
            
            return new Texture(name, buffer, width, height, colorFormat, unit, filterMode, wrapMode);
        }

        internal static Texture CreateTextureFromTextureSlot(string? name, string assetPath, TextureSlot slot, TextureUnit unit)
        {
            int width, height, comp;
            byte[] buffer = Image.LoadImageFromPath(Path.Combine(assetPath, slot.FilePath), &width, &height, &comp, ColorFormat.Default);
            if (buffer == null)
            {
                throw new FatalEmissionException(EmissionException.ERR_IO, $"An error occured while loading '{name}'.");
            }
            
            return new Texture(name, buffer, width, height, ColorFormat.Default, unit, TextureFilterMode.Linear, TextureWrapMode.Repeat, (TextureType)(int)slot.TextureType);
        }
    }
}