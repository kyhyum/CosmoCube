using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [System.Serializable]
    public class PoolItem
    {
        public PoolItemType type;
        public GameObject prefab;
        public int preloadCount = 5;
    }

    public Transform poolRoot;
    public List<PoolItem> preload = new List<PoolItem>();

    // prefab -> queue of instances
    private Dictionary<PoolItemType, Queue<GameObject>> pools = new Dictionary<PoolItemType, Queue<GameObject>>();
    private Dictionary<PoolItemType, GameObject> prefabLookup = new Dictionary<PoolItemType, GameObject>();
    
   void Awake()
    {
        if (poolRoot == null)
        {
            GameObject go = new GameObject("ObjectPoolRoot");
            poolRoot = go.transform;
            DontDestroyOnLoad(go);
        }

        foreach (var item in preload)
        {
            if (item.prefab == null) continue;

            // 매핑 등록
            prefabLookup[item.type] = item.prefab;

            if (!pools.ContainsKey(item.type))
                pools[item.type] = new Queue<GameObject>();

            for (int i = 0; i < Mathf.Max(1, item.preloadCount); i++)
            {
                var inst = CreateNewInstance(item.prefab);
                ReturnToPool(item.type, inst);
            }
        }
    }

    GameObject CreateNewInstance(GameObject prefab)
    {
        var inst = Instantiate(prefab, poolRoot);
        inst.SetActive(false);
        return inst;
    }

    
    public GameObject Spawn(PoolItemType type, Vector3 position, Transform parent = null, float lifeTime = 0f)
    {
        if (!prefabLookup.TryGetValue(type, out var prefab))
            return null;

        if (!pools.TryGetValue(type, out var q))
            pools[type] = q = new Queue<GameObject>();

        GameObject inst = q.Count > 0 ? q.Dequeue() : Instantiate(prefab, poolRoot);
        var tr = inst.transform;
        
        if (tr.parent != parent) tr.SetParent(parent, false);
        tr.position = position;
        tr.rotation = Quaternion.identity;

        if (!inst.activeSelf) inst.SetActive(true);

        if (lifeTime > 0f)
            StartCoroutine(AutoReturnRoutine(type, inst, lifeTime));

        return inst;
    }

    public void ReturnToPool(PoolItemType type, GameObject instance)
    {
        if (instance == null) return;

        instance.SetActive(false);
        instance.transform.SetParent(poolRoot, false);

        if (!pools.ContainsKey(type))
            pools[type] = new Queue<GameObject>();

        pools[type].Enqueue(instance);
    }

    IEnumerator AutoReturnRoutine(PoolItemType type, GameObject instance, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (instance != null)
            ReturnToPool(type, instance);
    }
}