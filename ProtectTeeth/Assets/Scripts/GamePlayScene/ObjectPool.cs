using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.ZombiesScript;
public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;      
        public Zombie zombie;     
        public int size;              
    }

    public List<Pool> pools;          
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    public static ObjectPool Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        GameObject parentObject = new GameObject("ObjectPoolParent");
        DontDestroyOnLoad(parentObject);
        // 각 프리팹에 대해 풀 생성
        foreach (var pool in pools)
        {
            GameObject poolParent = new GameObject(pool.tag + "_Pool");
            poolParent.transform.SetParent(parentObject.transform);
            DontDestroyOnLoad(poolParent);
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.zombie.prefab);
                obj.transform.SetParent(poolParent.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
        
    }

    public GameObject GetFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} does not exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        if (objectToSpawn == null)
        {
            Debug.LogWarning("Object in pool is null!");
            return null;
        }

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn); // 다시 큐에 추가
        return objectToSpawn;
    }
    public void DeactivateAll()
    {
        foreach (var kvp in poolDictionary)
        {
            foreach (var obj in kvp.Value)
            {
                if (obj != null && obj.activeInHierarchy)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}
