using System.Collections;
using Emission;
using Emission.Annotations;
using Emission.Core;
using Emission.Graphics;

namespace Emission.Engine.Page
{
    [Serializable]
    public class Page : IEngineBehaviour, IEquatable<Page>, IDisposable, IEnumerable<Actor>
    {
        public readonly string Name;

        public IEngineBehaviour Behaviour => this;
        public string Uuid => _uuid.ToString();

        public bool IsActive { get; set; }
        public int ActorCount => _actors.Count;

        public Actor this[int i]
        {
            get
            {
                if (i > 0 || i < _actors.Count) throw new IndexOutOfRangeException();
                return _actors[i];
            }
            
            protected set
            {
                if (i > 0 || i < _actors.Count) throw new IndexOutOfRangeException();
                _actors[i] = value;
            }
        }
        
        protected Camera? Camera;
        protected Window.Window? Window => Application.GetInstanceAs<Game>().Window;
        
        private readonly Guid _uuid;
        private readonly List<Actor> _actors;

        public Page() : this(string.Empty) {}
        public Page(string? name)
        {
            ArgumentNullException.ThrowIfNull(name);
            
            Name = name;
            Camera = null;

            _uuid = Guid.NewGuid();
            _actors = new List<Actor>() { Actor.Empty } ;

            Behaviour.BindBehaviour();
            Application.GetInstanceAs<Game>().PageManager?.Register(this);
        }

        public void Enable()
        {
            IsActive = true;
            OnEnable();
            
            if (Camera == null)
                throw new EmissionException(EmissionException.ERR_PAGE, "Camera does not exists in current context!");
            
            for (ushort i = 0; i < _actors.Count; i++) _actors[i].Enable(i, _uuid);
        }

        public void Disable()
        {
            IsActive = false;
            Behaviour.IsActive = false;
            OnDisable();

            foreach (Actor actor in _actors) actor.Disable();
        }
        
        public virtual void Initialize() 
        {
            foreach (Actor actor in _actors)
            {
                if (DisableEngineBehaviorOnActor.IsActorDisabled(actor.GetType())) continue;
                actor.Initialize();
            }
        }

        public virtual void Start()
        {
            foreach (Actor actor in _actors)
            {
                if (DisableEngineBehaviorOnActor.IsActorDisabled(actor.GetType())) continue;
                actor.Start();
            }
        }

        public virtual void Update(float delta)
        {
            Camera.Update(delta);
            
            foreach (Actor actor in _actors)
            {
                if (DisableEngineBehaviorOnActor.IsActorDisabled(actor.GetType())) continue;
                actor.Update(delta);
            }
        }

        public virtual void Render()
        {
            foreach (Actor actor in _actors)
            {
                if (DisableEngineBehaviorOnActor.IsActorDisabled(actor.GetType())) continue;
                actor.Render();
            }
        }

        public virtual void Stop(int status)
        {
            foreach (Actor actor in _actors)
            {
                if (DisableEngineBehaviorOnActor.IsActorDisabled(actor.GetType())) continue;
                actor.Stop();
            }

            Disable();
        }

        public virtual void OnEnable() { }
        public virtual void OnDisable() { }

        public void Dispose()
        {
            if(IsActive) Disable();
            
            Application.GetInstanceAs<Game>().PageManager?.Remove(this);
        }

        public void Draw(Model model, Shader shader)
        {
            model.Draw(shader, model.Transform.ToMatrix(), Camera.View, Camera.Projection);
        }

        public void RegisterActor(Actor? actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));
            
            if (_actors.Count >= ushort.MaxValue)
            {
                Debug.LogWarning($"[Warning] Cannot add more actor to {Name}!");
                return;
            }
            
            _actors.Add(actor);
        }

        public void RemoveActor(Actor actor)
        {
            if (!Actor.IsValidActor(actor))
                throw new ArgumentNullException(nameof(actor));
            
            if(_actors.Contains(actor))
                _actors.Remove(actor);
        }

        public Actor? GetActor(int actorIndex)
        {
            if (_actors.Count == 0) return null;

            if (actorIndex < 0 || actorIndex > _actors.Count)
                throw new ArgumentOutOfRangeException(nameof(actorIndex));
            
            return _actors[actorIndex];
        }

        public Actor? GetActor(Type type)
        {
            if (_actors.Count == 0) return null;

            if (type.IsSubclassOf(typeof(Actor)))
            {
                return _actors.Find(x => x.GetType() == type);
            }
            
            throw new ArgumentException($"'{nameof(type)}' is not a child from the actor class!");
        }
        
        public Actor? GetActor<T>() where T : Actor
        {
            if (_actors.Count == 0) return null;

            return _actors.Find(x => x.GetType() == typeof(T));
        }

        public ushort HasActor(Actor? actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));
            
            if (_actors.Contains(actor))
                return (ushort)_actors.IndexOf(actor);
            return 0;
        }

        public bool Equals(Page? other)
        {
            return other != null && _uuid.ToString() == other._uuid.ToString();
        }

        public IEnumerator<Actor> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return _uuid.GetHashCode();
        }
        
        public override string ToString() => Uuid;
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
