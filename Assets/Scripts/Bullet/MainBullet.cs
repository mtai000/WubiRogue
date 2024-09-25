
using UnityEngine;

class MainBullet:BulletBase
{
    public float moveSpeed = 20f;
    public Vector3 target_position;

    void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target_position, moveSpeed * Time.deltaTime);
        if (Mathf.Abs(transform.localPosition.x) > 100 || Mathf.Abs(transform.localPosition.y) > 100)
            deactivateAction.Invoke(this);
    }


    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            deactivateAction.Invoke(this);
        }
    }
}