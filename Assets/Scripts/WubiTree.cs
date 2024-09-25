
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WubiTree : Singleton<WubiTree>
{
    [SerializeField] TextAsset[] textAsset;

    public static int enemyNumber = 0;

    public static WubiTreeNode wubiTreeRoot;
    private static List<string> lines = new List<string>();

    public void Awake()
    {
        foreach (TextAsset ta in textAsset)
        {
            List<string> tmp = ta.text.Split('\n').Select(x => x.Trim()).ToList();
            lines.AddRange(tmp);
        }
        LoadAssetToTree();
    }

    public static string GetRandomLine()
    {
        return lines[UnityEngine.Random.Range(0, lines.Count)];
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

    private void LoadAssetToTree()
    {
        WubiTreeNode root = new WubiTreeNode(new List<string> { });
        if (textAsset == null)
        {
            Debug.LogError("CSV file not assigned.");
            return;
        }

        foreach (string line in lines)
        {
            string[] fields = line.TrimEnd().Split(',');
            if (fields.Length != 2)
            {
                continue;
            }
            WubiTreeNode cur = root;
            foreach (char c in fields[0])
            {
                if (!cur.Children.ContainsKey(c))
                {
                    WubiTreeNode child = new WubiTreeNode(new List<string> { });
                    cur.AddChild(c, child);
                }
                cur = cur.Children[c];

            }
            cur.Data.Add(fields[1] + "(" + fields[0] + ")");
        }
        wubiTreeRoot = root;
    }

}