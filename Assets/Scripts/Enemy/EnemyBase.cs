using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] float lifeTimeAfterCollision = 0.05f;
    [SerializeField] public float moveSpeed = 1f;
    [SerializeField] public float hp;
    [SerializeField] public string text = "≤‚ ‘";
    [SerializeField] public string code = "imya";
    //public Vector3 moveDirection { get; set; }
    [SerializeField] private TextMesh textMeshPro;
    [SerializeField] private TextMesh codeMeshPro;

    System.Action<EnemyBase> deactivateAction;
    float deactivateTimer;
    [SerializeField]  bool hasCollision;
    void Start()
    {
        //moveDirection = -transform.position;
       AssignTexts();
    }

    public void AssignTexts()
    {
        var texts = EnemyTree.GetRandomLine().Split(',');
        textMeshPro.text = texts[1];
        codeMeshPro.text = texts[0];
        this.name = texts[1];
    }

    void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, 0, 0), moveSpeed * Time.deltaTime);

        if (hasCollision)
            deactivateTimer += Time.deltaTime;


        if (hp <= 0f || deactivateTimer > lifeTimeAfterCollision)
        {
            hasCollision = false;
            deactivateTimer = 0f;
            deactivateAction.Invoke(this);
            EnemyTree.decEnemy();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasCollision = true;
            //Debug.Log("Hit player!");
        }
    }

    public void SetDeactivateAction(System.Action<EnemyBase> deactivateAction)
    {
        this.deactivateAction = deactivateAction;
    }
}
