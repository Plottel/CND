using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deft
{
    public class Manager<T> : Singleton where T : MonoBehaviour
    {
        private static T _instance;

        public static T Get
        {
            get
            {
                return _instance;
            }

            private set { _instance = value; }
        }

        public static bool Exists
        {
            get { return _instance != null; }
        }

        void EnsureInstance()
        {
            if (_instance != null && _instance != this)
            {
                if (Application.isEditor)
                    DestroyImmediate(this);
                else
                    Destroy(this);
            }

            if (_instance == null)
                _instance = FindObjectOfType<T>();

            // Instance doesn't exist, create it.
            if (_instance == null)
            {
                _instance = new GameObject().AddComponent<T>();
                _instance.name = typeof(T).Name;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Awake()
        {
            EnsureInstance();
            OnAwake();
        }

        private void Start() => OnStart();
        private void Update() => OnUpdate();
        private void LateUpdate() => OnLateUpdate();

        public virtual void OnAwake() { }
        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
        public virtual void OnLateUpdate() { }
    }
}