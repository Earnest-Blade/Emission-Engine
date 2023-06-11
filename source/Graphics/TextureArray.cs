using System.Collections;
using System.Runtime.CompilerServices;
using Emission.Core;

namespace Emission.Graphics
{
    [Serializable]
    public class TextureArray : IEquatable<TextureArray>, IList<Texture>, IEnumerator<Texture>
    {
        private const int MaxTextureArrayLength = 32;

        public int Count { get => _textures.Length; }
        public bool IsReadOnly => _textures.IsReadOnly;

        public Texture this[int index]
        {
            get
            {
                if (index < 0 || index > _textures.Length)
                    throw new ArgumentOutOfRangeException(nameof(index));

                return _textures[index];
            }
            set
            {
                if (index < 0 || index > _textures.Length)
                    throw new ArgumentOutOfRangeException(nameof(index));

                _textures[index] = value;
            }
        }

        public Texture Current => _textures[_enumeratorIndex];
        object IEnumerator.Current => Current;

        private Texture[] _textures;
        private int _enumeratorIndex;

        public TextureArray() : this(Array.Empty<Texture>()) {}

        public TextureArray(Texture texture)
        {
            texture.TextureUnit = TextureUnit.Texture0;
            _textures = new[] { texture };
            _enumeratorIndex = -1;
        }
        
        public TextureArray(Texture[] textures)
        {
            _textures = textures;
            for (int i = 0; i < textures.Length; i++) SetTextureUnit(i);

            _enumeratorIndex = -1;
        }

        public void Add(Texture item)
        {
            if (_textures.Length + 1 <= MaxTextureArrayLength)
            {
                Array.Resize(ref _textures, _textures.Length + 1);
                _textures[^1] = item;
                SetTextureUnit(_textures.Length);

                return;
            }
            
            Debug.LogWarning("[WARNING] Cannot add more element to Texture Array!");
        }

        public void Clear()
        {
            _textures = Array.Empty<Texture>();
        }

        public bool Contains(Texture item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            return _textures.Any(texture => texture.Equals(item));
        }

        public void CopyTo(Texture[] array, int arrayIndex)
        {
            Array.Copy(_textures, 0, array, arrayIndex, _textures.Length);
        }

        public bool Remove(Texture item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            int index = IndexOf(item);
            if (index > 0)
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }
        
        public void RemoveAt(int index)
        {
            if (index >= _textures.Length)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (index < _textures.Length - 1)
            {
                Array.Copy(_textures, index + 1, _textures, index, _textures.Length - index - 1);
            }
            
            Array.Resize(ref _textures, _textures.Length - 1);
        }

        public int IndexOf(Texture item) => Array.IndexOf(_textures, item, 0, _textures.Length);

        public void Insert(int index, Texture item)
        {
            if (index > _textures.Length)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (index < _textures.Length)
            {
                Array.Copy(_textures, index, _textures, index + 1, _textures.Length - index);
            }

            _textures[index] = item;
            SetTextureUnit(index);
        }
        
        public bool MoveNext()
        {
            if (_enumeratorIndex + 1 >= _textures.Length) return false;
            _enumeratorIndex++;
            return true;
        }

        public void Reset()
        {
            _enumeratorIndex = -1;
        }

        public void Dispose()
        {
            
        }
        
        public TextureUnit GetTextureUnit(int index)
        {
            if (index < 0 || index > _textures.Length)
                throw new ArgumentOutOfRangeException(nameof(index));

            return (TextureUnit)Enum.GetValues(typeof(TextureUnit)).GetValue(index)!;
        }

        private void SetTextureUnit(int index)
        {
            if (index < 0 || index > _textures.Length)
                throw new ArgumentNullException(nameof(index));
            
            _textures[index].TextureUnit = Enum.GetValues<TextureUnit>()[index];
        }
        
        public IEnumerator<Texture> GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Texture[] ToArray() => _textures;
        
        public bool Equals(TextureArray? other)
        {
            if (other == null) return false;
            if (other.Count != Count) return false;
            
            for (int i = 0; i < other.Count; i++)
            {
                if (!other[i].Equals(_textures[i])) return false;
            }

            return true;
        }

       
    }
}
