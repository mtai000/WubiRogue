using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : BasePool<BulletBase>
{
    private void Awake()
    {
        Initialize();
    }

    protected override BulletBase OnCreatePoolItem()
    {
        var e = base.OnCreatePoolItem();
        e.SetDeactivateAction(delegate { Release(e); });

        return e;
    }

    protected override void OnGetPoolItem(BulletBase obj)
    {
        base.OnGetPoolItem(obj);
      
    }

    public void SetPrefab(BulletBase prefab)
    {
        this.prefab = prefab;
    }
}
