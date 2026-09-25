using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public float dmg = 2f;
    public System.Action<BulletBase> deactivateAction;
    // Start is called before the first frame update
    public virtual void Update()
    {
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
       
    }

    public void SetDeactivateAction(System.Action<BulletBase> deactivateAction)
    {
        this.deactivateAction = deactivateAction;
    }
}
