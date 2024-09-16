

using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnFromPool : MonoBehaviour
{
    [SerializeField] BulletBase[] bulletPrefabs;
    [SerializeField] BulletBase mainBulletPrefab;

    List<BulletPool> pools = new List<BulletPool>();
    private void Start()
    {
        foreach (var prefab in bulletPrefabs)
        {
            GameObject poolHandle = new GameObject($"Pool:{prefab.name}");

            poolHandle.transform.parent = transform;
            poolHandle.transform.position = Vector3.zero;
            poolHandle.SetActive(false);

            var pool = poolHandle.AddComponent<BulletPool>();
            pool.SetPrefab(prefab);
            poolHandle.SetActive(true);
            pools.Add(pool);
        }

    }

    void Shot(string message)
    {
        Debug.Log("Received message: " + message);
    }
}