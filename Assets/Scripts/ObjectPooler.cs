using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public GameObject objectToPool;
    public bool shouldExpand = true;
    public bool shouldStartActive = false;
    public bool shouldRandomizePosition = false;
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;

    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> pooledObjects;
    
    private void Awake()
    {
        SharedInstance = this;
    }
    
    private void Start()
    {
        foreach (ObjectPoolItem item in itemsToPool)
        {
            InitializePoolContent(item.objectToPool.tag);
        }
    }

    private void InitializePoolContent(string tagToCompare)
    {
        // We search in the list the object to pool and calculate the amount of objects left to pool
        ObjectPoolItem poolItemToInstantiate = new ObjectPoolItem();
        foreach (var item in itemsToPool.Where(item => item.objectToPool.CompareTag(tagToCompare)))
        {
            poolItemToInstantiate = item;
        }
        
        // The amount of existing items (of the tag) that are already on the scene
        int existingItems = pooledObjects.Count(item => item.CompareTag(tagToCompare));
        
        // if existing items is greater than the max amount, delete the ones that are not needed
        if (existingItems > poolItemToInstantiate.amountToPool)
        {
            int counter = existingItems - poolItemToInstantiate.amountToPool;
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (pooledObjects[i].CompareTag(tagToCompare))
                {
                    GameObject objectToClean = pooledObjects[i];
                    pooledObjects.Remove(objectToClean);
                    Destroy(objectToClean);
                    counter--;
                }
                
                if (counter <= 0) break;
            }
        }
        else if (existingItems < poolItemToInstantiate.amountToPool)
        {
            int leftItemsToCreate = poolItemToInstantiate.amountToPool - existingItems;
            
            // Instantiate left objects
            if (poolItemToInstantiate.objectToPool != null)
            {
                for (int i = 0; i < leftItemsToCreate; i++)
                {
                    GameObject obj = Instantiate(poolItemToInstantiate.objectToPool);
                    obj.SetActive(poolItemToInstantiate.shouldStartActive);
                    pooledObjects.Add(obj);
                }
            }
        }
        
        if (!poolItemToInstantiate.shouldRandomizePosition) return;
        
        // Randomize position of existing objects
        foreach (var item in pooledObjects.Where(item => item.CompareTag(tagToCompare)))
        {
            item.transform.position = Spawner.Instance.GetRandomPositionInBounds();
        }
    }


    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].CompareTag(tag))
            {
                return pooledObjects[i];
            }
        }
        
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.CompareTag(tag) &&  item.shouldExpand)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
                return obj;
            }
        }
        
        return null;
    }
}
