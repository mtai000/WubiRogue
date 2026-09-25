

using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnFromPool : MonoBehaviour
{
    [SerializeField] MainBullet mainBulletPrefab;
    BulletPool mainPool;
    [SerializeField] TeslaBullet teslaTowerPrefab;
    BulletPool teslaPool;
    List<BulletPool> pools = new List<BulletPool>();
    private BulletPool CreatePool(BulletBase prefab)
    {
        GameObject poolHandle = new GameObject($"Pool:{prefab.name}");

        poolHandle.transform.parent = transform;
        poolHandle.transform.position = Vector3.zero;
        poolHandle.SetActive(false);

        var pool = poolHandle.AddComponent<BulletPool>();
        pool.SetPrefab(prefab);
        poolHandle.SetActive(true);
        pools.Add(pool);
        return pool;
    }
    private void Start()
    {
        mainPool = CreatePool(mainBulletPrefab);
        teslaPool = CreatePool(teslaTowerPrefab);
    }

    void spawnMainBullet(Vector3 target)
    {
        var bullet = (MainBullet)(mainPool.Get());
        bullet.transform.position = Vector3.zero;
        bullet.target_position = target.normalized * 100;
        //Debug.Log("Shot main bullet");
    }

    void spawnTeslaBullet(Vector3 target)
    {
        var bullet = (TeslaBullet)(teslaPool.Get());
        bullet.deactivateTimer = 0f;
        bullet.transform.position = target;
        bullet.interval = 0.5f;
        bullet.dmg = 2f;
    }
}