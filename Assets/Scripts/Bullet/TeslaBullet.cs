using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TeslaBullet : BulletBase
{
    public float interval = 0.5f;
    public float distance = 10f;
    
    public float deactivateTimer;
    float liveTime = 10f;

    private void Start()
    {
        deactivateTimer = 0f;
    }

    public override void Update()
    {
        deactivateTimer += Time.deltaTime;
        if (deactivateTimer > liveTime) 
        {
            deactivateAction.Invoke(this);
        }
    }
}
