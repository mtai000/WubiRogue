
using System.Linq;
using UnityEngine;

public class EnemyTree : Singleton<EnemyTree>
{
    [SerializeField] TextAsset textAsset;

    public static int enemyNumber = 0;

    public static string[] texts;

    public void Awake()
    {
        texts = textAsset.text.Split('\n').Select(x => x.Trim()).ToArray();
    }

    public static string GetRandomLine()
    {
        return texts[UnityEngine.Random.Range(0,texts.Length)];
    }

    public static int GetEnemyCount()
    {
        return enemyNumber;
    }
    public static void incEnemy()
    {
        enemyNumber++;
    }
    public static void decEnemy()
    {
        enemyNumber--;
    }

}