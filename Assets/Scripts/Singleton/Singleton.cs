/* 
    create by baihan 2020.02.20 
    单例类，提供继承至object的单例类和monoBehaviour的单例 
*/

using System;
using UnityEngine;

namespace TPOne.Singleton
{
    public class Singleton<T> where T : class, new()
    {
        private Singleton()
        {
        }

        private static T m_instance;

        private static readonly object m_synclock = new object();

        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_synclock)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new T();
                        }
                    }
                }
                return m_instance;
            }
        }
    }

    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T m_instance;

        private static readonly object m_synclock = new object();

        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_synclock)
                    {
                        m_instance = GameObject.FindObjectOfType<T>();

                        if (m_instance == null)
                        {
                            GameObject obj = new GameObject(typeof(T).ToString());
                            m_instance = obj.AddComponent<T>();
                        }
                    }
                }
                return m_instance;
            }
        }

        public void Awake()
        {
            m_instance = this as T;
        }
    }
}