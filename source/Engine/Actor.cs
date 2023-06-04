using System.Collections;
using Emission;
using Emission.Annotations;
using Emission.Core;
using Emission.Core.Mathematics;
using Emission.Engine.Page;
using Emission.Graphics;

namespace Emission.Engine
{
    [Serializable]
    public abstract class Actor : IDisposable, IEquatable<Actor>, IEnumerable<Actor>
    {
        public static Actor Empty => new DefaultActor();

        public bool IsActive { get; protected set; }
        public string Name;

        public Actor? Parent
        {
            get
            {
                if (LonelyActor.IsActorLonely(GetType())) return null;
                
                Page.Page? p = Application.GetInstanceAs<Game>().PageManager?.FindPage(_page);
                if (p != null && p.IsActive)
                    return p.GetActor(_parent);
                
                return null;
            }
            set => SetParent(value);
        }

        public Actor[] Childs
        {
            get
            {
                if (LonelyActor.IsActorLonely(GetType())) return null!;

                Page.Page? p = Application.GetInstanceAs<Game>().PageManager?.FindPage(_page);
                if ((p != null && p.IsActive) || _childs.Count != 0)
                {
                    Actor[] childs = new Actor[_childs.Count];
                    for (int i = 0; i < childs.Length; i++) childs[i] = p.GetActor(_childs[i]);
                    return childs;
                }

                return Array.Empty<Actor>();
            }
        }

        public virtual Vector3 Position
        {
            get => Transform.Position;
            set => Transform.Position = value;
        }

        public virtual Vector3 EulerAngles
        {
            get => Transform.EulerAngle;
            set => Transform.EulerAngle = value;
        }

        public virtual Quaternion Rotation
        {
            get => Transform.Rotation;
            set => Transform.Rotation = value;
        }

        public virtual Vector3 Scale
        {
            get => Transform.Scale;
            set => Transform.Scale = value;
        }

        protected Transform Transform;
        
        protected Model? Model;
        protected Shader? Shader;

        private ushort _self;
        private ushort _parent;
        private List<ushort> _childs;
        private Guid _page;
        
        public Actor() : this(String.Empty) {}
        public Actor(string name)
        {
            Name = name;
            Transform = Transform.Zero;
            Model = null;
            Shader = null;
            
            _childs = new List<ushort>();
            _parent = 0;
            _self = 0;
        }

        public virtual void Initialize()
        {
            
        }

        public virtual void Start()
        {
            
        }

        public virtual void Update()
        {
            
        }

        public virtual void Render()
        {
            
        }

        public virtual void Stop()
        {
            
        }

        public virtual void Enable()
        {
            
        }

        internal void Enable(ushort id, Guid uuid)
        {
            SetActive(true);
            _self = id;
            _page = uuid;

            Enable();
        }

        public virtual void Disable()
        {
            SetActive(false);
            
            _self = 0;
        }
        
        public virtual void SetActive(bool value) => IsActive = value;

        // TODO: Change page reference
        public void SetParent(Actor? actor)
        {
            if (LonelyActor.IsActorLonely(GetType())) return;

            if (!IsValidActor(actor))
                throw new ArgumentNullException(nameof(actor));

            if (IsValidActor(actor))
                throw new EmissionException(EmissionException.ERR_PAGE, $"Cannot set an empty actor ({actor?.Name}) as parent!");

            Page.Page? page = ((Game)Application.Instance!).PageManager.FindPage(_page);
            if (page != null && page.IsActive)
            {
                ushort id = page.HasActor(actor);
                if (id != 0) 
                    _parent = id;
                else
                {
                    page.RegisterActor(actor);
                    _parent = (ushort)page.ActorCount;
                    
                    Debug.LogWarning($"[WARNING] '{actor?.Name}' is not registered in current page while trying to be set as parent!");
                }
            }
            else
                throw new EmissionException(EmissionException.ERR_PAGE, "Cannot get Actor from page!");
        }

        // TODO: Change page reference
        public void AddChild(Actor? actor)
        {
            if (LonelyActor.IsActorLonely(GetType())) return;

            if (!IsValidActor(actor))
                throw new ArgumentNullException(nameof(actor));

            if (IsValidActor(actor))
                throw new EmissionException(EmissionException.ERR_PAGE, $"Cannot set an empty actor ({actor?.Name}) as child!");
            
            Page.Page? page = ((Game)Application.Instance!).PageManager.FindPage(_page);
            if (page != null && page.IsActive)
            {
                ushort id = page.HasActor(actor);
                if (id != 0)
                {
                    _childs.Add(id);
                }
                else
                {
                    page.RegisterActor(actor);
                    _childs.Add((ushort)page.ActorCount);
                    
                    Debug.LogWarning($"[WARNING] '{actor?.Name}' is not registered in current page while trying to be add to childrens!");
                }
            }
            else
                throw new EmissionException(EmissionException.ERR_PAGE, "Cannot get Actor from page!");
        }

        public void Dispose() => Disable();

        public bool Equals(Actor? other)
        {
            return other != null && other.Transform == Transform && other._self == _self && other.IsActive == IsActive;
        }

        public static bool IsValidActor(Actor? actor)
        {
            return actor != null && actor._self != 0;
        }

        public static bool IsActorEmpty(Actor? actor)
        {
            return actor is DefaultActor;
        }

        public IEnumerator<Actor> GetEnumerator()
        {
            return (IEnumerator<Actor>)Childs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}