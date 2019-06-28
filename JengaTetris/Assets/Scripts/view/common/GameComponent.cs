using UnityEngine;
using Object = System.Object;

namespace Assets.Scripts.view.common
{
    public class GameComponent : MonoBehaviour
    {
        public virtual void Awake()
        {

        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        protected T InstantiateAndAdd<T>(Component parent, GameObject current) where T : Component
        {
            GameObject gb = Instantiate(current, parent.transform);
            T tp = gb.AddComponent<T>();
            return tp;
        }

        protected T LoadAndAdd<T>(string path) where T : Component
        {
            GameObject gb = Instantiate(Resources.Load<GameObject>("Prefab/" + path));
            T tp = gb.AddComponent<T>();
            return tp;
        }

        protected T LoadAndAdd<T>(Component parent, string path) where T : Component
        {
            GameObject gb = Instantiate(Resources.Load<GameObject>("Prefab/" + path), parent.transform);
            T tp = gb.AddComponent<T>();
            return tp;
        }

        protected T FindAndAdd<T>(Component parent, string name) where T : Component
        {
            return parent.transform.Find(name).gameObject?.AddComponent<T>();
        }

        protected T FindAndAdd<T>(string name) where T : Component
        {
            return GameObject.Find(name).gameObject?.AddComponent<T>();
        }

        protected T FindAndGet<T>(Component parent, string name) where T : Component
        {
            return parent.transform.Find(name)?.GetComponent<T>();
        }

        protected T FindAndGet<T>(GameObject me) where T : Component
        {
            return me?.GetComponent<T>();
        }

        public AudioClip GetSound(string path)
        {
            return Resources.Load<AudioClip>("Sounds/" + path);
        }

//        public T GetAsset<T>(string assetName) where T : Object => Resources.Load<T>(assetName);

    }
}
