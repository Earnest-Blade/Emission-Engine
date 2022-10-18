﻿using System;

using Emission.IO;
using static Emission.Graphics.GL;
using Emission.Mathematics.Numerics;

namespace Emission.Shading
{
    public class Texture : IDisposable
    {
        public string Path { get => _path; }
        public string Name { get => _name; }
        public uint ID { get => _texId; }
        
        public Sprite Sprite { get; }

        public TextureUnit TextureUnit
        {
            get => (TextureUnit)_texUnit;
            set
            {
                _texUnit = (int)value;
            }
        }

        public Vector2 Size => Sprite.Size;

        private string _path;
        private string _name;
        private uint _texId;
        private int _texUnit;

        public Texture(string name, Sprite sprite, TextureUnit unit = TextureUnit.Texture0) : this(sprite.Path, name, sprite, unit){}

        public Texture(string path, string name, Sprite data, TextureUnit unit = TextureUnit.Texture0)
        {
            Sprite = data;
            _path = path;
            _name = name;
            _texUnit = (int)unit;
        }

        public Texture(string path, string name, TextureUnit unit = TextureUnit.Texture0)
        {
            _path = path;
            _name = name;
            _texUnit = (int)unit;
            Sprite = new Sprite(path);
            
        }

        public void Bind()
        {
            _texId = Renderer.BindTexture2D(Sprite.Bytes, Sprite.Width, Sprite.Height);
        }
        
        public void Use()
        {
            glActiveTexture(_texUnit);
            glBindTexture(GL_TEXTURE_2D, _texId);
        }

        public void Dispose()
        {
            glDeleteTexture(_texId);
        }
        
        public void TextureWrapping(int wrap)
        {
            glActiveTexture(_texUnit);
            glBindTexture(GL_TEXTURE_2D, _texId);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, wrap);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, wrap);
            glActiveTexture(_texUnit);
            glBindTexture(GL_TEXTURE_2D, 0);
        }

        public void TextureBorderColor(Vector4 color)
        {
            glActiveTexture(_texUnit);
            glBindTexture(GL_TEXTURE_2D, _texId);
            glTexParameterfv(GL_TEXTURE_2D, GL_TEXTURE_BORDER_COLOR, new [] { color.X, color.Y, color.Z, color.W });
            glActiveTexture(_texUnit);
            glBindTexture(GL_TEXTURE_2D, 0);
        }

        public void TextureFilter(int minFilter, int magFilter)
        {
            glActiveTexture(_texUnit);
            glBindTexture(GL_TEXTURE_2D, _texId);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, minFilter);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, magFilter);
            glActiveTexture(_texUnit);
            glBindTexture(GL_TEXTURE_2D, 0);
        }
    }
}