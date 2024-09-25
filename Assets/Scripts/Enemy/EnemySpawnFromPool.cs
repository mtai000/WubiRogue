using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class EnemySpawnerPool : MonoBehaviour
{
    [SerializeField] EnemyBase[] enemyPrefabs;
    [SerializeField] int spawnAmount = 1;
    [SerializeField] float spawnInterval = 1f;

    [SerializeField] int maxNumber = 100;
    float spawnTimer;
    List<EnemyPool> pools = new List<EnemyPool>();
    //TreeNode<string> decodeTreeRoot;
   
    private void Awake()
    {
        
    }

    private void Start()
    {
        foreach (var prefab in enemyPrefabs)
        {
            GameObject poolHandle = new GameObject($"Pool:{prefab.name}");

            poolHandle.transform.parent = transform;
            poolHandle.transform.position = Vector3.zero;
            poolHandle.SetActive(false);

            var pool = poolHandle.AddComponent<EnemyPool>();
            pool.SetPrefab(prefab);
            poolHandle.SetActive(true);
            pools.Add(pool);
        }
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            Spawn();
        }
    }

    void Spawn()
    {
        for (int i = 0; i < spawnAmount && WubiTree.enemyNumber < maxNumber; i++)
        {
            var randomIndex = Random.Range(0, enemyPrefabs.Length);
            var pool = pools[randomIndex];
            pool.Get();
            WubiTree.incEnemy();
        }
    }


}