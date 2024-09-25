using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : BasePool<EnemyBase>
{
    private void Awake()
    {
        Initialize();
    }

    protected override EnemyBase OnCreatePoolItem()
    {
        var e = base.OnCreatePoolItem();
        e.SetDeactivateAction(delegate { Release(e); });
        return e;
    }

    protected override void OnGetPoolItem(EnemyBase obj)
    {
        base.OnGetPoolItem(obj);
        obj.transform.position = Utils.RandomOutscreenPosition();
        obj.init();
    }

    public void SetPrefab(EnemyBase prefab)
    {
        this.prefab = prefab;
    }
}
