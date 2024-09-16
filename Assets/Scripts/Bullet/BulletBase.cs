using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 10f;

    System.Action<BulletBase> deactivateAction;
    float deactivateTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            deactivateAction.Invoke(this);
        }
    }

    public void SetDeactivateAction(System.Action<BulletBase> deactivateAction)
    {
        this.deactivateAction = deactivateAction;
    }
}
