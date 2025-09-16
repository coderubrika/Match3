using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Test3
{
    public class MonoPool<T>
        where T: MonoBehaviour
    {
        private Queue<T> pool = new();
        private Transform root;
        private T prefab;

        private Action<T> initializer;
        
        public MonoPool(T prefab, Transform root, int initSize = 10, Action<T> initializer = null)
        {
            this.root = root;
            this.prefab = prefab;
            this.initializer = initializer;
            
            for (int i = 0; i < initSize; i++)
            {
                T item = CreateItem();
                pool.Enqueue(item);
            }
        }

        public T Spawn()
        {
            T item = pool.Count > 0 ? pool.Dequeue() : CreateItem();
            return item;
        }

        public void Despawn(T item)
        {
            pool.Enqueue(item);
            item.gameObject.SetActive(false);
            item.transform.SetParent(root);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
        }

        private T CreateItem()
        {
            T item = Object.Instantiate(prefab, root);
            initializer?.Invoke(item);
            item.gameObject.SetActive(false);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;

            return item;
        }
    }
}