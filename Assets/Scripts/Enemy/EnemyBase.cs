using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] float lifeTimeAfterCollision = 0.05f;
    [SerializeField] public float moveSpeed = 1f;
    [SerializeField] public float hp;
    //[SerializeField] public string text = "≤‚ ‘";
    //[SerializeField] public string code = "imya";
    //public Vector3 moveDirection { get; set; }
    [SerializeField] public TextMesh textMesh;
    [SerializeField] public TextMesh codeMesh;

    private GameObject teslaPool;

    System.Action<EnemyBase> deactivateAction;
    float deactivateTimer;
    [SerializeField] bool hasCollision;
    private bool enableEffect;
    private float default_hp;
    private float enableEffectTimer = 0f;
    private LightningEffect lightningEffect;
    private void Awake()
    {
        default_hp = hp;
        teslaPool = GameObject.Find("SpawnBullet/Pool:teslaBullet");

    }
    void Start()
    {
        init();
    }
    public void init()
    {
        hp = default_hp;
        AssignTexts();
        moveSpeed = Random.Range(4f, 5f);
        enableEffect = false;
        gameObject.GetComponent<LightningEffect>().StartObject = null;
        gameObject.GetComponent<LineRenderer>().enabled = false;
    }

    public void AssignTexts()
    {
        var texts = WubiTree.GetRandomLine().Split(',');
        textMesh.text = texts[1];
        codeMesh.text = texts[0];
        this.name = texts[1];
        tag = "Enemy";
    }

    void Update()
    {

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, 0, 0), moveSpeed * Time.deltaTime);

        if (hasCollision)
            deactivateTimer += Time.deltaTime;

        CheckTesla();

        if (hp <= 0f || deactivateTimer > lifeTimeAfterCollision)
        {
            hasCollision = false;
            deactivateTimer = 0f;
            deactivateAction.Invoke(this);
            WubiTree.decEnemy();
        }
    }

    void Injured(float dmg)
    {
        hp -= dmg;
    }

    private void CheckTesla()
    {
        foreach (Transform t in teslaPool.transform)
        {
            var bulletClass = t.gameObject.GetComponent<TeslaBullet>();
            var abs_pos = t.position - transform.position;
            if (Mathf.Abs(abs_pos.x) > bulletClass.distance ||
                Mathf.Abs(abs_pos.y) > bulletClass.distance)
            {
                if (gameObject.GetComponent<LightningEffect>().StartObject == t.gameObject)
                {
                    gameObject.GetComponent<LightningEffect>().StartObject = null;
                    gameObject.GetComponent<LineRenderer>().enabled = false;
                    enableEffectTimer = 0f;
                }
                continue;
            }
            var len2 = abs_pos.sqrMagnitude;

            if (len2 < bulletClass.distance * bulletClass.distance)
            {
                //
                gameObject.GetComponent<LightningEffect>().StartObject = t.gameObject;

                enableEffectTimer += Time.deltaTime;
                if (enableEffectTimer > 0.1f)
                {
                    gameObject.GetComponent<LineRenderer>().enabled = true;
                    if (enableEffectTimer > 0.3f)
                    {
                        Injured(t.gameObject.GetComponent<TeslaBullet>().dmg);
                        enableEffectTimer = 0f;
                    }
                }
            }
            else
            {
                //πÿ
                gameObject.GetComponent<LightningEffect>().StartObject = null;
                gameObject.GetComponent<LineRenderer>().enabled = false;
                enableEffectTimer = 0f;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasCollision = true;
            collision.gameObject.GetComponent<Player>().InjureCal();
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            Injured(collision.gameObject.GetComponent<BulletBase>().dmg);
        }
    }

    public void SetDeactivateAction(System.Action<EnemyBase> deactivateAction)
    {
        this.deactivateAction = deactivateAction;
    }
}
