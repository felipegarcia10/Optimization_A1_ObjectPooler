using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;

    void Awake()
    {
        SharedInstance = this;
    }

    [System.Serializable]
    public class ObjectPoolItem
    {
        public int amountToPool;
        public GameObject objectToPool;
        public bool shouldExpand = true;
    }

    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> pooledObjects;
    // public GameObject objectToPool;
    // public int amountToPool;
    // public bool shouldExpand = true;


    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }

    void Start()
    {
        //pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            //CONTINUE HERE: las torretas tambien se estan tomando en cuenta en este calculo, no deberia ser asi
            int amoutToPool = item.amountToPool - pooledObjects.Count;
            //for (int i = 0; i < item.amountToPool; i++)
            for (int i = 0; i < amoutToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }
}
