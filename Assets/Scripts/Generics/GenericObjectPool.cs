using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generics {
    public class GenericObjectPool<T>
    {
        Transform parentTransform;
        GameObject objectPrefab;
        
        public Queue<T> objectPool = new Queue<T>();

        public void GeneratePool(GameObject objPrefab, int poolCount, Transform parentTF) {
            parentTransform = parentTF;
            objectPrefab = objPrefab;
            for (int i = 0; i < poolCount; i++) {
                GameObject item = GameObject.Instantiate(objectPrefab, parentTransform);
                item.SetActive(false);
                T poolItem = item.GetComponent<T>();
                objectPool.Enqueue(poolItem);
            }
        }

        public T GetItem() {
            if (objectPool.Count > 0) {
                return objectPool.Dequeue();
            }
            else {
                GameObject item = GameObject.Instantiate(objectPrefab, parentTransform);
                item.SetActive(false);
                T poolItem = item.GetComponent<T>();
                objectPool.Enqueue(poolItem);
                return objectPool.Dequeue();
            }
        }

        public void ReturnItem(T poolItem) {
            objectPool.Enqueue(poolItem);
        }
    }

}

